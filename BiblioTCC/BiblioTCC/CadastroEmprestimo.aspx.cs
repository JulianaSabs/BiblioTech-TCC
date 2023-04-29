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
        protected void SubirUsuarioNoBanco()
        {

        }

        protected void SubirEmprestimoNoBanco()
        {

        }

        protected void SubirItensEmprestimoNoBanco()
        {

        }

    }
}