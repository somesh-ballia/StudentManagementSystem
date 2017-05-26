using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class ADMIN : Form
    {
        public ADMIN()
        {
            InitializeComponent();
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelStatus.Text = "";
            if (Program.AdminAccessControl == false)
            {
                Program.AdminAccessControl = true;
                AddUser objAdd = new AddUser();
                objAdd.MdiParent = this;
                objAdd.Show();
            }
            else
            {
                toolStripStatusLabelStatus.Text = "WARNING! CLOSE THE CURRENT VIEW FIRST";
            }
           
        }

        private void ADMIN_Load(object sender, EventArgs e)
        {

        }

        private void ADMIN_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.AccessControl = false;
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelStatus.Text = "";
            if (Program.AdminAccessControl == false)
            {
                Program.AdminAccessControl = true;
                DeleteUser objDEL = new DeleteUser();
                objDEL.MdiParent = this;
                objDEL.Show();
            }
            else
            {
                toolStripStatusLabelStatus.Text = "WARNING! CLOSE THE CURRENT VIEW FIRST";
            }
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelStatus.Text = "";
            if (Program.AdminAccessControl == false)
            {
                Program.AdminAccessControl = true;
                ChangePassword objCh = new ChangePassword();
                objCh.MdiParent = this;
                objCh.Show();
            }
            else
            {
                toolStripStatusLabelStatus.Text = "WARNING! CLOSE THE CURRENT VIEW FIRST";
            }
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {   
                AddStudent objAdd = new AddStudent();
                objAdd.MdiParent = this;
                objAdd.Show();
            
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SearchData objSearch = new SearchData();
            objSearch.MdiParent = this;
            objSearch.Show();
        }
    }
}
