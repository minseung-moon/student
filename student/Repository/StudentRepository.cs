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
            string sql = "EXEC sp_Student_i1 '" + model._Name + "'," + model._Kor + "," + model._Eng + "," + model._Math + " ";
            cmd.CommandText = sql;
            conn.Open();
            cmd.ExecuteNonQuery();

            // Db 닫기
            conn.Close();
        }

        public void DeleteByIdx(int index)
        {
            string sql = "EXEC sp_Student_d1 " + index + "";
            cmd.CommandText = sql;
            conn.Open();
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        // 데이터 조회
        public List<StudentModel> GetAll()
        {
            studentList = new List<StudentModel>();

            string sql = "EXEC sp_Student_s1";
            cmd.CommandText = sql;
            conn.Open();

            // 데이터 출력
            using (SqlDataReader SR = cmd.ExecuteReader())
            {
                while (SR.Read())
                {
                    StudentModel model = new StudentModel { _Idx = (int)SR[0], _Name = (string)SR[1], _Kor = (int)SR[2], _Eng = (int)SR[3], _Math = (int)SR[4], _Total = (int)SR[5], _Average = Convert.ToDouble(SR[6]), _Rank = (int)SR[7], _Result = (string)SR[8] };
                    // model._Result = (model._Average < 70.0 || model._Kor < 40 || model._Eng < 40 || model._Math < 40) ? "불합격" : "합격";
                    studentList.Add(model);
                }
                SR.Close();
            }
            conn.Close();

            return studentList;
        }

        public void UpdateByIdx(StudentModel model)
        {
            string sql = "EXEC sp_Student_u1 '" + model._Name + "', " + model._Kor + ",  " + model._Eng + ", " + model._Math + ", " + model._Idx + "";
            cmd.CommandText = sql;
            conn.Open();
            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
