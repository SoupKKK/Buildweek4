using System;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace Buildweek4
{
    public partial class EditProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddProductText.Visible = false;
            btnAddChanges.Visible = false;
            string loggedInUser = Session["Username"] as string;

            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(loggedInUser) && loggedInUser.ToLower() == Admin.UserName)
                {
                    if (Request.QueryString["product"] != null)
                    {
                        string productId = Request.QueryString["product"];
                        LoadProductDetails(productId);
                    }
                    else
                    {
                        // Nuovo prodotto: mostra il form vuoto
                        EditContainer.Visible = true;
                        WarningMsg.InnerHtml = "";

                        // Pulisci i campi nel form
                        lblProductId.Text = "";
                        txtNewProductName.Text = "";
                        txtNewProductPrice.Text = "";
                        txtNewProductImage.Text = "";
                        txtNewProductDescription.Text = "";

                        // Imposta i pulsanti corretti
                        AddProductText.Visible = true;
                        EditProductText.Visible = false;
                        btnSaveChanges.Visible = false;
                        btnDelete.Visible = false;
                        btnAddChanges.Visible = true;
                    }
                }
                else
                {
                    // Utente non autorizzato: nascondi il form e mostra un messaggio di avviso
                    EditContainer.Visible = false;
                    WarningMsg.InnerHtml = "<div class=\"alert alert-warning text-center\"> <h1> ERROR 401: UNAUTHORIZED <br> STAI CERCANDO DI ACCEDERE AD UNA PAGINA AMMINISTRATORE <br> </h1></div>";
                }
            }

        }

        private void LoadProductDetails(string productId)
        {
            try
            {
                DBConn.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prodotti WHERE Id = @ProductId", DBConn.conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                SqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    lblProductId.Text = dataReader["Id"].ToString();
                    txtNewProductName.Text = dataReader["nome"].ToString();
                    txtNewProductPrice.Text = dataReader["prezzo"].ToString();
                    txtNewProductImage.Text = dataReader["immagine"].ToString();
                    txtNewProductDescription.Text = dataReader["descrizione"].ToString();
                    btnSaveChanges.Visible = true;
                    btnDelete.Visible = true;
                    btnAddChanges.Visible = false;
                }
                else
                {
                    EditContainer.Visible = false;
                    WarningMsg.InnerHtml = "<div class=\"alert alert-danger text-center\"> <h1>ERROR 404 <br> NO ID FOUND</h1></div>";
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

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                DBConn.conn.Open();

                string productId = lblProductId.Text;
                string newProductName = txtNewProductName.Text;
                string newProductPrice = txtNewProductPrice.Text;
                string newProductImage = txtNewProductImage.Text;
                string newProductDescription = txtNewProductDescription.Text;

                // Controllo per il nome
                if (string.IsNullOrEmpty(newProductName))
                {
                    btnAddChanges.Visible = false;
                    WarningMsg.InnerHtml = "<div class=\"alert alert-warning text-center\"> <h1>INSERISCI UN NOME VALIDO</h1></div>";
                    return; // Interrompi l'esecuzione se il nome non è valido
                }

                // Controllo per l'URL dell'immagine
                if (!Regex.IsMatch(newProductImage, @"^(http(s)?://|www.)+[a-zA-Z0-9-_\.]+\.[a-zA-Z]{2,3}(/\S*)?$"))
                {
                    btnAddChanges.Visible = false;
                    WarningMsg.InnerHtml = "<div class=\"alert alert-warning text-center\"> <h1>INSERISCI UN URL IMMAGINE VALIDO</h1></div>";
                    return; // Interrompi l'esecuzione se l'URL dell'immagine non è valido
                }

                // Conversione del prezzo da stringa a decimal
                if (decimal.TryParse(newProductPrice, out decimal decimalPrice))
                {
                    string query = "UPDATE Prodotti SET nome = @NewProductName, prezzo = @NewProductPrice, immagine = @NewProductImage, descrizione = @NewProductDescription WHERE Id = @ProductId";
                    SqlCommand cmd = new SqlCommand(query, DBConn.conn);
                    cmd.Parameters.AddWithValue("@NewProductName", newProductName);
                    cmd.Parameters.AddWithValue("@NewProductPrice", decimalPrice);
                    cmd.Parameters.AddWithValue("@NewProductImage", newProductImage);
                    cmd.Parameters.AddWithValue("@NewProductDescription", newProductDescription);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", "alert('Prodotto aggiornato con successo'); window.location.href='Home.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Aggiornamento del prodotto non riuscito');", true);
                    }
                }
                else
                {
                    btnAddChanges.Visible = false;
                    WarningMsg.InnerHtml = "<div class=\"alert alert-warning text-center\"> <h1>INSERISCI UN NUMERO PER IL PREZZO</h1></div>";
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


        protected void btnSaveChanges_Click_Add(object sender, EventArgs e)
        {
            try
            {
                DBConn.conn.Open();

                string newProductName = txtNewProductName.Text;
                string newProductPrice = txtNewProductPrice.Text;
                string newProductImage = txtNewProductImage.Text;
                string newProductDescription = txtNewProductDescription.Text;

                // Conversione del prezzo da stringa a decimal
                if (string.IsNullOrEmpty(newProductName))
                {
                    btnAddChanges.Visible = true;
                    WarningMsg.InnerHtml = "<div class=\"alert alert-warning text-center\"> <h1>INSERISCI UN NOME VALIDO</h1></div>";
                }
                else if (!Regex.IsMatch(newProductImage, @"^(http(s)?://|www.)+[a-zA-Z0-9-_\.]+\.[a-zA-Z]{2,3}(/\S*)?$"))
                {
                    btnAddChanges.Visible = true;
                    WarningMsg.InnerHtml = "<div class=\"alert alert-warning text-center\"> <h1>INSERISCI UN URL IMMAGINE VALIDO</h1></div>";
                }

                else if (decimal.TryParse(newProductPrice, out decimal decimalPrice))
                {
                    string query = "INSERT INTO Prodotti (nome, prezzo, immagine, descrizione) VALUES (@NewProductName, @NewProductPrice, @NewProductImage, @NewProductDescription)";
                    SqlCommand cmd = new SqlCommand(query, DBConn.conn);
                    cmd.Parameters.AddWithValue("@NewProductName", newProductName);
                    cmd.Parameters.AddWithValue("@NewProductPrice", decimalPrice);
                    cmd.Parameters.AddWithValue("@NewProductImage", newProductImage);
                    cmd.Parameters.AddWithValue("@NewProductDescription", newProductDescription);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", "alert('Prodotto aggiunto con successo'); window.location.href='/Home.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Aggiunta del prodotto non riuscita');", true);
                    }
                }
                else
                {
                    btnAddChanges.Visible = true;
                    WarningMsg.InnerHtml = "<div class=\"alert alert-warning text-center\"> <h1>INSERISCI UN NUMERO PER IL PREZZO</h1></div>";
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Ottieni l'ID del prodotto
                string productId = lblProductId.Text;

                // Apri la connessione al database
                DBConn.conn.Open();

                // Query per eliminare il prodotto dal database
                string query = "DELETE FROM Prodotti WHERE Id = @ProductId";
                SqlCommand cmd = new SqlCommand(query, DBConn.conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                // Esegui l'istruzione SQL di eliminazione
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", "alert('Prodotto eliminato con successo'); window.location.href='Home.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Eliminazione del prodotto non riuscita');", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                // Chiudi la connessione al database
                if (DBConn.conn.State == ConnectionState.Open)
                {
                    DBConn.conn.Close();
                }
            }
        }
    }
}
