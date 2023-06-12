using System;
using System.Data.SqlClient;

namespace Projekt_Aplikacje2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string GetProductTypeName(int typeId)
        {
            string connectionString = SqlDataSource1.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT NazwaTypu FROM TypyProduktow WHERE TypProduktuID = @TypProduktuID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TypProduktuID", typeId);

                string productTypeName = command.ExecuteScalar()?.ToString();

                return string.IsNullOrEmpty(productTypeName) ? "Unknown" : productTypeName;
            }
        }

        protected void SubmitOrderButton_Click(object sender, EventArgs e)
        {
            int klientID = GetUserIdFromCookie();

            string connectionString = SqlDataSource1.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "ZlozZamowienie";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@KlientID", klientID);

                command.ExecuteNonQuery();
            }

            OrderStatusLabel.Text = "Zamówienie zrealizowane, dziękujemy!";
            OrderStatusLabel.Visible = true;
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
    }
}