using System.Diagnostics;
using UltimateTuner.Core;

namespace UltimateTuner.Modules
{
    /// <summary>
    /// Launches external tools for RAM testing and configuration.
    /// </summary>
    public class RamProfileHelper
    {
        public void LaunchTm5()
        {
            var path = Path.Combine("Resources", "Tools", "TM5.bat");
            Logger.Log("Launching TM5...");
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        }
    }
}
