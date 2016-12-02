using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Assignment
{
    public partial class hoadon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadHD();
            }
        }

        public void LoadHD()
        {
            try
            {
                DataTable temp = ProcessDatabase.getData("SELECT * FROM HoaDon");
                grvhoadon.DataSource = temp;
                grvhoadon.DataBind();
            }catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (mahoadon.Text == "" || makhachhanglist.Text == "" || ngaydathang.Text == "" || masothue.Text == "" || ngaygiaohang.Text == "" || noigiaohang.Text == "")
            {

            }
            else
            {
                DateTime ngaydh1 = DateTime.Parse(ngaydathang.Text);
                DateTime ngaygh1 = DateTime.Parse(ngaygiaohang.Text);
                ProcessDatabase.addData("INSERT INTO HoaDon (MaHD, CustomerID, NgayDatHang, MaSoThue, NgayGiaoHang, NoiGiaoHang) VALUES ('" + mahoadon.Text + "','" + makhachhanglist.Text + "','" + ngaydh1 + "','" + masothue.Text + "','" + ngaygh1 + "',N'"+noigiaohang.Text+"') ");
                LoadHD();
                mahoadon.Text = "";
                ngaydathang.Text = "";
                masothue.Text = "";
                ngaygiaohang.Text = "";
                noigiaohang.Text = "";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0) return;
            if (grvhoadon.EditIndex != e.Row.RowIndex)
            {
                LinkButton del = (LinkButton)e.Row.Cells[0].Controls[2];
                del.Attributes.Add("onclick", "return confirm('Dou you want delete this record?')");
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ma = grvhoadon.Rows[e.RowIndex].Cells[1].Text;
            string makh = ((TextBox)grvhoadon.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string ndh = ((TextBox)grvhoadon.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string mst = ((TextBox)grvhoadon.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string ngh = ((TextBox)grvhoadon.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string noigh = ((TextBox)grvhoadon.Rows[e.RowIndex].Cells[6].Controls[0]).Text;
            ProcessDatabase.UpdateData("update HoaDon set CustomerID='" + makh + "', NgayDatHang='" + ndh + "', MaSoThue='" + mst + "', NgayGiaoHang='" + ngh + "', NoiGiaoHang=N'"+noigh+"'  where MaHD='" + ma + "'");

            grvhoadon.EditIndex = -1;
            LoadHD();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvhoadon.EditIndex = e.NewEditIndex;
            LoadHD();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ma = grvhoadon.Rows[e.RowIndex].Cells[1].Text;
            ProcessDatabase.deleteData("DELETE FROM HoaDon WHERE MaHD='" + ma + "'");
            LoadHD();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvhoadon.EditIndex = -1;
            LoadHD();
        }
    }
}