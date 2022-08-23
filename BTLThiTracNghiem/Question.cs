using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTLThiTracNghiem
{
    public class Question
    {
        int id;
        int id_DeThi;
        string cauHoi;
        string url_img;
        string dapAn_A;
        string dapAn_B;
        string dapAn_C;
        string dapAn_D;
        string dapAn;

        public int Id { get => id; set => id = value; }
        public int Id_DeThi { get => id_DeThi; set => id_DeThi = value; }
        public string CauHoi { get => cauHoi; set => cauHoi = value; }
        public string Url_img { get => url_img; set => url_img = value; }
        public string DapAn_A { get => dapAn_A; set => dapAn_A = value; }
        public string DapAn_B { get => dapAn_B; set => dapAn_B = value; }
        public string DapAn_C { get => dapAn_C; set => dapAn_C = value; }
        public string DapAn_D { get => dapAn_D; set => dapAn_D = value; }
        public string DapAn { get => dapAn; set => dapAn = value; }
    }
}