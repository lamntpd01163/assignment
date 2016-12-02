using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Assignment
{
    public partial class hoadonchitiet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadHDCT();
            }
        }

        public void loadHDCT()
        {
            try
            {
                DataTable temp = ProcessDatabase.getData("SELECT * FROM ChiTietHoaDon");
                grvhoadonchitiet.DataSource = temp;
                grvhoadonchitiet.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (mahoadonlist.Text == "" || masanphamlist.Text == "" || soluong.Text == "" || tongtien.Text == "" || giamgia.Text == "")
            {

            }
            else
            {
                ProcessDatabase.addData("INSERT INTO ChiTietHoaDon (MaHD, MaSp, SoLuong, ThanhTien, MucGiamGia) VALUES ('" + mahoadonlist.Text + "','" + masanphamlist.Text + "','" + soluong.Text + "','" + tongtien.Text + "'," + giamgia.Text + ") ");
                loadHDCT();
                soluong.Text = "";
                tongtien.Text = "";
                giamgia.Text = "";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0) return;
            if (grvhoadonchitiet.EditIndex != e.Row.RowIndex)
            {
                LinkButton del = (LinkButton)e.Row.Cells[0].Controls[2];
                del.Attributes.Add("onclick", "return confirm('Dou you want delete this record?')");
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ma = grvhoadonchitiet.Rows[e.RowIndex].Cells[1].Text;
            string sl = ((TextBox)grvhoadonchitiet.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string tt = ((TextBox)grvhoadonchitiet.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string gg = ((TextBox)grvhoadonchitiet.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            ProcessDatabase.UpdateData("update ChiTietHoaDon set SoLuong='" + sl + "', ThanhTien='" + tt + "', MucGiamGia='"+gg+"'  where MaHD='" + ma + "'");

            grvhoadonchitiet.EditIndex = -1;
            loadHDCT();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvhoadonchitiet.EditIndex = e.NewEditIndex;
            loadHDCT();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ma = grvhoadonchitiet.Rows[e.RowIndex].Cells[1].Text;
            ProcessDatabase.deleteData("DELETE FROM ChiTietHoaDon WHERE MaHD='" + ma + "'");
            loadHDCT();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvhoadonchitiet.EditIndex = -1;
            loadHDCT();
        }
    }
}