using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace BiblioTCC
{
    public partial class CadastroEmprestimo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Iniciar();
            }
        }
        #region Iniciar Abas
        protected void Iniciar()
        {
            AcessarAba(1);
         

        }
        protected void TrocarAbas(object sender, EventArgs e)
        {
            int NumeroDaAba = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            AcessarAba(NumeroDaAba);
        }

        protected void AcessarAba(int NumeroDaAba)
        {
            switch (NumeroDaAba)
            {
                case 1:
                    {
                        novoEmprestimoDiv.Visible = true;
                        validarEmprestimo.Visible = false;

                        tab1.CssClass = "Clicked";
                        tab2.CssClass = "Initial";

                        emprestimoLi.Attributes.Add("class", "active");
                        validarLi.Attributes.Add("class", "");

                        break;
                    }
                case 2:
                    {
                        novoEmprestimoDiv.Visible = false;
                        validarEmprestimo.Visible = true;

                        tab1.CssClass = "Initial";
                        tab2.CssClass = "Clicked";

                        validarLi.Attributes.Add("class", "active");
                        emprestimoLi.Attributes.Add("class", "");

                        PreencherEmprestimoGridView();

                        break;
                    }
            }
        }
        #endregion
        #region Cadastrar Empréstimo No Banco

        public void RegistrarEmprestimo(string nomeUsuario)
        {
            //convertendo o valor da textbox para date
            DateTime dataSelecionada = DateTime.Parse(dataEmprestimoTextBox.Text);

            // definir a conexão com o banco de dados
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
           

            // abrir a conexão com o banco de dados
            conn.Open();

            // inserir os dados do usuário na tabela Usuario
            string queryUsuario = "INSERT INTO Usuario (NomeUsuario, EmailUsuario) VALUES (@NomeUsuario, @EmailUsuario); SELECT SCOPE_IDENTITY();";
            SqlCommand commandUsuario = new SqlCommand(queryUsuario, conn);
            commandUsuario.Parameters.AddWithValue("@NomeUsuario", nomeTextBox.Text);
            commandUsuario.Parameters.AddWithValue("@EmailUsuario", emailTextBox.Text);
            int idUsuario = Convert.ToInt32(commandUsuario.ExecuteScalar());

            // inserir os dados do empréstimo na tabela Emprestimo
            string queryEmprestimo = "INSERT INTO Emprestimo (IdUsuario, DataEmprestimo, Status) VALUES (@IdUsuario, @DataEmprestimo, @Status); SELECT SCOPE_IDENTITY();";
            SqlCommand commandEmprestimo = new SqlCommand(queryEmprestimo, conn);
            commandEmprestimo.Parameters.AddWithValue("@IdUsuario", idUsuario);
            commandEmprestimo.Parameters.Add("@DataEmprestimo", SqlDbType.Date).Value = dataSelecionada;
            commandEmprestimo.Parameters.AddWithValue("@Status", 1);
            int idEmprestimo = Convert.ToInt32(commandEmprestimo.ExecuteScalar());

            // inserir os dados do livro na tabela ItensEmprestimo
            string querySocorro = "INSERT INTO LivrosEmprestimo (IdEmprestimo, IdLivro) VALUES (@IdEmprestimo, @IdLivro)";
            SqlCommand commandSocorro = new SqlCommand(querySocorro, conn);
            commandSocorro.Parameters.AddWithValue("@IdEmprestimo", idEmprestimo);
            commandSocorro.Parameters.AddWithValue("@IdLivro", tomboTextBox.Text);
            commandSocorro.ExecuteNonQuery();

            // fechar a conexão com o banco de dados
            conn.Close();
            LimparCampos();
        }


        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text) && string.IsNullOrEmpty(emailTextBox.Text))
            {
                AbrirModal("Atenção", "Campo titulo e autor esta em branco.");
            }
            else
            {
                RegistrarEmprestimo("1");
                AbrirModal("Sucesso","Cadastro realizado");
            }
         
            
        }

        protected void LimparCampos()
        {
            nomeTextBox.Text = "";
            emailTextBox.Text = "";
            tomboTextBox.Text = "";
            dataEmprestimoTextBox.Text = "";
            livroTextBox.Text = "";


        }
        #endregion
        #region GridView e suas funções
        protected void PreencherEmprestimoGridView()
        {
           
            string sql = "SELECT u.IdUsuario, u.NomeUsuario as Nome, u.EmailUsuario As Email, FORMAT(e.DataEmprestimo, 'dd/MM/yyyy') as [Data de Empréstimo], s.Status AS Status FROM dbo.Usuario u LEFT JOIN dbo.Emprestimo e ON u.IdUsuario = e.IdUsuario LEFT JOIN dbo.Status s ON s.IdStatus = e.Status WHERE e.Status = 1";

            

            usuarioSqlDataSource.SelectCommand = sql;
        }

        protected void validarEmprestimoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
               
                // Obtém o valor da data de empréstimo da célula desejada
                string dataEmpréstimo = e.Row.Cells[4].Text;

                // Converte o valor da data para um objeto DateTime
                DateTime data;
                if (DateTime.TryParse(dataEmpréstimo, out data))
                {
                    // Verifica se a data de empréstimo passou de 7 dias
                    if (DateTime.Now > data.AddDays(7))
                    {
                        // Altera a cor da célula que contém o nome do usuário para vermelho
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
                e.Row.CssClass = "custom-header";
            }
        }

        protected void validarEmprestimoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            validarEmprestimoGridView.PageIndex = e.NewPageIndex;
            PreencherEmprestimoGridView();
            validarEmprestimoGridView.DataBind();
        }

        protected GridViewRow InstanciarLinha(object sender)
        {
            LinkButton lnk = (LinkButton)sender;
            TableCell cell = new TableCell();
            cell = (TableCell)lnk.Parent;
            GridViewRow row = (GridViewRow)cell.Parent;
            return row;
        }

        protected void editarCamposLinkButton_Click(object sender, EventArgs e)
        {
            int linha = InstanciarLinha(sender).RowIndex;
            string cod_teste = validarEmprestimoGridView.Rows[linha].Cells[1].Text;
            

            AbrirModalDeAtualizacaoDeStatus();
            
        }

        protected void AbrirModalDeAtualizacaoDeStatus()
        {
            atualizacaoModal.Visible = true;
        }

        protected void xLinkButton_Click(object sender, EventArgs e)
        {
            atualizacaoModal.Visible = false;
        }
        #endregion
        #region Modal e Botões
        protected void AbrirModal(string titulo, string mensagem)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "Javascript", "javascript: AbrirModal(`" + titulo + "`,`" + mensagem + "`);", true);
        }

        protected void AbrirModal2()
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "Javascript", "javascript: AbrirModal2();", true);
        }
        protected void btnMassa_Click(object sender, EventArgs e)
        {
            AbrirModal2();
        }

        protected void csvLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ArquivoUpload/emprestimo.csv");
        }

        #endregion


        //protected void CarregarUsuarioDoBanco()

        //{

        //    string sql = "SELECT IdUsuario FROM [dbo].[Usuario]";

        //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);

        //    SqlCommand comando = new SqlCommand(sql, conn);




        //    conn.Open();




        //    SqlDataReader resultado = comando.ExecuteReader();

        //    if (resultado.Read())

        //    {

        //        codIdUsuario.Text = Convert.ToString(resultado["IdUsuario"]);

        //    }

        //    conn.Close();

        //}





        //protected void SubirEmprestimoNoBanco(string dataEmprestimo)
        //{
        //    CarregarUsuarioDoBanco();
        //    string sql = "INSERT INTO [dbo].[Emprestimo] (DataEmprestimo, IdUsuario) VALUES (@DataEmprestimo, @IdUsuario)";
        //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
        //    SqlCommand comando = new SqlCommand(sql, conn);

        //    comando.Parameters.AddWithValue("@DataEmprestimo", dataEmprestimoTextBox.Text);
        //    comando.Parameters.AddWithValue("@IdUsuario", codIdUsuario.Text);


        //    conn.Open();
        //    comando.ExecuteReader();
        //    conn.Close();

        //    LimparCampos();
        //}
        //}

        //public void SubirEmprestimoNoBanco(string nomeUsuario)
        //{
        //    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString))
        //    {
        //        string sql = "INSERT INTO dbo.Emprestimo (IdUsuario, DataEmprestimo) " +
        //                     "VALUES ((SELECT IdUsuario FROM dbo.Usuario WHERE NomeUsuario = @NomeUsuario AND EXISTS (SELECT * FROM dbo.Usuario WHERE NomeUsuario = @NomeUsuario)), @DataEmprestimo)";

        //        using (SqlCommand comando = new SqlCommand(sql, conn))
        //        {
        //            comando.Parameters.AddWithValue("@NomeUsuario", nomeUsuario);
        //            comando.Parameters.AddWithValue("@DataEmprestimo", dataEmprestimoTextBox.Text);

        //            conn.Open();
        //            comando.ExecuteNonQuery();
        //            conn.Close();
        //        }
        //    }
        //}



        // public void SubirEmprestimoNoBanco(string codLivro)
        //{
        //     using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString))

        //     {
        //        string sql = "INSERT INTO dbo.Emprestimo (IdUsuario, DataEmprestimo) " +
        //                     "VALUES ((SELECT IdUsuario FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND EXISTS (SELECT * FROM dbo.Usuario WHERE IdUsuario = @IdUsuario)), @DataEmprestimo)";

        //       using (SqlCommand comando = new SqlCommand(sql, conn))
        //       {
        //           comando.Parameters.AddWithValue("@IdUsuario", codLivroLabel.Text);
        //           comando.Parameters.AddWithValue("@DataEmprestimo", dataEmprestimoTextBox.Text);

        //           conn.Open();
        //            comando.ExecuteNonQuery();
        //            conn.Close();

        //         }
        //    }
        // }

    }
}