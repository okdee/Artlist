using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using SharedLibrary.Interfaces;

namespace FFmpegWrapperLibrary
{
    class FFmpegProcessFactory
    {
        public static Process GetFFmpegProcess(IFile file, string arguments)
        {
            return new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    WorkingDirectory = FFmpegWrapper.workingDirectory,
                    FileName = $@"{Path.Combine(FFmpegWrapper.workingDirectory, file.FullName)}",
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = arguments,
                }
            };
        }
    }
}
