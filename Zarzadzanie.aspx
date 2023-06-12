<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Zarzadzanie.aspx.cs" Inherits="Projekt_Aplikacje2.Zarzadzanie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Strona zarządzania</h1>

    <h2>Dodaj nowy rekord:</h2>
    <asp:Label ID="TytulLabel" runat="server" Text="Tytuł:"></asp:Label>
    <asp:TextBox ID="TytulTextBox" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="AutorLabel" runat="server" Text="Autor:"></asp:Label>
    <asp:TextBox ID="AutorTextBox" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="CenaLabel" runat="server" Text="Cena:"></asp:Label>
    <asp:TextBox ID="CenaTextBox" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="TypProduktuIDLabel" runat="server" Text="TypProduktuID:"></asp:Label>
    <asp:TextBox ID="TypProduktuIDTextBox" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="DodajButton" runat="server" Text="Dodaj" OnClick="DodajButton_Click" />

    <br />
    <br />

    <asp:GridView ID="KsiazkiGridView" runat="server" AutoGenerateColumns="False" OnRowEditing="KsiazkiGridView_RowEditing"
        OnRowCancelingEdit="KsiazkiGridView_RowCancelingEdit" OnRowUpdating="KsiazkiGridView_RowUpdating"
        OnRowDeleting="KsiazkiGridView_RowDeleting" DataKeyNames="KsiazkaID">
        <Columns>
            <asp:BoundField DataField="KsiazkaID" HeaderText="ID" ReadOnly="True" />
            <asp:TemplateField HeaderText="Tytuł">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Tytul") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Tytul") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Autor">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Autor") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Autor") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cena">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Cena") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Cena") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TypProduktuID">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("TypProduktuID") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("TypProduktuID") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>
