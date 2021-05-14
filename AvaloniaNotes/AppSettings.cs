using System;
using AuraUtilities.Configuration;

namespace AvaloniaNotes
{
    [Serializable]
    public class AppSettings : Settings
    {
        public string AccentColor { get; set; }
        public Theme Theme { get; set; }
    }

    [Serializable]
    public enum Theme
    {
        Light,
        Dark
    }
}