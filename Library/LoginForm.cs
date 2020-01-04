using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class LoginForm : Form
    {
        SqlConnection Conn = new SqlConnection("workstation id=newlibrary.mssql.somee.com;packet size=4096;user id=kuzyabnn_SQLLogin_1;pwd=i7ix8nah9i;data source=newlibrary.mssql.somee.com;persist security info=False;initial catalog=newlibrary");
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            SqlCommand Cmd = new SqlCommand("SELECT COUNT(*) FROM Librarian WHERE Login = '"+textBox1.Text+ "' AND Password = '" + textBox2.Text + "' ", Conn);
            Conn.Open();
            object result = Cmd.ExecuteScalar();
            int a = Convert.ToInt32(result);
            Conn.Close();
            if (a > 0)
            {
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else MessageBox.Show("Неверный логин или пароль");
        }
    }
}
