using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Buildweek4
{
    public partial class UsersCarts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string loggedInUser = Session["Username"] as string;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(loggedInUser) && loggedInUser.ToLower() == Admin.UserName)
                {
                    BindUsersCarts();
                }
                else
                {
                    gridUsersCarts.Visible = false;
                    UsersCartsWarning.InnerHtml = "<div class=\"alert alert-warning text-center\"> <h1> ERROR 401: UNAUTHORIZED <br> STAI CERCANDO DI ACCEDERE AD UNA PAGINA AMMINISTRATORE <br> </h1></div>";
                }
                    
            }
        }

        private void BindUsersCarts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbShopConnectionString"].ToString()))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT U.IdUtenti AS UserId, U.Username, STRING_AGG(P.Nome + ' (' + CONVERT(NVARCHAR, C.Quantita) + ' pezzi)', ', ') AS ProdottiNelCarrello, " +
                                                    "SUM(C.Quantita) AS Quantita, SUM(P.Prezzo * C.Quantita) AS TotaleCarrello " +
                                                    "FROM Users U " +
                                                    "INNER JOIN Carrello C ON U.IdUtenti = C.UserId " +
                                                    "INNER JOIN Prodotti P ON C.ProductId = P.Id " +
                                                    "GROUP BY U.IdUtenti, U.Username " +
                                                    "HAVING COUNT(P.Id) > 0", conn);

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dataReader);

                    gridUsersCarts.DataSource = dt;
                    gridUsersCarts.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }


        protected void gridUsersCarts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Repeater rptUserCart = (Repeater)e.Row.FindControl("rptUserCart");
                string username = DataBinder.Eval(e.Row.DataItem, "Username").ToString();

                DataTable dtProducts = GetProductsForUser(username);
                rptUserCart.DataSource = dtProducts;
                rptUserCart.DataBind();
            }
        }

        private DataTable GetProductsForUser(string username)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbShopConnectionString"].ToString()))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT P.Nome FROM Users U " +
                                                    "INNER JOIN Carrello C ON U.IdUtenti = C.UserId " +
                                                    "INNER JOIN Prodotti P ON C.ProductId = P.Id " +
                                                    "WHERE U.Username = @Username", conn);
                    cmd.Parameters.AddWithValue("@Username", username);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            return dt;
        }
    }
}
