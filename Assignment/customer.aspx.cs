using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Assignment
{
    public partial class customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadKH();
            }
        }

        private void loadKH()
        {
            try
            {
                DataTable temp = ProcessDatabase.getData("SELECT * FROM KhachHang");
                grvkhachhang.DataSource = temp;
                grvkhachhang.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (makhachhang.Text == "" || tenkhachhang.Text == "" || sodienthoai.Text == "" || address.Text == "" || birthofyear.Text == "")
            {

            }
            else
            {
                DateTime ngaysinh1 = DateTime.Parse(birthofyear.Text);
                ProcessDatabase.addData("INSERT INTO KhachHang (CustomerID, Ten, DienThoai, DiaChi, NgaySinh) VALUES ('" + makhachhang.Text + "',N'" + tenkhachhang.Text + "','" + sodienthoai.Text + "',N'" + address.Text + "','" + ngaysinh1 + "') ");
                loadKH();
                makhachhang.Text = "";
                tenkhachhang.Text = "";
                sodienthoai.Text = "";
                address.Text = "";
                birthofyear.Text = "";
            }
        }

        protected void grvkhachhang_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ma = grvkhachhang.Rows[e.RowIndex].Cells[1].Text;
            ProcessDatabase.deleteData("DELETE FROM KhachHang WHERE CustomerID='" + ma + "'");
            loadKH();
        }

        protected void grvkhachhang_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvkhachhang.EditIndex = e.NewEditIndex;
            loadKH();
        }

        protected void grvkhachhang_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ma = grvkhachhang.Rows[e.RowIndex].Cells[1].Text;
            string ten = ((TextBox)grvkhachhang.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string phone = ((TextBox)grvkhachhang.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string address = ((TextBox)grvkhachhang.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string ngaysinh = ((TextBox)grvkhachhang.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            ProcessDatabase.UpdateData("update KhachHang set Ten=N'"+ten+"', DienThoai='"+phone+"', DiaChi=N'"+address+ "', NgaySinh='" +ngaysinh+"'  where CustomerID='"+ma+"'");

            grvkhachhang.EditIndex = -1;
            loadKH();
        }

        protected void grvkhachhang_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvkhachhang.EditIndex = -1;
            loadKH();
        }

        protected void grvkhachhang_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0) return;
            if(grvkhachhang.EditIndex != e.Row.RowIndex)
            {
                LinkButton del = (LinkButton)e.Row.Cells[0].Controls[2];
                del.Attributes.Add("onclick", "return confirm('Dou you want delete this record?')");
            }
        }
    }
}