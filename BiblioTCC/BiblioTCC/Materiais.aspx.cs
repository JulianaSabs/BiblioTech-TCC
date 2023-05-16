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
    public partial class Materiais : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Preenche a Repeater com todos os livros
                PreencherListaDeMateriais();
             
            }
        }

        private void PreencherListaDeMateriais()
        {
            string sql = "SELECT IdMaterial, tipoMaterial, unidadesMaterial, imagemMaterial FROM [dbo].[Material]";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            materiaisRepeater.DataSource = dt;
            materiaisRepeater.DataBind();
        }
    }
}