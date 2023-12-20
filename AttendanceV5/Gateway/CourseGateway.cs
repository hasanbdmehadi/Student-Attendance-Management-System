using AttendanceV5.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceV5.Gateway
{
    class CourseGateway
    {
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public bool CheckHas(Course acourse)
        {
            string s = acourse.CourseCode;
            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Course where CourseCode = '" + s + "'";
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



        public string insert(Course acourse)
        {
            string flag = "";
            if (CheckHas(acourse))
            {
                flag = "This Course is already exist";
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string q = "insert into Course (CourseCode, CourseName, CourseCredit, Department) values ('" + acourse.CourseCode + "', '" + acourse.CourseName + "'," + acourse.CourseCredit + ",'" + acourse.Department + "')";
                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                int reader = cmd.ExecuteNonQuery();
                if (reader > 0) flag = "Course is saved";
                else flag = "Couldn't save";
            }
            return flag;
        }

        public bool update(Course acourse)
        {

            SqlConnection con = new SqlConnection(cs);
            string q = "UPDATE Course SET CourseName = '" + acourse.CourseName + "', CourseCredit = " + acourse.CourseCredit + ", Department = '" + acourse.Department + "' Where CourseCode = '" + acourse.CourseCode + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            int rowAffected = cmd.ExecuteNonQuery();

            if (rowAffected > 0) return true;
            else return false;


        }

        public bool delete(string cCode)
        {

            SqlConnection con = new SqlConnection(cs);
            string q = "DELETE FROM Course WHERE CourseCode = '" + cCode + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            if (rowAffected > 0) return true;
            else return false;

        }

        public bool clear()
        {

            SqlConnection con = new SqlConnection(cs);
            string q = "DELETE FROM Course";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            if (rowAffected > 0) return true;
            else return false;

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
    }
}
