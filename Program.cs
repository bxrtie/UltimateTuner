using UltimateTuner.Core;
using UltimateTuner.Modules;

namespace UltimateTuner
{
    internal class Program
    {
        static void Main()
        {
            var windows = new WindowsTweaks();
            windows.Apply();

            var network = new NetworkTuning();
            network.Apply();

            Logger.Log("Tuning applied. Press any key to restore...");
            Console.ReadKey();

            network.Restore();
            windows.Restore();
        }
    }
}

