using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace StudentManagementSystem
{
    public partial class Configure : Form
    {
        public Configure()
        {
            InitializeComponent();
        }
             

        string ServerIP;
        string DatabaseName;
        string UserName;
        string Password;
        bool WindowsAuthentication = false;

        string connectionString;
        SqlConnection connection;
        SqlCommand command;

        private void checkBoxWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWindowsAuthentication.Checked == true)
            {
                textBoxUserName.Enabled = false;
                textBoxUserName.TabStop = false;
                textBoxPassword.Enabled = false;
                textBoxPassword.TabStop = false;
            }
            else
            {
                textBoxUserName.Enabled = true;
                textBoxUserName.TabStop = true;
                textBoxPassword.Enabled = true;
                textBoxPassword.TabStop = true;

            }
        }

        private void Configure_Load(object sender, EventArgs e)
        {
            if (checkBoxWindowsAuthentication.Checked == true)
            {
                textBoxUserName.Enabled = false;
                textBoxPassword.Enabled = false;
            }
        }

        public bool check()
        {
            if (textBoxServerIP.Text.Trim().Length == 0)
            {
                return false;
            }
            if (textBoxDatabase.Text.Trim().Length == 0)
            {
                return false;
            }
            if (checkBoxWindowsAuthentication.Checked == false)
            {
                if (textBoxUserName.Text.Trim().Length == 0)
                {
                    return false;
                }
                if (textBoxPassword.Text.Trim().Length == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool store()
        {
            if (check())
            {

                ServerIP = textBoxServerIP.Text.Trim();
                DatabaseName = textBoxDatabase.Text.Trim();
                UserName = textBoxUserName.Text.Trim();
                Password = textBoxPassword.Text.Trim();

                if (checkBoxWindowsAuthentication.Checked == true)
                {
                    WindowsAuthentication = true;
                }
                else
                {
                    WindowsAuthentication = false;
                }
                return true;
            }
            else
            {
                toolStripStatusLabelStatus.Text = "Data Missing";
                return false;
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            if (store())
            {
                toolStripStatusLabelStatus.Text = "Finding Server ...";
                if (WindowsAuthentication)
                {
                    connectionString = "Data Source=" + ServerIP + ";Initial Catalog=" + DatabaseName + ";Integrated Security=True;Pooling=False";
                }
                else
                {
                    connectionString = "Data Source=" + ServerIP + ";Initial Catalog=" + DatabaseName + ";Persist Security Info=True;User ID=" + UserName + "; Password=" + Password;
                }
                connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    toolStripStatusLabelStatus.Text = "Connection Successfull";
                    connection.Close();
                }
                catch (Exception ex)
                {
                    ErrorLog.Write(ex.Message);
                    toolStripStatusLabelStatus.Text = "Connection Failed";
                }
                finally
                {
                    try
                    {
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.Write(ex.Message);
                    }
                }
            }

        }

        public string encrypt(string data)
        {
            int length = data.Length;
            int count = 0;
            char[] dataChar = data.ToCharArray();
            string cypher ="";
            foreach (char ch in dataChar)
            {
                int charEquivalent = Convert.ToInt32(ch);
                charEquivalent += 5 ;
                cypher += (char) charEquivalent;                
                count++;

            }             
            return cypher;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (store())
            {
                string fileName = "data.txt";
                try
                {
                    TextWriter tw = new StreamWriter(fileName);                   
                    tw.WriteLine(ServerIP);
                    tw.WriteLine(DatabaseName);
                    tw.WriteLine(WindowsAuthentication.ToString());
                    tw.WriteLine(UserName);
                    tw.WriteLine(encrypt(Password));
                    tw.Close();
                    toolStripStatusLabelStatus.Text = "File Writen";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
