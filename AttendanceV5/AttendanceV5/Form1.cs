using AttendanceV5.Gateway;
using AttendanceV5.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AttendanceV5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string fid = "";

        LogInGateway log = new LogInGateway();

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";

        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            string username = bunifuMaterialTextbox1.Text;
            string password = textBox1.Text;

           
            

            if (username == "admin")
            {
                int flag = log.TestAdmin(username, password);

                if (flag == 1)
                {
                    this.Hide();
                    AdminDashboard mmf = new AdminDashboard();
                    mmf.Closed += (s, args) => this.Close();
                    mmf.Show();
                }
                else MessageBox.Show("Incorrect Username or Password");
            }
            else
            {
                int flag = log.TestFaculty(username, password);

                fid = log.GetFacultyId(username, password);
                // MessageBox.Show(fid);

                if (flag == 1)
                {
                    this.Hide();
                    FacultyDashboard mmf = new FacultyDashboard();
                    mmf.Closed += (s, args) => this.Close();
                    mmf.Show();
                }
                else MessageBox.Show("Incorrect Username or Password");
            }
        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            
        }

        
    }
}
