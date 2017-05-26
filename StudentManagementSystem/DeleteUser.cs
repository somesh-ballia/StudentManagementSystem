using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentManagementSystem
{
    public partial class DeleteUser : Form
    {
        public DeleteUser()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;      


        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxUserName.Text.Trim().Length > 0)
                {
                    if (textBoxUserName.Text.Trim() != Program.UserName)
                    {
                        connection = new SqlConnection(Program.ConnectionString);
                        SqlParameter user = new SqlParameter("@user", SqlDbType.VarChar, 50);
                        SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar, 50);
                        user.Direction = ParameterDirection.Input;
                        type.Direction = ParameterDirection.Output;
                        user.Value = textBoxUserName.Text.Trim();
                        command = new SqlCommand("select @type = Type from login where UserName=@user", connection);
                        command.Parameters.Add(user);
                        command.Parameters.Add(type);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            if (type.Value.ToString().Length > 0)
                            {
                                try
                                {
                                    command = new SqlCommand("DELETE from login where UserName = '" + textBoxUserName.Text.Trim() + "' ", connection);
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                    connection.Close();
                                    toolStripStatusLabelStatus.Text = "DONE";
                                    textBoxUserName.Clear();

                                }
                                catch (Exception ex)
                                {
                                    toolStripStatusLabelStatus.Text = "SOMETHING WENT WRONG";
                                    textBoxUserName.Clear();
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
                            }
                            else
                            {
                                toolStripStatusLabelStatus.Text = "INVALID USER NAME";
                                textBoxUserName.Clear();
                            }
                        }
                        catch (Exception ex)
                        {
                            toolStripStatusLabelStatus.Text = "ERROR CONNECTING TO DATABASE";
                            textBoxUserName.Clear();
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
                    }
                    else
                    {
                        toolStripStatusLabelStatus.Text = "SELF DESTRUCT DENIED";
                        textBoxUserName.Clear();
                    }

                }
                else
                {
                    toolStripStatusLabelStatus.Text = "NO USER NAME FOUND";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(ex.Message);                
            }


        }

        private void DeleteUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.AdminAccessControl = false;
        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {      }
    }
}
