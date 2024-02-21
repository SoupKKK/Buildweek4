using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Buildweek4
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
                {
                    divMessage.Visible = true;
                    divMessage.InnerHtml = "<div class=\"text-center alert alert-warning\"> <h1>Effettuare il login prima di poter visualizzare il carrello</h1>" +
                        "<a href=\"Login.aspx\" class=\"btn btn-primary\" >Effettua il login</a> </div>";
                    return;
                }

                LoadCartItems();
            }
        }

        private void LoadCartItems()
        {
            if (Session["LoggedIn"] != null && (bool)Session["LoggedIn"])
            {
                int userId = Convert.ToInt32(Session["IdUtenti"]);

                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("nome", typeof(string));
                dt.Columns.Add("prezzo", typeof(double));
                dt.Columns.Add("quantita", typeof(int));
                dt.Columns.Add("immagine", typeof(string));

                // Imposta la chiave primaria sulla colonna "Id"
                dt.PrimaryKey = new DataColumn[] { dt.Columns["Id"] };

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbShopConnectionString"].ToString());

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT C.Quantita, P.* FROM Carrello C " +
                                                    $"INNER JOIN Prodotti P ON C.ProductId = P.ID " +
                                                    $"WHERE C.UserId = {userId}", conn);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int productId = Convert.ToInt32(dataReader["ID"]);
                        DataRow existingRow = dt.Rows.Find(productId);

                        if (existingRow != null)
                        {
                            // Articolo già presente nel carrello, incrementa la quantità
                            existingRow["quantita"] = (int)existingRow["quantita"] + 1;
                        }
                        else
                        {
                            // Aggiungi un nuovo record al DataTable
                            dt.Rows.Add(productId, dataReader["nome"], dataReader["prezzo"], dataReader["Quantita"], dataReader["immagine"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }

                rptCartItems.DataSource = dt;
                rptCartItems.DataBind();

                double totalCartAmount = GetTotalCartAmount(dt);
                contentTot.InnerHtml = $"<h2>Totale: {totalCartAmount}€</h2>";
            }
        }

        private double GetTotalCartAmount(DataTable dt)
        {
            double totalAmount = 0;

            foreach (DataRow row in dt.Rows)
            {
                totalAmount += Convert.ToDouble(row["prezzo"]) * Convert.ToInt32(row["quantita"]);
            }

            return totalAmount;
        }

        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(Session["IdUtenti"]);

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbShopConnectionString"].ToString());

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Carrello WHERE UserId = @UserId AND ProductId = @ProductId", conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Prodotto rimosso con successo dal carrello nel database
                        LoadCartItems();
                    }
                    else
                    {
                        // Errore nella rimozione del prodotto dal carrello nel database
                        // Puoi gestire l'errore o visualizzare un messaggio
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}
