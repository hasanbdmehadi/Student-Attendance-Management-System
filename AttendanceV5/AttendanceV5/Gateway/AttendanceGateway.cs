using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AttendanceV5.UI;

namespace AttendanceV5.Gateway
{
    class AttendanceGateway
    {
        //string cs = @"Data Source=MEHADI-PC\SQLEXPRESS;Initial Catalog=AttendanceV4;Integrated Security=True";
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        
        
        



        public List<string> GetCourseCodes(string sem)
        {
            List<string> list = new List<string>();

            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT CourseCode FROM Course";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                string s = "";
                while (reader.Read())
                {
                    s = reader["CourseCode"].ToString();
                    list.Add(s);
                }
            }

            return list;
        }

        public string GetCourseName(string cCode)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT CourseName FROM Course WHERE CourseCode = '" + cCode + "' ";

            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    s = reader["CourseName"].ToString();
                }
            }
            return s;
        }

        public string GetCourseCredit(string cCode)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT CourseCredit FROM Course WHERE CourseCode = '" + cCode + "' ";

            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    s = reader["CourseCredit"].ToString();
                }
            }
            return s;
        }

        public string GetNumberById(List<string> id)
        {
            string s = "";
            foreach(string ids in id)
            {
                SqlConnection con = new SqlConnection(cs);
                string q = "SELECT GuardianPhone FROM Student WHERE StudentId = '" +ids +"' ";

                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        s += reader["GuardianPhone"].ToString();
                        s += ",";
                    }
                }
            }
            
            return s;
        }




        //-----------Insert Function- busy--------------//

        public bool CheckHasDate(Attendance at)
        {
            string s = at.dt;
            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Attendances where";
            q += " Date = '" + at.dt + "' AND StudentId = " + at.StudentId + " AND StudentName = '" + at.StudentName + "' AND Program = '" + at.Program + "' AND Department = '" + at.Department + "' AND Semester = '" + at.Semester + "' AND CourseCode = '" + at.CourseCode + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

        public bool Insert(Attendance at)
        {
            bool chk = CheckHasDate(at);
            bool flag = false;
            if (chk)
            {
                flag = false;
            }
            else
            {

                //Faculty Table insertion
                SqlConnection con = new SqlConnection(cs);
                string q = "insert into Attendances (Program, Department, Semester, CourseCode, StudentId, StudentName, PorA, Date) values ('" + at.Program + "','" + at.Department + "','" + at.Semester + "','" + at.CourseCode + "'," + at.StudentId + ",'" + at.StudentName + "','" + at.PorA + "','" + at.dt + "')";
                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                int flag1 = cmd.ExecuteNonQuery();

                if (flag1 > 0) flag = true;
                else flag = false;



            }

            return flag;

        }

        
    }
}
