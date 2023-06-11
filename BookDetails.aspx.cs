using System;
using System.Data;
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

                    switch (bookType)
                    {
                        case 1:
                            AddToCartPlaceHolder.Controls.Add(new Button { Text = "Dodaj książkę do koszyka", CssClass = "add-to-cart-button" });
                            AddOtherTypeButtons(2, "Dodaj audiobook do koszyka");
                            AddOtherTypeButtons(3, "Dodaj ebook do koszyka");
                            break;
                        case 2:
                            AddToCartPlaceHolder.Controls.Add(new Button { Text = "Dodaj audiobook do koszyka", CssClass = "add-to-cart-button" });
                            AddOtherTypeButtons(1, "Dodaj książkę do koszyka");
                            AddOtherTypeButtons(3, "Dodaj ebook do koszyka");
                            break;
                        case 3:
                            AddToCartPlaceHolder.Controls.Add(new Button { Text = "Dodaj ebook do koszyka", CssClass = "add-to-cart-button" });
                            AddOtherTypeButtons(1, "Dodaj książkę do koszyka");
                            AddOtherTypeButtons(2, "Dodaj audiobook do koszyka");
                            break;
                        default:
                            // Brak obsługi dla innych typów książek
                            break;
                    }
                }
            }
        }

        private void AddOtherTypeButtons(int type, string buttonText)
        {
            // Sprawdź, czy istnieje inny typ książki o tym samym tytule
            SqlDataSource1.SelectCommand = "SELECT Tytul, Autor, Opis, Cena FROM Ksiazki WHERE Tytul = @Title AND TypProduktuID = @TypProduktuID";
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("Title", TitleLabel.Text);
            SqlDataSource1.SelectParameters.Add("TypProduktuID", type.ToString());

            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

            if (dv != null && dv.Count > 0)
            {
                // Dodaj przycisk dla danego typu książki
                if(dv != null && dv.Count > 0)
{
                    foreach (DataRowView rowView in dv)
                    {
                        Button button = new Button
                        {
                            Text = buttonText,
                            CssClass = "add-to-cart-button"
                        };

                        // Dodaj guzik wraz z kontenerem
                        Panel buttonContainer = new Panel();
                        buttonContainer.Controls.Add(button);
                        AddToCartPlaceHolder.Controls.Add(buttonContainer);
                    }
                }
            }
        }
    }
}