using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class Forms_Seguimiento : System.Web.UI.Page
{
    DataSolicitud Datosolicitud = new DataSolicitud();
    Usuario objUsuario = new Usuario();
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> List_Apoyos = "392, 393".Replace(" ", "").Split(',').ToList<string>();
        int inass = List_Apoyos.Count();

        if (!IsPostBack)
        {
            objUsuario = (Usuario)Session["loginUsuario"];
            if (objUsuario == null)
            {
                Aviso("warning", "No tienes Permisos", "");
                logOut();
            }
            else
            {
                try
                {


                    //if (objUsuario.TipoUsuario == "Editor")
                    //{

                        lblArea.Text = objUsuario.Area;
                        Hd_TipoUsr.Value = objUsuario.TipoUsuario;
                        Hd_Idarea.Value = objUsuario.ID_Area;
                       
                        DD_Colonia.DataSource = Datosolicitud.llenaCombos("Colonias", "1");
                        DD_Colonia.DataBind();
                        DD_Jornadas.DataSource = Datosolicitud.llenaCombos("Jornadas2", "1");
                        DD_Jornadas.DataBind();
                        BinData();


                        if (objUsuario.ID_Area == "1")//Atención Ciudadana
                        {

                            DD_Apoyo_multi.DataSource = Datosolicitud.llenaCombos("AtnApoyos", objUsuario.ID_Area);
                            DD_Apoyo_multi.DataBind();
                        }
                        else
                        {
                            DD_Apoyo_multi.DataSource = Datosolicitud.llenaCombos("ApoyosGeneral", objUsuario.ID_Area);
                            DD_Apoyo_multi.DataBind();
                        }


                    //}
                }
                catch (Exception ex)
                {
                    //string mensaje = ex.Message.Replace("'", "").Replace("\r","").Replace("\n","");
                    string mensaje = Server.HtmlDecode(ex.Message);
                    Aviso("warning", "ups!", mensaje);

                }



            }
        }

    }

    protected void Buscar(object sender, EventArgs e)
    {
      DataTable dt = Datosolicitud.llenarSolicitudes(Hd_Idarea.Value, "BuscarXJornada", DD_Jornadas.SelectedValue);

        GridSol.Columns[4].Visible = true;
        GridSol.Columns[11].Visible = true;
        GridSol.Columns[12].Visible = true;
        GridSol.Columns[13].Visible = true;
        GridSol.Columns[14].Visible = true;
        GridSol.Columns[15].Visible = true;
        GridSol.Columns[16].Visible = true;
        GridSol.Columns[17].Visible = true;
        GridSol.Columns[18].Visible = true;
        GridSol.Columns[19].Visible = true;
        GridSol.Columns[20].Visible = true;
        GridSol.Columns[21].Visible = true;

        GridSol.DataSource = dt;
        GridSol.DataBind();

        GridSol.Columns[4].Visible = false;

        GridSol.Columns[11].Visible = false;
        GridSol.Columns[12].Visible = false;
        GridSol.Columns[13].Visible = false;
        GridSol.Columns[14].Visible = false;
        GridSol.Columns[15].Visible = false;
        GridSol.Columns[16].Visible = false;
        GridSol.Columns[17].Visible = false;
        GridSol.Columns[18].Visible = false;
        GridSol.Columns[19].Visible = false;
        GridSol.Columns[20].Visible = false;
        GridSol.Columns[21].Visible = false;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        
    }
    protected void Soli_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {


            LimpiarForm();
            btn_Guardar.Text = "Actualizar";
            btn_Guardar.CssClass = "btn btn-w-m btn-success";
            lbl_registro.Text = "<i  class='mdi text-danger mdi-pencil'></i> Actualizar Datos";
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridSol.Rows[index];


            if (e.CommandName == "editar")
            {

                //llenar formulario
                txt_Nombre.Text = Server.HtmlDecode(row.Cells[11].Text);
                txt_Apaterno.Text = Server.HtmlDecode(row.Cells[12].Text);
                txt_Amaterno.Text = Server.HtmlDecode(row.Cells[13].Text);
                if (row.Cells[4].Text == "0") { txt_Celular.Text = ""; } else { txt_Celular.Text = row.Cells[4].Text; }
                txt_Direccion.Text = Server.HtmlDecode(row.Cells[2].Text);
                DD_Colonia.SelectedValue = row.Cells[14].Text;
                txt_email.Text = Server.HtmlDecode(row.Cells[5].Text);
                //txt_Jornada.Text = row.Cells[20].Text;
                txt_FechaNacimiento.Text = Convert.ToDateTime(row.Cells[17].Text).ToString("dd/MM/yyyy");
                //txt_FechaNacimiento.Text = Convert.ToDateTime(row.Cells[16].Text).ToShortDateString();
                DD_Sexo.SelectedValue = row.Cells[18].Text;
                ID_Persona_Hidden.Value = row.Cells[21].Text;
                List<string> List_Apoyos = row.Cells[19].Text.Replace(" ", "").Split(',').ToList<string>();

                foreach (string item in List_Apoyos)
                    DD_Apoyo_multi.Items.FindByValue(item).Selected = true;


                //DD_Apoyo.SelectedValue = row.Cells[18].Text;
                //DD_TipoApoyo.SelectedValue = row.Cells[19].Text;
                btn_Borrar.Enabled = true;
                btn_Borrar.Visible = true;


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Registro2", "$('#Registro_Div').modal('show');", true);
            }
            else
            {
                if (e.CommandName == "segui")
                {
                    ID_Persona_Hidden_segui.Value = row.Cells[21].Text;
                    lblNombre2.Text = Server.HtmlDecode(row.Cells[1].Text);
                    text_asunto2.Text = Server.HtmlDecode(row.Cells[6].Text);
                    RB_estatus.SelectedValue = row.Cells[15].Text;
                    //if (row.Cells[15].Text == "&nbsp;") { txtObservacion.Text = ""; } else { txtObservacion.Text = row.Cells[15].Text; }
                    txtObservacion.Text = Server.HtmlDecode(row.Cells[16].Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Registro3", "$('#Seguimiento_Div').modal('show');", true);
                }
            }

        }
        catch (Exception ex)
        {
            //string mensaje = ex.Message.Replace("'", "").Replace("\r","").Replace("\n","");
            string mensaje = Server.HtmlDecode(ex.Message);
            Aviso("warning", "ups!", mensaje);

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        //BinData();

    }
    protected void GridSol_PreRender(object sender, EventArgs e)
    {
        if (GridSol.Rows.Count > 0)
        {
            GridSol.UseAccessibleHeader = true;
            GridSol.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridSol.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void Nuevo_Registro(object sender, EventArgs e) //Actualizar Regisitro
    {

        try
        {

            if (DD_Apoyo_multi.SelectedValue == "" || DD_Colonia.SelectedValue == "0" || DD_Sexo.SelectedValue == "0")
            {
                string msj = "";
                if (DD_Apoyo_multi.SelectedValue == "")
                    msj = "[Apoyo] ";
                if (DD_Colonia.SelectedValue == "0")
                    msj = msj + " [Colonia] ";
                if (DD_Sexo.SelectedValue == "0")
                    msj = msj + " [Sexo] ";

                Aviso("warning", "Selecciona opción:", msj);
            }
            else
            {
                string id_persona;
                Dictionary<string, string> datosPersona = new Dictionary<string, string>();
                datosPersona.Add("Nombre", txt_Nombre.Text.ToUpper());
                datosPersona.Add("aPaterno", txt_Apaterno.Text.ToUpper());
                datosPersona.Add("aMaterno", txt_Amaterno.Text.ToUpper());
                datosPersona.Add("Celular", txt_Celular.Text);
                datosPersona.Add("Direccion", txt_Direccion.Text.ToUpper());
                datosPersona.Add("Colonia", DD_Colonia.SelectedValue);
                datosPersona.Add("email", txt_email.Text.ToUpper());
                datosPersona.Add("Asunto", "");
                datosPersona.Add("id_area", Hd_Idarea.Value);
                datosPersona.Add("edad", txt_FechaNacimiento.Text);
                datosPersona.Add("sexo", DD_Sexo.SelectedValue);
                //datosPersona.Add("id_jornada", DD_Jornadas.SelectedValue);
                

                if (btn_Guardar.Text == "Actualizar")
                {
                    id_persona = ID_Persona_Hidden.Value;
                    datosPersona.Add("idPersona", id_persona);
                    Datosolicitud.ActualizaPersona(datosPersona);

                    Datosolicitud.BorraApoyos(id_persona);
                    foreach (int i in DD_Apoyo_multi.GetSelectedIndices())
                    {
                        string ApoyoId = DD_Apoyo_multi.Items[i].Value;
                        //inserta apoyos
                        Datosolicitud.InsertaApoyos(id_persona, ApoyoId);
                    }

                    Aviso("success", "Se Actualizó Registro :)", "");
                }
                


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Registro", "$('#Registro_Div').modal('hide');", true);
                LimpiarForm();

            }


        }
        catch (Exception ex)
        {
            //string mensaje = ex.Message.Replace("'", "").Replace("\r","").Replace("\n","");
            string mensaje = Server.HtmlDecode(ex.Message);
            Aviso("warning", "ups!", mensaje);

        }


        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        BinData();

    }
    protected void Borra_Registro(object sender, EventArgs e)
    {

        Datosolicitud.BorraPerona(ID_Persona_Hidden.Value);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Registro2", "$('#Registro_Div').modal('hide');", true);
        LimpiarForm();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        BinData();
        Response.Redirect(Request.RawUrl);
        // Aviso("success", "¡Registro Borrado!");
    }
    protected void UPSeguimiento(object sender, EventArgs e)
    {
        try
        {

            if (RB_estatus.SelectedIndex != -1)
            {
                Datosolicitud.ActualizaSeguimiento(ID_Persona_Hidden_segui.Value, RB_estatus.SelectedItem.Value, txtObservacion.Text);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Registro2", "$('#Seguimiento_Div').modal('hide');", true);
                LimpiarForm();

                // Response.Redirect(Request.RawUrl);
                Aviso("success", "¡Se Guardó Seguimiento!", "");
            }
            else
            {
                Aviso("warning", "Selecciona opción para validar", "");
            }

        }
        catch (Exception ex)
        {
            //string mensaje = ex.Message.Replace("'", "").Replace("\r","").Replace("\n","");
            string mensaje = Server.HtmlDecode(ex.Message);
            Aviso("warning", "ups!", mensaje);

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        BinData();
    }
    protected void Pintate1(object sender, GridViewRowEventArgs e)//colorea la columna de estatus
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell cell = e.Row.Cells[8];
            string deste = cell.Text;
            switch (deste)
            {
                case "PENDIENTE":
                    cell.BackColor = Color.Yellow;
                    break;
                case "NEGATIVO":
                    cell.BackColor = Color.Red;
                    break;
                case "POSITIVO":
                    cell.BackColor = Color.Lime;
                    break;
            }


        }
    }
    protected void BinData()
    {
        GridSol.Columns[4].Visible = true;
       
        GridSol.Columns[11].Visible = true;
        GridSol.Columns[12].Visible = true;
        GridSol.Columns[13].Visible = true;
        GridSol.Columns[14].Visible = true;
        GridSol.Columns[15].Visible = true;
        GridSol.Columns[16].Visible = true;
        GridSol.Columns[17].Visible = true;
        GridSol.Columns[18].Visible = true;
        GridSol.Columns[19].Visible = true;
        GridSol.Columns[20].Visible = true;
        GridSol.Columns[21].Visible = true;

        GridSol.DataSource = Datosolicitud.llenarSolicitudes(Hd_Idarea.Value, "BuscarXJornada", DD_Jornadas.SelectedValue);
        GridSol.DataBind();

        GridSol.Columns[4].Visible = false;
       
        GridSol.Columns[11].Visible = false;
        GridSol.Columns[12].Visible = false;
        GridSol.Columns[13].Visible = false;
        GridSol.Columns[14].Visible = false;
        GridSol.Columns[15].Visible = false;
        GridSol.Columns[16].Visible = false;
        GridSol.Columns[17].Visible = false;
        GridSol.Columns[18].Visible = false;
        GridSol.Columns[19].Visible = false;
        GridSol.Columns[20].Visible = false;
        GridSol.Columns[21].Visible = false;
    }//carga Grid y esconde columnas
    protected void Aviso(string tipo, string msj1, string msj2)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "mensaje", "despliega_aviso('" + tipo + "','" + msj1 + "','" + msj2 + "','txt_btn_no','txt_btn_si','txt_msj_no','txt_msj_si');", true);
    }
    protected void LimpiarForm()
    {
        txt_Nombre.Text = string.Empty;
        txt_Apaterno.Text = string.Empty;
        txt_Amaterno.Text = string.Empty;
        txt_Celular.Text = string.Empty;
        txt_Direccion.Text = string.Empty;
        txt_email.Text = string.Empty;
        //txt_Jornada.Text = string.Empty;
        txt_FechaNacimiento.Text = DateTime.Now.ToString("dd/MM/yyyy");
        DD_Colonia.ClearSelection();
        DD_Sexo.ClearSelection();
        DD_Apoyo_multi.ClearSelection();
        //DD_Apoyo.ClearSelection();
        //DD_TipoApoyo.ClearSelection();
        lbl_registro.Text = "<i  class='fa text-info fa-vcard-o'></i> Agregar Persona";
        btn_Guardar.Text = "Guardar";
        btn_Guardar.CssClass = "btn btn-w-m btn-primary";
        btn_Borrar.Enabled = false;
        btn_Borrar.Visible = false;


        txtObservacion.Text = string.Empty;
        lblNombre2.Text = string.Empty;
        text_asunto2.Text = string.Empty;
        RB_estatus.ClearSelection();

    }

    protected void Positivos(object sender, EventArgs e)
    {
        Datosolicitud.Positivos(Hd_Idarea.Value, "UPpositivos", DD_Jornadas.SelectedValue);
        Response.Redirect("Seguimiento.aspx",false);
       
        //Aviso("success", "¡chido!","");
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