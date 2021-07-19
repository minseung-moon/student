using student.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace student.Repository
{
    class StudentRepositoryExcel : IStudentRepository
    {
        static Excel.Application excelApp = null;
        static Excel.Workbook workBook = null;
        static Excel.Worksheet workSheet = null;
        string desktopPath = null;
        string path = null;

        private IStudentRepository repository = null;
        private List<StudentModel> studentList;

        StudentRepositoryExcel()
        {
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // 바탕화면
            path = Path.Combine(desktopPath, "Excel.xlsx"); // 엑셀파일

            repository = new StudentRepository();
        }

        public void saveExcelFile()
        {
            try { 
                excelApp = new Excel.Application(); // 엑셀 어플리케이션 생성
                workBook = excelApp.Workbooks.Add(); // 워크북 추가
                workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet; // 엑셀 첫번째 워크시트 가져오기

                workSheet.Cells[1, 1] = "학생 번호";
                workSheet.Cells[1, 2] = "학생 성명";
                workSheet.Cells[1, 3] = "국어점수";
                workSheet.Cells[1, 4] = "영어점수";
                workSheet.Cells[1, 5] = "수학점수";
                workSheet.Cells[1, 6] = "총합점수";
                workSheet.Cells[1, 7] = "평균점수";
                workSheet.Cells[1, 8] = "등수";

                // 엑셀에 저장할 데이터
                studentList = repository.GetAll();

                foreach(StudentModel model in studentList)
                {
                    int index = model._Idx;
                    workSheet.Cells[1 + index, 1] = index;
                    workSheet.Cells[1 + index, 2] = model._Name;
                    workSheet.Cells[1 + index, 3] = model._Kor;
                    workSheet.Cells[1 + index, 4] = model._Eng;
                    workSheet.Cells[1 + index, 5] = model._Math;
                    workSheet.Cells[1 + index, 6] = model._Total;
                    workSheet.Cells[1 + index, 7] = model._Average;
                    workSheet.Cells[1 + index, 8] = model._Rank;
                }

                workSheet.Columns.AutoFit(); // 열 너비 자동 맞춤
                workBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookDefault); // 엑셀 파일 저장
                excelApp.Quit();
            }catch(Exception e)
            {
            }
            finally
            {
                ReleaseObject(workSheet);
                ReleaseObject(workBook);
                ReleaseObject(excelApp);
            }

        }

        private void ReleaseObject(object obj)
        { 
            try { 
                if(obj != null) 
                { 
                    Marshal.ReleaseComObject(obj); // 액셀 객체 해제 
                    obj = null;
                }
            } catch(Exception ex) 
            {
                obj = null; throw ex; 
            } finally 
            {
                GC.Collect(); // 가비지 수집 
            }
        }


        public void Add(StudentModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteByIdx(int index)
        {
            throw new NotImplementedException();
        }

        public List<StudentModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void UpdateByIdx(StudentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
