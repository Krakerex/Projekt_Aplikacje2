<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Projekt_Aplikacje2.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
<html>
<head>
    <title>Strona główna księgarni</title>
    <style>
        /* Dodaj stylizację strony */
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 20px;
        }
        
        h1 {
            color: #333;
        }
        
        .book {
            display: inline-block;
            width: 200px;
            margin: 10px;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            text-align: center;
        }
        
        .book img {
            width: 150px;
            height: 200px;
        }
        
        .book-title {
            margin-top: 10px;
            font-weight: bold;
            cursor: pointer;
        }
        
        .book-author {
            margin-top: 5px;
            color: #666;
        }
        
        .book-price {
            margin-top: 5px;
            font-weight: bold;
        }
    </style>
</head>
<body>

    <h1>Witaj w naszej księgarni!</h1>
    
    <div>
        <h2>Dostępne produkty:<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KsiegarniaConnectionString1 %>" SelectCommand="SELECT *
FROM Ksiazki
WHERE KsiazkaID IN (
    SELECT MIN(KsiazkaID)
    FROM Ksiazki
    GROUP BY Tytul
);"></asp:SqlDataSource>
        </h2>
        
      <asp:Repeater ID="BooksRepeater" runat="server" DataSourceID="SqlDataSource1">
    <ItemTemplate>
        <div class="book">
            <img src="images/cover.jpg" alt='<%# Eval("Tytul") %>' />
            <div class="book-title">
                <a href='<%# "BookDetails.aspx?title=" + Server.UrlEncode(Eval("Tytul").ToString()) %>'><%# Eval("Tytul") %></a>
            </div>
            <div class="book-author"><%# Eval("Autor") %></div>
            <div class="book-price"><%# Eval("Cena", "{0:C}") %></div>
            
        </div>
    </ItemTemplate>
</asp:Repeater>
    </div>

    <script>
        function openBookDetails(bookId) {
            window.location.href = "BookDetails.aspx?bookId=" + bookId;
        }
    </script>

</body>
</html>
   </asp:Content>