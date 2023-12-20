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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            panel4.Hide();
            panel6.Hide();
            panel7.Hide();
            panel3.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        
        

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel4.Hide();
            panel6.Hide();
            panel7.Hide();
            panel3.Show();

            panel5.Controls.Clear();
            AddStudent ast = new AddStudent();
            ast.TopLevel = false;
            panel5.Controls.Add(ast);
            ast.Show();
            label1.Text = "Student Information";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel6.Hide();
            panel7.Hide();
            panel4.Show();

            panel5.Controls.Clear();
            AddCourse ast = new AddCourse();
            ast.TopLevel = false;
            panel5.Controls.Add(ast);
            ast.Show();
            label1.Text = "Course Details";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Hide();
            panel6.Show();
            panel7.Hide();
            panel3.Hide();

            panel5.Controls.Clear();
            AddFaculty ast = new AddFaculty();
            ast.TopLevel = false;
            panel5.Controls.Add(ast);
            ast.Show();


            label1.Text = "Faculty Information";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Hide();
            panel6.Hide();
            panel7.Show();
            panel3.Hide();

            panel5.Controls.Clear();
            EnrollCourseForm ast = new EnrollCourseForm();
            ast.TopLevel = false;
            panel5.Controls.Add(ast);
            ast.Show();

            label1.Text = "Enroll Course to Student";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 atf = new Form1();
            atf.Closed += (s, args) => this.Close();
            atf.Show();
        }
    }
}
