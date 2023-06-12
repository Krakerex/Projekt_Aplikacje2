using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Projekt_Aplikacje2
{
    public partial class Zarzadzanie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void KsiazkiGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            KsiazkiGridView.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void KsiazkiGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            KsiazkiGridView.EditIndex = -1;
            BindGrid();
        }

        protected void KsiazkiGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ksiazkaID = Convert.ToInt32(KsiazkiGridView.DataKeys[e.RowIndex].Value);

            TextBox tytulTextBox = (TextBox)KsiazkiGridView.Rows[e.RowIndex].FindControl("TytulTextBox");
            TextBox autorTextBox = (TextBox)KsiazkiGridView.Rows[e.RowIndex].FindControl("AutorTextBox");
            TextBox cenaTextBox = (TextBox)KsiazkiGridView.Rows[e.RowIndex].FindControl("CenaTextBox");
            TextBox typProduktuIDTextBox = (TextBox)KsiazkiGridView.Rows[e.RowIndex].FindControl("TypProduktuIDTextBox");

            string tytul = tytulTextBox.Text;
            string autor = autorTextBox.Text;
            decimal cena = Convert.ToDecimal(cenaTextBox.Text);
            int typProduktuID = Convert.ToInt32(typProduktuIDTextBox.Text);

            UpdateKsiazka(ksiazkaID, tytul, autor, cena, typProduktuID);

            KsiazkiGridView.EditIndex = -1;
            BindGrid();
        }

        protected void KsiazkiGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ksiazkaID = Convert.ToInt32(KsiazkiGridView.DataKeys[e.RowIndex].Value);

            DeleteKsiazka(ksiazkaID);

            BindGrid();
        }

        protected void DodajButton_Click(object sender, EventArgs e)
        {
            string tytul = TytulTextBox.Text;
            string autor = AutorTextBox.Text;
            decimal cena = Convert.ToDecimal(CenaTextBox.Text);
            int typProduktuID = Convert.ToInt32(TypProduktuIDTextBox.Text);

            InsertKsiazka(tytul, autor, cena, typProduktuID);

            BindGrid();

            TytulTextBox.Text = string.Empty;
            AutorTextBox.Text = string.Empty;
            CenaTextBox.Text = string.Empty;
            TypProduktuIDTextBox.Text = string.Empty;
        }

        private void BindGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["KsiegarniaConnectionString1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Ksiazki";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    KsiazkiGridView.DataSource = reader;
                    KsiazkiGridView.DataBind();
                }
            }
        }

        private void UpdateKsiazka(int ksiazkaID, string tytul, string autor, decimal cena, int typProduktuID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["KsiegarniaConnectionString1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Ksiazki SET Tytul = @Tytul, Autor = @Autor, Cena = @Cena, TypProduktuID = @TypProduktuID WHERE KsiazkaID = @KsiazkaID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tytul", tytul);
                    command.Parameters.AddWithValue("@Autor", autor);
                    command.Parameters.AddWithValue("@Cena", cena);
                    command.Parameters.AddWithValue("@TypProduktuID", typProduktuID);
                    command.Parameters.AddWithValue("@KsiazkaID", ksiazkaID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void DeleteKsiazka(int ksiazkaID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["KsiegarniaConnectionString1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Ksiazki WHERE KsiazkaID = @KsiazkaID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KsiazkaID", ksiazkaID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void InsertKsiazka(string tytul, string autor, decimal cena, int typProduktuID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["KsiegarniaConnectionString1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Ksiazki (Tytul, Autor, Cena, TypProduktuID) VALUES (@Tytul, @Autor, @Cena, @TypProduktuID)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tytul", tytul);
                    command.Parameters.AddWithValue("@Autor", autor);
                    command.Parameters.AddWithValue("@Cena", cena);
                    command.Parameters.AddWithValue("@TypProduktuID", typProduktuID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}