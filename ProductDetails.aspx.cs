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


            if (!string.IsNullOrEmpty(loggedInUser) && loggedInUser.Equals(Admin.UserName, StringComparison.OrdinalIgnoreCase))
            {
                // L'utente loggato è l'amministratore
                btnAddCart.Visible = false;
                ProductID = Request.QueryString["product"].ToString();
                ProductDetailsBtns.InnerHtml = "<a href=\"EditProduct.aspx?product=" + ProductID + "\" class=\"btn btn-warning me-2\">Modifica</a>";
            }
            else if (!string.IsNullOrEmpty(loggedInUser))
            {
                // L'utente non è l'amministratore, ma è loggato
                btnAddCart.Visible = true;
            }
            else
            {
                // Nessun utente è loggato
                btnAddCart.Visible = false;
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
            if (Session["LoggedIn"] != null && (bool)Session["LoggedIn"])
            {
                int userId = Convert.ToInt32(Session["IdUtenti"]);
                int productId = int.Parse(ProductID);
                int quantity = 1; // Puoi impostare la quantità iniziale a 1, o come necessario

                try
                {
                    DBConn.conn.Open();
                    SqlCommand cmd = new SqlCommand("MERGE INTO Carrello AS target " +
                                                    "USING (VALUES (@UserId, @ProductId, @Quantity)) AS source (UserId, ProductId, Quantity) " +
                                                    "ON target.UserId = source.UserId AND target.ProductId = source.ProductId " +
                                                    "WHEN MATCHED THEN " +
                                                    "UPDATE SET target.Quantita = target.Quantita + source.Quantity " +
                                                    "WHEN NOT MATCHED THEN " +
                                                    "INSERT (UserId, ProductId, Quantita) VALUES (source.UserId, source.ProductId, source.Quantity);", DBConn.conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Il prodotto è stato aggiunto con successo al carrello
                    }
                    else
                    {
                        // Errore nell'aggiunta del prodotto al carrello
                    }
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
}