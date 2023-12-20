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

using System.Net;
using System.IO;

namespace AttendanceV5
{
    public partial class TakeAttendance : Form
    {
        public TakeAttendance()
        {
            InitializeComponent();
        }

        AttendanceGateway ag = new AttendanceGateway();
        Attendance at = new Attendance();
        //string cs = @"Data Source=MEHADI-PC\SQLEXPRESS;Initial Catalog=AttendanceV4;Integrated Security=True";
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void ShowGridView()
        {
            //SqlConnection con = new SqlConnection(cs);
            //string q = "select StudentId, StudentName from EnrollCourse WHERE Department = '"+comboBox4.Text+"' AND Program = '"+comboBox1.Text+"' AND Semester = '"+comboBox2.Text+"' AND CourseCode = '"+comboBox3.Text+"'";
            //SqlCommand cmd = new SqlCommand(q, con);
            //con.Open();

            //DataTable dt = new DataTable();
            //SqlDataAdapter sda = new SqlDataAdapter(q, con);
            //sda.Fill(dt);


            //dataGridView1.DataSource = dt;
            //con.Close();

            //DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            //chk.HeaderText="Present";
            //dataGridView1.Columns.Add(chk);

            //DataGridViewCheckBoxColumn chk1 = new DataGridViewCheckBoxColumn();
            //chk1.HeaderText = "Absent";
            //dataGridView1.Columns.Add(chk1);

            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Present", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Absent", System.Type.GetType("System.Boolean"));



            SqlConnection con = new SqlConnection(cs);
            string q = "select StudentId, StudentName from EnrollCourse WHERE Department = '" + comboBox4.Text + "' AND Program = '" + comboBox1.Text + "' AND Semester = '" + comboBox2.Text + "' AND CourseCode = '" + comboBox3.Text + "' ORDER BY StudentId";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow rw = dt.NewRow();
                    rw[0] = Convert.ToInt32(reader["StudentId"]);
                    rw[1] = reader["StudentName"].ToString();
                    dt.Rows.Add(rw);
                }
            }
            con.Close();
            dataGridView1.DataSource = dt;


        }

        private void TakeAttendance_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = comboBox1.Text;
            if (s == "PGDIT")
            {
                comboBox2.Items.Clear();

                comboBox2.Items.Add("1st");
                comboBox2.Items.Add("2nd");
                comboBox2.Items.Add("3rd");
            }
            if (s == "PMIT")
            {
                comboBox2.Items.Clear();

                comboBox2.Items.Add("1st");
                comboBox2.Items.Add("2nd");
                comboBox2.Items.Add("3rd");
            }
            if (s == "M.Sc")
            {
                comboBox2.Items.Clear();

                comboBox2.Items.Add("1st");
                comboBox2.Items.Add("2nd");

            }
            if (s == "B.Sc")
            {
                comboBox2.Items.Clear();

                comboBox2.Items.Add("1st");
                comboBox2.Items.Add("2nd");
                comboBox2.Items.Add("3rd");
                comboBox2.Items.Add("4th");
                comboBox2.Items.Add("5th");
                comboBox2.Items.Add("6th");
                comboBox2.Items.Add("7th");
                comboBox2.Items.Add("8th");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sem = comboBox2.Text;
            List<string> codes = new List<string>();
            codes = ag.GetCourseCodes(sem);
            comboBox3.Items.Clear();
            foreach (string p in codes)
            {
                comboBox3.Items.Add(p);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cCode = comboBox3.Text;
            bunifuMaterialTextbox1.Text = ag.GetCourseName(cCode);
            //textBox3.Text = ag.GetCourseCredit(cCode);
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            //textBox4.Text = dateTimePicker1.Text;
            ShowGridView();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            dataGridView1.Rows[RowIndex].Cells["Present"].Value = false;
            dataGridView1.Rows[RowIndex].Cells["Absent"].Value = false;
        }

        public void sendSms(List<string> id)
        {
            string num = ag.GetNumberById(id);
            num = num.Substring(0, num.Length-1);
           // MessageBox.Show(num);




            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                String to = num; //Recipient Phone Number multiple number must be separated by comma
                String token = "f2e8ee5a2d1fd5a9e30586b6ffe972bf"; //generate token from the control panel
                String message = System.Uri.EscapeUriString("Dear Guardian, Your child did not attend in todays class."); //do not use single quotation (') in the message to avoid forbidden result
                String url = "http://api.greenweb.com.bd/api.php?token=" + token + "&to=" + to + "&message=" + message;
                request = WebRequest.Create(url);

                // Send the 'HttpWebRequest' and wait for response.
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new
                System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                //Console.WriteLine(result);
               // MessageBox.Show(result);
                reader.Close();
                stream.Close();
            }
            catch (Exception exp)
            {
                // Console.WriteLine(exp.ToString());
                MessageBox.Show(exp.ToString());
            }
            finally
            {
                if (response != null)
                    response.Close();
            }





        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            at.Program = comboBox1.Text;
            at.Department = comboBox4.Text;
            at.Semester = comboBox2.Text;
            at.CourseCode = comboBox3.Text;

            bool isSave = false;

            List<string> id = new List<string>();
            id.Clear();
            

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                int p = Convert.ToInt32(item.Cells[0].Value);


                if (p != 0)
                {
                    string s = item.Cells[0].Value.ToString();
                    at.StudentId = Convert.ToInt32(s);

                    string s1 = s;

                    s = item.Cells[1].Value.ToString();
                    at.StudentName = s;

                    

                    s = item.Cells[2].Value.ToString();
                    //MessageBox.Show(s);
                    if (s == "False")
                    {
                        at.PorA = "Absent";
                        id.Add(s1);
                    }
                    if (s == "True") at.PorA = "Present";
                    at.dt = bunifuDatepicker1.Value.ToString("yyyy-MM-dd");
                    isSave = ag.Insert(at);

                }

            }
            if (isSave) MessageBox.Show("Data Saved");
            else MessageBox.Show("some problem occured");

            if(checkBox1.Checked == true)
            {
                sendSms(id);
            }
           
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.RowCount;
            for (int i = 0; i < row; i++)
            {
                dataGridView1.Rows[i].Cells["Present"].Value = true;
                dataGridView1.Rows[i].Cells["Absent"].Value = false;

            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.RowCount;
            for (int i = 0; i < row; i++)
            {
                dataGridView1.Rows[i].Cells["Absent"].Value = true;
                dataGridView1.Rows[i].Cells["Present"].Value = false;
            }
        }
    }
}
