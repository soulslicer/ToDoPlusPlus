//@raaj A0081202Y
using System;

namespace ToDo
{
    public static class EventHandlers
    {
        public static event EventHandler StayOnTopHandler;
        public static void StayOnTop(bool status){ StayOnTopHandler(status, EventArgs.Empty);}

        public static event EventHandler UpdateSettingsHandler;
        public static void UpdateSettings(SettingInformation settingsList) { UpdateSettingsHandler(settingsList, EventArgs.Empty); }

        public static event EventHandler UpdateUIHandler;
        public static void UpdateUI() { UpdateUIHandler(null, EventArgs.Empty); }
    }
}
