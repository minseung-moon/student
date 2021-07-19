using static student.Conn.DBConn;
using student.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using student.Repository;

namespace student
{
    
    public partial class Form1 : Form
    {
        private IStudentRepository repository = null;
        private List<StudentModel> studentList;
        private int index = -1;
        string name = null;
        string kor = null;
        string eng = null;
        string math = null;

        // 접속
        public Form1()
        {
            InitializeComponent();

            repository = new StudentRepository();
        }

        // form 로드 이벤트
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowList();
        }

        // 추가 이벤트
        private void button1_Click(object sender, EventArgs e)
        {
            // text box에 데이터 확인
            if(TextBoxValidate())
            {
                MessageBox.Show("데이터를 입력해주세요");
                return;
            }

            repository.Add(new StudentModel { _Idx = index, _Name = name, _Kor = Convert.ToInt32(kor), _Eng = Convert.ToInt32(eng), _Math = Convert.ToInt32(math) });

            // 초기화
            InitTextBox();
            ShowList();
        }

        // 삭제 이벤트
        private void Delete_Click(object sender, EventArgs e)
        {
            // 그리드 뷰 컬럼이 선택이 되었는지 확인
            if (index > -1)
            {
                repository.DeleteByIdx(index);

                InitTextBox();
                ShowList();
            }else MessageBox.Show("삭제할 데이터를 입력해주세요.");
            
        }
        
        // 업데이트 이벤트
        private void Update_Click(object sender, EventArgs e)
        {
            // 그리드 뷰 컬럼이 선택이 되었는지 확인
            if (index > -1)
            {
                // text box 데이터 확인
                if (TextBoxValidate())
                {
                    MessageBox.Show("데이터를 입력해주세요");
                    return;
                }

                repository.UpdateByIdx(new StudentModel { _Idx = index, _Name = name, _Kor = Convert.ToInt32(kor), _Eng = Convert.ToInt32(eng), _Math = Convert.ToInt32(math) });

                InitTextBox();
                ShowList();
            }
            else MessageBox.Show("수정할 데이터를 입력해주세요.");
        }

        // 그리드 뷰 클릭 이벤트
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) => SelectRows();

        // 선택된 컬럼의 데이터 할당
        private void SelectRows()
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                index = (int)dr.Cells[0].Value;
                tbname.Text = dr.Cells[1].Value.ToString();
                tbkor.Text = dr.Cells[2].Value.ToString();
                tbeng.Text = dr.Cells[3].Value.ToString();
                tbmath.Text = dr.Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 텍스트 박스 초기화
        private void InitTextBox()
        {
            tbname.Text = "";
            tbkor.Text = "";
            tbeng.Text = "";
            tbmath.Text = "";
            index = -1;
        }

        // 텍스트박스에 값이 있는지 체크
        private bool TextBoxValidate()
        {
            name = tbname.Text;
            kor = tbkor.Text;
            eng = tbeng.Text;
            math = tbmath.Text;

            if (name == null || name == "" ||
               kor == null || kor == "" ||
               eng == null || eng == "" ||
               math == null || math == "")
                return true;
            else return false;
        }

        // 그리드 뷰에 데이터 출력
        private void ShowList()
        {
            // List 초기화
            studentList = new List<StudentModel>();

            studentList = repository.GetAll();
            dataGridView1.DataSource = studentList;
            dataGridView1.Columns[0].HeaderText = "학생 번호";
            dataGridView1.Columns[1].HeaderText = "학생 성명";
            dataGridView1.Columns[2].HeaderText = "국어";
            dataGridView1.Columns[3].HeaderText = "영어";
            dataGridView1.Columns[4].HeaderText = "수학";
            dataGridView1.Columns[5].HeaderText = "총합";
            dataGridView1.Columns[6].HeaderText = "평균";
            dataGridView1.Columns[7].HeaderText = "등수";
        }

    }
}
