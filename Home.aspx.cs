using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.ComponentModel;

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

                string loggedInUser = Session["Username"] as string;

                string content = "<div class=\"shell\"><div class=\"container\"><div class=\"row\">";

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        content += $@"<div class=""col-6 col-sm-4 col-md-3"">
                        <div class=""wsk-cp-product"">
                            <div class=""wsk-cp-img"">
                                <img src=""{dataReader["Immagine"]}"" alt=""Product"" class=""img-responsive"" />
                            </div>
                            <div class=""wsk-cp-text"">
                                <div class=""title-product"">
                                    <h3>{dataReader["nome"]}</h3>
                                </div>
                                <div class=""category"">
                                    <a href=""ProductDetails.aspx?product={dataReader["Id"]}"">Dettagli</a>
                                </div>
    
                                <div class=""card-footer"">
                                    <div class="" text-center""><span class=""price "">{dataReader["prezzo"]}€</span></div>
                                    ";

                        // Aggiungi il pulsante "Aggiungi al carrello" solo se l'utente non è l'amministratore
                        if (string.IsNullOrEmpty(loggedInUser) || loggedInUser.ToLower() != Admin.UserName.ToLower())
                        {
                            content += $@"<asp:Button runat=""server"" ID=""btnAddCart_{dataReader["Id"]}"" Text=""Aggiungi al carrello"" CssClass=""btn btn-outline-secondary"" OnClick=""btnAddCart_Click"" />";
                        }

                        // Aggiungi il pulsante "Modifica" solo se l'utente è l'amministratore
                        if (!string.IsNullOrEmpty(loggedInUser) && loggedInUser.ToLower() == Admin.UserName.ToLower())
                        {
                            content += $@"<a href=""EditProduct.aspx?product={dataReader["Id"]}"" class=""btn btn-warning float-end my-1"">Modifica</a>";
                        }

                        content += "</div></div></div></div>";
                    }
                }


                if (!string.IsNullOrEmpty(loggedInUser) && loggedInUser.ToLower() == Admin.UserName)
                {
                    content += $@"<a href=""EditProduct.aspx"" class=""btn btn-success m-3 p-2"">AGGIUNGI ALLO SHOP </a>";
                }


                productContainer.InnerHtml = content;
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                if (DBConn.conn.State != ConnectionState.Closed)
                {
                    DBConn.conn.Close();
                }
            }

        }


    }

}
