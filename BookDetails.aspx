<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="Projekt_Aplikacje2.BookDetails" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="styles/bookdetails.css" rel="stylesheet" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KsiegarniaConnectionString1 %>"
        SelectCommand="SELECT Tytul, Autor, Opis, Cena, TypProduktuID FROM Ksiazki WHERE Tytul = @Title">
        <SelectParameters>
            <asp:QueryStringParameter Name="Title" QueryStringField="title" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="AddToCartDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:KsiegarniaConnectionString1 %>"
    InsertCommand="DodajDoKoszyka" InsertCommandType="StoredProcedure">
    <InsertParameters>
        <asp:CookieParameter CookieName="UserID" Name="KlientID" Type="Int32" />
        <asp:Parameter Name="KsiazkaID" Type="Int32" />
        <asp:Parameter Name="Ilosc" Type="Int32" DefaultValue="1" />
    </InsertParameters>
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
            <% if (CheckProductExists(3)) { %>
    <asp:Button ID="BookButton" runat="server" Text="Add Book to Cart" OnClick="AddBookToCart_Click" CommandArgument="3"/>
<% } %>

<% if (CheckProductExists(1)) { %>
    <asp:Button ID="AudiobookButton" runat="server" Text="Add Audiobook to Cart" OnClick="AddBookToCart_Click" CommandArgument="1"/>
<% } %>

<% if (CheckProductExists(2)) { %>
    <asp:Button ID="EbookButton" runat="server" Text="Add Ebook to Cart" OnClick="AddBookToCart_Click" CommandArgument="2"/>
<% } %>
        </div>
    </div>
</asp:Content>