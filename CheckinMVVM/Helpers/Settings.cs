using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CheckinMVVM.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string BaseUriKey = "Base_Uri";
        private static readonly string BaseUriDefault = "https://checkinserviceapi.azurewebsites.net/api/";

        private const string GatewayUriKey = "Gateway_Uri";
        private static readonly string GatewayUriDefault = "https://nwgateway.keells.lk:44300";

        private const string HotelCodeKey = "Hotel_Code";
        private static readonly string HotelCodeDefault = "3000";

        private const string UserNameKey = "User_Name";
        private static readonly string UserNameDefault = "thimiraa";

        #endregion


        public static string BaseUri
        {
            get
            {
                return AppSettings.GetValueOrDefault(BaseUriKey, BaseUriDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(BaseUriKey, value);
            }
        }

        public static string GatewayUri
        {
            get
            {
                return AppSettings.GetValueOrDefault(GatewayUriKey, GatewayUriDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(GatewayUriKey, value);
            }
        }

        public static string HotelCode
        {
            get
            {
                return AppSettings.GetValueOrDefault(HotelCodeKey, HotelCodeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(HotelCodeKey, value);
            }
        }

        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserNameKey, value);
            }
        }

    }
}
