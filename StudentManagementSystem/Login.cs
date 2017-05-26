using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace StudentManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void clean()
        {
            textBoxPassword.Clear();
            textBoxUserName.Clear();
        }

        string ServerIP;
        string DatabaseName;
        string UserName;
        string Password;        
        string authentication = "";

        string Type = "ERROR!!!";
        SqlConnection connection;
        SqlCommand command;        

        private string authenticateUser(string connectionString, string UserName, String Password)
        {
            
            connection = new SqlConnection(connectionString);
            SqlParameter user = new SqlParameter("@user", SqlDbType.VarChar, 50);
            SqlParameter pass = new SqlParameter("@pass", SqlDbType.VarChar, 50);
            SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar, 50);
            user.Direction = ParameterDirection.Input;
            pass.Direction = ParameterDirection.Input;
            type.Direction = ParameterDirection.Output;
            user.Value = UserName;
            pass.Value = Password;
            command = new SqlCommand("select @type = Type from login where UserName=@user and Password=@pass", connection);
            command.Parameters.Add(user);
            command.Parameters.Add(pass);
            command.Parameters.Add(type);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                if (type.Value.ToString().Length > 0)
                {
                    Type = type.Value.ToString();
                }
                else
                {
                    Type = "ERROR!!!";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(ex.Message);
                MessageBox.Show("Error Connecting to Database");
            }
            finally
            {
                try
                {
                    connection.Close();
                }
                catch (Exception ex)
                {

                }
            }

            return Type;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            readFile();
            if (Program.AccessControl == false)
            {
                if (textBoxUserName.Text.Trim().Length > 0)
                {
                    if (textBoxPassword.Text.Trim().Length > 0)
                    {
                        Type = authenticateUser(Program.ConnectionString, textBoxUserName.Text.Trim(), textBoxPassword.Text.Trim());
                        Program.UserName = textBoxUserName.Text.Trim();
                        switch (Type.ToLower())
                        {
                            case "user": USER objU = new USER();
                                Program.AccessControl = true;
                                clean();
                                objU.Show();
                                break;
                            case "admin": ADMIN objA = new ADMIN();
                                Program.AccessControl = true;
                                clean();
                                objA.Show();
                                break;
                            case "super": //SUPER objS = new SUPER();
                                Program.AccessControl = true;
                                clean();
                                //objS.Show();
                                break;
                            default: toolStripStatusLabelStatus.Text = "INVALID USER NAME OR PASSWORD";
                                clean();
                                break;
                        }
                    }
                    else
                    {
                        toolStripStatusLabelStatus.Text = "NO PASSWORD FOUND";
                        textBoxUserName.Clear();
                    }
                }
                else
                {
                    toolStripStatusLabelStatus.Text = "NO USER NAME FOUND";
                    textBoxPassword.Clear();
                }
            }
            else
            {
                toolStripStatusLabelStatus.Text = "ALREADY LOGGED IN";
                clean();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Configure obj = new Configure();
            obj.Show();
        }

        public string decrypt(string data)
        {
            int length = data.Length;
            int count = 0;
            char[] dataChar = data.ToCharArray();
            string cypher = "";
            foreach (char ch in dataChar)
            {
                int charEquivalent = Convert.ToInt32(ch);
                charEquivalent -= 5;
                cypher += (char)charEquivalent;
                count++;

            }
            return cypher;
        }

        public void readFile()
        {
            string fileName = "data.txt";
            try
            {
                TextReader tr = new StreamReader(fileName);
                ServerIP = tr.ReadLine();
                DatabaseName = tr.ReadLine();
                authentication = tr.ReadLine();
                if (authentication == "False")
                {
                    UserName = tr.ReadLine();
                    Password = decrypt(tr.ReadLine());
                }
                else
                {
                    UserName = "";
                    Password = "";
                }
                tr.Close();
                Program.ConnectionString = generateConnectionString();

            }
            catch (Exception ex)
            {
                Configure obj = new Configure();
                obj.Show();
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
            string fileName = "Log.txt";
            if (File.Exists(fileName) == false)
            {
                File.Create(fileName);
            }
        }
        
        public string generateConnectionString()
        {
            string connectionString="";
            if (authentication == "True")
            {
                connectionString = "Data Source=" + ServerIP + ";Initial Catalog=" + DatabaseName + ";Integrated Security=True;Pooling=False";
                
            }
            else
            {
                connectionString = "Data Source=" + ServerIP + ";Initial Catalog=" + DatabaseName + ";Persist Security Info=True;User ID=" + UserName + "; Password=" + Password;
            }

            return connectionString;
        }
    }
}
