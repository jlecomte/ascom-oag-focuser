/*
 * Arduino_Firmware.ino
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

// Note that I don't use the standard Arduino stepper library because I need
// to be able to respond to commands while the motor is moving, so I manually
// move the stepper motor 1 step at a time.

#include <EEPROM.h>

//-- CONSTANTS ----------------------------------------------------------------

constexpr auto DEVICE_GUID = "6e18ce4b-0d7b-4850-8470-80df623bf0a4";

constexpr auto OK = "OK";
constexpr auto NOK = "NOK";

constexpr auto TRUE = "TRUE";
constexpr auto FALSE = "FALSE";

constexpr auto COMMAND_PING = "COMMAND:PING";
constexpr auto RESULT_PING = "RESULT:PING:OK:";

constexpr auto COMMAND_INFO = "COMMAND:INFO";
constexpr auto RESULT_INFO = "RESULT:INFO:DarkSkyGeek's OAG Focuser Firmware v1.0";

constexpr auto COMMAND_FOCUSER_GETPOSITION = "COMMAND:FOCUSER:GETPOSITION";
constexpr auto RESULT_FOCUSER_POSITION = "RESULT:FOCUSER:POSITION:";

constexpr auto COMMAND_FOCUSER_ISMOVING = "COMMAND:FOCUSER:ISMOVING";
constexpr auto RESULT_FOCUSER_ISMOVING = "RESULT:FOCUSER:ISMOVING:";

constexpr auto COMMAND_FOCUSER_SETZEROPOSITION = "COMMAND:FOCUSER:SETZEROPOSITION";
constexpr auto RESULT_FOCUSER_SETZEROPOSITION = "RESULT:FOCUSER:SETZEROPOSITION:";

constexpr auto COMMAND_FOCUSER_MOVE = "COMMAND:FOCUSER:MOVE:";
constexpr auto RESULT_FOCUSER_MOVE = "RESULT:FOCUSER:MOVE:";

constexpr auto COMMAND_FOCUSER_HALT = "COMMAND:FOCUSER:HALT";
constexpr auto RESULT_FOCUSER_HALT = "RESULT:FOCUSER:HALT:";

constexpr auto ERROR_INVALID_COMMAND = "ERROR:INVALID_COMMAND";

// According to the data sheet, when the 28BYJ-48 motor runs in full step mode,
// each step corresponds to a rotation of 11.25°. That means there are 32 steps
// per revolution. Additionally, the motor is outfitted with a gearbox that has
// a ratio of 64:1, which means that a full revolution requires 2,048 steps.
const unsigned int STEPS_PER_REVOLUTION = 2048;

// The ZWO helical focuser moves only over about a 220° range.
// The gear ratio between small pulley and the focuser body is roughly 1:4.
// Therefore, we set MAX_STEPS at 5,000 steps.
const unsigned int MAX_STEPS = 5000;

// 6RPM = 1 revolution of the motor axle takes 10 seconds.
// No need to go crazy fast (stepper motors lose torque at higher speeds),
// and no need to go crazy slow. This value seems like a happy medium...
const unsigned int RPM_SPEED = 6;

// How long do we wait between each step in order to achieve the desired speed?
const unsigned long STEP_DELAY_MICROSEC = (60L * 1000L * 1000L) / (STEPS_PER_REVOLUTION * RPM_SPEED);

// Pins controlling the motor. Change this depending on your exact wiring!
const unsigned int MOTOR_PIN_1 = 11; // Blue   - 28BYJ48 pin 1
const unsigned int MOTOR_PIN_2 =  9; // Yellow - 28BYJ48 pin 3
const unsigned int MOTOR_PIN_3 = 10; // Pink   - 28BYJ48 pin 2
const unsigned int MOTOR_PIN_4 =  8; // Orange - 28BYJ48 pin 4

const unsigned int POSITION_EEPROM_BASE_ADDR = 0;

//-- VARIABLES ----------------------------------------------------------------

// While moving, steps_left > 0
// When not moving, steps_left == 0
int steps_left;

enum Direction {
    forward = 1,
    backward = -1
} direction;

// The current position, which we store in EEPROM
int position;

unsigned long last_step_time;

//-- MICROCONTROLLER FUNCTIONS ------------------------------------------------

// The `setup` function runs once when you press reset or power the board.
void setup() {
    // Initialize serial port I/O.
    Serial.begin(57600);
    while (!Serial) {
        ; // Wait for serial port to connect. Required for native USB!
    }
    Serial.flush();

    // Initialize motor control pins...
    pinMode(MOTOR_PIN_1, OUTPUT);
    pinMode(MOTOR_PIN_2, OUTPUT);
    pinMode(MOTOR_PIN_3, OUTPUT);
    pinMode(MOTOR_PIN_4, OUTPUT);

    steps_left = 0;
    direction = forward;
    position = 0;
    last_step_time = 0L;

    EEPROM.get(POSITION_EEPROM_BASE_ADDR, position);
    if (position > MAX_STEPS) {
        // The position likely had never been stored in EEPROM...
        position = 0;
    }
}

// The `loop` function runs over and over again until power down or reset.
void loop() {
    if (Serial.available() > 0) {
        String command = Serial.readStringUntil('\n');
        if (command == COMMAND_PING) {
            handlePing();
        }
        else if (command == COMMAND_INFO) {
            sendFirmwareInfo();
        }
        else if (command == COMMAND_FOCUSER_ISMOVING) {
            sendFocuserState();
        }
        else if (command == COMMAND_FOCUSER_GETPOSITION) {
            sendFocuserPosition();
        }
        else if (command == COMMAND_FOCUSER_SETZEROPOSITION) {
            setFocuserZeroPosition();
        }
        else if (command.startsWith(COMMAND_FOCUSER_MOVE)) {
            String arg = command.substring(strlen(COMMAND_FOCUSER_MOVE));
            int value = arg.toInt();
            moveFocuser(value);
        }
        else if (command == COMMAND_FOCUSER_HALT) {
            haltFocuser();
        }
        else {
            handleInvalidCommand();
        }
    }

    // Make the stepper motor move, if needed, 1 step at a time...
    step();
}

//-- UTILITY FUNCTIONS -----------------------------------------------------

void step() {
    if (steps_left > 0) {
        // Make sure we don't prematurely take a step if it's too early...
        unsigned long now = micros();
        if (now - last_step_time < STEP_DELAY_MICROSEC) {
            return;
        }
    
        last_step_time = now;
    
        steps_left--;

        if (direction == forward) {
            position++;
        } else {
            position--;
        }
    
        switch (position % 4) {
            case 0: // 1010
                digitalWrite(MOTOR_PIN_1, HIGH);
                digitalWrite(MOTOR_PIN_2, LOW);
                digitalWrite(MOTOR_PIN_3, HIGH);
                digitalWrite(MOTOR_PIN_4, LOW);
                break;
            case 1: // 0110
                digitalWrite(MOTOR_PIN_1, LOW);
                digitalWrite(MOTOR_PIN_2, HIGH);
                digitalWrite(MOTOR_PIN_3, HIGH);
                digitalWrite(MOTOR_PIN_4, LOW);
                break;
            case 2: // 0101
                digitalWrite(MOTOR_PIN_1, LOW);
                digitalWrite(MOTOR_PIN_2, HIGH);
                digitalWrite(MOTOR_PIN_3, LOW);
                digitalWrite(MOTOR_PIN_4, HIGH);
                break;
            case 3: // 1001
                digitalWrite(MOTOR_PIN_1, HIGH);
                digitalWrite(MOTOR_PIN_2, LOW);
                digitalWrite(MOTOR_PIN_3, LOW);
                digitalWrite(MOTOR_PIN_4, HIGH);
                break;
        }

        if (steps_left == 0) {
            stop();
        }
    }
}

void stop() {
    // Make sure we don't take another step.
    steps_left = 0;

    // Store the final position in EEPROM.
    EEPROM.put(POSITION_EEPROM_BASE_ADDR, position);

    // And de-energize the stepper by setting all the pins to LOW to prevent heat build up.
    digitalWrite(MOTOR_PIN_1, LOW);
    digitalWrite(MOTOR_PIN_2, LOW);
    digitalWrite(MOTOR_PIN_3, LOW);
    digitalWrite(MOTOR_PIN_4, LOW);
}

//-- FOCUSER HANDLING ------------------------------------------------------

void sendFocuserState() {
    Serial.print(RESULT_FOCUSER_ISMOVING);
    Serial.println(steps_left != 0 ? TRUE : FALSE);
}

void sendFocuserPosition() {
    Serial.print(RESULT_FOCUSER_POSITION);
    Serial.println(position);
}

void setFocuserZeroPosition() {
    Serial.print(RESULT_FOCUSER_SETZEROPOSITION);
    if (steps_left == 0) {
        position = 0;
        EEPROM.put(POSITION_EEPROM_BASE_ADDR, position);
        Serial.println(OK);
    } else {
        // Cannot set zero position while focuser is still moving...
        Serial.println(NOK);
    }
}

void moveFocuser(int target_position) {
    Serial.print(RESULT_FOCUSER_MOVE);

    if (steps_left > 0) {
        // Cannot move while focuser is still moving from previous request...
        Serial.println(NOK);
        return;
    }

    if (target_position < 0 || target_position > MAX_STEPS) {
        // Cannot move to a position that is out of range...
        Serial.println(NOK);
        return;
    }

    steps_left = abs(target_position - position);

    if (target_position >= position) {
        direction = forward;
    } else {
        direction = backward;
    }

    Serial.println(OK);
}

void haltFocuser() {
    stop();
    Serial.print(RESULT_FOCUSER_HALT);
    Serial.println(OK);
}

//-- MISCELLANEOUS ------------------------------------------------------------

void handlePing() {
    Serial.print(RESULT_PING);
    Serial.println(DEVICE_GUID);
}

void sendFirmwareInfo() {
    Serial.println(RESULT_INFO);
}

void handleInvalidCommand() {
    Serial.println(ERROR_INVALID_COMMAND);
}
