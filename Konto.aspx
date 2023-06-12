<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Konto.aspx.cs" Inherits="Projekt_Aplikacje2.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KsiegarniaConnectionString1 %>"
        SelectCommand="SELECT Tytul, Autor, Opis, Cena, TypProduktuID FROM Ksiazki WHERE Tytul = @Title">
        <SelectParameters>
            <asp:QueryStringParameter Name="Title" QueryStringField="title" />
        </SelectParameters>
    </asp:SqlDataSource>
    <h2>Konto użytkownika</h2>
    <asp:Label ID="UserInfoLabel" runat="server" CssClass="info-label" />
</asp:Content>
