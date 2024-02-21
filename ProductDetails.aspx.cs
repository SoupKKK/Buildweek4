using Buildweek4;
using BuildWeek4;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuildWeek4
{
    
    public partial class ProductDetails : System.Web.UI.Page
    {

    private string ProductID;

     
        protected void Page_Load(object sender, EventArgs e)
        {
            string loggedInUser = Session["Username"] as string;


            if (!string.IsNullOrEmpty(loggedInUser) && !loggedInUser.Equals(Admin.UserName, StringComparison.OrdinalIgnoreCase))
            {
                btnAddCart.Visible = true;
            }
            else
            {
                ProductID = Request.QueryString["product"].ToString();
                ProductDetailsBtns.InnerHtml = "<a href=\"EditProduct.aspx?product=" + ProductID + "\" class=\"btn btn-warning me-2\">Modifica</a>";
            }



            if (Request.QueryString["product"] == null)
            {
                Response.Redirect("Home.aspx");
            }
            ProductID = Request.QueryString["product"].ToString();

            try
            {
                DBConn.conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Prodotti WHERE ID={ProductID}", DBConn.conn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    ttlProduct.InnerText = dataReader["nome"].ToString();
                    img.Src = dataReader["immagine"].ToString();
                    txtDescription.InnerText = dataReader["descrizione"].ToString();
                    txtPrice.InnerText = dataReader["prezzo"].ToString() + " €";


                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                if (DBConn.conn.State == System.Data.ConnectionState.Open)
                {
                    DBConn.conn.Close();
                }
            }


        }

        protected void btnAddCart_Click(object sender, EventArgs e)
        {
            int prodID = int.Parse(ProductID);
            List<int> products;
            if (Session["ProductID"] == null)
            {
                products = new List<int>();
            }
            else
            {
                products = (List<int>)Session["ProductID"];
            }

            products.Add(prodID);

            Session["ProductID"] = products;

        }
    }
}