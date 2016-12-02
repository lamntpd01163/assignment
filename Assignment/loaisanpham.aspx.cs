using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Assignment
{
    public partial class loaisanpham : System.Web.UI.Page
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
                DataTable temp = ProcessDatabase.getData("SELECT * FROM LoaiSanPham");
                grvloaisanpham.DataSource = temp;
                grvloaisanpham.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void grvloaisanpham_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0) return;
            if (grvloaisanpham.EditIndex != e.Row.RowIndex)
            {
                LinkButton del = (LinkButton)e.Row.Cells[0].Controls[2];
                del.Attributes.Add("onclick", "return confirm('Dou you want delete this record?')");
            }
        }

        protected void grvloaisanpham_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ma = grvloaisanpham.Rows[e.RowIndex].Cells[1].Text;
            string tenloai = ((TextBox)grvloaisanpham.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string motaloai = ((TextBox)grvloaisanpham.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            ProcessDatabase.UpdateData("update LoaiSanPham set TenLoaiSp=N'" + tenloai + "', MotaLoaiSp=N'" + motaloai + "' where MaLoaiSp='" + ma + "'");
            grvloaisanpham.EditIndex = -1;
            loadKH();
        }

        protected void grvloaisanpham_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvloaisanpham.EditIndex = e.NewEditIndex;
            loadKH();
        }

        protected void grvloaisanpham_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ma = grvloaisanpham.Rows[e.RowIndex].Cells[1].Text;
            ProcessDatabase.deleteData("DELETE FROM LoaiSanPham WHERE MaLoaiSp='" + ma + "'");
            loadKH();
        }

        protected void grvloaisanpham_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvloaisanpham.EditIndex = -1;
            loadKH();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (maloaisanpham.Text == "" || tenloaisanpham.Text == "" || motaloaisanpham.Text == "" )
            {

            }
            else
            {
                ProcessDatabase.addData("INSERT INTO LoaiSanPham (MaLoaiSp, TenLoaiSp, MotaLoaiSp) VALUES ('" + maloaisanpham.Text + "',N'" + tenloaisanpham.Text + "',N'"+ motaloaisanpham.Text + "') ");
                loadKH();
                motaloaisanpham.Text = "";
                tenloaisanpham.Text = "";
                maloaisanpham.Text = "";
                
            }
        }
    }
}