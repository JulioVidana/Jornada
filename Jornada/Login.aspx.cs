using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    Usuario objUsuario = new Usuario();
    DataSolicitud Datosolicitud = new DataSolicitud();
    string tipousr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["Session_Id_Usuario"] != null)
                Response.Redirect(ResolveUrl("~/Forms/Default.aspx"), true);
        }

    }

    protected void Ingresar_Button_Click(object sender, EventArgs e)
    {
       
        bool logeado = false;
        DataTable dt = Datosolicitud.Login(Usuario_TextBox.Text, Password_TextBox.Text);

        if (dt.Rows.Count != 0)
        {
            int id_usuario = Convert.ToInt32(dt.Rows[0]["id_usuario"]);
            string nombre = dt.Rows[0]["nombre"].ToString();
            logeado = this.Logear(id_usuario, nombre);
            objUsuario = Datosolicitud.DatosUsuario(Session["Session_Nombre_Usuario"].ToString(), tipousr);
            Session["loginUsuario"] = objUsuario;
        }
        else
        {
            Usuario_TextBox.Text = "";
            Password_TextBox.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mensaje", "despliega_aviso('warning','Usuario ó Contraseña Incorrectos...','El sistema valida letras mayúsculas y minúsculas','txt_btn_no','txt_btn_si','txt_msj_no','txt_msj_si');", true);
        }

        if (logeado)
            Response.Redirect(ResolveUrl("~/Forms/Default.aspx"), true);
    }


    private bool Logear(int id_usuario, string nombre)
    {
        Session["Session_Id_Usuario"] = id_usuario;
        Session["Session_Nombre_Usuario"] = nombre;
        return true;
    }
}