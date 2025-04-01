using CefSharp.WinForms;
using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        public static ChromiumWebBrowser Browser;
        private readonly string _token;
        public MainForm(string token)
        {
            InitializeComponent();
            _token = token;
            Browser = new ChromiumWebBrowser("http://localhost:3000");
            Browser.Dock = DockStyle.Fill;
            Browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
            //Browser.LoadingStateChanged += Browser_LoadingStateChanged;
            this.Controls.Add(Browser);
            //SessionStore.Instance.SetValue("JWT", token);
        }
        private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                
                Console.WriteLine("CEF: Page Loaded");
            }
        }
        private void Browser_IsBrowserInitializedChanged(object sender, EventArgs e)
        {
            if (Browser.IsBrowserInitialized)
            {
                //Console.WriteLine("Browser Initialized");
                //Console.WriteLine($"Setting JWT: {_token}");
                //Browser.ShowDevTools();

               

                // Store session data only when browser is ready
                SessionStore.Instance.SetValue("Username", "admin");
                SessionStore.Instance.SetValue("Role", "Admin");
                SessionStore.Instance.SetValue("JWT", _token);
            }
        }
    }

}
