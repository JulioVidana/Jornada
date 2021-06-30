using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_catalogo_usuarios : System.Web.UI.Page
{
    
    DataSolicitud Datosolicitud = new DataSolicitud();
    Usuario objUsuario = new Usuario();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(Session["Session_Id_Usuario"]) <= 0)
        //    Response.Redirect(ResolveUrl("~/Login.aspx"), true);

        if (!Page.IsPostBack)
        {
            objUsuario = (Usuario)Session["loginUsuario"];
            if (objUsuario == null)
            {
                Aviso("warning", "No tienes Permisos");
                logOut();
            }
            else
            {
                if (objUsuario.TipoUsuario == "Propietario")//CORREGIR
                {
                    Hid_IDUsuario.Value = "";
                    DD_Area.DataSource = Datosolicitud.llenarAreas();
                    DD_Area.DataBind();
                    //DD_Usrs.DataBind();
                    BinData();
                    //lee_datos();//contraseñas


                }
                else
                {
                    logOut();
                }
            }

          
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);

        }
    }

    protected void Listado_GridView_PreRender(object sender, EventArgs e)
    {
        if (Listado_GridView.Rows.Count > 0)
        {
            Listado_GridView.UseAccessibleHeader = true;
            Listado_GridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            Listado_GridView.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void Listado_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = Listado_GridView.Rows[index];

        if (e.CommandName == "Editar")
        {

            Hid_IDUsuario.Value = row.Cells[6].Text;
            Nombre_TextBox.Text = Server.HtmlDecode(row.Cells[1].Text);
            Correo_TextBox.Text = Server.HtmlDecode(row.Cells[2].Text);
            if (Server.HtmlDecode(row.Cells[7].Text) != " ") { DD_Area.SelectedValue = row.Cells[7].Text; } else { DD_Area.SelectedValue = "0"; }
            Activo_RadioButtonList.SelectedValue = row.Cells[8].Text;
            Btn_Agregar.Enabled = false; Btn_Agregar.Visible = false;
            Btn_Actualizar.Enabled = true; Btn_Actualizar.Visible = true;
            Btn_Borrar.Enabled = true; Btn_Borrar.Visible = true;
            btn_Limpiar.Visible = true;

        }
        else
        {
            if (e.CommandName == "Pass")
            {
                txt_usurio_pass.Text = Server.HtmlDecode(row.Cells[1].Text);
                ID_Usuario_Hidden.Value = row.Cells[6].Text;
                Contraseña_Actual_TextBox.Text = "";
                Password1_TextBox.Text = "";
                Password2_TextBox.Text = "";
                lee_datos(ID_Usuario_Hidden.Value);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass", "$('#Pass_Div').modal('show');", true);
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        BinData();
    }

   

    protected void Nuevo_Registro(object sender, EventArgs e) //Crear y actualizar Regisitro
    {
        Dictionary<string, string> datosUsuario = new Dictionary<string, string>();
        datosUsuario.Add("Nombre", Nombre_TextBox.Text);
        datosUsuario.Add("Correo", Correo_TextBox.Text);
        datosUsuario.Add("idArea", DD_Area.SelectedValue);
        
        datosUsuario.Add("id_usuario", Hid_IDUsuario.Value);

        if (Hid_IDUsuario.Value == "")//NUEVO REGISTRO
        {
            if (Nombre_TextBox.Text != "" && Correo_TextBox.Text !="" && Activo_RadioButtonList.SelectedIndex != -1)
            {
                datosUsuario.Add("activo", Activo_RadioButtonList.SelectedItem.Value);
                datosUsuario.Add("Funcion", "guarda");
                Datosolicitud.AgregaUsuario(datosUsuario);
                Aviso("success", "Registro Exitoso :)");
                LimpiarForm(null, null);
            } else{ Aviso("warning", "Faltan Campos");}
           
        }
        else //ACTUALIZA REGISTRO
        {
            if (Nombre_TextBox.Text != "" && Correo_TextBox.Text != "" && Activo_RadioButtonList.SelectedIndex != -1)
            {
                datosUsuario.Add("Funcion", "Actualiza");
                datosUsuario.Add("activo", Activo_RadioButtonList.SelectedItem.Value);
                Datosolicitud.AgregaUsuario(datosUsuario);
                Aviso("success", "Se Actualizó Registro :)");
                LimpiarForm(null, null);
            }
            else{ Aviso("warning", "Faltan Campos");}
            
        }
        

        
       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        BinData();

    }


    protected void Aviso(string tipo, string msj1)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "mensaje", "despliega_aviso('" + tipo + "','" + msj1 + "','','txt_btn_no','txt_btn_si','txt_msj_no','txt_msj_si');", true);
    }


    protected void Borra_Registro(object sender, EventArgs e)
    {
        Datosolicitud.BorraUsuario(Hid_IDUsuario.Value);
        LimpiarForm(null, null);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        BinData();
        Response.Redirect(Request.RawUrl);
    }


    private void lee_datos(string idUsuario)//Lee contraseña
    {

        Data objData = new Data();
        objData.OpenConnection();

        SqlParameter[] Params = new SqlParameter[1];
        Params[0] = new SqlParameter("@id_usuario", idUsuario);
        Params[0].SqlDbType = SqlDbType.Int;

        DataTable dt = objData.ExecuteSPQuery(Params, "spr_portal_lee_contraseña_usuario");

        if (dt.Rows.Count != 0)
        {
            try { Contraseña_Actual_TextBox.Text = csSecurityHandler.Desencriptar(dt.Rows[0]["password"].ToString()); } catch (Exception) { }
        }
    }

    protected void Cambiar_contra(object sender, EventArgs e)//cambiar contraseñas
    {
        Data objData = new Data();
        string strStoreProcedure = "spr_portal_realiza_cambio_contraseña";

        objData.OpenConnection();

        SqlParameter[] Params = new SqlParameter[2];
        Params[0] = new SqlParameter("@id_usuario", ID_Usuario_Hidden.Value);
        Params[0].SqlDbType = SqlDbType.Int;
        Params[1] = new SqlParameter("@password", csSecurityHandler.Encriptar(Password1_TextBox.Text));
        Params[1].SqlDbType = SqlDbType.NVarChar;

        DataTable dt = objData.ExecuteSPQuery(Params, strStoreProcedure);

        Contraseña_Actual_TextBox.Text = Password1_TextBox.Text;

        Aviso("success", "Se Cambió Contraseña :)");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass", "$('#Pass_Div').modal('hide');", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        BinData();

    }

    protected void LimpiarForm(object sender, EventArgs e)
    {
        Nombre_TextBox .Text = string.Empty;
        Correo_TextBox.Text = string.Empty;
        DD_Area.ClearSelection();
        Activo_RadioButtonList.ClearSelection();
        Hid_IDUsuario.Value = "";
        Btn_Agregar.Enabled = true; Btn_Agregar.Visible = true;
        Btn_Actualizar.Enabled = false; Btn_Actualizar.Visible = false;
        Btn_Borrar.Enabled = false; Btn_Borrar.Visible = false;
        btn_Limpiar.Visible = false;
    }

    protected void BinData()
    {
        Listado_GridView.Columns[6].Visible = true;
        Listado_GridView.Columns[7].Visible = true;
        Listado_GridView.Columns[8].Visible = true;

        Listado_GridView.DataSource = Datosolicitud.llenarUsuarios();
        Listado_GridView.DataBind();

        Listado_GridView.Columns[6].Visible = false;
        Listado_GridView.Columns[7].Visible = false;
        Listado_GridView.Columns[8].Visible = false;
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