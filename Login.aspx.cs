using System;
using System.Data.SqlClient;

namespace Buildweek4
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se l'utente è già loggato
            if (Session["LoggedIn"] != null && (bool)Session["LoggedIn"])
            {
                // Utente già loggato, mostra messaggio di errore
                divErrorMessage.Visible = true;
                divErrorMessage.InnerHtml = "<div class=\"alert alert-danger\"> <h1>Sei già loggato. Nessuna necessità di accedere nuovamente.</h1>" +
                    "</div>";
                LoginContainer.Visible = false;                                

            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string registerUsername = txtRegisterUsername.Value;
            string registerPassword = txtRegisterPassword.Value;

            if (string.IsNullOrEmpty(registerUsername) || string.IsNullOrEmpty(registerPassword))
            {
                divErrorMessage.Visible = true;
                divErrorMessage.InnerHtml = "<div class=\"alert alert-danger\"><h1>Inserisci Username e Password validi</h1></div>";
            }
            else
            { 
                if (AddNewUser(registerUsername, registerPassword))
                {
                    // Registrazione riuscita, puoi eseguire azioni aggiuntive se necessario
                    divErrorMessage.Visible = true;
                    divErrorMessage.InnerHtml = "<div class=\"alert alert-success\"><h1>Registrazione riuscita! Puoi accedere ora.</h1></div>";
                }
                else
                {
                    // Errore durante la registrazione, gestisci l'errore o visualizza un messaggio
                    divErrorMessage.Visible = true;
                    divErrorMessage.InnerHtml = "<div class=\"alert alert-danger\"><h1>Errore durante la registrazione. <br> Username già in uso.</h1></div>";
                }
            }
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Value;
            string password = txtPassword.Value;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                divErrorMessage.Visible = true;
                divErrorMessage.InnerHtml = "<div class=\"alert alert-danger\"> <h1>Errore: Inserisci username e password.</h1></div>";
            }
            else
            {
                if (ValidateUser(username, password))
                {
                    int userId = GetUserId(username);
                    Session["LoggedIn"] = true;
                    Session["Username"] = username;
                    Session["IdUtenti"] = userId;

                    Response.Redirect("Home.aspx");
                }
                else
                {
                    divErrorMessage.Visible = true;
                    divErrorMessage.InnerHtml = "<div class=\"alert alert-danger\"> <h1>Errore: Username non trovato. <br> Verifica le credenziali.</h1></div>";
                }
            }
        }


        private int GetUserId(string username)
        {
            // Ottieni l'ID dell'utente dalla tabella Users
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbShopConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT IdUtenti FROM Users WHERE Username=@Username", con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    // Esegui la query per ottenere l'ID dell'utente
                    object result = cmd.ExecuteScalar();

                    // Verifica se la query ha restituito un valore e restituisci l'ID
                    return (result != null) ? Convert.ToInt32(result) : 0;
                }
            }
        }


        private bool ValidateUser(string username, string password)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbShopConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username=@Username AND Password=@Password", con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }


        private bool AddNewUser(string username, string password)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbShopConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Verifica se l'utente esiste già
                    if (UserExists(username, con))
                    {
                        return false; // L'utente esiste già
                    }

                    // Aggiungi un nuovo utente
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password) VALUES (@Username, @Password)", con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0; // Restituisce true se è stata aggiunta almeno una riga
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante l'inserimento dell'utente: {ex.Message}");
                return false;
            }
        }

        private bool UserExists(string username, SqlConnection con)
        {
            // Verifica se l'utente esiste già
            using (SqlCommand cmd = new SqlCommand("SELECT 1 FROM Users WHERE Username=@Username", con))
            {
                cmd.Parameters.AddWithValue("@Username", username);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

    }
}
