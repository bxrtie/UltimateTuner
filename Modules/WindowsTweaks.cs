using System.Collections.Generic;
using UltimateTuner.Core;

namespace UltimateTuner.Modules
{
    /// <summary>
    /// Applies common Windows tweaks for gaming performance.
    /// </summary>
    public class WindowsTweaks
    {
        private readonly RegistryEditor _reg = new();
        private readonly ServiceManager _svc = new();

        public void Apply()
        {
            Logger.Log("Applying registry tweaks...");
            var tweaks = new Dictionary<string, object>
            {
                { "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\NetworkThrottlingIndex", 0xffffffff },
                { "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\SystemResponsiveness", 0 }
            };
            _reg.ApplyRegistryTweaks(tweaks);

            Logger.Log("Disabling unnecessary services...");
            _svc.StopAndDisable("SysMain", "DiagTrack", "WSearch", "WaaSMedicSvc");
        }

        public void Restore()
        {
            Logger.Log("Restoring registry tweaks and services...");
            _reg.RestoreDefaults();
            _svc.Restore();
        }
    }
}
