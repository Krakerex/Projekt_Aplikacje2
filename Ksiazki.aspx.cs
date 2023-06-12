using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Projekt_Aplikacje2
{
    public partial class Ksiazki : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Wywołaj metodę do wyświetlania domyślnych danych
                DisplayDefaultData();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            // Wywołaj metodę do wyświetlania danych na podstawie wprowadzonych wartości wyszukiwania
            DisplaySearchResults();
        }

        private void DisplayDefaultData()
        {

        }

        private void DisplaySearchResults()
        {
            string title = TitleTextBox.Text.Trim();
            string author = AuthorTextBox.Text.Trim();

            // Przygotuj zapytanie SQL na podstawie wprowadzonych wartości wyszukiwania
            string query = "SELECT * FROM Ksiazki WHERE KsiazkaID IN (SELECT MIN(KsiazkaID) FROM Ksiazki GROUP BY Tytul)";
            if (!string.IsNullOrEmpty(title))
            {
                query += $" AND Tytul LIKE '%{title}%'";
            }
            if (!string.IsNullOrEmpty(author))
            {
                query += $" AND Autor LIKE '%{author}%'";
            }

            // Wyświetl wyniki wyszukiwania w kontrolce Repeater
            BindBooksRepeater(query);
        }

        private void BindBooksRepeater(string query)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["KsiegarniaConnectionString1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                BooksRepeater.DataSource = reader;
                BooksRepeater.DataBind();
                reader.Close();
            }
        }
    }
}
