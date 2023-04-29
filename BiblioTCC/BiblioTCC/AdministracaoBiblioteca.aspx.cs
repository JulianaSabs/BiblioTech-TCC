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
    public partial class AdminitracaoBiblioteca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregartTextBox();
            }
        }


        protected void CarregartTextBox()
        {
            string sql = "SELECT NomeBiblio, EnderecoBiblio, MultaMaximoDias, TelefoneBiblio, ValorMulta FROM [dbo].[Biblioteca]";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlCommand comando = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataReader resultado = comando.ExecuteReader();
            if (resultado.Read())
            {
                bibliotecaTextBox.Text = Convert.ToString(resultado["NomeBiblio"]);
                enderecoTextBox.Text = Convert.ToString(resultado["EnderecoBiblio"]);
                classeDropDownList.SelectedValue = Convert.ToString(resultado["MultaMaximoDias"]);
                multaTextBox.Text = Convert.ToString(resultado["ValorMulta"]);
                telefoneTextBox.Text = Convert.ToString(resultado["TelefoneBiblio"]);
                
            }

            conn.Close();
        }


       
    }
}