﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Livro.aspx.cs" Inherits="BiblioTCC.Livro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/livro.css" />

    <div class="menu-container">
        <div class="menu-container-div">
            <div class="container-home">
                <section class="section section-left">
                    <div class="section-left-items">
                        <h1 class="section-left-items-h1" runat="server" id="tituloLivro"></h1>
                        <br />
                        <h3 class="section-left-items-h3" id="autorLivroo" runat="server"></h3>
                        <div class="section-left-items-div">
                          <p runat="server" id="sinopseLivroo"></p>
                        </div>
                        <div class="section-left-items-div2">
                        </div>
                    </div>
                </section>
                <section class="section section-right">
                    <img id="capaLivro" runat="server" src="assets/img/pesquisa/image%2068.png" />
                </section>
            </div>
        </div>
    </div>



   <script src="Assets/js/livro.js"></script>
    <asp:label runat="server" id="codLivroLabel" visible="false"></asp:label>

</asp:Content>
