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
    public partial class USER : Form
    {
        public USER()
        {
            InitializeComponent();
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            if (Program.UserAccessControl == false)
            {
                toolStripStatusLStatus.Text = "";
                Program.UserAccessControl = true;
                ChangeSelfPassword objCh = new ChangeSelfPassword();
                objCh.MdiParent = this;
                objCh.Show();               
            }
            else
            {
                toolStripStatusLStatus.Text = "WARNING! CLOSE THE CURRENT VIEW FIRST";
            }   
        }

        private void USER_Load(object sender, EventArgs e)
        {

        }

        private void USER_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.AccessControl = false;
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
