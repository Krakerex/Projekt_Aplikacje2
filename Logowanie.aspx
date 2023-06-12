<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logowanie.aspx.cs" Inherits="Projekt_Aplikacje2.Logowanie" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Logowanie</h2>

    <asp:Label ID="ErrorMessageLabel" runat="server" ForeColor="Red" Visible="False"></asp:Label>

    <div>
        <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="EmailTextBox" Text="Email:"></asp:Label>
        <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ControlToValidate="EmailTextBox" ErrorMessage="Pole Email jest wymagane"></asp:RequiredFieldValidator>
    </div>

    <div>
        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="PasswordTextBox" Text="Hasło:"></asp:Label>
        <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="PasswordValidator" runat="server" ControlToValidate="PasswordTextBox" ErrorMessage="Pole Hasło jest wymagane"></asp:RequiredFieldValidator>
    </div>

    <div>
        <asp:Button ID="LoginButton" runat="server" Text="Zaloguj" OnClick="LoginButton_Click" />
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KsiegarniaConnectionString1 %>"
        SelectCommand="SELECT * FROM [Klienci] WHERE [Email] = @Email">
        <SelectParameters>
            <asp:ControlParameter ControlID="EmailTextBox" Name="Email" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>