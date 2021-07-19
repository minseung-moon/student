using student.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student.Repository
{
    interface IStudentRepository
    {
        void Add(StudentModel model); // 입력
        List<StudentModel> GetAll(); // 전체 출력
        void DeleteByIdx(int index);  // 특정 학생 삭제
        void UpdateByIdx(StudentModel model); // 특정 학생 업데이트
    }
}
