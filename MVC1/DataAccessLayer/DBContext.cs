using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC1.DataAccessLayer
{
    public class DBContext
    {
        private static SqlConnection GetSqlConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudentDBConnection"].ToString();

            return new SqlConnection(connectionString);

        }

        public static IEnumerable<Models.Student> SelectAllStudents()
        {
            IEnumerable<Models.Student> studentList = new List<Models.Student>();
  

            using (SqlConnection cnn = GetSqlConnection())
            {
                String sql = $"Select * From Student";

                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        Models.Student student;
                        while (dataReader.Read())
                        {
                            student = new Models.Student();
                            student.StudentId = (int)dataReader.GetValue(0);
                            student.StudentName = (string)dataReader.GetValue(1);
                            student.Age = (int)dataReader.GetValue(2);

                            //(studentList.ToList<Models.Student>()).Add(student);
                            ((List<Models.Student>)studentList).Add(student);
                        }
                    }
                }
            }

            return studentList;
        }

        public static int CreateStudent(Models.Student student)
        {

            int result;

            using (SqlConnection cnn = GetSqlConnection())
            {
                String sql = $"Insert into Student(StudentName,Age) values ('{student.StudentName}',{student.Age})";

                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = sql;
                    result = command.ExecuteNonQuery();
                }
            }

            return result;

        }

        public static Models.Student SelectStudentById(int? studentId)
        {
            Models.Student studentInfo = new Models.Student();

            using (SqlConnection cnn = GetSqlConnection())
            {
                String sql = $"Select * From Student Where Id = {studentId}";

                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            studentInfo.StudentId = (int)dataReader.GetValue(0);
                            studentInfo.StudentName = (string)dataReader.GetValue(1);
                            studentInfo.Age = (int)dataReader.GetValue(2);
                           
                        }
                    }
                }
            }

            return studentInfo;
        }

        public static int UpdateStudentById(int? studentId, Models.Student student)
        {
            int result;

            using (SqlConnection cnn = GetSqlConnection())
            {
                String sql = $"Update Student Set StudentName='{student.StudentName}',Age={student.Age} Where Id = {studentId}";

                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = sql;
                    result = command.ExecuteNonQuery();
                }
            }

            return result;
        }

        public static int DeleteStudentById(int? studentId)
        {
            int result;

            using (SqlConnection cnn = GetSqlConnection())
            {
                String sql = $"Delete from Student Where Id = {studentId}";

                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = sql;
                    result = command.ExecuteNonQuery();
                }
            }

            return result;
        }
    }
}