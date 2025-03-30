using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class SessionStore
    {
        private static readonly SessionStore _instance = new SessionStore();
        private Dictionary<string, string> _store = new Dictionary<string, string>();

        private SessionStore() { }

        public static SessionStore Instance => _instance;

        public void SetValue(string key, string value)
        {
            _store[key] = value;
            Console.WriteLine($"Setting {key}: {value}");
            FireEvent(key, value);

        }

        public string GetValue(string key)
        {
            return _store.ContainsKey(key) ? _store[key] : null;
        }

        private void FireEvent(string key, string value)
        {
            Console.WriteLine($"Attempting to Inject {key}: {value}");
            if (MainForm.Browser != null && MainForm.Browser.IsBrowserInitialized && !MainForm.Browser.IsDisposed)
            {
                string script = $"window.sessionData = window.sessionData || {{}}; window.sessionData['{key}'] = '{value}';";
                MainForm.Browser.ExecuteScriptAsync(script);
                Console.WriteLine($"Injected {key}: {value}");
            }
            else
            {
                Console.WriteLine("Browser not ready or disposed");
            }
        }

    }

}
