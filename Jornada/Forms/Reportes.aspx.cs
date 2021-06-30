using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Microsoft.Reporting.WebForms;

public partial class Forms_Reportes : System.Web.UI.Page
{
    DataSolicitud Datosolicitud = new DataSolicitud();
    Usuario objUsuario = new Usuario();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objUsuario = (Usuario)Session["loginUsuario"];
            if (objUsuario == null)
            {
                Aviso("warning", "No tienes Permisos");
                logOut();
            }
            else
            {
               
                if (objUsuario.TipoUsuario == "Propietario")
                {
                    DD_Area.DataSource = Datosolicitud.llenarAreas();
                    DD_Area.DataBind();
                    DD_Colonia.DataSource = Datosolicitud.llenaCombos("Colonias2", "1");
                    DD_Colonia.DataBind();
                    DD_Jornadas.DataSource = Datosolicitud.llenaCombos("Jornadas2", "1");
                    DD_Jornadas.DataBind();
                    BinData();
                }
                else
                {
                    GridSol.Visible = false;
                    Btn_Buscar.Visible = false;
                    Btn_Reporte.Visible = false;
                    logOut();

                }
            }
        }
    }

    protected void Filtrar(object seder, EventArgs e)
    {
        
        dt = Datosolicitud.Filtros(DD_Area.SelectedValue, DD_Sexo.SelectedValue, DD_Colonia.SelectedValue,DD_Jornadas.SelectedValue, "spr_Reportes");

        GridSol.Columns[10].Visible = true;
        GridSol.Columns[11].Visible = true;
        GridSol.Columns[12].Visible = true;
        GridSol.Columns[13].Visible = true;
        GridSol.Columns[14].Visible = true;
        GridSol.Columns[15].Visible = true;
        GridSol.Columns[16].Visible = true;
        GridSol.Columns[17].Visible = true;
        GridSol.Columns[18].Visible = true;
        GridSol.Columns[19].Visible = true;
        //GridSol.Columns[20].Visible = true;

        GridSol.DataSource = dt;
        GridSol.DataBind();

        GridSol.Columns[10].Visible = false;
        GridSol.Columns[11].Visible = false;
        GridSol.Columns[12].Visible = false;
        GridSol.Columns[13].Visible = false;
        GridSol.Columns[14].Visible = false;
        GridSol.Columns[15].Visible = false;
        GridSol.Columns[16].Visible = false;
        GridSol.Columns[17].Visible = false;
        GridSol.Columns[18].Visible = false;
        GridSol.Columns[19].Visible = false;
        //GridSol.Columns[20].Visible = false;

        lblbeneficiarios.Text = dt.Rows.Count.ToString();
        lblHombres.Text = Convert.ToString((int)dt.Compute("COUNT(sexo)", "sexo = 'M'"));
        lblMujeres.Text = Convert.ToString((int)dt.Compute("COUNT(sexo)", "sexo = 'F'"));
        int edad1 = (int)dt.Compute("COUNT(edad)", "edad >= '0' AND edad <= '9'");
        int edad2 = (int)dt.Compute("COUNT(edad)", "edad >= '10' AND edad <= '19'");
        int edad3 = (int)dt.Compute("COUNT(edad)", "edad >= '20' AND edad <= '29'");
        int edad4 = (int)dt.Compute("COUNT(edad)", "edad >= '30' AND edad <= '39'");
        int edad5 = (int)dt.Compute("COUNT(edad)", "edad >= '40' AND edad <= '49'");
        int edad6 = (int)dt.Compute("COUNT(edad)", "edad >= '50' AND edad <= '59'");
        int edad7 = (int)dt.Compute("COUNT(edad)", "edad >= '60' ");


        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        

    }
    protected void clear(object sender, EventArgs e)
    {
        DD_Area.ClearSelection();
        DD_Sexo.ClearSelection();
        DD_Colonia.ClearSelection();
        DD_Jornadas.ClearSelection();
        //GridSol.DataSource = null;
        //GridSol.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
    }
    protected void Soli_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GridSol.Rows[index];


            if (e.CommandName == "segui")
            {
            lblNombre2.Text = Server.HtmlDecode(row.Cells[10].Text + " " + row.Cells[11].Text + " " + row.Cells[12].Text);
            //if (row.Cells[5].Text == "&nbsp;") { text_asunto2.Text = ""; } else { text_asunto2.Text = row.Cells[5].Text; }
            text_asunto2.Text = Server.HtmlDecode(row.Cells[5].Text);
            RB_estatus.SelectedValue = row.Cells[14].Text;
            //if (row.Cells[15].Text == "&nbsp;") { txtObservacion.Text = ""; } else { txtObservacion.Text = row.Cells[15].Text; }
            txtObservacion.Text = Server.HtmlDecode(row.Cells[15].Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Div_Seguimiento", "$('#Div_Seguimiento').modal('show');", true);
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
        BinData();

    }

    protected void BinData()
    {
        dt = Datosolicitud.Filtros(DD_Area.SelectedValue, DD_Sexo.SelectedValue, DD_Colonia.SelectedValue, DD_Jornadas.SelectedValue, "spr_Reportes");

        GridSol.Columns[10].Visible = true;
        GridSol.Columns[11].Visible = true;
        GridSol.Columns[12].Visible = true;
        GridSol.Columns[13].Visible = true;
        GridSol.Columns[14].Visible = true;
        GridSol.Columns[15].Visible = true;
        GridSol.Columns[16].Visible = true;
        GridSol.Columns[17].Visible = true;
        //GridSol.Columns[18].Visible = true;
        GridSol.Columns[19].Visible = true;
        //GridSol.Columns[20].Visible = true;

        //GridSol.DataSource = Datosolicitud.llenarSolicitudes();
        GridSol.DataSource = dt;
        GridSol.DataBind();

        GridSol.Columns[10].Visible = false;
        GridSol.Columns[11].Visible = false;
        GridSol.Columns[12].Visible = false;
        GridSol.Columns[13].Visible = false;
        GridSol.Columns[14].Visible = false;
        GridSol.Columns[15].Visible = false;
        GridSol.Columns[16].Visible = false;
        GridSol.Columns[17].Visible = false;
        //GridSol.Columns[18].Visible = false;
        GridSol.Columns[19].Visible = false;
        //GridSol.Columns[20].Visible = false;

        lblbeneficiarios.Text = dt.Rows.Count.ToString();
        lblHombres.Text = Convert.ToString((int)dt.Compute("COUNT(sexo)", "sexo = 'M'"));
        lblMujeres.Text = Convert.ToString((int)dt.Compute("COUNT(sexo)", "sexo = 'F'"));
    }

    protected void Pintate1(object sender, GridViewRowEventArgs e)//colorea la columna de estatus
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell cell = e.Row.Cells[7];
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

    protected void Aviso(string tipo, string msj1)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "mensaje", "despliega_aviso('" + tipo + "','" + msj1 + "','','txt_btn_no','txt_btn_si','txt_msj_no','txt_msj_si');", true);
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

  
    protected void Imprimir_Button_Click(object sender, EventArgs e)
    {
        genera_reporte();
    }
    protected void genera_reporte()
    {
        // DEFINIMOS EL REPORTE
        Reporte_ReportViewer.ProcessingMode = ProcessingMode.Local;
        Reporte_ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reportes/Reporte1.rdlc");

        // CREAMOS DATATABLE PARA EL DETALLE
        DataTable dt_Sexo = Datosolicitud.Filtros(DD_Area.SelectedValue, DD_Sexo.SelectedValue, DD_Colonia.SelectedValue, DD_Jornadas.SelectedValue, "spr_Reportes_Sexo");
        DataTable dt_edades = Datosolicitud.Filtros(DD_Area.SelectedValue, DD_Sexo.SelectedValue, DD_Colonia.SelectedValue, DD_Jornadas.SelectedValue, "spr_Reportes_Edades");
        DataTable dt_Areas = Datosolicitud.Filtros(DD_Area.SelectedValue, DD_Sexo.SelectedValue, DD_Colonia.SelectedValue, DD_Jornadas.SelectedValue, "spr_Reportes_Areas");
        DataTable dt_Colonias = Datosolicitud.Filtros(DD_Area.SelectedValue, DD_Sexo.SelectedValue, DD_Colonia.SelectedValue, DD_Jornadas.SelectedValue, "spr_Reportes_Colonias");
        DataTable dt_Jornadas = Datosolicitud.Filtros(DD_Area.SelectedValue, DD_Sexo.SelectedValue, DD_Colonia.SelectedValue, DD_Jornadas.SelectedValue, "spr_Reportes_Jornadas");

        //Array que contendrá los parámetros
        ReportParameter[] parametros = new ReportParameter[3]; 
        string area = "";
        string jornada = "";
        string colonia = "";
        //Establecemos el valor de los parámetros
        if (DD_Area.SelectedValue == "0") { area = "TODAS"; } else { area = DD_Area.SelectedItem.Text; }
        if (DD_Jornadas.SelectedValue == "0") { jornada = "TODAS"; } else { jornada = DD_Jornadas.SelectedItem.Text; }
        if (DD_Colonia.SelectedValue == "0") { colonia = "TODAS"; } else { colonia = DD_Colonia.SelectedItem.Text; } 
        parametros[0] = new ReportParameter("Par_Area", area);
        parametros[1] = new ReportParameter("Par_Jornada", jornada);
        parametros[2] = new ReportParameter("Par_Colonia", colonia);

        // CREAMOS DATASET
        Reporte_ReportViewer.LocalReport.DataSources.Clear();
        
        ReportDataSource VReportDataSource_Sexo = new ReportDataSource("Sexo_ds", dt_Sexo);
        ReportDataSource VReportDataSource_Edades = new ReportDataSource("Edades_ds", dt_edades);
        ReportDataSource VReportDataSource_Areas = new ReportDataSource("Areas_ds", dt_Areas);
        ReportDataSource VReportDataSource_colonias = new ReportDataSource("Colonias_ds", dt_Colonias);
        ReportDataSource VReportDataSource_Jornadas = new ReportDataSource("Jornadas_ds", dt_Jornadas);

        // AGREGAMOS DATASET AL REPORTE
        Reporte_ReportViewer.LocalReport.DataSources.Add(VReportDataSource_Sexo);
        Reporte_ReportViewer.LocalReport.DataSources.Add(VReportDataSource_Edades);
        Reporte_ReportViewer.LocalReport.DataSources.Add(VReportDataSource_Areas);
        Reporte_ReportViewer.LocalReport.DataSources.Add(VReportDataSource_colonias);
        Reporte_ReportViewer.LocalReport.DataSources.Add(VReportDataSource_Jornadas);
        Reporte_ReportViewer.LocalReport.SetParameters(parametros);

        // REFRESH AL REPORTE
        Reporte_ReportViewer.LocalReport.Refresh();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Registro", "$('#Imprimir_Div').modal('show');", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);

    }

    protected void Reporte_ReportViewer_PageNavigation(object sender, PageNavigationEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Registro", "$('#Imprimir_Div').modal('show');", true);
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