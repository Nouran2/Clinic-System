using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem
{
    public partial class Doctors : Form
    {
        /*
          DocNameTb
        DocPhoneTb
        DocPassTb
        DocDob
        DocSepCb
        DocExpTb
        DocAddTb
         */
        public Doctors()
        {
            InitializeComponent();
            DisplayRec();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Abdo\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void clear()
        {

            DocNameTb.Text = "";
            DocPhoneTb.Text = "";
            DocPassTb.Text = "";
            DocSepCb.SelectedIndex = 0;
            DocGenderCb.SelectedIndex = 0;
            DocExpTb.Text = "";
            DocAddTb.Text = "";
            key = 0;
        }
        private void DisplayRec()
        {
            Con.Open();
            string Query = "Select * from DoctorTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);

            // Instantiate SqlCommandBuilder and associate it with the SqlDataAdapter
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);

            var ds = new DataSet();
            sda.Fill(ds);

            DocDG.DataSource = ds.Tables[0];
            clear();
            Con.Close();


        }

        private void DocAddBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DocNameTb.Text) ||
                string.IsNullOrWhiteSpace(DocPhoneTb.Text) ||
                string.IsNullOrWhiteSpace(DocPassTb.Text) ||
                DocSepCb.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(DocExpTb.Text) ||
                string.IsNullOrWhiteSpace(DocAddTb.Text))
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
                        SqlCommand sqlCommand = new SqlCommand("INSERT INTO DoctorTable(DocName, DocDob, DocGender, DocSpec, DocExp, DocPhone, DocAddress, DocPassword) VALUES(@DN, @DD, @DG, @DS, @DE, @DP, @DA, @DPASS)", Con);

                        sqlCommand.Parameters.AddWithValue("@DN", DocNameTb.Text);
                        sqlCommand.Parameters.AddWithValue("@DD", DocDob.Value.Date);
                        sqlCommand.Parameters.AddWithValue("@DG", DocGenderCb.SelectedItem.ToString());
                        sqlCommand.Parameters.AddWithValue("@DS", DocSepCb.SelectedItem.ToString());
                        sqlCommand.Parameters.AddWithValue("@DE", DocExpTb.Text);
                        sqlCommand.Parameters.AddWithValue("@DP", DocPhoneTb.Text);
                        sqlCommand.Parameters.AddWithValue("@DA", DocAddTb.Text);
                        sqlCommand.Parameters.AddWithValue("@DPASS", DocPassTb.Text);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int key = 0;
        private void DocEditBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DocNameTb.Text) ||
               string.IsNullOrWhiteSpace(DocPhoneTb.Text) ||
               string.IsNullOrWhiteSpace(DocPassTb.Text) ||
               DocSepCb.SelectedIndex == -1 ||
               string.IsNullOrWhiteSpace(DocExpTb.Text) ||
               string.IsNullOrWhiteSpace(DocAddTb.Text))
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
                        SqlCommand sqlCommand = new SqlCommand("UPDATE DoctorTable SET DocName = @DN, DocDob = @DD, DocGender = @DG, DocSpec = @DS, DocExp = @DE, DocPhone = @DP, DocAddress = @DA, DocPassword = @DPASS WHERE DocId=@Dkey", Con);

                        sqlCommand.Parameters.AddWithValue("@DN", DocNameTb.Text);
                        sqlCommand.Parameters.AddWithValue("@DD", DocDob.Value.Date);
                        sqlCommand.Parameters.AddWithValue("@DG", DocGenderCb.SelectedItem.ToString());
                        sqlCommand.Parameters.AddWithValue("@DS", DocSepCb.SelectedItem.ToString());
                        sqlCommand.Parameters.AddWithValue("@DE", DocExpTb.Text);
                        sqlCommand.Parameters.AddWithValue("@DP", DocPhoneTb.Text);
                        sqlCommand.Parameters.AddWithValue("@DA", DocAddTb.Text);
                        sqlCommand.Parameters.AddWithValue("@DPASS", DocPassTb.Text);
                        sqlCommand.Parameters.AddWithValue("@Dkey", key);
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

        private void DocDG_SelectionChanged(object sender, EventArgs e)
        {
            if (DocDG.SelectedRows.Count > 0)
            {
                DocNameTb.Text = DocDG.SelectedRows[0].Cells[1].Value.ToString();
                DocDob.Value = Convert.ToDateTime(DocDG.SelectedRows[0].Cells[2].Value);
                DocGenderCb.SelectedItem = DocDG.SelectedRows[0].Cells[3].Value.ToString();
                DocSepCb.SelectedItem = DocDG.SelectedRows[0].Cells[4].Value.ToString();
                DocExpTb.Text = DocDG.SelectedRows[0].Cells[5].Value.ToString();
                DocPhoneTb.Text = DocDG.SelectedRows[0].Cells[6].Value.ToString();
                DocAddTb.Text = DocDG.SelectedRows[0].Cells[7].Value.ToString();
                DocPassTb.Text = DocDG.SelectedRows[0].Cells[8].Value.ToString();

                key = Convert.ToInt32(DocDG.SelectedRows[0].Cells[0].Value);
            }


        }

        private void DocDElBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Doctor");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM DoctorTable  WHERE DocId = @DKey", Con);
                    cmd.Parameters.AddWithValue("@DKey", key);
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

        private void label15_Click(object sender, EventArgs e)
        {
            patient obj = new patient();
            obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            labtest obj = new labtest();
            obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            receptionist obj = new receptionist();
            obj.Show();
            this.Hide();
        }

        private void rjButton5_Click(object sender, EventArgs e)
        {
            Doctors obj = new Doctors();
            obj.Show();
            this.Hide();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }
    }
}
