<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="Projekt_Aplikacje2.BookDetails" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="styles/bookdetails.css" rel="stylesheet" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KsiegarniaConnectionString1 %>"
        SelectCommand="SELECT Tytul, Autor, Opis, Cena, TypProduktuID FROM Ksiazki WHERE Tytul = @Title">
        <SelectParameters>
            <asp:QueryStringParameter Name="Title" QueryStringField="title" />
        </SelectParameters>
    </asp:SqlDataSource>

    <div class="book-details">
        <div class="book-image">
            <img src="images/cover.jpg" alt="Tytuł książki" />
        </div>
        <div class="book-info">
            <h2 class="book-title">
                <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Tytul") %>'></asp:Label>
            </h2>
            <div class="book-author">
                Autor: <asp:Label ID="AuthorLabel" runat="server" Text='<%# Eval("Autor") %>'></asp:Label>
            </div>
            <div class="book-description">
                Opis: <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Opis") %>'></asp:Label>
            </div>
            <div class="book-price">
                Cena: <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Cena", "{0:C}") %>'></asp:Label>
            </div>
            <asp:PlaceHolder ID="AddToCartPlaceHolder" runat="server"></asp:PlaceHolder>
        </div>
    </div>
</asp:Content>