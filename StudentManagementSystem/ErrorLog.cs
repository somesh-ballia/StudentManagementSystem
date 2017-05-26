using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    class ErrorLog
    {
        public static void Write(string error)
        {
            string fileName = "Log.txt";           
            try
            {
                string time = System.DateTime.Now.ToString();
                string data = "\n" + time + "\t : " + error;
                //File.Open(fileName, FileMode.Append, FileAccess.ReadWrite);
                File.AppendAllText(fileName, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
