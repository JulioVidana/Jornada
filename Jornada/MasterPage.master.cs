using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    Usuario objUsuario = new Usuario();
    DataSolicitud Datosolicitud = new DataSolicitud();
    string tipousr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["Session_Id_Usuario"] != null)
                {
                    Session["Session_String_Conneccion"] = "";
                    CreateNavBar();
                    objUsuario = Datosolicitud.DatosUsuario(Session["Session_Nombre_Usuario"].ToString(), tipousr);
                    Session["loginUsuario"] = objUsuario;
                    lblUsuario.Text = Session["Session_Nombre_Usuario"].ToString();
                    LblArea.Text = objUsuario.Area;
                }
                else
                    Response.Redirect(ResolveUrl("~/Login.aspx"), true);



            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "mensaje", "despliega_aviso('warning','Error','" + ex.Message.Replace("'", "") + "','txt_btn_no','txt_btn_si','txt_msj_no','txt_msj_si');", true);
            }
        }
    }


    public void CreateNavBar()
    {

        int idusuario = Convert.ToInt32(Session["Session_Id_Usuario"].ToString());
        // LEEMOS ACCESOS POR USUARIO
        DataTable dtAccesosUsuarios = Datosolicitud.Menu(idusuario);

        string html = "";
        csFileReader fileReader = new csFileReader(Request.PhysicalApplicationPath);
        StringBuilder strHtml = new StringBuilder();

        bool first = true;
        int LengthSistema = 0;
        int countRows = dtAccesosUsuarios.Rows.Count;
        for (int x = 0; x < countRows; x++)
        {
            string nombreNivel = "padre";
            string nombreAcceso = dtAccesosUsuarios.Rows[x]["nombre_acceso"].ToString().Trim();
            string url = dtAccesosUsuarios.Rows[x]["url"].ToString().Trim();
            int strNodoLength = dtAccesosUsuarios.Rows[x]["orden"].ToString().Trim().Length;
            string icono = dtAccesosUsuarios.Rows[x]["imagen_acceso"].ToString().Trim(); //JCVG

            int row = x + 1;

            if (first || dtAccesosUsuarios.Rows[0]["orden"].ToString().Trim().Length == strNodoLength)
                nombreNivel = "padre";
            else if (row < countRows && dtAccesosUsuarios.Rows[row]["orden"].ToString().Trim().Length > strNodoLength)
                nombreNivel = "hijoPadre";
            else
                nombreNivel = "hijo";

            if (nombreNivel == "padre")
            {
                if (strNodoLength < LengthSistema)
                {
                    //strHtml.AppendLine("</ul></li>");
                }


                string strRowPadre = fileReader.ReadFile("Padre");
                Dictionary<string, string> values = new Dictionary<string, string>
                    {
                        {"{Href}", url}, //JCVG
                         {"{icono}", icono}, //JCVG
                        {"{Nombre}", nombreAcceso},
                    };

                string strPadre = fileReader.ReadText(strRowPadre, values);
                strHtml.AppendLine(strPadre);
            }
            else if (nombreNivel == "hijoPadre")
            {
                //string strTemplate = fileReader.ReadFile("HijoPadre");
                //Dictionary<string, string> values = new Dictionary<string, string>
                //    {
                //        {"{Nombre}", nombreAcceso},
                //    };

                //string strHijoPadre = fileReader.ReadText(strTemplate, values);
                //strHtml.AppendLine(strHijoPadre);
            }
            else if (nombreNivel == "hijo")
            {
                //objUsuario.TipoUsuario = dtAccesosUsuarios.Rows[x]["nombre_acceso"].ToString().Trim();
                tipousr = dtAccesosUsuarios.Rows[x]["nombre_acceso"].ToString().Trim();
                //if (strNodoLength < LengthSistema)
                //    strHtml.AppendLine("</ul></li>");

                //string strTemplate = fileReader.ReadFile("Hijo");
                //Dictionary<string, string> values = new Dictionary<string, string>
                //    {
                //        {"{Href}", url},
                //        {"{NombreHijo}", nombreAcceso},
                //    };

                //string strHijo = fileReader.ReadText(strTemplate, values);
                //strHtml.AppendLine(strHijo);
            }

            LengthSistema = strNodoLength;
            first = false;
        }

        html = strHtml.ToString() + "</li>";

        fileReader = null;
        strHtml = null;

        liNav.InnerHtml = html;
    }
    protected void logOut(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Session.Clear();
        Session.Abandon();
        Session["Session_Id_Usuario"] = null;
        Session["loginUsuario"] = null;
        //Page_Load(sender, e);
        Response.Redirect("~/Login.aspx");
    }
}
