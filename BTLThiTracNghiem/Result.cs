using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTLThiTracNghiem
{
    public class Result
    {
        int id;
        int idUser;
        int idDeThi;
        string hoTen;
        string tieuDeDeThi;
        string thoiGianBatDau;
        string thoiGianKetThuc;
        string thoiGianLamBai;
        double diemSo;

        public int Id { get => id; set => id = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string ThoiGianBatDau { get => thoiGianBatDau; set => thoiGianBatDau = value; }
        public string ThoiGianKetThuc { get => thoiGianKetThuc; set => thoiGianKetThuc = value; }
        public string ThoiGianLamBai { get => thoiGianLamBai; set => thoiGianLamBai = value; }
        public double DiemSo { get => diemSo; set => diemSo = value; }
        public int IdUser { get => idUser; set => idUser = value; }
        public string TieuDeDeThi { get => tieuDeDeThi; set => tieuDeDeThi = value; }
        public int IdDeThi { get => idDeThi; set => idDeThi = value; }
    }
}