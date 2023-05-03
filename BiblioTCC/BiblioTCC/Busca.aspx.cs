using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Data;
using System.Data.Sql;
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
    public partial class Busca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Preenche a Repeater com todos os livros
                PreencherListaDeLivros();
                PreencherGenero();
            }
        }
        protected void PreencherGenero()
        {
            string sql = "SELECT IdGenero, GeneroLivro FROM [dbo].[Genero]";
            genSqlDataSource.SelectCommand = sql;

        }


        protected void genDropDownList_PreRender(object sender, EventArgs e)
        {
            genDropDownList.Items.Remove("");
            genDropDownList.Items.Insert(0, "");
        }

        private void PreencherListaDeLivros()
        {
            string sql = "SELECT IdLivro, TituloLivro, AutorLivro, CapaLivro, GeneroLivro FROM [dbo].[Livros]";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            timesRepeater.DataSource = dt;
            timesRepeater.DataBind();
        }

        private void CarregarRepeater()
        {
            string sql = "SELECT IdLivro, TituloLivro, AutorLivro, CapaLivro, GeneroLivro FROM [dbo].[Livros]";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);

            if (!string.IsNullOrEmpty(pesquisarTextBox.Text))
            {
                sql += " WHERE (TituloLivro LIKE '%' + @pesquisa + '%' OR AutorLivro LIKE '%' + @pesquisa + '%' OR GeneroLivro LIKE '%' + @pesquisa + '%')";
            }
            else
            {
                PreencherListaDeLivros();
            }

            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!string.IsNullOrEmpty(pesquisarTextBox.Text))
            {
                cmd.Parameters.AddWithValue("@pesquisa", pesquisarTextBox.Text);
            }
           

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            timesRepeater.DataSource = dt;
            timesRepeater.DataBind();
        }

    
        protected void pesquisarButton_Click(object sender, EventArgs e)
        {
            CarregarRepeater();
        }
    }
}