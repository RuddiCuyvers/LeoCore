using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WGK.Lib.Helpers
{
    /// <summary>
    /// Copies large streams by allocating a small temporary buffer
    /// </summary>
    public static class StreamHelper
    {
        public static void CopyStream(Stream pInput, Stream pOutput)
        {
            var vBuffer = new byte[32768];
            while (true)
            {
                int vRead = pInput.Read(vBuffer, 0, vBuffer.Length);
                if (vRead <= 0)
                {
                    return;
                }
                pOutput.Write(vBuffer, 0, vRead);
            }
        }
    }
}
