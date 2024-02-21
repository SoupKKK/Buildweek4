using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

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
            List<int> productIds = (List<int>)Session["ProductID"];
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("nome", typeof(string));
            dt.Columns.Add("prezzo", typeof(double));
            dt.Columns.Add("quantita", typeof(int));
            dt.Columns.Add("immagine", typeof(string));


            // Imposta la chiave primaria sulla colonna "Id"
            dt.PrimaryKey = new DataColumn[] { dt.Columns["Id"] };

            if(productIds != null)
            {
                foreach (int productId in productIds)
                {
                    try
                    {
                        DBConn.conn.Open();
                        SqlCommand cmd = new SqlCommand($"SELECT * FROM Prodotti WHERE ID='{productId}'", DBConn.conn);
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                            DataRow existingRow = dt.Rows.Find(productId);

                            if (existingRow != null)
                            {
                                // Articolo già presente nel carrello, incrementa la quantità
                                existingRow["quantita"] = (int)existingRow["quantita"] + 1;
                            }
                            else
                            {
                                // Aggiungi un nuovo record al DataTable
                                dt.Rows.Add(dataReader["Id"], dataReader["nome"], dataReader["prezzo"], 1, dataReader["immagine"]); // Imposta la quantità a 1
                            }
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
            else
            {
                cartContainer.Visible = false;
                divMessage.Visible = true;
                divMessage.InnerHtml = "<div class=\"text-center alert alert-warning\"> <h1>CARRELLO VUOTO</h1>";
                  
            }


            rptCartItems.DataSource = dt;
            rptCartItems.DataBind();

            double totalCartAmount = GetTotalCartAmount(productIds);
            contentTot.InnerHtml = $"<h2>Totale: {totalCartAmount}€</h2>";
        }


        private double GetTotalCartAmount(List<int> productIds)
        {
            double totalAmount = 0;

            if (productIds != null)
            {
                foreach (int productId in productIds)
                {
                    try
                    {
                        DBConn.conn.Open();
                        SqlCommand cmd = new SqlCommand($"SELECT Prezzo FROM Prodotti WHERE ID='{productId}'", DBConn.conn);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            totalAmount += Convert.ToDouble(result);
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

            return totalAmount;
        }

        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                List<int> productIds = (List<int>)Session["ProductID"];

                if (productIds != null)
                {
                    // Rimuovi l'articolo dal carrello
                    productIds.Remove(productId);
                    Session["ProductID"] = productIds;

                    // Ricarica gli elementi del carrello dopo l'eliminazione
                    LoadCartItems();
                }
            }
        }
    }
}
