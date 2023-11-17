using System.Collections.Generic;


namespace WGK.Lib.Helpers
{
    public class VersionInfo
    {
        /// <summary>
        /// Versienummer van de toepassing
        /// </summary>
        public string ApplicationVersion { get; set; }

        /// <summary>
        /// Build datum en tijd van de toepassing
        /// </summary>
        public string ApplicationDate { get; set; }

        /// <summary>
        /// Versienummer van het .NET framework dat door de toepassing gebruikt wordt
        /// </summary>
        public string RuntimeVersion { get; set; }

        /// <summary>
        /// Build omgeving van de toepassing (debug/release, development/test/productie)
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Copyright info
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// Host versie nummer
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Culture string (taal-land) gebruikt op de server
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Collectie van gerefereerde .NET assemblies
        /// </summary>
        public IEnumerable<AssemblyInfo> ReferencedAssemblies { get; set; }
    }
}
