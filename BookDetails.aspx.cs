using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projekt_Aplikacje2
{
    public partial class BookDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string bookTitle = Request.QueryString["title"]; // Pobierz wartość parametru z adresu URL

                // Przypisz parametr do SqlDataSource
                SqlDataSource1.SelectParameters["Title"].DefaultValue = bookTitle;

                // Pobierz dane dla podanego tytułu z SqlDataSource
                DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

                // Sprawdź, czy dane są dostępne
                if (dv != null && dv.Count > 0)
                {
                    DataRowView row = dv[0];

                    // Przypisz wartości do kontrolek
                    TitleLabel.Text = row["Tytul"].ToString();
                    AuthorLabel.Text = row["Autor"].ToString();
                    DescriptionLabel.Text = row["Opis"].ToString();
                    PriceLabel.Text = string.Format("{0:C}", row["Cena"]);

                    // Sprawdź typ książki i dodaj odpowiednie przyciski
                    int bookType = Convert.ToInt32(row["TypProduktuID"]);


                }

            }
        }

        protected bool CheckProductExists(int type)
        {
            string title = Request.QueryString["title"]; // Get the value of the "title" parameter from the URL
            string connectionString = SqlDataSource1.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Ksiazki WHERE Tytul = @Title AND TypProduktuID = @Type";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Type", type);

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        protected void AddBookToCart_Click(object sender, EventArgs e)
        {
            Button addButton = (Button)sender; // Get the button control that raised the event
            int bookType = Convert.ToInt32(addButton.CommandArgument); // Get the book type from the button's command argument

            string bookTitle = Request.QueryString["title"]; // Get the value of the "title" parameter from the URL
            int userId = GetUserIdFromCookie(); // Implement this method to retrieve the user ID from the cookie
            int bookId = GetBookIdByTitleAndType(bookTitle, bookType); // Implement this method to retrieve the book ID based on the title and type
            int quantity = 1; // You can modify this value to set the desired quantity

            string connectionString = SqlDataSource1.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertCommand = "DodajDoKoszyka";
                SqlCommand command = new SqlCommand(insertCommand, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@KlientID", userId);
                command.Parameters.AddWithValue("@KsiazkaID", bookId);
                command.Parameters.AddWithValue("@Ilosc", quantity);

                command.ExecuteNonQuery();
            }
        }
        private int GetUserIdFromCookie()
        {
            if (Request.Cookies["UserID"] != null && int.TryParse(Request.Cookies["UserID"].Value, out int userId))
            {
                return userId;
            }

            // Return a default value or handle the case when the user ID is not found in the cookie
            return 0;
        }
        private int GetBookIdByTitleAndType(string title, int type)
        {
            string connectionString = SqlDataSource1.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT KsiazkaID FROM Ksiazki WHERE Tytul = @Title AND TypProduktuID = @Type";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Type", type);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }

                // Return a default value or handle the case when the book ID is not found
                return 0;
            }
        }
    }

    


}

