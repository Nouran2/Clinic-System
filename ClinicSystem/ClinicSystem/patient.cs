using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClinicSystem
{
    public partial class patient : Form
    {
        public patient()
        {
            InitializeComponent();
            DisplayRec();
        }
        int key = 0;
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Abdo\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void clear()
        {

            PatNameTb.Text = "";
            PatPhoneTb.Text = "";
            PatDOB.Value = DateTime.Now;
            PatHivCb.SelectedIndex = 0;
            PatGenCb.SelectedIndex = 0;
            PatAllergiesTb.Text = "";
            PatAddTb.Text = "";

            key = 0;
        }


        private void DisplayRec()
        {
            Con.Open();
            string Query = "Select * from PatientTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);


            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);

            var ds = new DataSet();
            sda.Fill(ds);

            PatDG.DataSource = ds.Tables[0];
            clear();
            Con.Close();


        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (
                 string.IsNullOrWhiteSpace(PatNameTb.Text) ||
                 string.IsNullOrWhiteSpace(PatPhoneTb.Text) ||
                 PatHivCb.SelectedIndex == -1 ||
                 PatGenCb.SelectedIndex == -1 ||
                 string.IsNullOrWhiteSpace(PatAllergiesTb.Text) ||
                 string.IsNullOrWhiteSpace(PatAddTb.Text)
                 )
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
                        SqlCommand sqlCommand = new SqlCommand("INSERT INTO PatientTable(PatName, PatDob, PatGen, PatAddress, PatPhone, PatAll, PatHiv) VALUES(@PN, @PD, @PG, @PA, @PP, @PAL, @PH)", Con);
                        //@PN, @PD, @PG, @PA, @PP, @PA, @PH
                        //PatName, PatDob, PatGen, PatAddress, PatPhone, PatAll, PatHiv

                        sqlCommand.Parameters.AddWithValue("@PN", PatNameTb.Text);
                        sqlCommand.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                        sqlCommand.Parameters.AddWithValue("@PG", PatGenCb.SelectedItem.ToString());
                        sqlCommand.Parameters.AddWithValue("@PA", PatAddTb.Text);
                        sqlCommand.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                        sqlCommand.Parameters.AddWithValue("@PAL", PatAllergiesTb.Text);
                        sqlCommand.Parameters.AddWithValue("@PH", PatHivCb.SelectedItem.ToString()[0]);

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

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatNameTb.Text) ||
                string.IsNullOrWhiteSpace(PatPhoneTb.Text) ||
                PatHivCb.SelectedIndex == -1 ||
                PatGenCb.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(PatAllergiesTb.Text) ||
                string.IsNullOrWhiteSpace(PatAddTb.Text))
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
                        SqlCommand sqlCommand = new SqlCommand("UPDATE PatientTable SET PatName = @PN, PatDob = @PD, PatGen = @PG, PatAddress = @PA, PatPhone = @PP, PatAll = @PAL, PatHiv = @PH WHERE PatID = @PatID", Con);

                        sqlCommand.Parameters.AddWithValue("@PN", PatNameTb.Text);
                        sqlCommand.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                        sqlCommand.Parameters.AddWithValue("@PG", PatGenCb.SelectedItem.ToString());
                        sqlCommand.Parameters.AddWithValue("@PA", PatAddTb.Text);
                        sqlCommand.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                        sqlCommand.Parameters.AddWithValue("@PAL", PatAllergiesTb.Text);
                        sqlCommand.Parameters.AddWithValue("@PH", PatHivCb.SelectedItem.ToString()[0]);

                        // You need to have a PatID field to uniquely identify each patient record.
                        // Replace PatID with the actual identifier field of your PatientTable.
                        sqlCommand.Parameters.AddWithValue("@PatID", key);

                        sqlCommand.ExecuteNonQuery();

                        DisplayRec(); // This method needs to refresh the displayed records after the update.
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }



        private void PatDG_Sc(object sender, EventArgs e)
        {
            if (PatDG.SelectedRows.Count == 1)
            {
                PatNameTb.Text = PatDG.SelectedRows[0].Cells["PatName"].Value.ToString();
                PatDOB.Value = Convert.ToDateTime(PatDG.SelectedRows[0].Cells["PatDob"].Value);
                PatGenCb.SelectedItem = PatDG.SelectedRows[0].Cells["PatGen"].Value.ToString();
                PatAddTb.Text = PatDG.SelectedRows[0].Cells["PatAddress"].Value.ToString();
                PatPhoneTb.Text = PatDG.SelectedRows[0].Cells["PatPhone"].Value.ToString();
                PatAllergiesTb.Text = PatDG.SelectedRows[0].Cells["PatAll"].Value.ToString();
                PatHivCb.SelectedItem = PatDG.SelectedRows[0].Cells["PatHiv"].Value.ToString();


                key = Convert.ToInt32(PatDG.SelectedRows[0].Cells["PatID"].Value);
            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Patient");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM PatientTable  WHERE PatId = @PatId", Con);
                    cmd.Parameters.AddWithValue("@PatId", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Deleted");
                    Con.Close();
                    DisplayRec();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                }

            }
        }

        private void rjButton6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PatDG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }
        /*
PatNameTb 
PatPhoneTb
PatDOB
PatHivCb
PatGenCb
PatAllergiesTb
PatAddTb

*/

    }
}
