using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        //static string Connection = " Data Source=208.91.198.196;Initial Catalog=ParkingProject;Persist Security Info=True;User ID=ParkingProject; Password=someshpathak ";
       // static string Connection = "Data Source=GHOST-MACHINE;Initial Catalog=KIETSMS;Integrated Security=True;Pooling=False";
        static string Connection = "";
        public static string ConnectionString
        {
            get
            {
                return Connection;

            }
            set
            {
                Connection = value;
            }
        }

        static bool accessControl = false;
        public static bool AccessControl
        {
            get
            {
                return accessControl;

            }
            set
            {
                accessControl = value;
            }
        }

        //For controlling admin actions
        static bool adminAccessControl = false;
        public static bool AdminAccessControl
        {
            get
            {
                return adminAccessControl;

            }
            set
            {
                adminAccessControl = value;
            }
        }

        //For controlling user actions
        static bool userAccessControl = false;
        public static bool UserAccessControl
        {
            get
            {
                return userAccessControl;

            }
            set
            {
                userAccessControl = value;
            }
        }


        static string Number = "";
        public static string RollNumber
        {
            get
            {
                return Number;

            }
            set
            {
                Number = value;
            }
        }
        
        static string User;
        public static string UserName
        {
            get
            {
                return User;

            }
            set
            {
                User = value;
            }
        }


        static bool Valid;
        public static bool Execute
        {
            get
            {
                return Valid;

            }
            set
            {
                Valid = value;
            }
        }


    }
}
