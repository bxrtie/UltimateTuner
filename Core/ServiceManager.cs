using System;
using System.ServiceProcess;
using System.Collections.Generic;

namespace UltimateTuner.Core
{
    /// <summary>
    /// Provides control over Windows services.
    /// </summary>
    public class ServiceManager
    {
        private readonly Dictionary<string, ServiceStartMode> _backup = new();

        public void StopAndDisable(params string[] serviceNames)
        {
            foreach (var name in serviceNames)
            {
                using var svc = new ServiceController(name);
                _backup[name] = svc.StartType;
                if (svc.Status != ServiceControllerStatus.Stopped)
                {
                    svc.Stop();
                    svc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                }
                SetStartMode(name, ServiceStartMode.Disabled);
            }
        }

        public void Restore()
        {
            foreach (var kvp in _backup)
            {
                SetStartMode(kvp.Key, kvp.Value);
            }
            _backup.Clear();
        }

        private void SetStartMode(string serviceName, ServiceStartMode mode)
        {
            using var mgr = new ServiceController(serviceName);
            var keyPath = $"SYSTEM\\CurrentControlSet\\Services\\{serviceName}";
            using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath, true);
            key?.SetValue("Start", (int)mode, Microsoft.Win32.RegistryValueKind.DWord);
        }
    }
}
