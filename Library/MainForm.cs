﻿using System;
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
        SqlConnection Conn = new SqlConnection(@"Data Source=DESKTOP-KKGIP26\SQLEXPRESS;Initial Catalog=newlibrary;Integrated Security=True");
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            button4.Enabled = true;
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
                        "Students.ID_Student = Journal.Student_ID and Book.Author_ID = Author.ID_Author ORDER BY DIssue; ", Conn);
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
                    if ((date > Convert.ToDateTime(dataGridView1.Rows[i].Cells[4].Value) && dataGridView1.Rows[i].Cells[5].Value.ToString() == ""))
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
                        button4.Enabled = false;
                    }
                    else if (dataGridView1.Rows[i].Cells[6].Value.ToString() != "" && Convert.ToBoolean(dataGridView1.Rows[i].Cells[8].Value) == false)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
                        button4.Enabled = false;
                    }
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;

            try
            {
                SqlCommand Cmd = new SqlCommand("SELECT Name FROM Institute", Conn);
                Conn.Open();
                SqlDataReader sdr = Cmd.ExecuteReader();
                while (sdr.Read())
                {
                    comboBox1.Items.Add(sdr["Name"].ToString());
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            if (textBox2.Text != "")
            {           
                try
                {
                    SqlCommand Cmd = new SqlCommand("SELECT Book.Name as BName, Author.Full_name as AName, Publisher.Name as PName, Book.Book_year, Quantity " +
                        "FROM Book, Author, Publisher WHERE Book.Author_ID = Author.ID_Author and Book.Publisher_ID = Publisher.ID_Publisher " +
                        "and(Book.Name LIKE '" + '%' + textBox2.Text + "%'" + "or Author.Full_name LIKE '" + '%' + textBox2.Text + "%'" + ")", Conn);
                    Conn.Open();
                    SqlDataReader sdr = Cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        string[] row = new string[] {Convert.ToString(sdr["BName"]), Convert.ToString(sdr["AName"]), Convert.ToString(sdr["PName"]), Convert.ToString(sdr["Book_year"]),
                        Convert.ToString(sdr["Quantity"])};
                        dataGridView2.Rows.Add(row);
                    }
                    Conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



            }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox5.Text != "") { 
                string idStudent = textBox1.Text; 
            string idBook = ""; string codeBook = textBox5.Text;
            string bookName = (string)dataGridView2.CurrentRow.Cells[0].Value;
            try
            {
                SqlCommand Cmd = new SqlCommand("SELECT ID_Book FROM Book WHERE Name = '" + bookName + "'", Conn);
                Conn.Open();
                SqlDataReader sdr = Cmd.ExecuteReader();
                while (sdr.Read())
                {
                    string[] row = new string[] {
                    idBook = Convert.ToString(sdr["ID_Book"]) };
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                SqlCommand Cmd = new SqlCommand("INSERT INTO Journal (Book_ID, Librarian_ID, Student_ID, Code, Date_issue, Date_return_P, Paid) VALUES " +
                  "('" + idBook + "', '" + 1 + "', '" + idStudent + "', '" + codeBook + "', '" + DateTime.Now.Date + "', '" + dateTimePicker2.Value + "', '" + false + "')", Conn);
                Conn.Open();
                SqlDataReader sdr = Cmd.ExecuteReader();
                MessageBox.Show("Книга выдана");
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                try
                {
                    SqlCommand Cmd = new SqlCommand("UPDATE Book SET Quantity = Quantity - 1 WHERE ID_Book = '" + idBook + "'", Conn);
                    Conn.Open();
                    SqlDataReader sdr = Cmd.ExecuteReader();
                    Conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            textBox5.Text = "";
           button1_Click(sender, e);
           button2_Click_1(sender, e);
            } else MessageBox.Show("Заполните все поля");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            try
            {
                SqlCommand Cmd = new SqlCommand("SELECT Specialty.Name as SName FROM Specialty WHERE Institute_ID = (SELECT ID_Institute FROM Institute WHERE Institute.Name  ='" + comboBox1.Text + "')", Conn);
                Conn.Open();
                SqlDataReader sdr = Cmd.ExecuteReader();
                while (sdr.Read())
                {
                    comboBox2.Items.Add(sdr["SName"].ToString());
                }
                Conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка " + ex);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            try
            {
                SqlCommand Cmd = new SqlCommand("SELECT Class.Name as CName FROM Class WHERE Specialty_ID = (SELECT ID_Specialty FROM Specialty WHERE Specialty.Name  ='" + comboBox2.Text + "')", Conn);
                Conn.Open();
                SqlDataReader sdr = Cmd.ExecuteReader();
                while (sdr.Read())
                {
                    comboBox3.Items.Add(sdr["CName"].ToString());
                }
                Conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка " + ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string idClass = ""; string newId = "";
            try
            {
                SqlCommand Cmd = new SqlCommand("SELECT ID_Class FROM CLass WHERE Class.Name = '" + comboBox3.Text + "'", Conn);
                Conn.Open();
                SqlDataReader sdr = Cmd.ExecuteReader();
                while (sdr.Read())
                {
                    string[] row = new string[] {
                    idClass = Convert.ToString(sdr["ID_Class"]) };
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                SqlCommand Cmd = new SqlCommand("SELECT IDENT_CURRENT('Students')+1 as newId", Conn);
                Conn.Open();
                SqlDataReader sdr = Cmd.ExecuteReader();
                while (sdr.Read())
                {
                    string[] row = new string[] {
                    newId = Convert.ToString(sdr["newId"]) };
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                SqlCommand Cmd = new SqlCommand("INSERT INTO Students (Class_ID, Full_Name, Data_SC, Number_SC, Data_LC)  VALUES " +
                  "('" + idClass + "', '" + textBox3.Text + "', '" + dateTimePicker1.Value + "', '" + textBox4.Text + "', '" + DateTime.Now.Date + "')", Conn);
                Conn.Open();
                SqlDataReader sdr = Cmd.ExecuteReader();
                MessageBox.Show("№ читательского билета: " + newId);
                Conn.Close();
                textBox3.Text = ""; textBox4.Text = ""; dateTimePicker1.Text = ""; comboBox1.Text = ""; comboBox2.Text = ""; comboBox3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
