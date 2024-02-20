using System;

namespace Buildweek4.Models
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetNavigationVisibility();
            }
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session["LoggedIn"] = false;
            Session["Username"] = "";
            Response.Redirect("/Home.aspx");
        }

        private void SetNavigationVisibility()
        {
            bool isLoggedIn = Session["LoggedIn"] != null && (bool)Session["LoggedIn"];
            bool isAdmin = isLoggedIn && Session["Username"].ToString().ToLower() == Admin.UserName.ToLower();

            // Imposta la visibilità dei bottoni nella navbar
            liLogin.Visible = !isLoggedIn;
            btnLogout1.Visible = isLoggedIn;
            liAdminShop.Visible = isAdmin;
        }
    }
}
