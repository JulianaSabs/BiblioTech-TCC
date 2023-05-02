﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpageside.Master" AutoEventWireup="true" CodeBehind="AdministracaoBiblioteca.aspx.cs" Inherits="BiblioTCC.AdminitracaoBiblioteca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/adminbiblio.css" />
  

   <div class="container-all">
        <div class="left">
            <br />
            <h2>Configurações Biblioteca <img src="Assets/img/add-user.png" alt="Alternate Text" height="30px" /> </h2>
            <label>Nome Instituição</label>
            <asp:TextBox ID="bibliotecaTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
            <label>Endereço</label>
            <asp:TextBox ID="enderecoTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
            <label>Máximo dias permitido</label>
            <asp:DropDownList ID="classeDropDownList" runat="server" class="form-select" >
                <asp:ListItem></asp:ListItem>
                 <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="10">15</asp:ListItem>
                <asp:ListItem Value="10">20</asp:ListItem>
             </asp:DropDownList>
            <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:Button ID="btnSalvar" CssClass="btn btn-warning" runat="server" Text="Salvar"  />
                </div>
                <div class="col-md-1"></div>
            </div>
        </div>
          <div class="right">
             <div class="row">
                 <div class="cold-md-3">
                      <label>Telefone Instituição</label>
                        <asp:TextBox ID="telefoneTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                 </div>
             </div>
              <br />
           <div class="row">
                 <div class="cold-md-3">
                      <label>Valor Multa</label>
                        <asp:TextBox ID="multaTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                 </div>
             </div>
            <br />
          </div>
           <div class="footer">
            </div>
     </div>
    <script src="Assets/js/Admin.js"></script>
</asp:Content>
 