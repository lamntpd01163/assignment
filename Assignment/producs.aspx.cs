using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Assignment
{
    public partial class producs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSP();
            }
        }

        public void LoadSP()
        {
            try
            {
                DataTable tem = ProcessDatabase.getData("SELECT * FROM SanPham");
                grvsanpham.DataSource = tem;
                grvsanpham.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (masanpham.Text == "" || maloaisanpham.Text == "" || tensanpham.Text == "" || giasanpham.Text == "" || motasanpham.Text == "" || manhasanxuat.Text == "" || xuatsusanpham.Text == "") 
            {

            }
            else
            {
                ProcessDatabase.addData("INSERT INTO SanPham (MaSp, MaNhaSX, MaLoaiSp, TenSp, GiaSp, MotaSp, XuatXuSp) VALUES ('" + masanpham.Text + "','" + manhasanxuat.Text + "','" + maloaisanpham.Text + "',N'" + tensanpham.Text + "'," + giasanpham.Text + ",N'"+motasanpham.Text+"',N'"+xuatsusanpham.Text+"') ");
                LoadSP();
                masanpham.Text = "";
                manhasanxuat.Text = "";
                tensanpham.Text = "";
                giasanpham.Text = "";
                motasanpham.Text = "";
                xuatsusanpham.Text = "";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0) return;
            if (grvsanpham.EditIndex != e.Row.RowIndex)
            {
                LinkButton del = (LinkButton)e.Row.Cells[0].Controls[2];
                del.Attributes.Add("onclick", "return confirm('Dou you want delete this record?')");
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ma = grvsanpham.Rows[e.RowIndex].Cells[1].Text;
            string manhsx = ((TextBox)grvsanpham.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string maloaisanpaham1 = ((TextBox)grvsanpham.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string tensanpham1 = ((TextBox)grvsanpham.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string giasanpham1 = ((TextBox)grvsanpham.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string motasanpham1 = ((TextBox)grvsanpham.Rows[e.RowIndex].Cells[6].Controls[0]).Text;
            string xuatxusanpham1 = ((TextBox)grvsanpham.Rows[e.RowIndex].Cells[7].Controls[0]).Text;
            ProcessDatabase.UpdateData("update SanPham set MaNhaSX=N'" + manhsx + "', MaLoaiSp='" + maloaisanpaham1 + "', TenSp=N'" + tensanpham1 + "', GiaSp='" + giasanpham1 + "', MotaSp=N'"+motasanpham1+"', XuatXuSp=N'"+xuatxusanpham1+"'  where MaSp='" + ma + "'");

            grvsanpham.EditIndex = -1;
            LoadSP();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvsanpham.EditIndex = e.NewEditIndex;
            LoadSP();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ma = grvsanpham.Rows[e.RowIndex].Cells[1].Text;
            ProcessDatabase.deleteData("DELETE FROM SanPham WHERE MaSp='" + ma + "'");
            LoadSP();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvsanpham.EditIndex = -1;
            LoadSP();
        }
    }
}