using System.Collections.Generic;
using UltimateTuner.Core;

namespace UltimateTuner.Modules
{
    /// <summary>
    /// Implements network stack optimizations.
    /// </summary>
    public class NetworkTuning
    {
        private readonly RegistryEditor _reg = new();

        public void Apply()
        {
            var tweaks = new Dictionary<string, object>
            {
                { "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\TcpAckFrequency", 1 },
                { "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\TcpNoDelay", 1 }
            };
            _reg.ApplyRegistryTweaks(tweaks);
        }

        public void Restore()
        {
            _reg.RestoreDefaults();
        }
    }
}
