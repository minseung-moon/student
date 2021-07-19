using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student.Model
{
    class StudentModel
    {
        private int idx;
        private string name;
        private int kor;
        private int eng;
        private int math;

        private int total;
        private double average;
        private int rank;

        public int _Idx { get { return idx; } set { idx = value; } }
        public string _Name { get { return name; } set { name = value; } }
        public int _Kor { get { return kor; } set { kor = value; } }
        public int _Eng { get { return eng; } set { eng = value; } }
        public int _Math { get { return math; } set { math = value;  } }
        
        public int _Total { get { return total; } set { total = value; } }
        public double _Average { get { return average; } set { average = value; } }
        public int _Rank { get { return rank; } set { rank = value; } }
    }
}