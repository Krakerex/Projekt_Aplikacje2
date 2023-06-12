<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Koszyk.aspx.cs" Inherits="Projekt_Aplikacje2.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Koszyk</h2>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ZamowienieID"
        DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
        <Columns>
            <asp:BoundField DataField="Tytul" HeaderText="Tytuł" SortExpression="Tytul" />
            <asp:BoundField DataField="Autor" HeaderText="Autor" SortExpression="Autor" />
            <asp:BoundField DataField="Cena" DataFormatString="{0:C}" HeaderText="Cena" SortExpression="Cena" />
            <asp:TemplateField HeaderText="Typ">
                <ItemTemplate>
                    <%# GetProductTypeName((int)Eval("TypProduktuID")) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KsiegarniaConnectionString1 %>"
        SelectCommand="SELECT Ksiazki.Tytul, Ksiazki.Autor, Ksiazki.Cena, ZamowienieProdukt.ZamowienieID, Ksiazki.TypProduktuID
                       FROM Ksiazki
                       INNER JOIN ZamowienieProdukt ON Ksiazki.KsiazkaID = ZamowienieProdukt.KsiazkaID
                       INNER JOIN Zamowienia ON ZamowienieProdukt.ZamowienieID = Zamowienia.ZamowienieID
                       WHERE Zamowienia.KlientID = @KlientID">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserID" Name="KlientID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:Button ID="SubmitOrderButton" runat="server" Text="Złóż Zamówienie" OnClick="SubmitOrderButton_Click" />
    
    <asp:Label ID="OrderStatusLabel" runat="server" ForeColor="Green" Visible="false"></asp:Label>
</asp:Content>
