using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Buildweek4
{
    public partial class ProductsTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string loggedInUser = Session["Username"] as string;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(loggedInUser) && loggedInUser.ToLower() == Admin.UserName)
                {
                    BindGridView();
                }
                else {
                    ProductsTableContainer.InnerHtml = "<div class=\"alert alert-warning text-center\"> <h1> ERROR 401: UNAUTHORIZED <br> STAI CERCANDO DI ACCEDERE AD UNA PAGINA AMMINISTRATORE <br> </h1></div>";
                }
            }
        }

        private void BindGridView()
        {
            try
            {
                DBConn.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Nome, Prezzo, Descrizione, Immagine FROM Prodotti", DBConn.conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    productsGridView.DataSource = dt;
                    productsGridView.DataBind();
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
