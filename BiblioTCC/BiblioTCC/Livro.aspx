<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Livro.aspx.cs" Inherits="BiblioTCC.Livro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/livro.css" />

    <div class="container">
         <asp:Button runat="server" ID="voltarBusca" OnClick="voltarBusca_Click" CssClass="seila" Text="Voltar"></asp:Button>
        <div class=" main-container">
            <div class="capaLivro">
                <h1 runat="server" id="tituloLivro"></h1>
                <img src="" ID="capaLivro" runat="server"/>
            </div>
        </div>
    </div>



   <script src="Assets/js/livro.js"></script>
    <asp:label runat="server" id="codLivroLabel" visible="false"></asp:label>

</asp:Content>
