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
    public partial class LoginForm : Form
    {
        //private StateObject _state;
        public LoginForm()
        {
            InitializeComponent();
            //_state = state;
        }


        private bool AuthenticateUser(string username, string password)
        {
            // Dummy authentication - Replace with API call or database check
            return username == "admin" && password == "password";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (AuthenticateUser(txtUserName.Text, txtPassword.Text))
            {
                string token = JwtHelper.GenerateToken(txtUserName.Text);
               // _state.SetValue("JWT", token);
                //Console.WriteLine($"Generated Token: {token}");
                MainForm main = new MainForm(token);
                this.Hide();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid Credentials");
            }
        }
    }
}
