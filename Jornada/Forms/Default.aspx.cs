using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;

public partial class Forms_Default : System.Web.UI.Page
{
    DataSolicitud Datosolicitud = new DataSolicitud();
    Usuario objUsuario = new Usuario();

  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objUsuario = (Usuario)Session["loginUsuario"];
            if (objUsuario == null)
            {
                logOut();
            }
            else
            {
                EscondeBotones();
                int idusuario = Convert.ToInt32(objUsuario.IdUsuario);
                // LEEMOS ACCESOS POR USUARIO
                DataTable dtAccesosUsuarios = Datosolicitud.Menu(idusuario);
                int countRows = dtAccesosUsuarios.Rows.Count;
                for (int x = 0; x < countRows; x++)
                {

                    string id_acceso = dtAccesosUsuarios.Rows[x]["id_acceso"].ToString().Trim();

                    if (id_acceso == "2")
                        div_btn_capturas.Visible = true;
                    if (id_acceso == "3")
                        div_btn_reportes.Visible = true;
                    if (id_acceso == "4")
                        div_btn_usuarios.Visible = true;
                    if (id_acceso == "5")
                        div_tbn_accessos.Visible = true;
                    if (id_acceso == "8")
                        div_btn_jornadas.Visible = true;
                    if (id_acceso == "1008")
                        div_btn_seguimiento.Visible = true;
                    if (id_acceso == "1009")
                        div_btn_repjornadas.Visible = true;
                    if (id_acceso == "1010")
                        div_btn_repmunicipios.Visible = true;


                }

                   
            }

        }
    }

    protected void EscondeBotones()
    {
        div_btn_capturas.Visible = false;
        div_btn_reportes.Visible = false;
        div_btn_usuarios.Visible = false;
        div_tbn_accessos.Visible = false;
        div_btn_jornadas.Visible = false;
    }

    protected void logOut()
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