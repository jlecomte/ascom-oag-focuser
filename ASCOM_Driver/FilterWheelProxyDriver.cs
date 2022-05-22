/*
 * FilterWheelDriver.cs
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

using ASCOM.DeviceInterface;
using ASCOM.Utilities;

using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace ASCOM.DarkSkyGeek
{
    //
    // Your driver's DeviceID is ASCOM.DarkSkyGeek.FilterWheelProxy
    //
    // The Guid attribute sets the CLSID for ASCOM.DarkSkyGeek.FilterWheel
    // The ClassInterface/None attribute prevents an empty interface called
    // _DarkSkyGeek from being created and used as the [default] interface
    //

    /// <summary>
    /// ASCOM FilterWheel Driver for DarkSkyGeek.
    /// </summary>
    [Guid("07cc86cc-afc0-4371-8cff-81fe5f9e64b6")]
    [ClassInterface(ClassInterfaceType.None)]
    public class FilterWheelProxy : IFilterWheelV2
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.DarkSkyGeek.FilterWheelProxy";

        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string deviceName = "DarkSkyGeek’s Filter Wheel Proxy For OAG Focuser";

        // Constants used for Profile persistence
        internal static int MAX_FILTER_COUNT = 8;

        internal static string filterWheelIdProfileName = "Filter Wheel ID";
        internal static string filterWheelIdDefault = string.Empty;

        internal static string filterNamesProfileName = "Filter Names";
        internal static string[] filterNamesDefault = Enumerable.Repeat(string.Empty, MAX_FILTER_COUNT).ToArray();

        internal static string filterOffsetsProfileName = "Filter Offsets";
        internal static int[] filterOffsetsDefault = Enumerable.Repeat(0, MAX_FILTER_COUNT).ToArray();

        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";

        // Variables to hold the current device configuration
        internal static string filterWheelId = string.Empty;
        internal static string[] filterNames = filterNamesDefault;
        internal static int[] filterOffsets = filterOffsetsDefault;

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        private bool connectedState;

        /// <summary>
        /// Variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        internal TraceLogger tl;

        /// <summary>
        /// Private variable to hold a reference to the real filter wheel we're controlling
        /// </summary>
        private ASCOM.DriverAccess.FilterWheel filterWheel;

        /// <summary>
        /// Private variable to hold a reference to the real focuser we're controlling
        /// </summary>
        private ASCOM.DriverAccess.Focuser focuser;

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkSkyGeek"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public FilterWheelProxy()
        {
            tl = new TraceLogger("", "DarkSkyGeek");
            ReadProfile();
            tl.LogMessage("FilterWheel", "Starting initialization");
            connectedState = false;
            tl.LogMessage("FilterWheel", "Completed initialization");
        }

        //
        // PUBLIC COM INTERFACE IFilterWheelV2 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (IsConnected)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (FilterWheelSetupDialogForm F = new FilterWheelSetupDialogForm(tl))
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            LogMessage("", "Action {0} not implemented", actionName);
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
        }

        public bool Connected
        {
            get
            {
                LogMessage("Connected", "Get {0}", IsConnected);
                return IsConnected;
            }
            set
            {
                tl.LogMessage("Connected", "Set {0}", value);
                if (value == IsConnected)
                    return;

                if (value)
                {
                    if (string.IsNullOrEmpty(filterWheelId))
                    {
                        throw new ASCOM.InvalidValueException("You have not specified which filter wheel to connect to");
                    }
                    filterWheel = new ASCOM.DriverAccess.FilterWheel(filterWheelId);
                    focuser = new ASCOM.DriverAccess.Focuser(Focuser.driverID);
                    connectedState = true;
                }
                else
                {
                    connectedState = false;
                    filterWheel.Dispose();
                    focuser.Dispose();
                }
            }
        }

        public string Description
        {
            get
            {
                tl.LogMessage("Description Get", deviceName);
                return deviceName;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = deviceName + " ASCOM Driver Version " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            get
            {
                LogMessage("InterfaceVersion Get", "2");
                return Convert.ToInt16("2");
            }
        }

        public string Name
        {
            get
            {
                tl.LogMessage("Name Get", deviceName);
                return deviceName;
            }
        }

        #endregion

        #region IFilerWheel Implementation

        public int[] FocusOffsets
        {
            get
            {
                tl.LogMessage("FocusOffsets Get", "[ " + String.Join(", ", filterOffsets) + " ]");
                return filterOffsets;
            }
        }

        public string[] Names
        {
            get
            {
                tl.LogMessage("Names Get", "[ " + String.Join(", ", filterNames) + " ]");
                return filterNames;
            }
        }

        public short Position
        {
            get
            {
                CheckConnected("Position");
                return filterWheel.Position;
            }
            set
            {
                CheckConnected("Position");
                filterWheel.Position = value;
            }
        }

        #endregion

        #region Private properties and methods

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "FilterWheel";
                if (bRegister)
                {
                    P.Register(driverID, deviceName);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "FilterWheel";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
                filterWheelId = driverProfile.GetValue(driverID, filterWheelIdProfileName, string.Empty, filterWheelIdDefault);

                string filterNamesProfileValue = driverProfile.GetValue(driverID, filterNamesProfileName, string.Empty, string.Empty);
                if (filterNamesProfileValue != string.Empty)
                {
                    filterNames = filterNamesProfileValue.Split(',');
                }

                string filterOffsetsProfileValue = driverProfile.GetValue(driverID, filterOffsetsProfileName, string.Empty, string.Empty);
                if (filterOffsetsProfileValue != string.Empty)
                {
                    filterOffsets = Array.ConvertAll(filterOffsetsProfileValue.Split(','), int.Parse);
                }
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "FilterWheel";
                driverProfile.WriteValue(driverID, traceStateProfileName, tl.Enabled.ToString());
                driverProfile.WriteValue(driverID, filterWheelIdProfileName, filterWheelId);
                driverProfile.WriteValue(driverID, filterNamesProfileName, String.Join(",", filterNames));
                driverProfile.WriteValue(driverID, filterOffsetsProfileName, String.Join(",", filterOffsets));
            }
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            tl.LogMessage(identifier, msg);
        }

        #endregion
    }
}
