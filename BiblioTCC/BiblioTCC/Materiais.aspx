﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpageside.Master" AutoEventWireup="true" CodeBehind="Materiais.aspx.cs" Inherits="BiblioTCC.Materiais" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/materiais.css" />

    <div class="main">
        <div class="main-content">

       
                <asp:Repeater ID="materiaisRepeater" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <div class="materiais">
                            <div class="imagem">
                                <img class="logo-img" src="" />
                            </div>
                            <span><%# %></span>
                            <span><%# %></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

        </div>
    </div>


</asp:Content>
