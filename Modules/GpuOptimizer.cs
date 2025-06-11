using System;
using UltimateTuner.Core;

namespace UltimateTuner.Modules
{
    /// <summary>
    /// Applies NVIDIA GPU optimizations using registry imports.
    /// </summary>
    public class GpuOptimizer
    {
        public void Apply(string regFilePath)
        {
            Logger.Log("Importing GPU profile...");
            CmdRunner.RunAsync("reg", $"import \"{regFilePath}\"").Wait();
        }
    }
}
