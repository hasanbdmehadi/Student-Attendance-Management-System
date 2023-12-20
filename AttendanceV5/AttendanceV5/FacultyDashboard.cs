using AttendanceV5.Gateway;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AttendanceV5.UI;

namespace AttendanceV5
{
    public partial class FacultyDashboard : Form
    {

        //public static Image facultyImage = null;
        public FacultyDashboard()
        {
            InitializeComponent();
       
        }
        FacultyProfileGateway fpg = new FacultyProfileGateway();

        Faculty faculty = new Faculty();

        string imgLocation = "";

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Faculty Profile";
            panel4.Show();
            panel6.Hide();
            panel7.Hide();

            panel5.Hide();

            label3.Hide();
            label4.Hide();
            label5.Hide();

            //panel5.Controls.Clear();
            //FacultyProfile ast = new FacultyProfile();
            //ast.TopLevel = false;
            //panel5.Controls.Add(ast);
            //ast.Show();

            //this.Hide();
            //FacultyProfile atf = new FacultyProfile();
            //atf.Closed += (s, args) => this.Close();
            //atf.Show();


        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "Take Attendance";

            panel4.Hide();
            panel6.Show();
            panel7.Hide();

            label3.Hide();
            label4.Hide();
            label5.Hide();


            panel5.Controls.Clear();
            panel5.Show();
            TakeAttendance ast = new TakeAttendance();
            ast.TopLevel = false;
            panel5.Controls.Add(ast);
            ast.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "Generate Report";
            panel4.Hide();
            panel6.Hide();
            panel7.Show();

            label3.Hide();
            label4.Hide();
            label5.Hide();

            panel5.Controls.Clear();
            panel5.Show();
            GetReport ast = new GetReport();
            ast.TopLevel = false;
            panel5.Controls.Add(ast);
            ast.Show();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 atf = new Form1();
            atf.Closed += (s, args) => this.Close();
            atf.Show();
        }

        private void FacultyDashboard_Load(object sender, EventArgs e)
        {
            //pictureBox1.Image = facultyImage;
            label7.Text = Form1.fid;
            //label7.Text = "12132103018";
            ShowFacultyInfo(label7.Text);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public string sourcePath = "", destPath = "", tdestPath = "";
        public int flag = 0;
        public int del = 0;
        public string delString = "";
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            flag = 1;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpg files (*.jpg)|*.jpg| All files (*.*)|*.*";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dialog.CheckFileExists)
                {

                    int len = Application.StartupPath.Length - 10;
                    string imageName = label7.Text + ".jpg";
                    string path = Application.StartupPath.Substring(0, len) + "\\Images\\" + imageName;
                    destPath = path;
                    sourcePath = dialog.FileName;
                    // pictureBox4.Image = Image.FromFile(sourcePath);

                    Image image;
                    using (Stream stream = File.OpenRead(sourcePath))
                    {
                        image = System.Drawing.Image.FromStream(stream);
                        pictureBox4.Image = image;
                        // pictureBox1.Image = image;
                    }

                }
            }

        }

        public void ShowFacultyInfo(string fid)
        {
            Faculty faculty2 = new Faculty();
            faculty2 = fpg.GetFacultyInformation(fid);

            bunifuMaterialTextbox2.Text = faculty2.name;
            bunifuMaterialTextbox3.Text = faculty2.shortName;
            bunifuMaterialTextbox4.Text = faculty2.password;
            bunifuMaterialTextbox5.Text = faculty2.username;
            label11.Text = faculty2.name;

            // Image image = Image.FromFile(faculty2.path);

            tdestPath = faculty2.path;

            Image image;
            using (Stream stream = File.OpenRead(faculty2.path))
            {
                image = System.Drawing.Image.FromStream(stream);
                pictureBox4.Image = image;
                pictureBox1.Image = image;
            }




        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            faculty.id = label7.Text;
            faculty.name = bunifuMaterialTextbox2.Text;
            faculty.shortName = bunifuMaterialTextbox3.Text;
            faculty.username = bunifuMaterialTextbox5.Text;
            faculty.password = bunifuMaterialTextbox4.Text;

            if (flag == 1) faculty.path = destPath;
            if (flag == 0)
            {
                Faculty faculty2 = new Faculty();
                faculty2 = fpg.GetFacultyInformation(label7.Text);
                faculty.path = faculty2.path;
                destPath = faculty2.path;
            }
            if(del == 1)
            {
                faculty.path = delString;
            }


            bool isUpdate = fpg.Update(faculty);
            if (isUpdate) MessageBox.Show("Updated Successfully");
            else MessageBox.Show("Something went wrong, Try again");


            if (flag == 1)
            {
                if (File.Exists(destPath))
                {
                    File.Delete(destPath);
                }
                System.IO.File.Copy(sourcePath, destPath);
            }


            if(del==1)
            {
                Image image;
                using (Stream stream = File.OpenRead(delString))
                {
                    image = System.Drawing.Image.FromStream(stream);
                    // pictureBox4.Image = image;
                    pictureBox1.Image = image;
                }
                del = 0;
            }
            else
            {
                Image image;
                using (Stream stream = File.OpenRead(destPath))
                {
                    image = System.Drawing.Image.FromStream(stream);
                    // pictureBox4.Image = image;
                    pictureBox1.Image = image;
                }
            }
           

            //ShowFacultyInfo(label7.Text);

            

         
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            int len = Application.StartupPath.Length - 10;
            string path = Application.StartupPath.Substring(0, len) + "\\Images\\avatar.png";

            string imagename = label7.Text + ".jpg";
            string Path2 = Application.StartupPath.Substring(0, len) + "\\Images\\" + imagename;

           

            if (File.Exists(Path2))
            {
                File.Delete(Path2);
            }

            
            Image image;
            using (Stream stream = File.OpenRead(path))
            {
                image = System.Drawing.Image.FromStream(stream);
                pictureBox4.Image = image;
                //pictureBox1.Image = image;
            }

            del = 1;
            delString = path;
        }
    }
}
