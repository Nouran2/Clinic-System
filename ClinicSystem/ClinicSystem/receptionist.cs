using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;


namespace ClinicSystem
{
    public partial class receptionist : Form
    {
        /*
         RNameTb
        RPhoneTb
        RPasswordTb
        RAddressTb
         */
        public receptionist()
        {
            InitializeComponent();
            DisplayRec();
            receptionistDG.SelectionChanged += receptionistDG_SelectionChanged;
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Abdo\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayRec()
        {
            Con.Open();
            string Query = "Select * from RecepTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);

            receptionistDG.DataSource = ds.Tables[0];
            clear();
            Con.Close();


        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (RNameTb.Text == "" ||
                RPhoneTb.Text == "" ||
                RPasswordTb.Text == "" ||
                RAddressTb.Text == "")
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into RecepTable(RecepName,RecepPhone,RecepAdd,RecepPass)values(@RN,@RP,@RA,@RPA)", Con);
                    cmd.Parameters.AddWithValue("@RN", RNameTb.Text);
                    cmd.Parameters.AddWithValue("@RP", RPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@RA", RAddressTb.Text);
                    cmd.Parameters.AddWithValue("@RPA", RPasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Added");
                    Con.Close();
                    DisplayRec();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void receptionist_Load(object sender, EventArgs e)
        {
        }
        int Key = 0;
        private void receptionistDG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RNameTb.Text = receptionistDG.SelectedRows[0].Cells[1].Value.ToString();
            RPhoneTb.Text = receptionistDG.SelectedRows[0].Cells[2].Value.ToString();
            RAddressTb.Text = receptionistDG.SelectedRows[0].Cells[3].Value.ToString();
            RPasswordTb.Text = receptionistDG.SelectedRows[0].Cells[4].Value.ToString();
            if (RNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(RNameTb.Text = receptionistDG.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void receptionistDG_SelectionChanged(object sender, EventArgs e)
        {
            if (receptionistDG.SelectedRows.Count > 0)
            {
                RNameTb.Text = receptionistDG.SelectedRows[0].Cells[1].Value.ToString();
                RPhoneTb.Text = receptionistDG.SelectedRows[0].Cells[2].Value.ToString();
                RAddressTb.Text = receptionistDG.SelectedRows[0].Cells[3].Value.ToString();
                RPasswordTb.Text = receptionistDG.SelectedRows[0].Cells[4].Value.ToString();
                Key = Convert.ToInt32(receptionistDG.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (RNameTb.Text == "" ||
                RPhoneTb.Text == "" ||
                RPasswordTb.Text == "" ||
                RAddressTb.Text == "")
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update  RecepTable set RecepName=@RN , RecepPhone=@RP ,RecepAdd=@RA,RecepPass=@RPA  where RecepId=@RKey ", Con);
                    cmd.Parameters.AddWithValue("@RN", RNameTb.Text);
                    cmd.Parameters.AddWithValue("@RP", RPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@RA", RAddressTb.Text);
                    cmd.Parameters.AddWithValue("@RPA", RPasswordTb.Text);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist updated ");
                    Con.Close();
                    DisplayRec();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Receptionist");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM RecepTable WHERE RecepId = @RKey", Con);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Deleted");
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
        private void clear()
        {
            RNameTb.Text = "";
             RPhoneTb.Text = "";
            RPasswordTb.Text = "";
            RAddressTb.Text = "";
            Key = 0;
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}