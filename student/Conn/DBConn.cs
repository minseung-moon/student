using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student.Conn
{
    class DBConn
    {
        private static string uid = ""; // DB 접속 아이디
        private static string password = ""; // DB 접속 비밀번호
        private static string database = "";  // 사용할 DB 이름
        private static string server = "";  // 서버 주소 -- sql 서버 접속시 사용한 주소
        private static SqlConnection conn = null;

        public static SqlConnection getConn()
        {
            string connStr = "SERVER = " + server
                            + ";DATABASE = " + database
                            + ";UID = " + uid
                            + "; PASSWORD = " + password
                            + ";";

            conn = new SqlConnection(connStr);

            return conn;
        }

    }
}
