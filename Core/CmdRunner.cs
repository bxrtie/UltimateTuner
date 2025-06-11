using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace UltimateTuner.Core
{
    /// <summary>
    /// Utility to run command line processes.
    /// </summary>
    public static class CmdRunner
    {
        public static async Task<string> RunAsync(string fileName, string arguments)
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            if (proc == null)
                throw new InvalidOperationException($"Failed to start {fileName}");

            string output = await proc.StandardOutput.ReadToEndAsync();
            string error = await proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();

            if (!string.IsNullOrWhiteSpace(error))
                Logger.Log(error);

            return output;
        }
    }
}
