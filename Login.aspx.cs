using System;
using System.Data.SqlClient;

namespace Buildweek4
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (ValidateUser(username, password))
            {
                // Utente esistente, esegui l'accesso
                Response.Redirect("Home.aspx");
            }
            else
            {
               
                if (AddNewUser(username, password))
                {
                    
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    // Gestisci eventuali errori nell'aggiunta dell'utente
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
