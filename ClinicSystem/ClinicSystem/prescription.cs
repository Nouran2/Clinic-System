using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClinicSystem
{
    public partial class prescription : Form
    {
        public prescription()
        {
            InitializeComponent();
            DisplayRec();
            GetDocId();
            GetPatId();
            GetTestId();
        }

        private void clear()
        {
            DocIdCb.SelectedIndex = -1;
            PatIdCb.SelectedIndex = -1;
            TestidCb.SelectedIndex = -1;

            DocNameTb.Text = "";
            PatNameTb.Text = "";
            TestTb.Text = "";
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Abdo\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayRec()
        {
            Con.Open();
            string Query = "Select * from PreseriptionTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);

            PreseriptionDG.DataSource = ds.Tables[0];
            clear();
            Con.Close();
        }

        private void GetDocId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select DocId from DoctorTable", Con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DocId", typeof(int));
            dt.Load(dr);
            DocIdCb.ValueMember = "DocId";
            DocIdCb.DataSource = dt;
            Con.Close();

        }
        private void GetPatId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select PatId from PatientTable", Con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PatId", typeof(int));
            dt.Load(dr);
            PatIdCb.ValueMember = "PatId";
            PatIdCb.DataSource = dt;
            Con.Close();

        }

        private void GetTestId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TestNum from TestTable", Con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TestNum", typeof(int));
            dt.Load(dr);
            TestidCb.ValueMember = "TestNum";
            TestidCb.DataSource = dt;
            Con.Close();

        }

        private void GetDocName()
        {
            Con.Open();
            string query = "Select * from DoctorTable where DocId=" + DocIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                DocNameTb.Text = dr["DocName"].ToString();
            }
            Con.Close();
        }
        private void GetPatName()
        {
            Con.Open();
            string query = "Select * from PatientTable where PatId=" + PatIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PatNameTb.Text = dr["PatName"].ToString();
            }
            Con.Close();
        }
        private void GetTest()
        {
            Con.Open();
            string query = "Select * from TestTable where TestNum=" + TestidCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TestTb.Text = dr["TestName"].ToString();
                CostTb.Text = dr["TestCost"].ToString();
            }
            Con.Close();
        }

        /*
         DocNameTb
        DocIdCb

        PatNameTb
        PatIdCb
        
        
        TestTb
        TestidCb

        CostTb
        MedicineTb
        
         */

        private void DocIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDocName();
        }

        private void PatIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetPatName();
        }

        private void TestidCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTest();
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DocNameTb.Text) ||
                            string.IsNullOrWhiteSpace(PatNameTb.Text) ||
                            string.IsNullOrWhiteSpace(TestTb.Text) ||
                            TestidCb.SelectedIndex == -1 ||
                            PatIdCb.SelectedIndex == -1 ||
                            DocIdCb.SelectedIndex == -1 ||
                            string.IsNullOrWhiteSpace(CostTb.Text) ||
                            string.IsNullOrWhiteSpace(MedicineTb.Text))
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Abdo\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        Con.Open();
                        /*
                         
                          DocNameTb
        DocIdCb

        PatNameTb
        PatIdCb
        
        
        TestTb
        TestidCb

        CostTb
        MedicineTb
        */
                        SqlCommand sqlCommand = new SqlCommand("INSERT INTO PreseriptionTable(DocId, DocName, PatId, PatName, LabTestid, LabTestName, Medicines, Cost) VALUES(@DI , @DN , @PI , @PN , @LN ,@LTN , @M , @C)", Con);
                        //@DI , @DN , @PI , @PN , @LN ,@LTN , @M , @C
                        sqlCommand.Parameters.AddWithValue("@DI", DocIdCb.SelectedValue.ToString());
                        sqlCommand.Parameters.AddWithValue("@DN", DocNameTb.Text);
                        sqlCommand.Parameters.AddWithValue("@PI", PatIdCb.SelectedValue.ToString());
                        sqlCommand.Parameters.AddWithValue("@PN", PatNameTb.Text);
                        sqlCommand.Parameters.AddWithValue("@LN", TestidCb.SelectedValue.ToString());
                        sqlCommand.Parameters.AddWithValue("@LTN", TestTb.Text);
                        sqlCommand.Parameters.AddWithValue("@M", MedicineTb.Text);
                        sqlCommand.Parameters.AddWithValue("@C", CostTb.Text);
                        sqlCommand.ExecuteNonQuery();
                        DisplayRec();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        int key = 0;
        private void PreseriptionDG_SelectionChanged(object sender, EventArgs e)
        {
            if (PreseriptionDG.SelectedRows.Count == 1)
            {


                string selectedDocName = PreseriptionDG.SelectedRows[0].Cells["DocName"].Value.ToString();
                string selectedPatName = PreseriptionDG.SelectedRows[0].Cells["PatName"].Value.ToString();
                string selectedTestName = PreseriptionDG.SelectedRows[0].Cells["LabTestName"].Value.ToString();
                string selectedCost = PreseriptionDG.SelectedRows[0].Cells["Cost"].Value.ToString();
                string selectedMedicines = PreseriptionDG.SelectedRows[0].Cells["Medicines"].Value.ToString();
                string framedText = $"\t\t\tPrescription\n" +
                                            $"\tDoctor:   {selectedDocName}\t" +
                                            $"\tPatient:  {selectedPatName}\n" +
                                            $"\tTest:     {selectedTestName}\t" +
                                            $"\tCost:     {selectedCost}\n" +
                                            $"\tMedicines:{selectedMedicines}\n" +
                                            $"";

                PreTxt.Text = framedText;
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font mainFont = new Font("Averia", 18, FontStyle.Regular);
            Brush mainBrush = Brushes.Black;

            // Define the font and brush for the footer text
            Font footerFont = new Font("Averia", 15, FontStyle.Regular);
            Brush footerBrush = Brushes.Red;

            // Define the text content
            string mainText = PreTxt.Text;
            string footerText = "Thanks";

            // Define the position for the main text
            PointF mainTextPosition = new PointF(95, 80);

            // Define the position for the footer text
            PointF footerTextPosition = new PointF(95, 400);

            // Draw the main text
            e.Graphics.DrawString(mainText + "\n", mainFont, mainBrush, mainTextPosition);

            // Draw the footer text
            e.Graphics.DrawString(footerText, footerFont, footerBrush, footerTextPosition);
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }
    }
}
