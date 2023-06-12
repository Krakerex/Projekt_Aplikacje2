using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projekt_Aplikacje2
{
    public partial class Logowanie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Text;

            // Sprawdź czy email istnieje w bazie danych
            SqlDataSource1.SelectParameters["Email"].DefaultValue = email;
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

            if (dv != null && dv.Count > 0 && password == "admin")
            {
                // Pobierz identyfikator użytkownika z bazy danych (zakładając, że jest w kolumnie o nazwie "UserID")
                int userID = Convert.ToInt32(dv[0]["KlientID"]);

                // Ustaw flagę zalogowania w sesji
                Session["LoggedIn"] = true;

                // Utwórz ciasteczko "LoggedIn" i ustaw jego wartość na "true"
                HttpCookie loginCookie = new HttpCookie("LoggedIn", "true");
                Response.Cookies.Add(loginCookie);

                // Utwórz ciasteczko "UserID" i ustaw jego wartość na identyfikator użytkownika
                HttpCookie userIDCookie = new HttpCookie("UserID", userID.ToString());
                Response.Cookies.Add(userIDCookie);

                // Przekieruj do strony domyślnej po zalogowaniu
                Response.Redirect("Default.aspx");
            }
            else
            {
                // Wyświetl komunikat o błędzie
                ErrorMessageLabel.Text = "Nieprawidłowy email lub hasło";
                ErrorMessageLabel.Visible = true;
            }
        }


    }
}