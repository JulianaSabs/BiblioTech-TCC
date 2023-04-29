<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Relatorio.aspx.cs" Inherits="BiblioTCC.Relatorio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/relatorio.css" />
    <div class="area">
    <nav class="main-menu">
            <ul>
                <li>
                    <a href="https://jbfarrow.com">
                        <i class="fa fa-home fa-2x"></i>
                        <span class="nav-text">
                           Configurações da Biblioteca
                        </span>
                    </a>
                  
                </li>
                <li class="has-subnav">
                    <a href="#">
                        <i class="fa fa-cogs fa-2x"></i>
                        <span class="nav-text">
                            Gerenciador
                        </span>
                    </a>
                    
                </li>
                <li class="has-subnav">
                    <a href="#">
                       <i class="fa fa-book fa-2x"></i>
                        <span class="nav-text">
                            Relatórios
                        </span>
                    </a>
                </li>
                </ul>
        </nav>
    </div>


</asp:Content>
