using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace StudentManagementSystem
{
    public partial class SearchData : Form
    {
        public SearchData()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlDataAdapter DA;        
        DataSet DS = new DataSet();
        
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(Program.ConnectionString);
                string query = "select * from Basic,General,RollNumber,Aggregate_Marks where basic.rollno = general.rollno and basic.rollno = rollnumber.rollno and aggregate_marks.rollno = basic.rollno";
                string query2 = query;
                if (textBoxRollNo.Text.Trim().Length > 0 && textBoxExamRollNumber.Text.Trim().Length > 0)
                {
                    query = query + " and basic.rollno = '" + textBoxRollNo.Text.Trim() + "' and examrollno = '" + textBoxExamRollNumber.Text.Trim() + "'";
                }
                else if (textBoxRollNo.Text.Trim().Length > 0)
                {
                    query = query + " and basic.rollno = '" + textBoxRollNo.Text.Trim() + "'";
                }
                else if (textBoxExamRollNumber.Text.Trim().Length > 0)
                {
                    query = query + " and examrollno = '" + textBoxExamRollNumber.Text.Trim() + "'";
                }
                else
                {
                    if (textBoxName.Text.Trim().Length > 0)
                        query = query + " and name = '" + textBoxName.Text.Trim() + "'";
                    if (comboBoxBatch.SelectedIndex > 0)
                        query = query + " and batch = '" + comboBoxBatch.Text + "'";
                    if (textBoxPercentStart.Text.Trim().Length > 0)
                        query = query + " and Basic.PCM >= " + textBoxPercentStart.Text;
                    if (textBoxPercentTo.Text.Trim().Length > 0)
                        query = query + " and Basic.PCM <= " + textBoxPercentTo.Text;
                    if (comboBoxCourse.SelectedIndex > 0)
                        query = query + " and Course = '" + comboBoxCourse.Text + "'";
                    if (comboBoxBranch.SelectedIndex > 0)
                        query = query + " and Branch = '" + comboBoxBranch.Text + "'";
                    if (comboBoxStatus.SelectedIndex > 0)
                        query = query + " and basic.status = '" + comboBoxStatus.Text + "'";
                    if (comboBoxSemester.SelectedIndex > 0)
                        query = query + " and semester = '" + comboBoxSemester.Text + "'";
                    if (radioButtonYes.Checked)
                        query = query + " and hostel = '" + radioButtonYes.Text + "'";
                    if (radioButtonNo.Checked)
                        query = query + " and hostel = '" + radioButtonNo.Text + "'";
                    if (comboBoxGender.SelectedIndex > 0)
                        query = query + " and gender = '" + comboBoxGender.Text + "'";
                    if (comboBoxCategory.SelectedIndex > 0)
                        query = query + " and category = '" + comboBoxCategory.Text + "'";
                    if (comboBoxOccupation.SelectedIndex > 0)
                        query = query + " and occupation = '" + comboBoxOccupation.Text + "'";
                    if (textBoxAggFrom.Text.Trim().Length > 0)
                        query = query + " and Aggregate_Marks.AggregatePercent >= " + textBoxAggFrom.Text.Trim();
                    if (textBoxAggTo.Text.Trim().Length > 0)
                        query = query + " and Aggregate_Marks.AggregatePercent <= " + textBoxAggTo.Text.Trim();
                    if (textBoxTBFrom.Text.Trim().Length > 0)
                        query = query + " and TotalBacklog >= " + textBoxTBFrom.Text.Trim();
                    if (textBoxTBTo.Text.Trim().Length > 0)
                        query = query + " and TotalBacklog <= " + textBoxTBTo.Text.Trim();
                    if (textBoxCBFrom.Text.Trim().Length > 0)
                        query = query + " and CurrentBacklog >= " + textBoxCBFrom.Text.Trim();
                    if (textBoxCBTo.Text.Trim().Length > 0)
                        query = query + " and CurrentBacklog <= " + textBoxCBTo.Text.Trim();
                    if (textBoxYBFrom.Text.Trim().Length > 0)
                        query = query + " and TotalYearBack >= " + textBoxYBFrom.Text.Trim();
                    if (textBoxYBTo.Text.Trim().Length > 0)
                        query = query + " and TotalYearBack <= " + textBoxYBTo.Text.Trim();
                    if (textBoxIncomeFrom.Text.Trim().Length > 0)
                        query = query + " and AnnualIncome >= " + textBoxIncomeFrom.Text.Trim();
                    if (textBoxIncomeTo.Text.Trim().Length > 0)
                        query = query + " and AnnualIncome <= " + textBoxIncomeTo.Text.Trim();
                }
                if (query == query2)
                {
                    MessageBox.Show("No Paramater Selected!");
                    return;
                }
                DS.Clear();
                DA = new SqlDataAdapter(query, connection);
                DA.Fill(DS, "Data");
                dataGridViewSearch.DataSource = DS.Tables["Data"];
            }
            catch (Exception ex)
            {
                ErrorLog.Write(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxSearch_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }
               
        private void SearchData_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewSearch_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int i = e.RowIndex;
                string DesiredRollNo = DS.Tables[0].Rows[i][0].ToString();
                Program.RollNumber = DesiredRollNo;
                View objView = new View();
                objView.MdiParent = this.MdiParent;
                objView.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Search"+ex.Message);
            }
        }

        private string FindTables()
        {
            string tables = "";            
            if (checkBoxGeneral.Checked == true)
              tables = tables + ",General";
            if(checkBoxAddress.Checked == true)
                tables = tables + ",Correspondence_Address,Permanent_Address,Correspondence_Address_Backup,Permanent_Address_Backup";
            if (checkBoxAcademics.Checked == true)
                tables = tables + ",Academic_Performance";
            if (checkBoxMarks.Checked == true)
                tables = tables + ",Marks";
            if (checkBoxFinencial.Checked == true)
                tables = tables + ",Draft,Fee";
            if (checkBoxCollegeMarks.Checked == true)
                tables = tables + ",Semester_Marks,Aggregate_Marks";
            if (checkBoxRoll.Checked == true)
                tables = tables + ",RollNumber";
            return tables;
        }

        private string FindCondition()
        {            
            string condition = "";
            if (checkBoxGeneral.Checked == true)
                condition = condition + " and Basic.RollNo = General.RollNo";
            if (checkBoxAddress.Checked == true)
                condition = condition + " and Basic.RollNo = Correspondence_Address.RollNo and Basic.RollNo = Permanent_Address.RollNo and Basic.RollNo = Correspondence_Address_Backup.RollNo and Basic.RollNo = Permanent_Address_Backup.RollNo";
            if (checkBoxAcademics.Checked == true)
                condition = condition + " and Basic.RollNo = Academic_Performance.RollNo";
            if (checkBoxMarks.Checked == true)
                condition = condition +  " and Basic.RollNo = Marks.RollNo";
            if (checkBoxFinencial.Checked == true)
                condition = condition +  " and Basic.RollNo = Draft.RollNo and Basic.RollNo = Fee.RollNo";
            if (checkBoxCollegeMarks.Checked == true)
                condition = condition +  " and Basic.RollNo = Semester_Marks.RollNo and Basic.RollNo = Aggregate_Marks.RollNo";
            if (checkBoxRoll.Checked == true)
                condition = condition + " and Basic.RollNo = RollNumber.RollNo";
            return condition;
        }

        private void DeleteRedundent()
        {
            //int length = dataGridView1.ColumnCount;
            foreach(DataColumn dc in dataGridView1.Columns)
            {
                string name = dc.ColumnName;
                //string subName = ";
                if (name.Length>6 && name.Contains("RollNo"));
                {
                  // delete this column
                    dataGridView1.Columns.Remove(name);
                    
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tables = "";
                string condition = "";
                int rowCount = 0;
                string[] RollNumber;
                string fileName;
                string querry;

                tables = FindTables();
                condition = FindCondition();

                try
                {
                    if (DS.Tables["Data"].Rows.Count > 0)
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        saveFileDialog1.Filter = "Excel|*.xls";
                        saveFileDialog1.Title = "Save an Excel File";
                        Excel.Application xlApp;
                        Excel.Workbook xlWorkBook;
                        Excel.Worksheet xlWorkSheet;
                        object misValue = System.Reflection.Missing.Value;

                        xlApp = new Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Add(misValue);
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                        rowCount = DS.Tables["Data"].Rows.Count;
                        RollNumber = new string[rowCount];
                        saveFileDialog1.ShowDialog();

                        if (saveFileDialog1.FileName != "")
                        {
                            for (int k = 0; k < rowCount; k++)
                            {
                                RollNumber[k] = DS.Tables["Data"].Rows[k][0].ToString();
                            }
                            try
                            {
                                int i = 0;
                                int j = 0;
                                int l; // for mainting columns in excel sheet
                                for (int k = 0; k < rowCount; k++)
                                {
                                    DS = new DataSet();
                                    querry = "select * from Basic" + tables + " where Basic.RollNo = '" + RollNumber[k] + "' " + condition;
                                    DA = new SqlDataAdapter(querry, connection);
                                    DA.Fill(DS, "Excel");
                                    dataGridView1.DataSource = DS.Tables["Excel"];
                                    i = 0;
                                    l = 0;
                                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        string name = dataGridView1.Columns[j].Name;
                                        if (!(name.Length > 6 && name.Contains("RollNo") == true))
                                        {
                                            if (k == 0)
                                                xlWorkSheet.Cells[k + 1, l + 1] = name;
                                            DataGridViewCell cell = dataGridView1[j, i];
                                            xlWorkSheet.Cells[k + 2, l + 1] = cell.Value;
                                            l++;
                                        }
                                    }
                                }

                                xlWorkBook.SaveAs(saveFileDialog1.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                                xlWorkBook.Close(true, misValue, misValue);
                                xlApp.Quit();

                                releaseObject(xlWorkSheet);
                                releaseObject(xlWorkBook);
                                releaseObject(xlApp);

                                MessageBox.Show("Excel file created ");

                            }
                            catch (Exception ex)
                            {
                                ErrorLog.Write(ex.Message);
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No file name");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nothing to export");
                    }

                    buttonSearch_Click(this, EventArgs.Empty);
                }
                catch (NullReferenceException ex)
                {
                    ErrorLog.Write(ex.Message);
                    MessageBox.Show("Search Not Performend");
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
             

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void checkBoxBasic_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxBasic.Checked == false)
            {
                checkBoxBasic.Checked = true;
            }
        }

        private void dataGridViewSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
