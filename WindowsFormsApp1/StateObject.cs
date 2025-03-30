using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class StateObject
    {
        public Dictionary<string, string> Data { get; private set; } = new Dictionary<string, string>();

        public void SetValue(string key, string value)
        {
            if (Data.ContainsKey(key))
                Data[key] = value;
            else
                Data.Add(key, value);
        }

        public string GetValue(string key) => Data.ContainsKey(key) ? Data[key] : string.Empty;
    }

}
