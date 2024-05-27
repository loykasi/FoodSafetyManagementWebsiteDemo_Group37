using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;

namespace WebAnToanVeSinhThucPhamDemo.Controllers
{
    //Class dùng để chạy mấy cái procedure trên SQLServer
    public class DataContext
    {
        private SqlConnection _connection;

        public DataContext()
        {
            _connection = new SqlConnection("Data Source=.;Initial Catalog=QLATTP;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            // _connection = new SqlConnection("Data Source=SQL6032.site4now.net;Initial Catalog=db_aa8cda_quanlyattp;User Id=db_aa8cda_quanlyattp_admin;Password=SmarterASP!1");
        }

        public int insertGiayChungNhan_CoSo(string idchucoso, string tencoso, int phuongxa, string diachi, string loaihinh, string giayphepkd, DateOnly ngaycap, string loaithucpham, string hinhanh)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = " execute insertGiayChungNhan_CoSo @idchucoso,@ten,@idphuongxa,@diachi,@loaihinh,@giayphep,@ngaycap,@loaithucpham,@hinhanh"; //null la IdChuCoSo

            cmd.Parameters.Add("@idchucoso", SqlDbType.NVarChar);
            cmd.Parameters["@idchucoso"].Value = idchucoso;
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar);
            cmd.Parameters["@ten"].Value = tencoso;
            cmd.Parameters.Add("@idphuongxa", SqlDbType.Int);
            cmd.Parameters["@idphuongxa"].Value = phuongxa;
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar);
            cmd.Parameters["@diachi"].Value = diachi;
            cmd.Parameters.Add("@loaihinh", SqlDbType.NVarChar);
            cmd.Parameters["@loaihinh"].Value = loaihinh;
            cmd.Parameters.Add("@giayphep", SqlDbType.VarChar);
            cmd.Parameters["@giayphep"].Value = giayphepkd;
            cmd.Parameters.Add("@ngaycap", SqlDbType.Date);
            cmd.Parameters["@ngaycap"].Value = ngaycap;
            cmd.Parameters.Add("@loaithucpham", SqlDbType.NVarChar);
            cmd.Parameters["@loaithucpham"].Value = loaithucpham;
            cmd.Parameters.Add("@hinhanh", SqlDbType.VarChar);
            cmd.Parameters["@hinhanh"].Value = hinhanh;
            _connection.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            int result = Convert.ToInt32(dataReader.GetDecimal(0));
            _connection.Close();
            return result;
        }
        public int insertGiayChungNhan(int idCoSo, string loaiThucPham, string hinhAnh)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "execute insertGiayChungNhan @idCoSo,@loaiThucPham,@hinhAnh";

            cmd.Parameters.Add("@idCoSo", SqlDbType.Int);
            cmd.Parameters["@idCoSo"].Value = idCoSo;
            cmd.Parameters.Add("@loaithucpham", SqlDbType.NVarChar);
            cmd.Parameters["@loaithucpham"].Value = loaiThucPham;
            cmd.Parameters.Add("@hinhanh", SqlDbType.VarChar);
            cmd.Parameters["@hinhanh"].Value = hinhAnh;

            _connection.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            int result = Convert.ToInt32(dataReader.GetDecimal(0));
            _connection.Close();
            return result;
        }
    }
}
