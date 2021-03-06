﻿namespace BaristaLabs.ChromeDevTools
{
    using Newtonsoft.Json;
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents the chrome version information retrieved from chrome.
    /// </summary>
    public class ChromeVersion
    {
        [JsonProperty(PropertyName = "Browser")]
        public string Browser
        {
            get;
            set;
        }

        [JsonIgnore]
        public string BrowserVersion
        {
            get { return Browser.Replace("Chrome/", ""); }
        }

        [JsonProperty(PropertyName = "Protocol-Version")]
        public string ProtocolVersion
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "User-Agent")]
        public string UserAgent
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "V8-Version")]
        public string V8Version
        {
            get;
            set;
        }

        [JsonIgnore]
        public string V8VersionNumber
        {
            get
            {
                //Get the v8 version
                var v8VersionRegex = new Regex(@"^(\d+)\.(\d+)\.(\d+)(\.\d+.*)?");
                var v8VersionMatch = v8VersionRegex.Match(V8Version);
                if (v8VersionMatch.Success == false || v8VersionMatch.Groups.Count < 4)
                    throw new InvalidOperationException($"Unable to determine v8 version number from v8 version string ({V8Version})");

                return $"{v8VersionMatch.Groups[1].Value}.{v8VersionMatch.Groups[2].Value}.{v8VersionMatch.Groups[3].Value}";
            }
        }

        [JsonProperty(PropertyName = "WebKit-Version")]
        public string WebKitVersion
        {
            get;
            set;
        }

        [JsonIgnore]
        public string WebKitVersionHash
        {
            get
            {
                //Get the webkit version hash.
                var webkitVersionRegex = new Regex(@"\s\(@(\b[0-9a-f]{5,40}\b)");
                var webkitVersionMatch = webkitVersionRegex.Match(WebKitVersion);
                if (webkitVersionMatch.Success == false || webkitVersionMatch.Groups.Count != 2)
                    throw new InvalidOperationException($"Unable to determine webkit version hash from webkit version string ({WebKitVersion})");

                return webkitVersionMatch.Groups[1].Value;
            }
        }
    }
}
