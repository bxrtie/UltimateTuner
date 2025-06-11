using System;
using UltimateTuner.Core;

namespace UltimateTuner.Modules
{
    /// <summary>
    /// Provides CPU affinity and parking tweaks.
    /// </summary>
    public class CpuTweaks
    {
        public void SetProcessAffinity(int processId, ulong mask)
        {
            try
            {
                var process = System.Diagnostics.Process.GetProcessById(processId);
                process.ProcessorAffinity = (IntPtr)mask;
                Logger.Log($"Set affinity mask 0x{mask:X} for PID {processId}");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
        }
    }
}
