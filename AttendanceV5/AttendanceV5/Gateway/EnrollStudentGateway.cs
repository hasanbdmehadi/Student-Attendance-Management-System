using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceV5.UI;

namespace AttendanceV5.Gateway
{
    class EnrollStudentGateway
    {
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        //-------------Student information Load --------------//
        public List<int> GetStudentIdList()
        {
            List<int> list = new List<int>();
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT StudentId FROM Student";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                int s;
                while (reader.Read())
                {
                    s = (int)reader["StudentId"];
                    list.Add(s);
                }
            }

            return list;
        }

        public List<string> GetCourseCodeList()
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

        public string GetStudentName(int sid)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT StudentName FROM Student WHERE StudentId = " + sid + "";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    s = reader["StudentName"].ToString();
                }
            }
            return s;
        }

        public string GetStudentDeptName(int sid)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT StudentDepartment FROM Student WHERE StudentId = " + sid + "";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    s = reader["StudentDepartment"].ToString();
                }
            }
            return s;
        }

        public string GetStudentSemester(int sid)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT StudentSemester FROM Student WHERE StudentId = " + sid + "";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    s = reader["StudentSemester"].ToString();
                }
            }
            return s;
        }

        public string GetStudentProgram(int sid)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT StudentProgram FROM Student WHERE StudentId = " + sid + "";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    s = reader["StudentProgram"].ToString();
                }
            }
            return s;
        }


        //--------Course information Load-----//
        public string GetCourseCredit(string cCode)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT CourseCredit FROM Course WHERE CourseCode = '" + cCode + "'";
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

        public string GetCourseName(string cCode)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT CourseName FROM Course WHERE CourseCode = '" + cCode + "'";
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

        public string GetCourseId(string cCode)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT CourseId FROM Course WHERE CourseCode = '" + cCode + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    s = reader["CourseId"].ToString();
                }
            }
            return s;
        }

        public List<int> GetStudentIdListFromEnrollCourseTable()
        {
            List<int> list = new List<int>();
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT DISTINCT StudentId FROM EnrollCourse";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                int s;
                while (reader.Read())
                {
                    s = (int)reader["StudentId"];
                    list.Add(s);
                }
            }

            return list;
        }

        public List<string> GetDepartmentListFromEnrollCourseTable()
        {
            List<string> list = new List<string>();
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT DISTINCT Department FROM EnrollCourse";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                string s = "";

                while (reader.Read())
                {
                    s = reader["Department"].ToString();
                    list.Add(s);
                }
            }

            return list;
        }

        public List<string> GetCourseCodeListFromEnrollCourseTable()
        {
            List<string> list = new List<string>();
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT DISTINCT CourseCode FROM EnrollCourse";
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

        //-------------For Data Insert Purpose--------------//

        public bool CheckHas(EnrollCourseC Enc)
        {
            EnrollCourseC enc = new EnrollCourseC();
            enc = Enc;

            SqlConnection con = new SqlConnection(cs);
            string q = "select * from EnrollCourse where StudentId = " + enc.StudentId + " AND CourseId = " + enc.CourseId + "";
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
        public string Insert(EnrollCourseC enc)
        {
            bool chk = CheckHas(enc);
            string flag = "";
            if (chk)
            {
                flag = "This Course is already enrolled to this Student";
            }
            else
            {
                //EnrollStudent Table insertion
                SqlConnection con = new SqlConnection(cs);
                string q = "insert into EnrollCourse (StudentId, StudentName, Department,CourseId, CourseCode, CourseName, CourseCredit, Semester, Program)";
                q += " values ('" + enc.StudentId + "','" + enc.StudentName + "','" + enc.Department + "',"+enc.CourseId+",'" + enc.CourseCode + "','" + enc.CourseName + "','" + enc.CourseCredit + "','" + enc.semester + "','" + enc.program + "')";
                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                int flag1 = cmd.ExecuteNonQuery();

                if (flag1 > 0) flag = "Course is Enrolled";
                else flag = "Problem in Enrolling";

            }

            return flag;
            
        }

        public bool Delete(int sid, int cid)
        {

            SqlConnection con = new SqlConnection(cs);
            string q = "DELETE FROM EnrollCourse WHERE StudentId = " + sid + " AND CourseID = " + cid + "";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            if (rowAffected > 0) return true;
            else return false;

        }



    }
}
