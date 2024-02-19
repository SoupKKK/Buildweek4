using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Buildweek4
{
    public partial class Home : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DBConn.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prodotti", DBConn.conn);
                SqlDataReader dataReader = cmd.ExecuteReader();

                string content = "";
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        content += $@"<div class=""card col-6 col-sm-4 col-md-3 m-3"">
                                <img src=""{dataReader["immagine"]}"" class=""card-img-top m-auto imgArt "" alt=""{dataReader["nome"]}"">
                                <div class=""card-body"">
                                <h5 class=""card-title nomeArt"">{dataReader["nome"]}</h5>
                                <p class=""card-text priceArt"">{dataReader["prezzo"]}€</p>
                                <a href=""ProductDetails.aspx?product={dataReader["Id"]}"" class=""btn btn-success"">Dettaglio</a>
                                </div>
                               </div>";
                    }
                }

                productContainer.InnerHtml = content;

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                if (DBConn.conn.State == ConnectionState.Open)
                {
                    DBConn.conn.Close();
                }
            }
        }

    }
}