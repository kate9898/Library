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
    
    public partial class MainForm : Form
    {
        SqlConnection Conn = new SqlConnection("workstation id=newlibrary.mssql.somee.com;packet size=4096;user id=kuzyabnn_SQLLogin_1;pwd=i7ix8nah9i;data source=newlibrary.mssql.somee.com;persist security info=False;initial catalog=newlibrary");
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (textBox1.Text != "")
            {
                label17.Visible = true; label18.Visible = true;
                label15.Visible = true; label16.Visible = true;
                label14.Visible = true; label13.Visible = true;
                label12.Visible = true; label11.Visible = true;
                try
                {
                    SqlCommand Cmd = new SqlCommand("SELECT Full_name, ID_Student, Data_LC, Number_SC, Data_SC, Class.Name as CName, Specialty.Name as SName, Institute.Name as IName " +
                        "FROM Students, Class, Specialty, Institute WHERE ID_Student  = '" + textBox1.Text + "' AND Students.Class_ID = Class.ID_Class AND Specialty.ID_Specialty = Class.Specialty_ID " +
                        "AND Institute.ID_Institute = Specialty.Institute_ID; ", Conn);
                    Conn.Open();
                    SqlDataReader sdr = Cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                    string[] row = new string[] {
                    label17.Text = Convert.ToString(sdr["Full_name"]),
                    label18.Text = Convert.ToString(sdr["ID_Student"]),
                    label15.Text = Convert.ToString(sdr["Data_LC"]),
                    label16.Text =   Convert.ToString(sdr["Number_SC"]),
                    label14.Text =   Convert.ToString(sdr["Data_SC"]),
                    label13.Text = Convert.ToString(sdr["CName"]),
                    label12.Text = Convert.ToString(sdr["SName"]),
                    label11.Text =   Convert.ToString(sdr["IName"])};
                    }
                    Conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
                try
                {
                    SqlCommand Cmd = new SqlCommand("SELECT Book.Name AS BName, Author.Full_name AS AName, Journal.Code AS JCode, Journal.Date_issue AS DIssue, Journal.Date_return_P AS DReturn, Journal.Date_return_F AS DReturnF, Journal.Number_account AS JNum, Journal.Amount AS Amount, Journal.Paid AS Paid, Librarian.Full_name AS LName " +
                        "FROM Journal, Book, Students, Author, Librarian WHERE Student_ID = '" + textBox1.Text + "' and Book.ID_Book = Journal.Book_ID AND Librarian.ID_Librarian = Journal.Librarian_ID AND " +
                        "Students.ID_Student = Journal.Student_ID and Book.Author_ID = Author.ID_Author ORDER BY Journal.Date_issue; ", Conn);
                    Conn.Open();
                    SqlDataReader sdr = Cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        string[] row = new string[] { Convert.ToString(sdr["BName"]), Convert.ToString(sdr["AName"]), Convert.ToString(sdr["JCode"]), Convert.ToString(sdr["DIssue"]),
                        Convert.ToString(sdr["DReturn"]), Convert.ToString(sdr["DReturnF"]), Convert.ToString(sdr["JNum"]), Convert.ToString(sdr["Amount"]),
                       Convert.ToString(sdr["Paid"]), Convert.ToString(sdr["LName"])};
                        dataGridView1.Rows.Add(row);
                    }
                    Conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                DateTime date = DateTime.Now.Date;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (date < Convert.ToDateTime(dataGridView1.Rows[i].Cells[4].Value))
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
