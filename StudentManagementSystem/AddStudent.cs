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
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;
        SqlTransaction transaction;
        FileStream Fs;


        //For flag
        bool Flag = false;

        // For Basic
        String Course;
        String RollNo;
        int Batch;        
        int UPSEERank;
        String Branch;
        int CategoryRank;
        string Status;
        float PCM;
        String Semester;
        float Graduation;
        String Hostel;
        float Diploma;

        // For Photo
        Byte[] Student;
        Byte[] Father;
        Byte[] Mother;

        // For General
        String Name;
        String DOB;
        String Gender;
        String Religion;
        String Category;
        String SubCategory;
        String FatherName;
        String MotherName;
        String Occupation;
        String OccupationOther;
        String OccupationDetails;
        String Professional;
        String ProfessionalOther;
        float AnualIncome;        

        //For Correspondance Address
        String CAddress;
        String CStreet1;
        String CStreet2;
        String CCity;
        int CPin;
        String CState;
        String CPhone;
        String CFather;
        String CMother;

        //For Permanent Address
        String PAddress;
        String PStreet1;
        String PStreet2;
        String PCity;
        int PPin;
        String PState;
        String PPhone;
        String PFather;
        String PMother;

        // For Accademic Performance
        String HSchool;
        String HBoard;
        int HYear;
        float HPercent;
        String ISchool;
        String IBoard;
        int IYear;
        float IPercent;
        String GSchool;
        String GBoard;
        int GYear;
        float GPercent;
        String DSchool;
        String DBoard;
        int DYear;
        float DPercent;

        // For Marks
        float EnglishMarks;
        float EnglishMax;
        float PhysicsMarks;
        float PhysicsMax;
        float ChemistryMarks;
        float ChemistryMax;
        float MathMarks;
        float MathMax;
        float BiologyMarks;
        float BiologyMax;
        float PCMMarks;
        float PCMMax;
        float PCBMarks;
        float PCBMax;
        float TotalMarks;
        float TotalMax;
        float PCMPercent;
        float PCBPercent;
        float AggregatePercent;
        
        // For Draft
        float DDAmmount;
        string DDNO;
        string DDDate;
        string Bank;

        // For Fee
        string Recipt;
        string ReciptDate;
        float ReciptAmmount;
        
        private void AddStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            try
            {
                radioButtonNo.Checked = true;
                openFileDialogStudent.FileName = "default.bmp";
                openFileDialogFather.FileName = "default.bmp";
                openFileDialogMother.FileName = "default.bmp";
                pictureBoxMother.ImageLocation = openFileDialogMother.FileName;
                pictureBoxStudent.ImageLocation = openFileDialogStudent.FileName;
                pictureBoxFather.ImageLocation = openFileDialogFather.FileName;
            }
            catch (Exception ex)
            {
                ErrorLog.Write(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public void Fetch()
        {
            try
            {
                // For Basic
                RollNo = textBoxRollNo.Text.Trim();
                Course = comboBoxCourse.Text;
                Branch = comboBoxBranch.Text;
                UPSEERank = toInt(textBoxGeneralRank.Text.Trim());
                CategoryRank = toInt(textBoxCategoryRank.Text.Trim());
                PCM = toFloat(textBoxPCM.Text.Trim());
                Diploma = toFloat(textBoxDiploma.Text.Trim());
                Graduation = toFloat(textBoxGraduation.Text.Trim());
                Semester = comboBoxSemester.Text;
                Batch = toInt(comboBoxBatch.Text);
                Status = comboBoxStatus.Text;
                Hostel = HostelStatus();

                //For general
                Name = textBoxStudentName.Text.Trim();
                DOB = dateTimePickerDOB.Text;
                Gender = comboBoxGender.Text;
                Religion = textBoxReligion.Text.Trim();
                Category = comboBoxCategory.Text;
                SubCategory = textBoxSubCategory.Text.Trim();
                FatherName = textBoxFather.Text.Trim();
                MotherName = textBoxMother.Text.Trim();
                Occupation = comboBoxOccupation.Text;
                OccupationOther = textBoxOccupationOther.Text.Trim();
                OccupationDetails = textBoxOccupationDetails.Text.Trim();
                Professional = comboBoxProfessional.Text;
                ProfessionalOther = textBoxProfessionOther.Text.Trim();
                AnualIncome = toFloat(textBoxIncome.Text.Trim());


                //For Correspondance Address
                CAddress = richTextBoxCAddress.Text.Trim();
                CStreet1 = textBoxCStreet1.Text.Trim();
                CStreet2 = textBoxCStreet2.Text.Trim();
                CCity = textBoxCCity.Text.Trim();
                CPin = toInt(textBoxCPin.Text.Trim());
                CState = textBoxCState.Text.Trim();
                CPhone = textBoxCPhone.Text.Trim();
                CFather = textBoxCFather.Text.Trim();
                CMother = textBoxCMother.Text.Trim();

                //For Permanent Address
                PAddress = richTextBoxPaddress.Text.Trim();
                PStreet1 = textBoxPStreet1.Text.Trim();
                PStreet2 = textBoxPStreet2.Text.Trim();
                PCity = textBoxPCity.Text.Trim();
                PPin = toInt(textBoxPPin.Text.Trim());
                PState = textBoxPState.Text.Trim();
                PPhone = textBoxPPhone.Text.Trim();
                PFather = textBoxPFather.Text.Trim();
                PMother = textBoxPMother.Text.Trim();

                // For accademic Perforamce
                HSchool = textBoxHSchool.Text.Trim();
                HBoard = textBoxHBoard.Text.Trim();
                HYear = toInt(textBoxHYear.Text.Trim());
                HPercent = toFloat(textBoxHMarks.Text.Trim());
                ISchool = textBoxISchool.Text.Trim();
                IBoard = textBoxIBoard.Text.Trim();
                IYear = toInt(textBoxIYear.Text.Trim());
                IPercent = toFloat(textBoxIMarks.Text.Trim());
                GSchool = textBoxGSchool.Text.Trim();
                GBoard = textBoxGBoard.Text.Trim();
                GYear = toInt(textBoxGYear.Text.Trim());
                GPercent = toFloat(textBoxGMarks.Text.Trim());
                DSchool = textBoxDSchool.Text.Trim();
                DBoard = textBoxDBoard.Text.Trim();
                DYear = toInt(textBoxDYear.Text.Trim());
                DPercent = toFloat(textBoxDMarks.Text.Trim());

                // For Marks
                EnglishMarks = toFloat(textBoxEnglishMarks.Text.Trim());
                EnglishMax = toFloat(textBoxEnglishOut.Text.Trim());
                PhysicsMarks = toFloat(textBoxPhysicsMarks.Text.Trim());
                PhysicsMax = toFloat(textBoxPhysicsOut.Text.Trim());
                ChemistryMarks = toFloat(textBoxChemMarks.Text.Trim());
                ChemistryMax = toFloat(textBoxChemOut.Text.Trim());
                MathMarks = toFloat(textBoxMathMarks.Text.Trim());
                MathMax = toFloat(textBoxMathOut.Text.Trim());
                BiologyMarks = toFloat(textBoxBioMarks.Text.Trim());
                BiologyMax = toFloat(textBoxBioOut.Text.Trim());
                PCMMarks = toFloat(textBoxPCMMarks.Text.Trim());
                PCMMax = toFloat(textBoxPCMOut.Text.Trim());
                PCBMarks = toFloat(textBoxPCBMarks.Text.Trim());
                PCBMax = toFloat(textBoxPCBOut.Text.Trim());
                TotalMarks = toFloat(textBoxAggMarks.Text.Trim());
                TotalMax = toFloat(textBoxAggOut.Text.Trim());
                PCMPercent = toFloat(textBoxPCMpercent.Text.Trim());
                PCBPercent = toFloat(textBoxPCBPercent.Text.Trim());
                AggregatePercent = toFloat(textBoxAggPercernt.Text.Trim());

                // For Draft
                DDAmmount = toFloat(textBoxDDAmount.Text.Trim());
                DDNO = textBoxDDno.Text.Trim();
                DDDate = dateTimePickerDD.Text;
                Bank = textBoxDDIssued.Text.Trim();

                // For Recipt
                Recipt = textBoxRecieptNo.Text.Trim();
                ReciptAmmount = toFloat(textBoxReceiptAmount.Text.Trim());
                ReciptDate = dateTimePickerReceiptDate.Text;

                // For Image
                try
                {
                    Fs = new FileStream(openFileDialogStudent.FileName, FileMode.Open, FileAccess.Read);
                    Student = new byte[Fs.Length];
                    Fs.Read(Student, 0, System.Convert.ToInt32(Fs.Length));
                    Fs.Close();

                    Fs = new FileStream(openFileDialogFather.FileName, FileMode.Open, FileAccess.Read);
                    Father = new byte[Fs.Length];
                    Fs.Read(Father, 0, System.Convert.ToInt32(Fs.Length));
                    Fs.Close();

                    Fs = new FileStream(openFileDialogMother.FileName, FileMode.Open, FileAccess.Read);
                    Mother = new byte[Fs.Length];
                    Fs.Read(Mother, 0, System.Convert.ToInt32(Fs.Length));
                    Fs.Close();
                }
                catch (Exception ex)
                {
                    ErrorLog.Write(ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(ex.Message);
                MessageBox.Show(ex.Message);
            }

        }
        
        public string HostelStatus()
        {
            if (radioButtonYes.Checked == true)
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }

        public int toInt(string data)
        {
            if (data.Length > 0)
            {
                int number = 0;
                try
                {
                    number = Convert.ToInt32(data);
                }
                catch (Exception ex)
                {
                    ErrorLog.Write(ex.Message);                    
                    MessageBox.Show("Wrong Data : " + data);
                    Flag = true;
                }
                    return number;                
            }
            else
            {
                return 0;
            }
        }

        public float toFloat(string data)
        {
            if (data.Length > 0)
            {
                double number = 0;
                try
                {
                    number = Convert.ToDouble(data);
                }
                catch (Exception ex)
                {
                    ErrorLog.Write(ex.Message);
                    MessageBox.Show("Wrong Data : " + data);
                    Flag = true;
                }
                             
                    float returnData = (float)number;
                    return returnData;
            }
            else
            {
                return 0;
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Fetch();
                if (Flag == true)
                {
                    Flag = false;
                    return;
                }
                if (RollNo.Length > 0)
                {
                    connection = new SqlConnection(Program.ConnectionString);
                    connection.Open();
                    command = connection.CreateCommand();
                    transaction = connection.BeginTransaction("InsertData");
                    command.Connection = connection;
                    command.Transaction = transaction;
                    // For Basic
                    command.CommandText = "insert into Basic values('" + RollNo + "','" + Course + "','" + Branch + "'," + UPSEERank + "," + CategoryRank + "," + PCM + "," + Diploma + "," + Graduation + ",'" + Semester + "'," +Batch+ ",'"+Status+"','" + Hostel + "')";
                    command.ExecuteNonQuery();

                    // For Photo
                    SqlParameter StudentImage = new SqlParameter("@Student", SqlDbType.Image);
                    SqlParameter FatherImage = new SqlParameter("@Father", SqlDbType.Image);
                    SqlParameter MotherImage = new SqlParameter("@Mother", SqlDbType.Image);
                    StudentImage.Direction = ParameterDirection.Input;
                    FatherImage.Direction = ParameterDirection.Input;
                    MotherImage.Direction = ParameterDirection.Input;
                    StudentImage.Value = Student;
                    FatherImage.Value = Father;
                    MotherImage.Value = Mother;
                    command.CommandText = "insert into Photo values('" + RollNo + "',@Student,@Father,@Mother)";
                    command.Parameters.Add(StudentImage);
                    command.Parameters.Add(FatherImage);
                    command.Parameters.Add(MotherImage);
                    command.ExecuteNonQuery();

                    // For General
                    command.CommandText = "insert into general values('" + RollNo + "','" + Name + "','" + DOB + "','" + Gender + "','" + Religion + "','" + Category + "','" + SubCategory + "','" + FatherName + "','" + MotherName + "','" + Occupation + "','" + OccupationOther + "','" + OccupationDetails + "','" + Professional + "','" + ProfessionalOther + "'," + AnualIncome + ")";
                    command.ExecuteNonQuery();

                    // For Correspondence Address
                    command.CommandText = "insert into Correspondence_Address values('" + RollNo + "','" + CAddress + "','" + CStreet1 + "','" + CStreet2 + "','" + CCity + "'," + CPin + ",'" + CState + "','" + CPhone + "','" + CFather + "','" + CMother + "')";
                    command.ExecuteNonQuery();

                    // For Permanent Address
                    command.CommandText = "insert into Permanent_Address values('" + RollNo + "','" + PAddress + "','" + PStreet1 + "','" + PStreet2 + "','" + PCity + "'," + PPin + ",'" + PState + "','" + PPhone + "','" + PFather + "','" + PMother + "')";
                    command.ExecuteNonQuery();

                    // For Correspondence Address Backup
                    command.CommandText = "insert into Correspondence_Address_Backup values('" + RollNo + "','" + CAddress + "','" + CStreet1 + "','" + CStreet2 + "','" + CCity + "'," + CPin + ",'" + CState + "','" + CPhone + "','" + CFather + "','" + CMother + "')";
                    command.ExecuteNonQuery();

                    // For Permanent Address Backup
                    command.CommandText = "insert into Permanent_Address_Backup values('" + RollNo + "','" + PAddress + "','" + PStreet1 + "','" + PStreet2 + "','" + PCity + "'," + PPin + ",'" + PState + "','" + PPhone + "','" + PFather + "','" + PMother + "')";
                    command.ExecuteNonQuery();

                    // For Academic_Performance
                    command.CommandText = "insert into Academic_Performance values('" + RollNo + "','" + HSchool + "','" + HBoard + "'," + HYear + "," + HPercent + ",'" + ISchool + "','" + IBoard + "'," + IYear + "," + IPercent + ",'" + GSchool + "','" + GBoard + "'," + GYear + "," + GPercent + ",'" + DSchool + "','" + DBoard + "'," + DYear + "," + DPercent + ")";
                    command.ExecuteNonQuery();

                    // For Marks
                    command.CommandText = "insert into Marks values('" + RollNo + "'," + EnglishMarks + "," + EnglishMax + "," + PhysicsMarks + "," + PhysicsMax + "," + ChemistryMarks + "," + ChemistryMax + "," + MathMarks + "," + MathMax + "," + BiologyMarks + "," + BiologyMax + "," + PCMMarks + "," + PCMMax + "," + PCBMarks + "," + PCBMax + "," + TotalMarks + "," + TotalMax + "," + PCMPercent + "," + PCBPercent + "," + AggregatePercent + ")";
                    command.ExecuteNonQuery();

                    //For Draft
                    command.CommandText = "insert into Draft values('" + RollNo + "'," + DDAmmount + ",'" + DDNO + "','" + DDDate + "','" + Bank + "')";
                    command.ExecuteNonQuery();

                    //For Fee
                    command.CommandText = "insert into Fee values('" + RollNo + "','" + Recipt + "','" + ReciptDate + "'," + ReciptAmmount + ")";
                    command.ExecuteNonQuery();

                    // filling in default values in other tables 

                    //For RollNumber
                    command.CommandText = "insert into RollNumber values('" + RollNo + "','')";
                    command.ExecuteNonQuery();

                    //For Backlog_Total
                    command.CommandText = "insert into Backlog_Total values('" + RollNo + "',0,0,0,0,0,0,0,0)";
                    command.ExecuteNonQuery();

                    //For Backlog_Current
                    command.CommandText = "insert into Backlog_Current values('" + RollNo + "',0,0,0,0,0,0,0,0)";
                    command.ExecuteNonQuery();

                    //For Semester_Marks
                    command.CommandText = "insert into Semester_Marks values('" + RollNo + "',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)";
                    command.ExecuteNonQuery();

                    //For Year_Back
                    command.CommandText = "insert into Year_Back values('" + RollNo + "',1,0,0,0,0,0)";
                    command.ExecuteNonQuery();

                    //For Aggregate_Marks
                    command.CommandText = "insert into Aggregate_Marks values('" + RollNo + "',0,0,0,0)";
                    command.ExecuteNonQuery();

                    //For Backlog_Subjext
                    command.CommandText = "insert into Backlog_Subject values('" + RollNo + "','None','None','None','None','None','None','None','None')";
                    command.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show("Roll No. NOT FOUND");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    ErrorLog.Write(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex1)
                {
                    ErrorLog.Write(ex.Message);
                }
            }
        }

        private void buttonStudentPhoto_Click(object sender, EventArgs e)
        {
            openFileDialogStudent.ShowDialog();
        }

        private void openFileDialogStudent_FileOk(object sender, CancelEventArgs e)
        {
            pictureBoxStudent.ImageLocation = openFileDialogStudent.FileName;           
        }

        private void buttonFatherPhoto_Click(object sender, EventArgs e)
        {
            openFileDialogFather.ShowDialog();
        }

        private void buttonMotherPhoto_Click(object sender, EventArgs e)
        {
            openFileDialogMother.ShowDialog();
        }

        private void openFileDialogFather_FileOk(object sender, CancelEventArgs e)
        {
            pictureBoxFather.ImageLocation = openFileDialogFather.FileName;
        }

        private void openFileDialogMother_FileOk(object sender, CancelEventArgs e)
        {
            pictureBoxMother.ImageLocation = openFileDialogMother.FileName;
        }

        private void checkBoxSame_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSame.Checked == true)
            {
                richTextBoxPaddress.Text = richTextBoxCAddress.Text;
                textBoxPStreet1.Text = textBoxCStreet1.Text;
                textBoxPStreet2.Text = textBoxCStreet2.Text;
                textBoxPCity.Text = textBoxCCity.Text;
                textBoxPState.Text = textBoxCState.Text;
                textBoxPPin.Text = textBoxCPin.Text;
                textBoxPPhone.Text = textBoxCPhone.Text;
                textBoxPFather.Text = textBoxCFather.Text;
                textBoxPMother.Text = textBoxCMother.Text;
            }
        }

        private void comboBoxOccupation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOccupation.Text == "Others")
            {
                textBoxOccupationOther.ReadOnly = false;
                textBoxOccupationOther.TabStop = true;
            }
            else
            {
                textBoxOccupationOther.ReadOnly = true;
                textBoxOccupationOther.TabStop = false;

            }
        }

        private void comboBoxProfessional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProfessional.Text == "Others")
            {
                textBoxProfessionOther.ReadOnly = false;
                textBoxProfessionOther.TabStop = true;
            }
            else
            {
                textBoxProfessionOther.ReadOnly = true;
                textBoxProfessionOther.TabStop = false;
            }
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCategory.Text == "SC" || comboBoxCategory.Text == "ST" || comboBoxCategory.Text == "OBC")
            {
                textBoxSubCategory.ReadOnly = false;
                textBoxSubCategory.TabStop = true;
            }
            else
            {
                textBoxSubCategory.ReadOnly = true;
                textBoxSubCategory.TabStop = false;
            }
        }

        private void comboBoxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCourse.SelectedIndex == 0)
            {
                comboBoxBranch.Items.Clear();
                comboBoxBranch.Items.Add("CSE");
                comboBoxBranch.Items.Add("ECE");
                comboBoxBranch.Items.Add("EN");
                comboBoxBranch.Items.Add("IT");
                comboBoxBranch.Items.Add("ME");
                comboBoxBranch.Items.Add("CIVIL");
                comboBoxBranch.Items.Add("EI");
            }
            else if (comboBoxCourse.SelectedIndex == 2)
            {
                comboBoxBranch.Items.Clear();
                comboBoxBranch.Items.Add("CSE");
                comboBoxBranch.Items.Add("ECE");
                comboBoxBranch.Items.Add("EN");                
                comboBoxBranch.Items.Add("ME");                
            }
            else if (comboBoxCourse.SelectedIndex == 5)
            {
                comboBoxBranch.Items.Clear();
                comboBoxBranch.Items.Add("Pharmecenitics");
                comboBoxBranch.Items.Add("Pharmecology");
            }
            else
            {
                comboBoxBranch.Items.Clear();
            }
            
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                float Maths = 0;
                float Physics = 0;
                float Chem = 0;
                float Bio = 0;
                float PCM = 0;
                float PCB = 0;

                float MathsO = 0;
                float PhysicsO = 0;
                float ChemO = 0;
                float BioO = 0;
                float PCMO = 0;
                float PCBO = 0;

                float PCMP = 0;
                float PCBP = 0;

                Maths = toFloat(textBoxMathMarks.Text.Trim());
                Physics = toFloat(textBoxPhysicsMarks.Text.Trim());
                Chem = toFloat(textBoxChemMarks.Text.Trim());
                Bio = toFloat(textBoxBioMarks.Text.Trim());
                MathsO = toFloat(textBoxMathOut.Text.Trim());
                PhysicsO = toFloat(textBoxPhysicsOut.Text.Trim());
                ChemO = toFloat(textBoxChemOut.Text.Trim());
                BioO = toFloat(textBoxBioOut.Text.Trim());

                if (Maths > 0)
                {
                    PCM = Maths + Physics + Chem;
                    PCMO = MathsO + PhysicsO + ChemO;
                    PCMP = (PCM / PCMO) * 100;
                    textBoxPCM.Text = PCMP.ToString();
                }

                if (Bio > 0)
                {
                    PCB = Physics + Chem + Bio;
                    PCBO = PhysicsO + ChemO + BioO;
                    PCBP = (PCB / PCBO) * 100;
                    textBoxPCM.Text = PCBP.ToString();
                }

                if (Maths > 0 && Bio > 0)
                {

                    DialogResult drResult = MessageBox.Show("Press Yes for PCM and Press No for PCB", "Select", MessageBoxButtons.YesNo);
                    if (drResult.ToString() == "Yes")
                    {
                        textBoxPCM.Text = PCMP.ToString();
                    }
                    else
                    {
                        textBoxPCM.Text = PCBP.ToString();
                    }

                }

                textBoxPCMMarks.Text = PCM.ToString();
                textBoxPCMOut.Text = PCMO.ToString();
                textBoxPCMpercent.Text = PCMP.ToString();

                textBoxPCBMarks.Text = PCB.ToString();
                textBoxPCBOut.Text = PCBO.ToString();
                textBoxPCBPercent.Text = PCBP.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog.Write(ex.Message);
                MessageBox.Show(ex.Message);
            }

        }
    }
}
