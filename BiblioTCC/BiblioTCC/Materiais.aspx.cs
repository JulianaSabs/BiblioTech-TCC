using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiblioTCC
{
    public partial class Materiais : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
            {
                PreencherMateriais();
            }
        }

        private void PreencherMateriais()
        {
            string sql = "SELECT IdLivro, TituloLivro, AutorLivro, CapaLivro, GeneroLivro FROM [dbo].[Livros]";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            materiaisRepeater.DataSource = dt;
            materiaisRepeater.DataBind();
        }
    }

}