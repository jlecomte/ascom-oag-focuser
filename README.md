# ASCOM-Compatible OAG Focuser

## Introduction

Shortly after I got started in astrophotography, I noticed that the stars in my images were slightly elongated due to some differential flexure somewhere in my imaging system (see [Cloudy Nights thread](https://www.cloudynights.com/topic/775260-good-guiding-but-elongated-stars-along-e-w-direction/) — I strongly suspect that it is sag in my focuser...) The permanent solution was to switch from using a guide scope to using an off-axis guider (OAG) and that immediately resolved my issue. Now, my stars are perfectly round, which is great! For reference, I purchased the [ZWO OAG](https://astronomy-imaging-camera.com/product/zwo-oag) and the accompanying [ZWO 1.25" helical focuser](https://astronomy-imaging-camera.com/product/zwo-1-25%E2%80%B3-helical-focuser).

As good as this sounds, the OAG brought a number of new challenges which I did not have to deal with before. First, when using an OAG, the field of view is very small, which sometimes makes it difficult for the guiding software to detect guide stars. Second, the OAG requires some amount of refocusing when switching filters. Indeed, filters are not always parfocal. Most importantly, a refractor (even an apochromatic refractor...) will focus various wavelengths at different distances, and this is the primary reason why it is required to change the focus of the main imaging camera (using filter offsets) when changing filters. While guiding software like PHD2 can accommodate slightly out-of-focus guide stars by computing their centroid, out-of-focus guide images can contain a drastically lower number of detected guide stars, and those that are detected will have a much lower Signal to Noise Ratio (SNR), further reducing the accuracy of the centroid computation. In some situations, PHD2 may not even be able to detect a single guide star, and guiding will be impossible. This has happened to me in the past, and I was forced to _manually_ refocus the guide camera (independently of the main imaging camera of course, using the OAG helical focuser)

Manually refocusing the guide camera throughout the night is not exactly one of the most enjoyable parts of the hobby, so I looked for ways to automate that. Pegasus Astro sells a motorized OAG named [SCOPS OAG](https://pegasusastro.com/products/scops-oag/). However, it is a little expensive for me ($750 US — although it is a really great looking unit!) Furthermore, I discussed with the N.I.N.A. developers on their [Discord server](https://discord.gg/fwpmHU4). Since N.I.N.A. can only connect to a single focuser at a time, there was no good solution yet to deal with SCOPS OAG. It is certainly possible to run a second instance of N.I.N.A. that connects to both SCOPS OAG and the guide camera, but there is no way to have those two N.I.N.A. instances communicate upon filter change.

All of this has led me to design and build my own solution to this problem. In this repository, you will find everything you need to motorize and automatically control the ZWO OAG (list of parts, 3D model, electronic schematics, Arduino firmware, ASCOM driver, standalone ASCOM client application, instructions, etc.) I hope you consider building this project if you find yourself in a similar situation. If you have a question or run into a problem, don't hesitate to file a [GitHub issue](https://github.com/jlecomte/ascom-oag-focuser/issues). And of course, Pull Requests are always welcome!

## Pre-Requisites

* A Windows computer (Windows 10 or newer)
* [Microsoft Visual Studio](https://visualstudio.microsoft.com/) (FYI, I used the 2022 edition...)
* [ASCOM Platform](https://ascom-standards.org/)
* [ASCOM Platform Developer Components](https://ascom-standards.org/COMDeveloper/Index.htm)
* [Arduino IDE](https://www.arduino.cc/en/software)
* [FreeCAD](https://www.freecadweb.org/), a free and open-source 3D parametric modeler
* A 3D printer able to print PETG, and a slicer (I use a heavily upgraded Creality Ender 3 v2, and Ultimaker Cura)
* A few basic tools that any tinkerer must own, such as a breadboard, a soldering iron, etc.

## Hardware

The following are just suggestions... Also, over time, some of the links may no longer work... But it should help get you started.

* [ZWO OAG](https://astronomy-imaging-camera.com/product/zwo-oag)
* [ZWO 1.25" helical focuser](https://astronomy-imaging-camera.com/product/zwo-1-25%E2%80%B3-helical-focuser)
* For the microcontroller, here are two options:
  * You can use the [Seeeduino XIAO](https://www.seeedstudio.com/Seeeduino-XIAO-Arduino-Microcontroller-SAMD21-Cortex-M0+-p-4426.html). It costs only $5 US if you purchase it directly from Seeed Studio, but be prepared to wait a long time for it to ship to your house! You can also get it a lot faster from Amazon, but you will pay 2 to 3 times as much! Also, you will need to power the stepper motor using a separate power supply, or some internal battery...
  * Another option is to use a [PD Micro](https://www.crowdsupply.com/ryan-ma/pd-micro). At $30 US, it is a significantly more expensive unit, but it allows powering the stepper motor from the USB cable. I like the idea of reducing the number of cables I have to deal with, and not having to worry about charging an internal battery, so this is the option that I have chosen for myself.
  * And of course, you can use any Arduino-compatible board, so if you have an Arduino Nano laying around, feel free to use that!
* ULN2003 Darlington transistor array
* 28BYJ-48 stepper motor
* [Pulley/timing belt](https://www.amazon.com/dp/B08QYYF6W4)

## ASCOM Driver

### Compiling The Driver

Open Microsoft Visual Studio as an administrator (right-click on the Microsoft Visual Studio shortcut, and select "Run as administrator"). This is required because when building the code, by default, Microsoft Visual Studio will register the compiled COM components, and this operation requires special privileges (Note: This is something you can disable in the project settings...) Then, open the solution (`ASCOM_Driver\ASCOM.DarkSkyGeek.OAGFocuser.sln`), change the solution configuration to `Release` (in the toolbar), open the `Build` menu, and click on `Build Solution`. As long as you have properly installed all the required dependencies, the build should succeed and the ASCOM driver will be registered on your system. The binary file generated will be `ASCOM_Driver\bin\Release\ASCOM.DarkSkyGeek.OAGFocuser.dll`. You may also download this file from the [Releases page](https://github.com/jlecomte/ascom-oag-focuser/releases).

### Installing The Driver

If you are planning to use the ASCOM driver on a separate computer, you can install it manually, using `RegAsm.exe`. Just don't forget to use the 64 bit version, and to pass the `/tlb /codebase` flags. Here is what it looked like on my imaging mini computer:

```
> C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe /tlb /codebase ASCOM.DarkSkyGeek.OAGFocuser.dll
Microsoft .NET Framework Assembly Registration Utility version 4.8.4161.0
for Microsoft .NET Framework version 4.8.4161.0
Copyright (C) Microsoft Corporation.  All rights reserved.

Types registered successfully
```

### Screenshots

The ASCOM driver registers two new components: `ASCOM.DarkSkyGeek.FilterWheelProxy`, which implements the `IFilterWheelV2` ASCOM interface, and `ASCOM.DarkSkyGeek.OAGFocuser`, which implements the `IFocuserV3` ASCOM interface. Both components have their own settings dialog. Here is what the filter wheel settings dialog looks like:

![Filter Wheel Settings Dialog](images/FilterWheelProxy-SetupDialog.png)

And here is what the focuser settings dialog looks like:

![Focuser Settings Dialog](images/Focuser-SetupDialog.png)

## Compiling The Standalone Focuser Application

Open the `Focuser_App\Focuser_App.sln` solution in Microsoft Visual Studio, change the solution configuration to `Release` (in the toolbar), open the `Build` menu, and click on `Build Solution`. Very simple! The binary file generated will be `Focuser_App\bin\Release\ASCOM.DarkSkyGeek.FocuserApp.exe`. You may also download this file from the [Releases page](https://github.com/jlecomte/ascom-oag-focuser/releases). Here is what that little standalone application looks like:

![Standalone Focuser Control Application](images/Standalone-Focuser-App.png)

## Upload The Firmware

* Add support for the board that you are using in your project. For example, for the Seeeduino XIAO, follow [the instructions from the manufacturer](https://wiki.seeedstudio.com/Seeeduino-XIAO/).
* To customize the name of the device when connected to your computer, open the file `boards.txt`. On my system and for the version of the Seeeduino board I use, it is located at `%LOCALAPPDATA%\Arduino15\packages\Seeeduino\hardware\samd\1.8.2\boards.txt`. It is different for other boards. Then, change the value of the `usb_product` key (e.g., `seeed_XIAO_m0.build.usb_product`) to whatever you'd like.
* Finally, connect your board to your computer using a USB cable, open the sketch file located at `Arduino_Firmware\Arduino_Firmware.ino`, and click on the `Upload` button in the toolbar.

## Electronic Circuit

The electronics circuit is fairly straightforward. I included a Fritzing file in the `Electronics/` folder. Here are the schematics:

![Breadboard Schematics](images/Breadboard-Schematics.jpg)

Here is what the prototype circuit looks like:

![Breadboard Prototype](images/Breadboard-Prototype.jpg)
