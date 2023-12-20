using AttendanceV5.Gateway;
using AttendanceV5.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AttendanceV5
{
    public partial class EnrollCourseForm : Form
    {
        public EnrollCourseForm()
        {
            InitializeComponent();
        }

        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        EnrollStudentGateway esg = new EnrollStudentGateway();
        EnrollCourseC enc = new EnrollCourseC();


        public void ShowGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string q = "select StudentId, StudentName, Department, CourseCode, CourseName, CourseCredit, Semester, Program from EnrollCourse";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(q, con);
            sda.Fill(dt);


            dataGridView1.DataSource = dt;
            con.Close();

        }

        public void ShowGridViewBySearch(string cat, string s)
        {
            string q = "hello";
            if (cat == "StudentId") { int num = Convert.ToInt32(s); q = "select StudentId, StudentName, Department, CourseCode, CourseName, CourseCredit, Semester, Program from EnrollCourse WHERE StudentId = " + num + ""; }
            if (cat == "Department") q = "select StudentId, StudentName, Department, CourseCode, CourseName, CourseCredit, Semester, Program from EnrollCourse WHERE Department = '" + s + "'";
            if (cat == "CourseCode") q = "select StudentId, StudentName, Department, CourseCode, CourseName, CourseCredit, Semester, Program from EnrollCourse WHERE CourseCode = '" + s + "'";

            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(q, con);
            sda.Fill(dt);


            dataGridView1.DataSource = dt;
            con.Close();
        }

        public void ClearField()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox6.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox7.Text = "";
            textBox5.Text = "";

        }

        public void GetStudentId()
        {
            comboBox1.Items.Clear();
            List<int> list = new List<int>();
            list = esg.GetStudentIdList();
            foreach (int s in list)
            {
                comboBox1.Items.Add(s);
            }
        }

        public void GetCourseName()
        {
            comboBox2.Items.Clear();
            List<String> list = new List<string>();
            list = esg.GetCourseCodeList();
            foreach (string s in list)
            {
                comboBox2.Items.Add(s);
            }
        }

        private void EnrollCourse_Load(object sender, EventArgs e)
        {
            GetStudentId();
            GetCourseName();
            ShowGridView();
        }

        public string StudentId { get; set; }

        public string CourseId { get; set; }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            enc.StudentId = Convert.ToInt32(comboBox1.Text);
            enc.StudentName = textBox1.Text;
            enc.Department = textBox2.Text;
            enc.CourseCode = comboBox2.Text;
            enc.CourseId = Convert.ToInt32(textBox5.Text);
            enc.CourseName = textBox4.Text;
            enc.CourseCredit = textBox3.Text;
            enc.semester = textBox6.Text;
            enc.program = textBox7.Text;

            string isSave = esg.Insert(enc);
            MessageBox.Show(isSave);

            ShowGridView();
            ClearField();
            GetStudentId();
            GetCourseName();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(comboBox1.Text);
            int cid = Convert.ToInt32(textBox5.Text);
            bool isSuccess = esg.Delete(sid, cid);
            if (isSuccess == true) MessageBox.Show("Disenrolled successfully");
            else MessageBox.Show("Something went wrong!");
            ShowGridView();

            ClearField();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            string s = comboBox4.Text;
            string cat = comboBox3.Text;
            if (s == "")
            {
                MessageBox.Show("Select From Dropdown Menu");
            }
            else
            {
                ShowGridViewBySearch(cat, s);
                comboBox3.Text = "";
                comboBox4.Text = "";
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cat = comboBox3.Text;
            if (cat == "StudentId")
            {
                List<int> list = new List<int>();
                list = esg.GetStudentIdListFromEnrollCourseTable();
                comboBox4.Items.Clear();
                foreach (int id in list)
                {
                    comboBox4.Items.Add(id);
                }

            }
            if (cat == "Department")
            {
                List<string> list = new List<string>();
                list = esg.GetDepartmentListFromEnrollCourseTable();
                comboBox4.Items.Clear();
                foreach (string dp in list)
                {
                    comboBox4.Items.Add(dp);
                }

            }
            if (cat == "CourseCode")
            {
                List<string> list = new List<string>();
                list = esg.GetCourseCodeListFromEnrollCourseTable();
                comboBox4.Items.Clear();
                foreach (string cc in list)
                {
                    comboBox4.Items.Add(cc);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(comboBox1.Text);
            string sname = "";
            sname = esg.GetStudentName(sid);
            textBox1.Text = sname;

            string sdept = "";
            sdept = esg.GetStudentDeptName(sid);
            textBox2.Text = sdept;

            string sem = "";
            sem = esg.GetStudentSemester(sid);
            textBox6.Text = sem;

            string sp = "";
            sp = esg.GetStudentProgram(sid);
            textBox7.Text = sp;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cCode = comboBox2.Text;
            textBox3.Text = esg.GetCourseCredit(cCode);
            textBox4.Text = esg.GetCourseName(cCode);
            textBox5.Text = esg.GetCourseId(cCode);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            comboBox1.Text = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[RowIndex].Cells[3].Value.ToString();
        }
    }
}
