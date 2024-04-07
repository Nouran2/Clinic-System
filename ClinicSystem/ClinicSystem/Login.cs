using System.Data;
using System.Data.SqlClient;

namespace ClinicSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Abdo\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");


        private void label4_Click(object sender, EventArgs e)
        {
            RoleCb.SelectedIndex = -1;
            UserTb.Text = "";
            PasswordTb.Text = "";
        }
        public static string Role;
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(RoleCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select Role");
            }
            else if (RoleCb.SelectedIndex == 0 ) { 
                if(PasswordTb.Text =="" || UserTb.Text == "")
                {
                    MessageBox.Show("Missing info");
                }
                else if(UserTb.Text=="Admin" && PasswordTb.Text=="Password")
                {
                    Role = "Admin";
                    patient obj = new patient();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("wrong Admin and Password");
                }
            }
            else if (RoleCb.SelectedIndex == 1) {
                if (PasswordTb.Text == "" || UserTb.Text == "")
                {
                    MessageBox.Show("Missing info");
                }
                else
                {
                    Con.Open();
                    string query = "SELECT COUNT(*) FROM DoctorTable WHERE DocName = @Username AND DocPassword = @Password";
                    SqlCommand command = new SqlCommand(query, Con);

                    // Add parameters
                    command.Parameters.AddWithValue("@Username", UserTb.Text);
                    command.Parameters.AddWithValue("@Password", PasswordTb.Text);

                    
                    int count = (int)command.ExecuteScalar(); // ExecuteScalar returns the first column of the first row

                    if (count == 1)
                    {
                        Role = "Doctor";
                        prescription obj = new prescription();
                        obj.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Doctor Not found");
                    }
                    Con.Close();
                }
            }
            else if (RoleCb.SelectedIndex == 2) {
                if (PasswordTb.Text == "" || UserTb.Text == "")
                {
                    MessageBox.Show("Missing info");
                }
                else
                {
                    Con.Open();
                    string query = "SELECT COUNT(*) FROM RecepTable WHERE RecepName = @Username AND RecepPass = @Password";
                    SqlCommand command = new SqlCommand(query, Con);

                    // Add parameters
                    command.Parameters.AddWithValue("@Username", UserTb.Text);
                    command.Parameters.AddWithValue("@Password", PasswordTb.Text);


                    int count = (int)command.ExecuteScalar(); // ExecuteScalar returns the first column of the first row

                    if (count == 1)
                    {
                        Role = "Receptionist";
                        Home obj = new Home();
                        obj.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Receptionist Not found");
                    }
                    Con.Close();
                }
            }
        }
    }
}