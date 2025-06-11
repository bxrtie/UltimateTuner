using UltimateTuner.Core;

namespace UltimateTuner.Modules
{
    /// <summary>
    /// Optimizes HID device settings.
    /// </summary>
    public class HidOptimizer
    {
        public void DisableSelectiveSuspend()
        {
            var tweaks = new Dictionary<string, object>
            {
                { "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\USB\\DisableSelectiveSuspend", 1 }
            };
            new RegistryEditor().ApplyRegistryTweaks(tweaks);
        }
    }
}
