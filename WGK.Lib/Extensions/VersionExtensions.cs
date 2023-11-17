using System;

namespace WGK.Lib.Extensions
{
    public static class VersionExtensions
    {
        public static string MajorMinorBuild(this Version pVersion)
        {
            return string.Format("{0}.{1}.{2}", pVersion.Major, pVersion.Minor, pVersion.Build);
        }
    }
}