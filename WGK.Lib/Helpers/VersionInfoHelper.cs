using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WGK.Lib.Configuration;


namespace WGK.Lib.Helpers
{
    public static class VersionInfoHelper
    {
        public static VersionInfo GetVersionInfo(Assembly pExecutingAssembly)
        {
            // Get the version from the assembly
            string vApplicationVersion = "v" + pExecutingAssembly.GetName().Version.ToString();

            // Get the Copyright info from the assembly
            var vAssemblyCopyrightAttribute = AssemblyCopyrightAttribute.GetCustomAttribute(
                pExecutingAssembly,
                typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute;
            string vCopyright = string.Empty;
            if (vAssemblyCopyrightAttribute != null)
            {
                vCopyright = vAssemblyCopyrightAttribute.Copyright;
            }

            // Get the date from the assembly
            var vFileInfo = new System.IO.FileInfo(pExecutingAssembly.Location);
            string vApplicationDate = vFileInfo.LastWriteTime.ToString("g"); // date + time

            // Get environment
            var vEnvironment = ApplicationConfigurationSection.Current.Environment;
#if DEBUG
            vEnvironment += " (debug build)";
#else
            vEnvironment += " (release build)";
#endif

            // Get host version
            // TODO Get version of the WCF host
            var vServer = string.Empty;

            // Get culture
            var vCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;

            // Get referenced assemblies
            var vAssemblyNames = new List<AssemblyName> { pExecutingAssembly.GetName() };
            vAssemblyNames.AddRange(
                pExecutingAssembly.GetReferencedAssemblies().OrderBy(p => p.Name)
            );
            var vAssemblyInfoArray = vAssemblyNames
                .Select(p => new AssemblyInfo()
                {
                    Name = p.Name,
                    Version = p.Version.ToString()
                })
                .ToArray();

            return new VersionInfo()
            {
                ApplicationVersion = vApplicationVersion,
                ApplicationDate = vApplicationDate,
                RuntimeVersion = pExecutingAssembly.ImageRuntimeVersion,
                Environment = vEnvironment,
                Copyright = vCopyright,

                Host = vServer,
                Culture = vCulture,

                ReferencedAssemblies = vAssemblyInfoArray
            };
        }
    }
}
