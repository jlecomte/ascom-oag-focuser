/*
 * FilterWheelDriver.cs
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

using ASCOM.DeviceInterface;
using ASCOM.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

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
        private static readonly string deviceName = "DarkSkyGeek's Filter Wheel Proxy For OAG Focuser";

        // Constants used for Profile persistence
        internal static int MAX_FILTER_COUNT = 8;

        internal FilterWheelProxyProfiles profiles = null;

        internal static string traceStateKeyName = "Trace Level";
        internal static string traceStateDefault = "true";

        internal static string profileXmlPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Dark Sky Geek",
            "Filter Wheel Proxy Configuration Profiles.xml"
        );

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
        private DriverAccess.FilterWheel filterWheel;

        /// <summary>
        /// Private variable to hold a reference to the real focuser we're controlling
        /// </summary>
        private DriverAccess.Focuser focuser;

        /// <summary>
        // Object used to synchronize access to the underlying devices. This is especially important
        // when used in a multi-threaded application and the `SetCurrentProfile` action is invoked.
        /// </summary>
        private readonly object lockObject = new object();

        // Various constants...
        private const string OK = "OK";

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkSkyGeek"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public FilterWheelProxy()
        {
            tl = new TraceLogger("", "DarkSkyGeek");

            LogMessage("FilterWheel", "Starting initialization");

            ReadProfile();

            if (profiles == null)
            {
                profiles = new FilterWheelProxyProfiles();
            }

            if (profiles.profiles.Count == 0)
            {
                var defaultProfile = CreateDefaultProfile();
                profiles.currentlySelectedProfileName = defaultProfile.name;
                profiles.profiles.Add(defaultProfile);
            }

            connectedState = false;

            LogMessage("FilterWheel", "Completed initialization");
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
            if (IsConnected)
            {
                MessageBox.Show("Settings cannot be updated once the device has been connected. Disconnect the device first, so you can update its settings.");
                return;
            }

            using (FilterWheelSetupDialogForm F = new FilterWheelSetupDialogForm(this))
            {
                var result = F.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                LogMessage("SupportedActions Get", "Returning [\"GetProfiles\", \"GetCurrentProfile\", \"SetCurrentProfile\"]");
                return new ArrayList()
                {
                    "GetProfiles",
                    "GetCurrentProfile",
                    "SetCurrentProfile"
                };
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            switch (actionName.ToUpper())
            {
                case "GETPROFILES":
                    return GetProfileNamesAsJSON();

                case "GETCURRENTPROFILE":
                    return profiles.currentlySelectedProfileName;

                case "SETCURRENTPROFILE":
                    var profileName = actionParameters;
                    SelectProfile(profileName);
                    return OK;

                default:
                    LogMessage("", "Action {0} not implemented", actionName);
                    throw new ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
            }
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            throw new MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            throw new MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            throw new MethodNotImplementedException("CommandString");
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
                LogMessage("Connected", "Set {0}", value);
                if (value == IsConnected)
                    return;

                lock (lockObject)
                {
                    if (value)
                    {
                        var profile = GetSelectedProfile();

                        if (string.IsNullOrEmpty(profile.filterWheelId))
                        {
                            throw new InvalidValueException("You have not specified which filter wheel to connect to");
                        }

                        if (string.IsNullOrEmpty(profile.focuserId))
                        {
                            throw new InvalidValueException("You have not specified which focuser to connect to");
                        }

                        try
                        {
                            filterWheel = new DriverAccess.FilterWheel(profile.filterWheelId)
                            {
                                Connected = true
                            };

                            focuser = new DriverAccess.Focuser(profile.focuserId)
                            {
                                Connected = true
                            };

                            connectedState = true;
                        }
                        catch (Exception e)
                        {
                            if (filterWheel != null)
                            {
                                filterWheel.Connected = false;
                                filterWheel.Dispose();
                                filterWheel = null;
                            }

                            if (focuser != null)
                            {
                                focuser.Connected = false;
                                focuser.Dispose();
                                focuser = null;
                            }

                            throw e;
                        }
                    }
                    else
                    {
                        connectedState = false;

                        filterWheel.Connected = false;
                        filterWheel.Dispose();
                        filterWheel = null;

                        focuser.Connected = false;
                        focuser.Dispose();
                        focuser = null;
                    }
                }
            }
        }

        public string Description
        {
            get
            {
                LogMessage("Description Get", deviceName);
                return deviceName;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = deviceName + " ASCOM Driver Version " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                LogMessage("DriverVersion Get", driverVersion);
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
                LogMessage("Name Get", deviceName);
                return deviceName;
            }
        }

        #endregion

        #region IFilerWheel Implementation

        public int[] FocusOffsets
        {
            get
            {
                var profile = GetSelectedProfile();
                LogMessage("FocusOffsets Get", "[ " + String.Join(", ", profile.filterOffsets) + " ]");
                return profile.filterOffsets.ToArray();
            }
        }

        public string[] Names
        {
            get
            {
                var profile = GetSelectedProfile();
                LogMessage("Names Get", "[ " + String.Join(", ", profile.filterNames) + " ]");
                return profile.filterNames.ToArray();
            }
        }

        // And this is where literally all the magic happens. No kidding!
        // This is where we intercept filter wheel position changes, forward
        // calls to the real filter wheel driver, and ask the OAG focuser
        // driver to change its focus position.
        public short Position
        {
            get
            {
                lock (lockObject)
                {
                    CheckConnected("Position");
                    return filterWheel.Position;
                }
            }
            set
            {
                lock (lockObject)
                {
                    CheckConnected("Position");

                    if (focuser.IsMoving)
                    {
                        throw new DriverException("Cannot switch filters while the OAG focuser is still moving from the previous filter change. Please wait and try again.");
                    }

                    var profile = GetSelectedProfile();
                    LogMessage("FilterWheel", $"Using profile {profile.name}");
                    short oldPosition = filterWheel.Position;
                    short newPosition = value;

                    filterWheel.Position = newPosition;

                    int oldFilterOffset = profile.filterOffsets[oldPosition];
                    int newFilterOffset = profile.filterOffsets[newPosition];
                    int delta = (int)((newFilterOffset - oldFilterOffset) * profile.stepRatio);
                    LogMessage("FilterWheel", $"oldFilterOffset = {oldFilterOffset}, newFilterOffset = {newFilterOffset}, delta = {delta}");
                    if (delta > 0)
                    {
                        // If we're moving OUT, we overshoot to deal with backlash...
                        focuser.Move(focuser.Position + profile.backlashCompSteps + delta);

                        // Wait for the focuser to reach the desired position...
                        while (focuser.IsMoving)
                        {
                            Thread.Sleep(100);
                        }

                        // Once the focuser has stopped moving, we tell it to move to its final position...
                        focuser.Move(focuser.Position - profile.backlashCompSteps);
                    }
                    else
                    {
                        // If we're moving IN, we don't have any backlash compensation code to apply.
                        focuser.Move(focuser.Position + delta);
                    }
                }
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
            using (var P = new Profile())
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
                throw new NotConnectedException(message);
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

                try
                {
                    // Read the "Trace on" config from the ASCOM device profile...
                    tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateKeyName, string.Empty, traceStateDefault));

                    // Read the profiles from disk:
                    var profilesXml = File.ReadAllText(profileXmlPath);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(FilterWheelProxyProfiles));
                    TextReader reader = new StringReader(profilesXml);
                    profiles = (FilterWheelProxyProfiles) xmlSerializer.Deserialize(reader);
                }
                catch (Exception e)
                {
                    LogMessage("FilterWheel", "ReadProfile: Exception handled: " + e.Message);
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

                // The "Trace on" config is persisted to the ASCOM device profile...
                driverProfile.WriteValue(driverID, traceStateKeyName, tl.Enabled.ToString());

                // The profiles are persisted to disk, in an XML file:
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(FilterWheelProxyProfiles));
                StringWriter textWriter = new StringWriter();
                xmlSerializer.Serialize(textWriter, profiles);

                // Make sure the directory exists first...
                var fileInfo = new FileInfo(profileXmlPath);
                fileInfo.Directory.Create();

                // Then, write the file.
                File.WriteAllText(profileXmlPath, textWriter.ToString());
            }
        }

        /// <summary>
        /// Creates a default profile
        /// </summary>
        internal FilterWheelProxyProfile CreateDefaultProfile()
        {
            var profile = new FilterWheelProxyProfile
            {
                name = "Default"
            };
            return profile;
        }

        /// <summary>
        /// Returns the profile with the specified name
        /// </summary>
        internal FilterWheelProxyProfile GetProfile(string name)
        {
            return profiles.profiles.Find(x => x.name.Trim() == name.Trim());
        }

        /// <summary>
        /// Returns the currently selected profile
        /// </summary>
        internal FilterWheelProxyProfile GetSelectedProfile()
        {
            var profile = GetProfile(profiles.currentlySelectedProfileName);

            if (profile == null)
            {
                if (profiles.profiles.Count > 0)
                {
                    profile = profiles.profiles[0];
                }
                else
                {
                    profile = CreateDefaultProfile();
                    profiles.profiles.Add(profile);
                }

                profiles.currentlySelectedProfileName = profile.name;
            }

            return profile;
        }

        /// <summary>
        /// Returns the list of known profiles as a JSON array
        /// </summary>
        internal string GetProfileNamesAsJSON()
        {
            return "[" + string.Join(",", profiles.profiles.Select(profile => "\"" + profile.name.Trim().Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"").ToArray()) + "]";
        }

        /// <summary>
        /// Changes the selected profile. If needed, this will cause the driver to connect to different underlying devices
        /// depending on how the user set up their profiles. In practice, this is unlikely, however, because profiles are
        /// mostly used to accommodate for different filter offsets (depending, for example, on whether you use a reducer)
        /// with the same filter wheel and OAG focuser devices...
        /// </summary>
        internal void SelectProfile(string profileName)
        {
            profileName = profileName.Trim();
            if (profileName == profiles.currentlySelectedProfileName.Trim())
            {
                // Nothing to do, my best kind of work :)
                return;
            }

            var newProfile = GetProfile(profileName) ?? throw new InvalidValueException("Unknown profile name: " + profileName);

            if (!Connected)
            {
                profiles.currentlySelectedProfileName = profileName;
                WriteProfile();
                return;
            }

            if (string.IsNullOrEmpty(newProfile.filterWheelId))
            {
                throw new InvalidValueException("You have not specified a filter wheel in profile " + profileName);
            }

            if (string.IsNullOrEmpty(newProfile.focuserId))
            {
                throw new InvalidValueException("You have not specified a focuser  in profile " + profileName);
            }

            lock (lockObject)
            {
                var oldProfile = GetSelectedProfile();

                if (newProfile.filterWheelId != oldProfile.filterWheelId)
                {
                    var oldFilterWheel = filterWheel;

                    filterWheel = new DriverAccess.FilterWheel(newProfile.filterWheelId)
                    {
                        Connected = true
                    };

                    oldFilterWheel.Connected = false;
                    oldFilterWheel.Dispose();
                }

                if (newProfile.focuserId != oldProfile.focuserId)
                {
                    var oldFocuser = focuser;

                    focuser = new DriverAccess.Focuser(newProfile.focuserId)
                    {
                        Connected = true
                    };

                    oldFocuser.Connected = false;
                    oldFocuser.Dispose();
                }

                profiles.currentlySelectedProfileName = profileName;
            }

            WriteProfile();
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

    public class FilterWheelProxyProfile
    {
        public string name = null;
        public string filterWheelId = null;
        public string focuserId = null;
        public List<string> filterNames = new List<string>();
        public List<int> filterOffsets = new List<int>();
        public int backlashCompSteps = 0;
        public decimal stepRatio = 1.0m;
    }

    public class FilterWheelProxyProfiles
    {
        public string currentlySelectedProfileName = null;
        public List<FilterWheelProxyProfile> profiles = new List<FilterWheelProxyProfile>();
    }
}
