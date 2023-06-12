using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Projekt_Aplikacje2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Pobierz ID użytkownika z ciasteczka
            int userId;
            if (int.TryParse(Request.Cookies["UserID"]?.Value, out userId))
            {
                // Pobierz informacje o użytkowniku z bazy danych
                DataTable userData = GetUserData(userId);

                // Wyświetl informacje o użytkowniku
                if (userData.Rows.Count > 0)
                {
                    UserInfoLabel.Text = "Informacje o użytkowniku: <br/>";
                    UserInfoLabel.Text += "ID: " + userData.Rows[0]["KlientID"] + "<br/>";
                    UserInfoLabel.Text += "Imię: " + userData.Rows[0]["Imie"] + "<br/>";
                    UserInfoLabel.Text += "Nazwisko: " + userData.Rows[0]["Nazwisko"] + "<br/>";
                    UserInfoLabel.Text += "Email: " + userData.Rows[0]["Email"] + "<br/>";
                    UserInfoLabel.Text += "Telefon: " + userData.Rows[0]["Telefon"] + "<br/>";
                }
            }
        }

        private DataTable GetUserData(int userId)
        {
            DataTable userData = new DataTable();

            // Utwórz połączenie do bazy danych
            string connectionString = SqlDataSource1.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Klienci WHERE KlientID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(userData);
            }

            return userData;
        }
    }
}