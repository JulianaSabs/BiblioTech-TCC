using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BiblioTCC
{
    public partial class Relatorio : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Iniciar()
        {
            PreencherStatusDropDownList();
        }

        protected void PreencherStatusDropDownList()
        {
            string sql = "SELECT IdStatus, Status FROM [dbo].[Status]";
            statusSqlDataSource.SelectCommand = sql;
        }

        protected void statusDropDownList_PreRender(object sender, EventArgs e)
        {
            statusDropDownList.Items.Remove("");
            statusDropDownList.Items.Insert(0, "");
        }

        protected void tipoDoRelatorioDropDownList_PreRender(object sender, EventArgs e)
        {
            tipoDoRelatorioDropDownList.Items.Remove("");
            tipoDoRelatorioDropDownList.Items.Insert(0, "");
        }

        protected void pesquisarButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dataInicialTextBox.Text))
            {
                if (!string.IsNullOrEmpty(dataFinalTextBox.Text))
                {
                    if (!string.IsNullOrEmpty(tipoDoRelatorioDropDownList.SelectedValue))
                    {
                        
                            CarregarRelatorioGridView();
                            //rightDiv.Visible = true;
                            pesquisaDiv.Visible = true;
                        
                    }
                    else
                    {
                        alertaLabel.Text = @"É necessário preencher o campo ""Tipo do Relatório"" antes de prosseguir.";
                    }
                }
                else
                {
                    alertaLabel.Text = @"É necessário preencher o campo ""Data Final"" antes de prosseguir.";
                }
            }
            else
            {
                alertaLabel.Text = @"É necessário preencher o campo ""Data Inicial"" antes de prosseguir.";
            }
        }

        private string FormatarData(string data)
        {
            string dataTexto = "";

            if (data.Length >= 10)
            {
                dataTexto = data.Substring(6, 4) + "-" + data.Substring(3, 2) + "-" + data.Substring(0, 2);
            }

            return dataTexto;
        }

        private void CarregarRelatorioGridView()
        {
            alertaLabel.Text = "";

    
            string sql = "";

            switch (tipoDoRelatorioDropDownList.SelectedValue)
            {
                case "1":
                    sql = "SELECT u.IdUsuario, u.NomeUsuario as Nome, u.EmailUsuario As Email, FORMAT(e.DataEmprestimo, 'dd/MM/yyyy') as [Data de Empréstimo], s.Status AS Status FROM dbo.Usuario u LEFT JOIN dbo.Emprestimo e ON u.IdUsuario = e.IdUsuario LEFT JOIN dbo.Status s ON s.IdStatus = e.Status WHERE e.Status in (2)";

                    break;
                case "2":
                    sql = "SELECT u.IdUsuario, u.NomeUsuario as Nome, u.EmailUsuario As Email, FORMAT(e.DataEmprestimo, 'dd/MM/yyyy') as [Data de Empréstimo], s.Status AS Status FROM dbo.Usuario u LEFT JOIN dbo.Emprestimo e ON u.IdUsuario = e.IdUsuario LEFT JOIN dbo.Status s ON s.IdStatus = e.Status WHERE e.Status in (1)";

                    break;
                case "3":
                    sql = "SELECT u.IdUsuario, u.NomeUsuario as Nome, u.EmailUsuario As Email, FORMAT(e.DataEmprestimo, 'dd/MM/yyyy') as [Data de Empréstimo], s.Status AS Status FROM dbo.Usuario u LEFT JOIN dbo.Emprestimo e ON u.IdUsuario = e.IdUsuario LEFT JOIN dbo.Status s ON s.IdStatus = e.Status WHERE e.Status in (3)";

                    break;
            }

            

            pesquisaSqlDataSource.SelectCommand = sql;
        }

        protected void VerificarSePossuiMaisDeUmaLinha(string sql)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlCommand comando = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader resultadoSql = comando.ExecuteReader();

            if (resultadoSql.Read())
            {
                if (!string.IsNullOrEmpty(Convert.ToString(resultadoSql["ID"])))
                {
                    extrairButton.Visible = true;
                }
            }

            conn.Close();
        }

        protected void extrairButton_Click(object sender, EventArgs e)
        {
           
        }

        protected void exportarExcelLinkButton_Click(object sender, EventArgs e)
        {
           
        }

    }
}