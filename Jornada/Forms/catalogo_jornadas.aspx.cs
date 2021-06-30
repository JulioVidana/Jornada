using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_catalogo_jornadas : System.Web.UI.Page
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
                Aviso("warning", "No tienes Permisos","");
                logOut();
            }
            else
            {
                if (objUsuario.TipoUsuario == "Propietario")//CORREGIR
                {
                    Hid_IDJornada.Value = "";
                    DD_Colonia.DataSource = Datosolicitud.llenaCombos("Colonias_Todas", "1");
                    DD_Colonia.DataBind();
                    BinData();
                    txt_Fecha.Text = DateTime.Now.ToString("dd/MM/yyyy");



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
        string idcolonia = row.Cells[9].Text;
        string fecha = row.Cells[10].Text;

        if (e.CommandName == "Editar")
        {
           
            Hid_IDJornada.Value = row.Cells[1].Text;
            Jornada_TextBox.Text = Server.HtmlDecode(row.Cells[2].Text);
            txt_anfitrion.Text = Server.HtmlDecode(row.Cells[3].Text);
            if(idcolonia == "&nbsp;") { DD_Colonia.SelectedValue = "0"; } else { DD_Colonia.SelectedValue = idcolonia; }
            txt_direccion.Text = Server.HtmlDecode(row.Cells[5].Text);
            txt_telefono.Text = Server.HtmlDecode(row.Cells[6].Text);
            Activo_RadioButtonList.SelectedValue = row.Cells[8].Text;
            if (fecha == "&nbsp;") { txt_Fecha.Text = DateTime.Now.ToString("dd/MM/yyyy"); } else { txt_Fecha.Text = fecha; }
            
            Btn_Agregar.Enabled = false; Btn_Agregar.Visible = false;
            Btn_Actualizar.Enabled = true; Btn_Actualizar.Visible = true;
            Btn_Borrar.Enabled = true; Btn_Borrar.Visible = true;
            btn_Limpiar.Visible = true;

        }
        else
        {
           
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        BinData();
    }



    protected void Nuevo_Registro(object sender, EventArgs e) //Crear y actualizar Regisitro
    {
        try
        {

            if (Jornada_TextBox.Text == "" || Activo_RadioButtonList.SelectedIndex == -1)
            {
                string msj = "";
                if (Jornada_TextBox.Text == "")
                    msj = "[Jornada] ";
                if (Activo_RadioButtonList.SelectedIndex == -1)
                    msj = msj + " [Estatus] ";

                Aviso("warning", "Falta Llenar Campos:", msj);
            }
            else
            {
                
                Dictionary<string, string> datosUsuario = new Dictionary<string, string>();
                datosUsuario.Add("Jornada", Jornada_TextBox.Text);
                datosUsuario.Add("id_jornada", Hid_IDJornada.Value);
                datosUsuario.Add("host", txt_anfitrion.Text);
                datosUsuario.Add("id_colonia", DD_Colonia.SelectedValue);
                datosUsuario.Add("direccion", txt_direccion.Text);
                datosUsuario.Add("telefono", txt_telefono.Text);
                datosUsuario.Add("fecha", txt_Fecha.Text);
                datosUsuario.Add("activo", Activo_RadioButtonList.SelectedItem.Value);

                if (Hid_IDJornada.Value == "")//NUEVO REGISTRO
                {
                        datosUsuario.Add("Funcion", "NEW_jor");
                        Datosolicitud.AgregaJornada(datosUsuario);
                        Aviso("success", "Registro Exitoso :)","");
                        LimpiarForm(null, null);

                }
                else //ACTUALIZA REGISTRO
                {
                        datosUsuario.Add("Funcion", "UP_jor");
                        Datosolicitud.AgregaJornada(datosUsuario);
                        Aviso("success", "Se Actualizó Registro :)","");
                        LimpiarForm(null, null);
                }

                
                BinData();
            }

        }
        catch (Exception ex)
        {
            //string mensaje = ex.Message.Replace("'", "").Replace("\r","").Replace("\n","");
            string mensaje = Server.HtmlDecode(ex.Message);
            Aviso("warning", "ups!", mensaje);
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
    }


    protected void Aviso(string tipo, string msj1, string msj2)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "mensaje", "despliega_aviso('" + tipo + "','" + msj1 + "','" + msj2 + "','txt_btn_no','txt_btn_si','txt_msj_no','txt_msj_si');", true);
    }


    protected void Borra_Registro(object sender, EventArgs e)
    {
        //Datosolicitud.BorraUsuario(Hid_IDJornada.Value);
        LimpiarForm(null, null);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        BinData();
        Response.Redirect(Request.RawUrl);
    }

    protected void LimpiarForm(object sender, EventArgs e)
    {
       Jornada_TextBox.Text = string.Empty;
        Hid_IDJornada.Value = "";
        txt_anfitrion.Text = string.Empty;
        txt_direccion.Text = string.Empty;
        txt_telefono.Text = string.Empty;
        txt_Fecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        DD_Colonia.ClearSelection();
        Activo_RadioButtonList.ClearSelection();
        Btn_Agregar.Enabled = true; Btn_Agregar.Visible = true;
        Btn_Actualizar.Enabled = false; Btn_Actualizar.Visible = false;
        Btn_Borrar.Enabled = false; Btn_Borrar.Visible = false;
        btn_Limpiar.Visible = false;
    }

    protected void BinData()
    {
        Listado_GridView.Columns[8].Visible = true;
        Listado_GridView.Columns[9].Visible = true;

        Listado_GridView.DataSource = Datosolicitud.llenaJornada();
        Listado_GridView.DataBind();

        Listado_GridView.Columns[8].Visible = false;
        Listado_GridView.Columns[9].Visible = false;

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