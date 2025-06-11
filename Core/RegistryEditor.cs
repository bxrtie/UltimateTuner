using Microsoft.Win32;
using System.Collections.Generic;

namespace UltimateTuner.Core
{
    /// <summary>
    /// Provides helper methods to modify the Windows registry with backup.
    /// </summary>
    public class RegistryEditor
    {
        private readonly Dictionary<string, object?> _backup = new();

        public void ApplyRegistryTweaks(Dictionary<string, object> tweaks)
        {
            foreach (var kvp in tweaks)
            {
                var hiveSplit = kvp.Key.Split('\\', 2);
                var hive = hiveSplit[0];
                var path = hiveSplit[1];
                using var key = OpenKey(hive, path, true);
                var name = path[(path.LastIndexOf('\\') + 1)..];
                if (!_backup.ContainsKey(kvp.Key))
                    _backup[kvp.Key] = key.GetValue(name);
                key.SetValue(name, kvp.Value);
            }
        }

        public void RestoreDefaults()
        {
            foreach (var kvp in _backup)
            {
                var hiveSplit = kvp.Key.Split('\\', 2);
                var hive = hiveSplit[0];
                var path = hiveSplit[1];
                using var key = OpenKey(hive, path, true);
                var name = path[(path.LastIndexOf('\\') + 1)..];
                if (kvp.Value == null)
                    key.DeleteValue(name, false);
                else
                    key.SetValue(name, kvp.Value);
            }
            _backup.Clear();
        }

        private RegistryKey OpenKey(string hive, string path, bool writable)
        {
            RegistryKey baseKey = hive switch
            {
                "HKEY_LOCAL_MACHINE" => Registry.LocalMachine,
                "HKEY_CURRENT_USER" => Registry.CurrentUser,
                _ => throw new System.ArgumentException("Unsupported hive")
            };
            return baseKey.CreateSubKey(path, writable);
        }
    }
}
