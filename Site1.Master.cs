using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projekt_Aplikacje2
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected bool IsUserLoggedIn()
        {
            // Sprawdź czy ciasteczko "LoggedIn" istnieje i ma wartość "true"
            HttpCookie loginCookie = Request.Cookies["LoggedIn"];

            if (loginCookie != null && loginCookie.Value == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool IsUserAdmin()
        {
            // Sprawdź czy ciasteczko "LoggedIn" istnieje i ma wartość "true"
            HttpCookie adminCookie = Request.Cookies["isAdmin"];

            if (adminCookie != null && adminCookie.Value == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            // Usuń ciasteczko "LoggedIn"
            HttpCookie loginCookie = new HttpCookie("LoggedIn");
            HttpCookie userID = new HttpCookie("UserID");
            HttpCookie isAdmin = new HttpCookie("isAdmin");
            loginCookie.Value = "false";
            userID.Value = null;
            isAdmin.Value = "0";
            Response.Cookies.Add(loginCookie);
            Response.Cookies.Add(userID);
            Response.Cookies.Add(isAdmin);
            // Przekieruj do strony logowania
            Response.Redirect("Logowanie.aspx");
        }
    }
}