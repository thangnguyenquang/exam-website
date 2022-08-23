using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTLThiTracNghiem
{
    public class Exam
    {
        int id;
        string tieuDe;
        string ngayTao;
        int soLuongCauHoi;
        float thoiGianLamBai;
        string loaiBaiThi;
        string maCaThi;

        public int Id { get => id; set => id = value; }
        public string TieuDe { get => tieuDe; set => tieuDe = value; }
        public string NgayTao { get => ngayTao; set => ngayTao = value; }
        public int SoLuongCauHoi { get => soLuongCauHoi; set => soLuongCauHoi = value; }
        public float ThoiGianLamBai { get => thoiGianLamBai; set => thoiGianLamBai = value; }
        public string LoaiBaiThi { get => loaiBaiThi; set => loaiBaiThi = value; }
        public string MaCaThi { get => maCaThi; set => maCaThi = value; }
    }
}