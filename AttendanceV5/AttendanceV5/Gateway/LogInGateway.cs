using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceV5.Gateway
{
    class LogInGateway
    {
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public int TestAdmin(string username, string password)
        {
            int isAdmin = 0;

            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT * FROM Admins WHERE Username = '" + username + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                string s = "";
                while (reader.Read())
                {

                    s = reader["Password"].ToString();
                }

                if (s == password)
                {
                    isAdmin = 1;
                }
                else
                {
                    isAdmin = 2;
                }
            }
            else
            {
                isAdmin = 3;
            }

            return isAdmin;
        }

        public int TestFaculty(string username, string password)
        {
            int isAdmin = 0;

            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT * FROM Faculty WHERE (ShortName = '" + username + "' OR Username = '" + username + "') And Password = '" + password + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                isAdmin = 1;
            }

            return isAdmin;
        }

        public string GetFacultyId(string username, string password)
        {
            string fid = "";

            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT Id FROM Faculty WHERE (ShortName = '" + username + "' OR Username = '"+username+"') AND Password = '"+password+"'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //string s = "";
                while (reader.Read())
                {

                    fid = reader["Id"].ToString();
                }
            }
            return fid;
                
        }

    }
}
