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
               
            }
        }

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
        protected void SubirUsuarioNoBanco(string nome)
        {
            
            string sql = "INSERT INTO [dbo].[Usuario] (NomeUsuario, EmailUsuario) VALUES (@NomeUsuario, @EmailUsuario)";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlCommand comando = new SqlCommand(sql, conn);

            comando.Parameters.AddWithValue("@NomeUsuario", nomeTextBox.Text);
            comando.Parameters.AddWithValue("@EmailUsuario", emailTextBox.Text);
            

            conn.Open();
            comando.ExecuteReader();
            conn.Close();

            LimparCampos();
          

        }

        public void RegistrarEmprestimo(string nomeUsuario)
        {
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
            string queryEmprestimo = "INSERT INTO Emprestimo (IdUsuario, DataEmprestimo) VALUES (@IdUsuario, @DataEmprestimo); SELECT SCOPE_IDENTITY();";
            SqlCommand commandEmprestimo = new SqlCommand(queryEmprestimo, conn);
            commandEmprestimo.Parameters.AddWithValue("@IdUsuario", idUsuario);
            commandEmprestimo.Parameters.AddWithValue("@DataEmprestimo", dataEmprestimoTextBox.Text);
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
           

        }

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