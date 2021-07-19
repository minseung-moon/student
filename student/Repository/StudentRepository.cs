using student.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static student.Conn.DBConn;

namespace student.Repository
{
    class StudentRepository : IStudentRepository
    {
        private static List<StudentModel> studentList = null;
        private SqlConnection conn = null;
        private SqlCommand cmd = null;

        public StudentRepository()
        {
            // DB 연동
            conn = getConn();
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        public void Add(StudentModel model)
        {
            string sql = "INSERT INTO sutdent VALUES('" + model._Name + "'," + model._Kor + "," + model._Eng + "," + model._Math + ");";
            cmd.CommandText = sql;
            conn.Open();
            cmd.ExecuteNonQuery();

            // Db 닫기
            conn.Close();
        }

        public void DeleteByIdx(int index)
        {
            string sql = "DELETE FROM sutdent WHERE idx = " + index + ";";
            cmd.CommandText = sql;
            conn.Open();
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<StudentModel> GetAll()
        {
            studentList = new List<StudentModel>();

            // string sql = "SELECT * FROM sutdent ORDER BY idx ASC";
            string sql = "SELECT *, (kor + eng + math) AS total, CONVERT(NUMERIC(12,2) , (kor + eng + math)/3.0) AS average, (SELECT COUNT(*) + 1 FROM sutdent s2 WHERE (s2.kor + s2.eng + s2.math) > (s1.kor + s1.eng + s1.math)) AS rank FROM sutdent s1 ORDER BY idx ASC;";
            cmd.CommandText = sql;
            conn.Open();

            // 데이터 출력
            using (SqlDataReader SR = cmd.ExecuteReader())
            {
                while (SR.Read())
                {
                    StudentModel model = new StudentModel { _Idx = (int)SR[0], _Name = (string)SR[1], _Kor = (int)SR[2], _Eng = (int)SR[3], _Math = (int)SR[4], _Total = (int)SR[5], _Average = Convert.ToDouble(SR[6]), _Rank = (int)SR[7] };
                    studentList.Add(model);
                }
                SR.Close();
            }
            conn.Close();

            return studentList;
        }

        public void UpdateByIdx(StudentModel model)
        {
            string sql = "UPDATE sutdent SET name = '" + model._Name + "', kor = " + model._Kor + ", eng = " + model._Eng + ", math = " + model._Math + " WHERE idx = " + model._Idx + ";";
            cmd.CommandText = sql;
            conn.Open();
            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
