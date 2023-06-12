﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroEmprestimo.aspx.cs" Inherits="BiblioTCC.CadastroEmprestimo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/cadastroEmprestimo.css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.13.2/themes/base/jquery-ui.min.css" rel="stylesheet" />
    
    <div class="main">
        <div id="AbasDiv" class="col-md-12">
            <ul class="nav nav-tabs">
                <li id="emprestimoLi" runat="server">
                    <asp:LinkButton ID="tab1" Text="Novo Empréstimo" runat="server" CommandArgument="1" OnClick="TrocarAbas" Font-Underline="false" CssClass="Clicked" />
                </li>
                &nbsp
                <li id="validarLi" runat="server">
                    <asp:LinkButton ID="tab2" Text="Validar Empréstimo" runat="server" CommandArgument="2" OnClick="TrocarAbas" Font-Underline="false" CssClass="Initial" />
                </li>
            </ul>
            <br />
        </div>
        <div class="container-all">
            <div id="novoEmprestimoDiv" runat="server" visible="false" class="tab-navigation">
                <div class="left">
                    <br />
                    <h2>Novo Empréstimo
                        <img src="Assets/img/add-user.png" alt="Alternate Text" height="30px" />
                    </h2>
                    <label>Data Empréstimo</label>
                    <asp:TextBox ID="dataEmprestimoTextBox" CssClass="form-control datepicker" runat="server" placeholder="mm/dd/yyyy" TextMode="Date" ReadOnly="false" />
                    <br />
                    <label>Nome</label>
                    <asp:TextBox ID="nomeTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                    <br />
                    <label>E-mail</label>
                    <asp:TextBox ID="emailTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                    <br />
                    <label>Livros</label>
                    <asp:TextBox ID="livroTextBox" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    <br />
                    <label>Tombo</label>
                    <asp:TextBox ID="tomboTextBox" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Button ID="btnEntrar" CssClass="pesquisarButton" runat="server" Text="Cadastrar" OnClick="btnEntrar_Click" />
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <asp:Button ID="btnMassa" runat="server" Text="Importar Usuários em Massa" CssClass="pesquisarButton" CausesValidation="False" OnClick="btnMassa_Click" />
                        </div>
                    </div>
                </div>
                <div class="right">
                    <img src="Assets/img/imagen.svg" alt="SOCOROOOO" height="800px" />
                </div>
            </div>
        </div>
        
        <div class="container-all">
            <div id="validarEmprestimo" runat="server" visible="false">
                <div class="second-tab">
                    <h2>Validar Emprestimo
                        <i class="fa-solid fa-check" style="color: #ffd369;"></i>
                    </h2>
                    <br />
                    <div class="col-md-12">
                     
                        <div class="row">
                            <div class="col-md-12">
                                <br />
                                <asp:GridView ID="validarEmprestimoGridView" runat="server" CssClass="table table-bordered table-hover table-striped table-condensed custom-gridview" DataSourceID="usuarioSqlDataSource" OnRowDataBound="validarEmprestimoGridView_RowDataBound" EmptyDataText="Nenhuma informação foi encontrada." AllowPaging="true" PageSize="20" OnPageIndexChanging="validarEmprestimoGridView_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="finalizarStatusLinkButton" CssClass="fa-solid fa-check-square" ForeColor="#20206B"  runat="server" OnClick="finalizarStatusLinkButton_Click" Font-Underline="false" ToolTip="Clique aqui para validar"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="usuarioSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:BancoConnectionString %>"></asp:SqlDataSource>
                            </div>
                        </div>
               
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="modal">
        <div>
            <h2 id="tituloModal"></h2>
            <p id="mensagemModal"></p>
        </div>
        <div class="modal-button">
            <a id="modal-button-close" onclick="FecharModal()">Fechar</a>
        </div>
    </div>


    <div id="modal2">
        <div>
            <h2>Selecione o arquivo .csv</h2>
            <p>
                <asp:LinkButton ID="csvLinkButton" runat="server" CausesValidation="false">Baixar Modelo Arquivo .CSV</asp:LinkButton>
                <br />
                <asp:FileUpload ID="csvFileUpload" runat="server" />
            </p>
        </div>
        <div class="modal-button">
            <a id="modal-button-close2" onclick="FecharModal2()">Fechar</a>&nbsp<asp:Button ID="btnCSV" CssClass="ver-card-button" runat="server" Text="Importar" />
        </div>
    </div>
  
  
    <asp:Label runat="server" ID="codIdUsuario" Visible="true" AutoPostBack="false"></asp:Label>
    <script type="text/javascript" src="Scripts/jquery-3.4.1.min.js "></script>
    <script src="Assets/js/cadastroEmprestimo.js"></script>
</asp:Content>
