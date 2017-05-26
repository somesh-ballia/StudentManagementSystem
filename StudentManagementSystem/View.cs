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
    public partial class View : Form
    {
        public View()
        {
            InitializeComponent();
            flagStatusRollBack[0] = flagStatusRollBack[1] = flagStatusRollBack[2] = flagStatusRollBack[3] = false;
        }

        SqlConnection connection;
        SqlDataAdapter DA;
        DataSet DS = new DataSet();
        SqlCommand command;
        SqlTransaction transaction;
        FileStream Fs;

        // For flag
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

        //For Correspondance Address Backup
        String BCAddress;
        String BCStreet1;
        String BCStreet2;
        String BCCity;
        int BCPin;
        String BCState;
        String BCPhone;
        String BCFather;
        String BCMother;

        //For Permanent Address Backup
        String BPAddress;
        String BPStreet1;
        String BPStreet2;
        String BPCity;
        int BPPin;
        String BPState;
        String BPPhone;
        String BPFather;
        String BPMother;

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

        // For semester marks
        float Sem1Marks;
        float Sem1Out;
        int Sem1Status;
        int Sem1Back;
        int Sem1BackMax;
        float Sem2Marks;
        float Sem2Out;
        int Sem2Back;
        int Sem2BackMax;
        float Sem3Marks;
        float Sem3Out;
        int Sem3Status;
        int Sem3Back;
        int Sem3BackMax;
        float Sem4Marks;
        float Sem4Out;
        int Sem4Back;
        int Sem4BackMax;
        float Sem5Marks;
        float Sem5Out;
        int Sem5Status;
        int Sem5Back;
        int Sem5BackMax;
        float Sem6Marks;
        float Sem6Out;
        int Sem6Back;
        int Sem6BackMax;
        float Sem7Marks;
        float Sem7Out;
        int Sem7Status;
        int Sem7Back;
        int Sem7BackMax;
        float Sem8Marks;
        float Sem8Out;
        int Sem8Back;
        int Sem8BackMax;
        int TotalBack;
        int CurrentBack;
        int TotalYearBack;
        int s1backup;
        int s1backup1;
        int s2backup;
        int s2backup1;
        int s3backup;
        int s3backup1;
        int s4backup;
        int s4backup1;
        int s5backup;
        int s5backup1;
        int s6backup;
        int s6backup1;
        int s7backup;
        int s7backup1;
        int s8backup;
        int s8backup1;
        float aggregate;

        // For Backlog_Subject
        string Sem1Subject;
        string Sem2Subject;
        string Sem3Subject;
        string Sem4Subject;
        string Sem5Subject;
        string Sem6Subject;
        string Sem7Subject;
        string Sem8Subject;

        //For exam roll number
        string ExamRollNuber;

        //Flag
        bool[] flagStatusRollBack = new bool[4];

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
                    Flag = true;
                    ErrorLog.Write(ex.Message);
                    MessageBox.Show("Wrong Data : " + data);
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
                    Flag = true;
                    ErrorLog.Write(ex.Message);
                    MessageBox.Show("Wrong Data : " + data);
                }

                float returnData = (float)number;
                return returnData;
            }
            else
            {
                return 0;
            }
        }
        public void exchange()
        {
            s1backup = Sem1Back;
            s1backup1 = Sem1BackMax;
            s2backup = Sem2Back;
            s2backup1 = Sem2BackMax;
            s3backup = Sem3Back;
            s3backup1 = Sem3BackMax;
            s4backup = Sem4Back;
            s4backup1 = Sem4BackMax;
            s5backup = Sem5Back;
            s5backup1 = Sem5BackMax;
            s6backup = Sem6Back;
            s6backup1 = Sem6BackMax;
            s7backup = Sem7Back;
            s7backup1 = Sem7BackMax;
            s8backup = Sem8Back;
            s8backup1 = Sem8BackMax;
        }

        public void exchangeBack()
        {
            Sem1Back = s1backup;
            Sem1BackMax = s1backup1;
            Sem2Back = s2backup;
            Sem2BackMax = s2backup1;
            Sem3Back = s3backup;
            Sem3BackMax = s3backup1;
            Sem4Back = s4backup;
            Sem4BackMax = s4backup1;
            Sem5Back = s5backup;
            Sem5BackMax = s5backup1;
            Sem6Back = s6backup;
            Sem6BackMax = s6backup1;
            Sem7Back = s7backup;
            Sem7BackMax = s7backup1;
            Sem8Back = s8backup;
            Sem8BackMax = s8backup1;
        }

        public void fill()
        {
            try
            {
                // For Basic
                textBoxRollNo.Text = RollNo;
                comboBoxCourse.Text = Course;
                comboBoxBranch.Text = Branch;
                textBoxGeneralRank.Text = UPSEERank.ToString();
                textBoxCategoryRank.Text = CategoryRank.ToString();
                textBoxPCM.Text = PCM.ToString();
                textBoxDiploma.Text = Diploma.ToString();
                textBoxGraduation.Text = Graduation.ToString();
                comboBoxSemester.Text = Semester;
                comboBoxBatch.Text = Batch.ToString();
                comboBoxStatus.Text = Status.ToString();
                if (Hostel == "YES")
                    radioButtonYes.Checked = true;
                else
                    radioButtonNo.Checked = true;

                //For general
                textBoxStudentName.Text = Name;
                dateTimePickerDOB.Text = DOB;
                comboBoxGender.Text = Gender;
                textBoxReligion.Text = Religion;
                comboBoxCategory.Text = Category;
                textBoxSubCategory.Text = SubCategory;
                textBoxFather.Text = FatherName;
                textBoxMother.Text = MotherName;
                comboBoxOccupation.Text = Occupation;
                textBoxOccupationOther.Text = OccupationOther;
                textBoxOccupationDetails.Text = OccupationDetails;
                comboBoxProfessional.Text = Professional;
                textBoxProfessionOther.Text = ProfessionalOther;
                textBoxIncome.Text = AnualIncome.ToString();

                //For Correspondance Address
                richTextBoxCAddress.Text = CAddress;
                textBoxCStreet1.Text = CStreet1;
                textBoxCStreet2.Text = CStreet2;
                textBoxCCity.Text = CCity;
                textBoxCPin.Text = CPin.ToString();
                textBoxCState.Text = CState;
                textBoxCPhone.Text = CPhone;
                textBoxCFather.Text = CFather;
                textBoxCMother.Text = CMother;

                //For Permanent Address Backup
                richTextBoxBPAddress.Text = BPAddress;
                textBoxBPStreet1.Text = BPStreet1;
                textBoxBPStreet2.Text = BPStreet2;
                textBoxBPCity.Text = BPCity;
                textBoxBPPin.Text = BPPin.ToString();
                textBoxBPState.Text = BPState;
                textBoxBPPhone.Text = BPPhone;
                textBoxBPFather.Text = BPFather;
                textBoxBPMother.Text = BPMother;

                //For Correspondance Address Backup
                richTextBoxBCAddress.Text = BCAddress;
                textBoxBCStreet1.Text = BCStreet1;
                textBoxBCStreet2.Text = BCStreet2;
                textBoxBCCity.Text = BCCity;
                textBoxBCPin.Text = BCPin.ToString();
                textBoxBCState.Text = BCState;
                textBoxBCPhone.Text = BCPhone;
                textBoxBCFather.Text = BCFather;
                textBoxBCMother.Text = BCMother;

                //For Permanent Address
                richTextBoxPaddress.Text = PAddress;
                textBoxPStreet1.Text = PStreet1;
                textBoxPStreet2.Text = PStreet2;
                textBoxPCity.Text = PCity;
                textBoxPPin.Text = PPin.ToString();
                textBoxPState.Text = PState;
                textBoxPPhone.Text = PPhone;
                textBoxPFather.Text = PFather;
                textBoxPMother.Text = PMother;

                // For accademic Perforamce
                textBoxHSchool.Text = HSchool;
                textBoxHBoard.Text = HBoard;
                textBoxHYear.Text = HYear.ToString();
                textBoxHMarks.Text = HPercent.ToString();
                textBoxISchool.Text = ISchool;
                textBoxIBoard.Text = IBoard;
                textBoxIYear.Text = IYear.ToString();
                textBoxIMarks.Text = IPercent.ToString();
                textBoxGSchool.Text = GSchool;
                textBoxGBoard.Text = GBoard;
                textBoxGYear.Text = GYear.ToString();
                textBoxGMarks.Text = GPercent.ToString();
                textBoxDSchool.Text = DSchool;
                textBoxDBoard.Text = DBoard;
                textBoxDYear.Text = DYear.ToString();
                textBoxDMarks.Text = DPercent.ToString();

                // FOR MARKS
                textBoxEnglishMarks.Text = EnglishMarks.ToString();
                textBoxEnglishOut.Text = EnglishMax.ToString();
                textBoxPhysicsMarks.Text = PhysicsMarks.ToString();
                textBoxPhysicsOut.Text = PhysicsMax.ToString();
                textBoxChemMarks.Text = ChemistryMarks.ToString();
                textBoxChemOut.Text = ChemistryMax.ToString();
                textBoxMathMarks.Text = MathMarks.ToString();
                textBoxMathOut.Text = MathMax.ToString();
                textBoxBioMarks.Text = BiologyMarks.ToString();
                textBoxBioOut.Text = BiologyMax.ToString();
                textBoxPCMMarks.Text = PCMMarks.ToString();
                textBoxPCMOut.Text = PCMMax.ToString();
                textBoxPCBMarks.Text = PCBMarks.ToString();
                textBoxPCBOut.Text = PCBMax.ToString();
                textBoxAggMarks.Text = TotalMarks.ToString();
                textBoxAggOut.Text = TotalMax.ToString();
                textBoxPCMpercent.Text = PCMPercent.ToString();
                textBoxPCBPercent.Text = PCBPercent.ToString();
                textBoxAggPercernt.Text = AggregatePercent.ToString();

                // For Draft
                textBoxDDAmount.Text = DDAmmount.ToString();
                textBoxDDno.Text = DDNO;
                dateTimePickerDD.Text = DDDate;
                textBoxDDIssued.Text = Bank;

                // For Recipt
                textBoxRecieptNo.Text = Recipt;
                textBoxReceiptAmount.Text = ReciptAmmount.ToString();
                dateTimePickerReceiptDate.Text = ReciptDate;

                // for Semester_Marks
                textBoxS1Marks.Text = Sem1Marks.ToString();
                textBoxS1Out.Text = Sem1Out.ToString();
                comboBoxS1Back.Text = Sem1Back.ToString();
                if (Sem1Status <= 1)
                    textBoxS1Status.Text = "REG";
                else
                    textBoxS1Status.Text = "YB";

                textBoxS2Marks.Text = Sem2Marks.ToString();
                textBoxS2Out.Text = Sem2Out.ToString();
                comboBoxS2Back.Text = Sem2Back.ToString();

                textBoxS3Marks.Text = Sem3Marks.ToString();
                textBoxS3Out.Text = Sem3Out.ToString();
                comboBoxS3Back.Text = Sem3Back.ToString();
                if (Sem3Status <= 1)
                    textBoxS3Status.Text = "REG";
                else
                    textBoxS3Status.Text = "YB";

                textBoxS4Marks.Text = Sem4Marks.ToString();
                textBoxS4Out.Text = Sem4Out.ToString();
                comboBoxS4Back.Text = Sem4Back.ToString();

                textBoxS5Marks.Text = Sem5Marks.ToString();
                textBoxS5Out.Text = Sem5Out.ToString();
                comboBoxS5Back.Text = Sem5Back.ToString();
                if (Sem5Status <= 1)
                    textBoxS5Status.Text = "REG";
                else
                    textBoxS5Status.Text = "YB";

                textBoxS6Marks.Text = Sem6Marks.ToString();
                textBoxS6Out.Text = Sem6Out.ToString();
                comboBoxS6Back.Text = Sem6Back.ToString();

                textBoxS7Marks.Text = Sem7Marks.ToString();
                textBoxS7Out.Text = Sem7Out.ToString();
                comboBoxS7Back.Text = Sem7Back.ToString();
                if (Sem7Status <= 1)
                    textBoxS7Status.Text = "REG";
                else
                    textBoxS7Status.Text = "YB";

                textBoxS8Marks.Text = Sem8Marks.ToString();
                textBoxS8Out.Text = Sem8Out.ToString();
                comboBoxS8Back.Text = Sem8Back.ToString();

                //For exam roll number
                textBoxExamRollNumber.Text = ExamRollNuber;

                //For Total and Current Backs
                textBoxTotalBacklogs.Text = TotalBack.ToString();
                textBoxCurrentBacklogs.Text = CurrentBack.ToString();
                textBoxYearBacks.Text = TotalYearBack.ToString();

                //For Aggregate
                textBoxAggregate.Text = aggregate.ToString();
                textBoxTotalBacklogs.Text = TotalBack.ToString();
                textBoxYearBacks.Text = TotalYearBack.ToString();
                textBoxCurrentBacklogs.Text = CurrentBack.ToString();

                // For backlog Subjext
                textBoxS1Subject.Text = Sem1Subject;
                textBoxS2Subject.Text = Sem2Subject;
                textBoxS3Subject.Text = Sem3Subject;
                textBoxS4Subject.Text = Sem4Subject;
                textBoxS5Subject.Text = Sem5Subject;
                textBoxS6Subject.Text = Sem6Subject;
                textBoxS7Subject.Text = Sem7Subject;
                textBoxS8Subject.Text = Sem8Subject;
            }
            catch (Exception ex)
            {
                ErrorLog.Write(ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        public void fetch()
        {
            RollNo = Program.RollNumber;
            connection = new SqlConnection(Program.ConnectionString);
            DS.Clear();
            try
            {
                string query = "select Basic.RollNo,Course,Branch,UPSEERank,CategoryRank,PCM,Diploma,Graduation,Semester,Batch,Status,Hostel,";
                query += "Name,Dob,Gender,Religion,General.Category,General.Category_Sub,General.Father,General.Mother,Occupation,Occupation_Other,Occupation_Details,Professional,Professional_Other,AnnualIncome,";
                query += "Correspondence_Address,Correspondence_Street1,Correspondence_Street2,Correspondence_City,Correspondence_Pin,Correspondence_State,Correspondence_Phone,Correspondence_FatherMobile,Correspondence_MotherMobile,";
                query += "Permanent_Address,Permanent_Street1,Permanent_Street2,Permanent_City,Permanent_Pin,Permanent_State,Permanent_Phone,Permanent_FatherMobile,Permanent_MotherMobile,";
                query += "Correspondence_Backup_Address,Correspondence_Backup_Street1,Correspondence_Backup_Street2,Correspondence_Backup_City,Correspondence_Backup_Pin,Correspondence_Backup_State,Correspondence_Backup_Phone,Correspondence_Backup_FatherMobile,Correspondence_Backup_MotherMobile,";
                query += "Permanent_Backup_Address,Permanent_Backup_Street1,Permanent_Backup_Street2,Permanent_Backup_City,Permanent_Backup_Pin,Permanent_Backup_State,Permanent_Backup_Phone,Permanent_Backup_FatherMobile,Permanent_Backup_MotherMobile,";
                query += "HS_School,HS_Board,HS_Year,HS_Percent,I_School,I_Board,I_Year,I_Percent,Graduation_School,Graduation_Board,Graduation_Year,Graduation_Percent,Diploma_School,Diploma_Board,Diploma_Year,Diploma_Percent,";
                query += "EnglishMarks,EnglishMax,PhysicsMarks,PhysicsMax,ChemistryMarks,ChemistryMax,MathMarks,MathMax,BioMarks,BioMax,PCMMarks,PCMMax,PCBMarks,PCBMax,TotalMarks,TotalMax,PCMPercent,PCBPercent,AggregatePercent,";
                query += "Draft_Ammount,DDNO,Draft_Date,Bank,Recipt,Recipt_Date,Recipt_Ammount";
                query += " from ";
                query += "Basic,General,Correspondence_Address,Permanent_Address,Correspondence_Address_Backup,Permanent_Address_Backup,Academic_Performance,Marks, Draft, Fee ";
                query += " where ";
                query += "Basic.RollNo = General.RollNo and Basic.RollNo = Correspondence_Address.RollNo and Basic.RollNo = Correspondence_Address_Backup.RollNo and Basic.RollNo = Permanent_Address.RollNo ";
                query += "and Basic.RollNo = Permanent_Address_Backup.RollNo and Basic.RollNo = Academic_Performance.RollNo and Basic.RollNo = Marks.RollNo and Basic.RollNo = Draft.RollNo and Basic.RollNo = Fee.RollNo and";
                query += " General.RollNO = " + RollNo;
                DA = new SqlDataAdapter(query, connection);
                DA.Fill(DS, "Full");

                if (DS.Tables["Full"].Rows.Count > 0)
                {
                    // For Basic
                    Course = DS.Tables["Full"].Rows[0][1].ToString();
                    Branch = DS.Tables["Full"].Rows[0][2].ToString();
                    UPSEERank = toInt(DS.Tables["Full"].Rows[0][3].ToString());
                    CategoryRank = toInt(DS.Tables["Full"].Rows[0][4].ToString());
                    PCM = toFloat(DS.Tables["Full"].Rows[0][5].ToString());
                    Diploma = toFloat(DS.Tables["Full"].Rows[0][6].ToString());
                    Graduation = toFloat(DS.Tables["Full"].Rows[0][7].ToString());
                    Semester = DS.Tables["Full"].Rows[0][8].ToString();
                    Batch = toInt(DS.Tables["Full"].Rows[0][9].ToString());
                    Status = DS.Tables["Full"].Rows[0][10].ToString();
                    Hostel = DS.Tables["Full"].Rows[0][11].ToString();

                    // For General
                    Name = DS.Tables["Full"].Rows[0][12].ToString();
                    DOB = DS.Tables["Full"].Rows[0][13].ToString();
                    Gender = DS.Tables["Full"].Rows[0][14].ToString();
                    Religion = DS.Tables["Full"].Rows[0][15].ToString();
                    Category = DS.Tables["Full"].Rows[0][16].ToString();
                    SubCategory = DS.Tables["Full"].Rows[0][17].ToString();
                    FatherName = DS.Tables["Full"].Rows[0][18].ToString();
                    MotherName = DS.Tables["Full"].Rows[0][19].ToString();
                    Occupation = DS.Tables["Full"].Rows[0][20].ToString();
                    OccupationOther = DS.Tables["Full"].Rows[0][21].ToString();
                    OccupationDetails = DS.Tables["Full"].Rows[0][22].ToString();
                    Professional = DS.Tables["Full"].Rows[0][23].ToString();
                    ProfessionalOther = DS.Tables["Full"].Rows[0][24].ToString();
                    AnualIncome = toFloat(DS.Tables["Full"].Rows[0][25].ToString());

                    //For Correspondence_Address
                    CAddress = DS.Tables["Full"].Rows[0][26].ToString();
                    CStreet1 = DS.Tables["Full"].Rows[0][27].ToString();
                    CStreet2 = DS.Tables["Full"].Rows[0][28].ToString();
                    CCity = DS.Tables["Full"].Rows[0][29].ToString();
                    CPin = toInt(DS.Tables["Full"].Rows[0][30].ToString());
                    CState = DS.Tables["Full"].Rows[0][31].ToString();
                    CPhone = DS.Tables["Full"].Rows[0][32].ToString();
                    CFather = DS.Tables["Full"].Rows[0][33].ToString();
                    CMother = DS.Tables["Full"].Rows[0][34].ToString();

                    // For Permananent_Address
                    PAddress = DS.Tables["Full"].Rows[0][35].ToString();
                    PStreet1 = DS.Tables["Full"].Rows[0][36].ToString();
                    PStreet2 = DS.Tables["Full"].Rows[0][37].ToString();
                    PCity = DS.Tables["Full"].Rows[0][38].ToString();
                    PPin = toInt(DS.Tables["Full"].Rows[0][39].ToString());
                    PState = DS.Tables["Full"].Rows[0][40].ToString();
                    PPhone = DS.Tables["Full"].Rows[0][41].ToString();
                    PFather = DS.Tables["Full"].Rows[0][42].ToString();
                    PMother = DS.Tables["Full"].Rows[0][43].ToString();

                    // for Correspondence_Address_Backup
                    BCAddress = DS.Tables["Full"].Rows[0][44].ToString();
                    BCStreet1 = DS.Tables["Full"].Rows[0][45].ToString();
                    BCStreet2 = DS.Tables["Full"].Rows[0][46].ToString();
                    BCCity = DS.Tables["Full"].Rows[0][47].ToString();
                    BCPin = toInt(DS.Tables["Full"].Rows[0][48].ToString());
                    BCState = DS.Tables["Full"].Rows[0][49].ToString();
                    BCPhone = DS.Tables["Full"].Rows[0][50].ToString();
                    BCFather = DS.Tables["Full"].Rows[0][51].ToString();
                    BCMother = DS.Tables["Full"].Rows[0][52].ToString();

                    // For permanent_address_Backup
                    BPAddress = DS.Tables["Full"].Rows[0][53].ToString();
                    BPStreet1 = DS.Tables["Full"].Rows[0][54].ToString();
                    BPStreet2 = DS.Tables["Full"].Rows[0][55].ToString();
                    BPCity = DS.Tables["Full"].Rows[0][56].ToString();
                    BPPin = toInt(DS.Tables["Full"].Rows[0][57].ToString());
                    BPState = DS.Tables["Full"].Rows[0][58].ToString();
                    BPPhone = DS.Tables["Full"].Rows[0][59].ToString();
                    BPFather = DS.Tables["Full"].Rows[0][60].ToString();
                    BPMother = DS.Tables["Full"].Rows[0][61].ToString();

                    // FOr Academic_Performance
                    HSchool = DS.Tables["Full"].Rows[0][62].ToString();
                    HBoard = DS.Tables["Full"].Rows[0][63].ToString();
                    HYear = toInt(DS.Tables["Full"].Rows[0][64].ToString());
                    HPercent = toFloat(DS.Tables["Full"].Rows[0][65].ToString());
                    ISchool = DS.Tables["Full"].Rows[0][66].ToString();
                    IBoard = DS.Tables["Full"].Rows[0][67].ToString();
                    IYear = toInt(DS.Tables["Full"].Rows[0][68].ToString());
                    IPercent = toFloat(DS.Tables["Full"].Rows[0][69].ToString());
                    GSchool = DS.Tables["Full"].Rows[0][70].ToString();
                    GBoard = DS.Tables["Full"].Rows[0][71].ToString();
                    GYear = toInt(DS.Tables["Full"].Rows[0][72].ToString());
                    GPercent = toFloat(DS.Tables["Full"].Rows[0][73].ToString());
                    DSchool = DS.Tables["Full"].Rows[0][74].ToString();
                    DBoard = DS.Tables["Full"].Rows[0][75].ToString();
                    DYear = toInt(DS.Tables["Full"].Rows[0][76].ToString());
                    DPercent = toFloat(DS.Tables["Full"].Rows[0][77].ToString());

                    // For marks
                    EnglishMarks = toFloat(DS.Tables["Full"].Rows[0][78].ToString());
                    EnglishMax = toFloat(DS.Tables["Full"].Rows[0][79].ToString());
                    PhysicsMarks = toFloat(DS.Tables["Full"].Rows[0][80].ToString());
                    PhysicsMax = toFloat(DS.Tables["Full"].Rows[0][81].ToString());
                    ChemistryMarks = toFloat(DS.Tables["Full"].Rows[0][82].ToString());
                    ChemistryMax = toFloat(DS.Tables["Full"].Rows[0][83].ToString());
                    MathMarks = toFloat(DS.Tables["Full"].Rows[0][84].ToString());
                    MathMax = toFloat(DS.Tables["Full"].Rows[0][85].ToString());
                    BiologyMarks = toFloat(DS.Tables["Full"].Rows[0][86].ToString());
                    BiologyMax = toFloat(DS.Tables["Full"].Rows[0][87].ToString());
                    PCMMarks = toFloat(DS.Tables["Full"].Rows[0][88].ToString());
                    PCMMax = toFloat(DS.Tables["Full"].Rows[0][89].ToString());
                    PCBMarks = toFloat(DS.Tables["Full"].Rows[0][90].ToString());
                    PCBMax = toFloat(DS.Tables["Full"].Rows[0][91].ToString());
                    TotalMarks = toFloat(DS.Tables["Full"].Rows[0][92].ToString());
                    TotalMax = toFloat(DS.Tables["Full"].Rows[0][93].ToString());
                    PCMPercent = toFloat(DS.Tables["Full"].Rows[0][94].ToString());
                    PCBPercent = toFloat(DS.Tables["Full"].Rows[0][95].ToString());
                    AggregatePercent = toFloat(DS.Tables["Full"].Rows[0][96].ToString());

                    // For draft
                    DDAmmount =toFloat(DS.Tables["Full"].Rows[0][97].ToString());
                    DDNO = DS.Tables["Full"].Rows[0][98].ToString();
                    DDDate = DS.Tables["Full"].Rows[0][99].ToString();
                    Bank = DS.Tables["Full"].Rows[0][100].ToString();

                    // For Recipt
                    Recipt = DS.Tables["Full"].Rows[0][101].ToString();
                    ReciptDate = DS.Tables["Full"].Rows[0][102].ToString();
                    ReciptAmmount = toFloat(DS.Tables["Full"].Rows[0][103].ToString());

                    // For images
                    MemoryStream stream = new MemoryStream();
                    connection.Open();
                    SqlCommand command = new SqlCommand("select student from Photo where RollNo = '" + RollNo + "' ", connection);
                    Student = (byte[])command.ExecuteScalar();
                    stream.Write(Student, 0, Student.Length);
                    connection.Close();
                    Bitmap bmp = new Bitmap(stream);
                    bmp.Save("Student.bmp");
                    pictureBoxStudent.ImageLocation = "Student.bmp";

                    stream = new MemoryStream();
                    connection.Open();
                    command = new SqlCommand("select Father from Photo where RollNo = '" + RollNo + "' ", connection);
                    Father = (byte[])command.ExecuteScalar();
                    stream.Write(Father, 0, Father.Length);
                    connection.Close();
                    bmp = new Bitmap(stream);
                    bmp.Save("Father.bmp");
                    pictureBoxFather.ImageLocation = "Father.bmp";

                    stream = new MemoryStream();
                    connection.Open();
                    command = new SqlCommand("select Mother from Photo where RollNo = '" + RollNo + "' ", connection);
                    Mother = (byte[])command.ExecuteScalar();
                    stream.Write(Mother, 0, Mother.Length);
                    connection.Close();
                    bmp = new Bitmap(stream);
                    bmp.Save("Mother.bmp");
                    pictureBoxMother.ImageLocation = "Mother.bmp";

                    //Data Adapter For Exam RollNo
                    DS.Clear();
                    DA = new SqlDataAdapter("select ExamRollNO from RollNumber where RollNo = '" + RollNo + "'", connection);
                    DA.Fill(DS, "RollNumber");

                    //Get RollNo
                    if (DS.Tables["RollNumber"].Rows.Count > 0)
                        ExamRollNuber = DS.Tables["RollNumber"].Rows[0][0].ToString();

                    //Data Adapter For Marks
                    DS.Clear();
                    DA = new SqlDataAdapter("select * from Semester_Marks where RollNO = '" + RollNo + "'", connection);
                    DA.Fill(DS, "Marks");

                }
                // Get Marks
                if (DS.Tables["Marks"].Rows.Count > 0)
                {
                    Sem1Marks = toFloat(DS.Tables["Marks"].Rows[0][1].ToString());
                    Sem2Marks = toFloat(DS.Tables["Marks"].Rows[0][3].ToString());
                    Sem3Marks = toFloat(DS.Tables["Marks"].Rows[0][5].ToString());
                    Sem4Marks = toFloat(DS.Tables["Marks"].Rows[0][7].ToString());
                    Sem5Marks = toFloat(DS.Tables["Marks"].Rows[0][9].ToString());
                    Sem6Marks = toFloat(DS.Tables["Marks"].Rows[0][11].ToString());
                    Sem7Marks = toFloat(DS.Tables["Marks"].Rows[0][13].ToString());
                    Sem8Marks = toFloat(DS.Tables["Marks"].Rows[0][15].ToString());
                    Sem1Out =   toFloat(DS.Tables["Marks"].Rows[0][2].ToString());
                    Sem2Out =   toFloat(DS.Tables["Marks"].Rows[0][4].ToString());
                    Sem3Out =   toFloat(DS.Tables["Marks"].Rows[0][6].ToString());
                    Sem4Out =   toFloat(DS.Tables["Marks"].Rows[0][8].ToString());
                    Sem5Out =   toFloat(DS.Tables["Marks"].Rows[0][10].ToString());
                    Sem6Out =   toFloat(DS.Tables["Marks"].Rows[0][12].ToString());
                    Sem7Out =   toFloat(DS.Tables["Marks"].Rows[0][14].ToString());
                    Sem8Out =   toFloat(DS.Tables["Marks"].Rows[0][16].ToString());
                }

                //Data Adapter For Backlogs
                DS.Clear();
                DA = new SqlDataAdapter("select * from Backlog_Current where RollNO = '" + RollNo + "'", connection);
                DA.Fill(DS, "Backs");

                // Get Backlogs
                if (DS.Tables["Backs"].Rows.Count > 0)
                {
                    Sem1Back = toInt(DS.Tables["Backs"].Rows[0][1].ToString());
                    Sem2Back = toInt(DS.Tables["Backs"].Rows[0][2].ToString());
                    Sem3Back = toInt(DS.Tables["Backs"].Rows[0][3].ToString());
                    Sem4Back = toInt(DS.Tables["Backs"].Rows[0][4].ToString());
                    Sem5Back = toInt(DS.Tables["Backs"].Rows[0][5].ToString());
                    Sem6Back = toInt(DS.Tables["Backs"].Rows[0][6].ToString());
                    Sem7Back = toInt(DS.Tables["Backs"].Rows[0][7].ToString());
                    Sem8Back = toInt(DS.Tables["Backs"].Rows[0][8].ToString());
                }

                //Data Adapter For total Backlogs values
                DS.Clear();
                DA = new SqlDataAdapter("select * from Backlog_Total where RollNO = '" + RollNo + "'", connection);
                DA.Fill(DS, "Backs");

                // Get Total Backlogs values
                if (DS.Tables["Backs"].Rows.Count > 0)
                {
                    Sem1BackMax = toInt(DS.Tables["Backs"].Rows[0][1].ToString());
                    Sem2BackMax = toInt(DS.Tables["Backs"].Rows[0][2].ToString());
                    Sem3BackMax = toInt(DS.Tables["Backs"].Rows[0][3].ToString());
                    Sem4BackMax = toInt(DS.Tables["Backs"].Rows[0][4].ToString());
                    Sem5BackMax = toInt(DS.Tables["Backs"].Rows[0][5].ToString());
                    Sem6BackMax = toInt(DS.Tables["Backs"].Rows[0][6].ToString());
                    Sem7BackMax = toInt(DS.Tables["Backs"].Rows[0][7].ToString());
                    Sem8BackMax = toInt(DS.Tables["Backs"].Rows[0][8].ToString());
                }

                //Data Adapter For YB Status
                DS.Clear();
                DA = new SqlDataAdapter("select Max(YearCount) from Year_Back where RollNo = '" + RollNo + "' group by AcadYear order by AcadYear asc", connection);
                DA.Fill(DS, "YB");
                
                // Get YB Status
                if (DS.Tables["YB"].Rows.Count > 0)
                {
                    Sem1Status = toInt(DS.Tables["YB"].Rows[0][0].ToString());
                    if(Sem1Status > 0)
                    TotalYearBack = Sem1Status - 1;
                }
                if (DS.Tables["YB"].Rows.Count > 1)
                {
                    Sem3Status = toInt(DS.Tables["YB"].Rows[1][0].ToString());
                    if (Sem3Status > 0)
                    TotalYearBack += (Sem3Status - 1);
                }
                if (DS.Tables["YB"].Rows.Count > 2)
                {
                    Sem5Status = toInt(DS.Tables["YB"].Rows[2][0].ToString());
                    if (Sem5Status > 0)
                    TotalYearBack += (Sem5Status - 1);
                }
                if (DS.Tables["YB"].Rows.Count > 3)
                {
                    Sem7Status = toInt(DS.Tables["YB"].Rows[3][0].ToString());
                    if (Sem7Status > 0)
                    TotalYearBack += (Sem7Status - 1);
                }
                
                // DA For Total Backlogs
                DS.Clear();
                DA = new SqlDataAdapter("select * from Aggregate_Marks where RollNo = '" + RollNo + "'", connection);
                DA.Fill(DS, "AggregatePercent");

                // Get aggregate percent
                if (DS.Tables["AggregatePercent"].Rows.Count > 0)
                    aggregate = float.Parse(DS.Tables["AggregatePercent"].Rows[0][1].ToString());

                // get current backs
                if (DS.Tables["AggregatePercent"].Rows.Count > 0)
                    CurrentBack = toInt(DS.Tables["AggregatePercent"].Rows[0][2].ToString());

                // Get Total Backs
                if (DS.Tables["AggregatePercent"].Rows.Count > 0)
                    TotalBack = toInt(DS.Tables["AggregatePercent"].Rows[0][3].ToString());


                // DA For Backlog  Subjects
                DS.Clear();
                DA = new SqlDataAdapter("select * from Backlog_Subject where RollNo = '" + RollNo + "'", connection);
                DA.Fill(DS, "BacklogSubject");
                if (DS.Tables["BacklogSubject"].Rows.Count > 0)
                {
                    Sem1Subject = DS.Tables["BacklogSubject"].Rows[0][1].ToString();
                    Sem2Subject = DS.Tables["BacklogSubject"].Rows[0][2].ToString();
                    Sem3Subject = DS.Tables["BacklogSubject"].Rows[0][3].ToString();
                    Sem4Subject = DS.Tables["BacklogSubject"].Rows[0][4].ToString();
                    Sem5Subject = DS.Tables["BacklogSubject"].Rows[0][5].ToString();
                    Sem6Subject = DS.Tables["BacklogSubject"].Rows[0][6].ToString();
                    Sem7Subject = DS.Tables["BacklogSubject"].Rows[0][7].ToString();
                    Sem8Subject = DS.Tables["BacklogSubject"].Rows[0][8].ToString();
                }

                // call fill
                fill();

            }
            catch (Exception ex)
            {
                ErrorLog.Write(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public void compareBacklog()
        {
            if (Sem1Back > Sem1BackMax)
                Sem1BackMax = Sem1Back;
            if (Sem2Back > Sem2BackMax)
                Sem2BackMax = Sem2Back;
            if (Sem3Back > Sem3BackMax)
                Sem3BackMax = Sem3Back;
            if (Sem4Back > Sem4BackMax)
                Sem4BackMax = Sem4Back;
            if (Sem5Back > Sem5BackMax)
                Sem5BackMax = Sem5Back;
            if (Sem6Back > Sem6BackMax)
                Sem6BackMax = Sem6Back;
            if (Sem7Back > Sem7BackMax)
                Sem7BackMax = Sem7Back;
            if (Sem8Back > Sem8BackMax)
                Sem8BackMax = Sem8Back;
            TotalBack = 0;
            TotalBack += Sem1BackMax;
            TotalBack += Sem2BackMax;
            TotalBack += Sem3BackMax;
            TotalBack += Sem4BackMax;
            TotalBack += Sem5BackMax;
            TotalBack += Sem6BackMax;
            TotalBack += Sem7BackMax;
            TotalBack += Sem8BackMax;

        }

        private void View_Load(object sender, EventArgs e)
        {
            if (this.MdiParent.Text == "USER")
            {
                buttonDelete.Visible = false;
                buttonFatherPhoto.Visible = false;
                buttonMotherPhoto.Visible = false;
                buttonStudentPhoto.Visible = false;
                buttonUpdate.Visible = false;
                buttonDeleteAcademics.Visible = false;
            }
            else
            {
                buttonDelete.Visible = true;
                buttonFatherPhoto.Visible = true;
                buttonMotherPhoto.Visible = true;
                buttonStudentPhoto.Visible = true;
                buttonUpdate.Visible = true;
                buttonDeleteAcademics.Visible = true;
            }

            fetch();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult Dresult = MessageBox.Show("Do you really want to Delete?", "Delete confirmation", MessageBoxButtons.YesNo);
            if (Dresult.ToString() == "Yes")
            {
                connection = new SqlConnection(Program.ConnectionString);
                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    transaction = connection.BeginTransaction("DeleteData");
                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandText = "delete from Aggregate_Marks where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Year_Back where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Semester_Marks where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Backlog_Current where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Backlog_Total where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from RollNumber where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Fee where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Draft where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Marks where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Academic_Performance where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Permanent_Address where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Correspondence_Address where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Permanent_Address_Backup where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Correspondence_Address_Backup where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from General where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Photo where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Backlog_Subject where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Basic where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Done");
                    this.Close();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        ErrorLog.Write(ex.Message);
                        MessageBox.Show("SOMETHING WENT WRONG");
                    }
                    catch (Exception exe)
                    {
                        ErrorLog.Write(ex.Message);
                    }
                }
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

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DialogResult Dresult = MessageBox.Show("Do you really want to UPDATE?", "Update confirmation", MessageBoxButtons.YesNo);
            if (Dresult.ToString() == "Yes")
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

                    //For Semester_marks
                    Sem1Marks = toFloat(textBoxS1Marks.Text.Trim());
                    Sem1Out = toFloat(textBoxS1Out.Text.Trim());
                    Sem1Back = toInt(comboBoxS1Back.Text);
                    if (comboBoxS1Status.SelectedIndex == 1 || Sem1Status == 0)
                    {
                        flagStatusRollBack[0] = true;
                        Sem1Status = Sem1Status + 1;
                    }

                    Sem2Marks = toFloat(textBoxS2Marks.Text.Trim());
                    Sem2Out = toFloat(textBoxS2Out.Text.Trim());
                    Sem2Back = toInt(comboBoxS2Back.Text);

                    Sem3Marks = toFloat(textBoxS3Marks.Text.Trim());
                    Sem3Out = toFloat(textBoxS3Out.Text.Trim());
                    Sem3Back = toInt(comboBoxS3Back.Text);
                    if (comboBoxS3Status.SelectedIndex == 1 || Sem3Status == 0)
                    {
                        flagStatusRollBack[1] = true;
                        Sem3Status = Sem3Status + 1;
                    }

                    Sem4Marks = toFloat(textBoxS4Marks.Text.Trim());
                    Sem4Out = toFloat(textBoxS4Out.Text.Trim());
                    Sem4Back = toInt(comboBoxS4Back.Text);

                    Sem5Marks = toFloat(textBoxS5Marks.Text.Trim());
                    Sem5Out = toFloat(textBoxS5Out.Text.Trim());
                    Sem5Back = toInt(comboBoxS5Back.Text);
                    if (comboBoxS5Status.SelectedIndex == 1 || Sem5Status == 0)
                    {
                        flagStatusRollBack[2] = true;
                        Sem5Status = Sem5Status + 1;
                    }

                    Sem6Marks = toFloat(textBoxS6Marks.Text.Trim());
                    Sem6Out = toFloat(textBoxS6Out.Text.Trim());
                    Sem6Back = toInt(comboBoxS6Back.Text);

                    Sem7Marks = toFloat(textBoxS7Marks.Text.Trim());
                    Sem7Out = toFloat(textBoxS7Out.Text.Trim());
                    Sem7Back = toInt(comboBoxS7Back.Text);
                    if (comboBoxS7Status.SelectedIndex == 1 || Sem7Status == 0)
                    {
                        flagStatusRollBack[3] = true;
                        Sem7Status = Sem7Status + 1;
                    }

                    Sem8Marks = toFloat(textBoxS8Marks.Text.Trim());
                    Sem8Out = toFloat(textBoxS8Out.Text.Trim());
                    Sem8Back = toInt(comboBoxS8Back.Text);

                    // for exam roll number
                    ExamRollNuber = textBoxExamRollNumber.Text.Trim();

                    // for Backlog Subject
                    Sem1Subject = textBoxS1Subject.Text.Trim();
                    Sem2Subject = textBoxS2Subject.Text.Trim();
                    Sem3Subject = textBoxS3Subject.Text.Trim();
                    Sem4Subject = textBoxS4Subject.Text.Trim();
                    Sem5Subject = textBoxS5Subject.Text.Trim();
                    Sem6Subject = textBoxS6Subject.Text.Trim();
                    Sem7Subject = textBoxS7Subject.Text.Trim();
                    Sem7Subject = textBoxS8Subject.Text.Trim();

                    if (Flag == true)
                    {
                        Flag = false;
                        return;
                    }

                    // For Image

                    try
                    {
                        Fs = new FileStream(pictureBoxStudent.ImageLocation, FileMode.Open, FileAccess.Read);
                        Student = new byte[Fs.Length];
                        Fs.Read(Student, 0, System.Convert.ToInt32(Fs.Length));
                        Fs.Close();

                        Fs = new FileStream(pictureBoxFather.ImageLocation, FileMode.Open, FileAccess.Read);
                        Father = new byte[Fs.Length];
                        Fs.Read(Father, 0, System.Convert.ToInt32(Fs.Length));
                        Fs.Close();

                        Fs = new FileStream(pictureBoxMother.ImageLocation, FileMode.Open, FileAccess.Read);
                        Mother = new byte[Fs.Length];
                        Fs.Read(Mother, 0, System.Convert.ToInt32(Fs.Length));
                        Fs.Close();

                        try
                        {
                            connection = new SqlConnection(Program.ConnectionString);
                            connection.Open();
                            command = connection.CreateCommand();
                            transaction = connection.BeginTransaction("InsertDataDeleteData");
                            command.Connection = connection;
                            command.Transaction = transaction;
                            // For Fee
                            command.CommandText = "delete from Fee where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            // For Draft
                            command.CommandText = "delete from Draft where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            // For Marks
                            command.CommandText = "delete from Marks where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            // For Academic
                            command.CommandText = "delete from Academic_Performance where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            // For Permanent address
                            command.CommandText = "delete from Permanent_Address where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            // For correspondence address
                            command.CommandText = "delete from Correspondence_Address where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            // for general
                            command.CommandText = "delete from General where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            // for photo
                            command.CommandText = "delete from Photo where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            // for rollno
                            command.CommandText = "delete from RollNumber where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            //for marks
                            command.CommandText = "delete from Semester_Marks where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            //for backlogs
                            command.CommandText = "delete from Backlog_Current where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            //for total backlogs
                            command.CommandText = "delete from Backlog_Total where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();
                            //for year back
                            if (comboBoxS1Status.SelectedIndex == -1 || comboBoxS1Status.SelectedIndex == 0)
                            {
                                command.CommandText = "delete from Year_Back where RollNo = '" + RollNo + "' and acadyear = 1 and yearcount = " + Sem1Status;
                                command.ExecuteNonQuery();
                            }
                            if (comboBoxS3Status.SelectedIndex == -1 || comboBoxS3Status.SelectedIndex == 0)
                            {
                                command.CommandText = "delete from Year_Back where RollNo = '" + RollNo + "' and acadyear = 2 and yearcount = " + Sem3Status;
                                command.ExecuteNonQuery();
                            }
                            if (comboBoxS5Status.SelectedIndex == -1 || comboBoxS5Status.SelectedIndex == 0)
                            {
                                command.CommandText = "delete from Year_Back where RollNo = '" + RollNo + "' and acadyear = 3 and yearcount = " + Sem5Status;
                                command.ExecuteNonQuery();
                            }
                            if (comboBoxS7Status.SelectedIndex == -1 || comboBoxS7Status.SelectedIndex == 0)
                            {
                                command.CommandText = "delete from Year_Back where RollNo = '" + RollNo + "' and acadyear = 4 and yearcount = " + Sem7Status;
                                command.ExecuteNonQuery();
                            }

                            // for aggregate
                            command.CommandText = "delete from Backlog_Subject where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();

                            // for backlog subject
                            command.CommandText = "delete from Aggregate_Marks where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();

                            // for basic
                            command.CommandText = "delete from Basic where RollNo = '" + RollNo + "'";
                            command.ExecuteNonQuery();


                            // For Basic
                            command.CommandText = "insert into Basic values('" + RollNo + "','" + Course + "','" + Branch + "'," + UPSEERank + "," + CategoryRank + "," + PCM + "," + Diploma + "," + Graduation + ",'" + Semester + "'," + Batch + ",'" + Status + "','" + Hostel + "')";
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

                            //For RollNo
                            command.CommandText = "insert into RollNumber values('" + RollNo + "','" + ExamRollNuber + "')";
                            command.ExecuteNonQuery();

                            //for semester marks
                            command.CommandText = "insert into Semester_Marks values('" + RollNo + "'," + Sem1Marks + "," + Sem1Out + "," + Sem2Marks + "," + Sem2Out + "," + Sem3Marks + "," + Sem3Out + "," + Sem4Marks + "," + Sem4Out + "," + Sem5Marks + "," + Sem5Out + "," + Sem6Marks + "," + Sem6Out + "," + Sem7Marks + "," + Sem7Out + "," + Sem8Marks + "," + Sem8Out + ")";
                            command.ExecuteNonQuery();

                            //conditions for updating backlogs in yearback
                            exchange();
                            if (comboBoxS1Status.SelectedIndex == 1)
                                Sem1Back = Sem1BackMax = Sem2Back = Sem2BackMax = 0;
                            if (comboBoxS3Status.SelectedIndex == 1)
                                Sem3Back = Sem3BackMax = Sem4Back = Sem4BackMax = 0;
                            if (comboBoxS5Status.SelectedIndex == 1)
                                Sem5Back = Sem5BackMax = Sem6Back = Sem6BackMax = 0;
                            if (comboBoxS7Status.SelectedIndex == 1)
                                Sem7Back = Sem7BackMax = Sem8Back = Sem8BackMax = 0;

                            //for backlogs
                            command.CommandText = "insert into Backlog_Current values('" + RollNo + "'," + Sem1Back + "," + Sem2Back + "," + Sem3Back + "," + Sem4Back + "," + Sem5Back + "," + Sem6Back + "," + Sem7Back + "," + Sem8Back + ")";
                            command.ExecuteNonQuery();

                            // for total backlogs
                            compareBacklog();
                            command.CommandText = "insert into Backlog_Total values('" + RollNo + "'," + Sem1BackMax + "," + Sem2BackMax + "," + Sem3BackMax + "," + Sem4BackMax + "," + Sem5BackMax + "," + Sem6BackMax + "," + Sem7BackMax + "," + Sem8BackMax + ")";
                            command.ExecuteNonQuery();

                            // for year back
                            command.CommandText = "insert into Year_Back values('" + RollNo + "'," + 1 + "," + Sem1Status + "," + Sem1Marks + "," + Sem1Out + "," + Sem2Marks + "," + Sem2Out + ")";
                            command.ExecuteNonQuery();
                            command.CommandText = "insert into Year_Back values('" + RollNo + "'," + 2 + "," + Sem3Status + "," + Sem3Marks + "," + Sem3Out + "," + Sem4Marks + "," + Sem4Out + ")";
                            command.ExecuteNonQuery();
                            command.CommandText = "insert into Year_Back values('" + RollNo + "'," + 3 + "," + Sem5Status + "," + Sem5Marks + "," + Sem5Out + "," + Sem6Marks + "," + Sem6Out + ")";
                            command.ExecuteNonQuery();
                            command.CommandText = "insert into Year_Back values('" + RollNo + "'," + 4 + "," + Sem7Status + "," + Sem7Marks + "," + Sem7Out + "," + Sem8Marks + "," + Sem8Out + ")";
                            command.ExecuteNonQuery();

                            //Current Backs
                            CurrentBack = Sem1Back + Sem2Back + Sem3Back + Sem4Back + Sem5Back + Sem6Back + Sem7Back + Sem8Back;

                            //for aggregate
                            aggregateCalc();
                            command.CommandText = "insert into Backlog_Subject values('" + RollNo + "','" + Sem1Subject + "','" + Sem2Subject + "','" + Sem3Subject + "','" + Sem4Subject + "','" + Sem5Subject + "','" + Sem6Subject + "','" + Sem7Subject + "','" + Sem8Subject + "')";
                            command.ExecuteNonQuery();

                            command.CommandText = "insert into Aggregate_Marks values('" + RollNo + "'," + aggregate + "," + CurrentBack + "," + TotalBack + "," + TotalYearBack + ")";
                            command.ExecuteNonQuery();

                            transaction.Commit();
                            MessageBox.Show("Done");
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
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        if (flagStatusRollBack[0])
                            Sem1Status--;
                        if (flagStatusRollBack[1])
                            Sem3Status--;
                        if (flagStatusRollBack[2])
                            Sem5Status--;
                        if (flagStatusRollBack[3])
                            Sem7Status--;
                        exchangeBack();
                        flagStatusRollBack[0] = flagStatusRollBack[1] = flagStatusRollBack[2] = flagStatusRollBack[3] = false;
                        MessageBox.Show(ex.Message);
                        // MessageBox.Show("SOMETHING WENT WRONG");
                    }
                    catch (Exception ex1)
                    {
                        ErrorLog.Write(ex1.Message);
                        MessageBox.Show("SOMETHING WENT WRONG");
                        
                    }
                }
            }
        }

        public void aggregateCalc()
        {
            float y1, y2, y3, y4;
            float ym1, ym2, ym3, ym4;
            y1 = ((float)Sem1Marks + (float)Sem2Marks) * (float)0.25;
            y2 = ((float)Sem3Marks + (float)Sem4Marks) * (float)0.5;
            y3 = ((float)Sem5Marks + (float)Sem6Marks) * (float)0.75;
            y4 = ((float)Sem7Marks + (float)Sem8Marks);
            ym1 = ((float)Sem1Out + (float)Sem2Out) * (float)0.25;
            ym2 = ((float)Sem3Out + (float)Sem4Out) * (float)0.5;
            ym3 = ((float)Sem5Out + (float)Sem6Out) * (float)0.75;
            ym4 = ((float)Sem7Out + (float)Sem8Out);
            if ((ym1 + ym2 + ym3 + ym4) != 0)
            {
                aggregate = ((y1 + y2 + y3 + y4) / (ym1 + ym2 + ym3 + ym4)) * 100;
            }
        }

        private void buttonStudentPhoto_Click(object sender, EventArgs e)
        {
            openFileDialogStudent.ShowDialog();
        }

        private void buttonFatherPhoto_Click(object sender, EventArgs e)
        {
            openFileDialogFather.ShowDialog();
        }

        private void buttonMotherPhoto_Click(object sender, EventArgs e)
        {
            openFileDialogMother.ShowDialog();
        }

        private void openFileDialogStudent_FileOk(object sender, CancelEventArgs e)
        {
            pictureBoxStudent.ImageLocation = openFileDialogStudent.FileName;
        }

        private void openFileDialogFather_FileOk(object sender, CancelEventArgs e)
        {
            pictureBoxFather.ImageLocation = openFileDialogFather.FileName;
        }

        private void openFileDialogMother_FileOk(object sender, CancelEventArgs e)
        {
            pictureBoxMother.ImageLocation = openFileDialogMother.FileName;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonDeleteAcademics_Click(object sender, EventArgs e)
        {
            DialogResult Dresult = MessageBox.Show("Do you really want to Delete?", "Delete confirmation", MessageBoxButtons.YesNo);
            if (Dresult.ToString() == "Yes")
            {
                connection = new SqlConnection(Program.ConnectionString);
                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    transaction = connection.BeginTransaction("DeleteData");
                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandText = "delete from Aggregate_Marks where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Year_Back where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Semester_Marks where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Backlog_Current where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Backlog_Total where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from RollNumber where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();

                    command.CommandText = "delete from Backlog_Subject where RollNo = '" + RollNo + "'";
                    command.ExecuteNonQuery();
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

                    // for backlog subject
                    command.CommandText = "insert into Backlog_Subject values('" + RollNo + "','None','None','None','None','None','None','None','None')";
                    command.ExecuteNonQuery();


                    transaction.Commit();
                    MessageBox.Show("Done");
                    this.Close();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        ErrorLog.Write(ex.Message);
                        MessageBox.Show("SOMETHING WENT WRONG");
                    }
                    catch (Exception exe)
                    {
                        ErrorLog.Write(ex.Message);
                    }
                }
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
            if (comboBoxCourse.SelectedIndex == 2)
            {
                comboBoxBranch.Items.Clear();
                comboBoxBranch.Items.Add("CSE");
                comboBoxBranch.Items.Add("ECE");
                comboBoxBranch.Items.Add("EN");
                comboBoxBranch.Items.Add("ME");
            }
            if (comboBoxCourse.SelectedIndex == 5)
            {
                comboBoxBranch.Items.Clear();
                comboBoxBranch.Items.Add("Pharmecenitics");
                comboBoxBranch.Items.Add("Pharmecology");
            }
            
        }
        
        private void buttonCalculate_Click(object sender, EventArgs e)
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

        private void buttonExport_Click(object sender, EventArgs e)
        {

        }       
    }
}
