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

public partial class Forms_Reportes_Jornadas : System.Web.UI.Page
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
                   
                    BinData();

                }
                else
                {
                    GridSol.Visible = false;
                  
                    logOut();

                }
            }
        }
    }

    protected void Soli_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GridSol.Rows[index];


        if (e.CommandName == "reporte")
        {
            // DEFINIMOS EL REPORTE
            Reporte_ReportViewer.ProcessingMode = ProcessingMode.Local;
            Reporte_ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reportes/Reporte_Jornada.rdlc");

            // CREAMOS DATATABLE PARA EL DETALLE
            DataTable dt_Sexo = Datosolicitud.Filtros("0", "0", "0", row.Cells[0].Text, "spr_Reportes_Sexo");
            DataTable dt_edades = Datosolicitud.Filtros("0", "0", "0", row.Cells[0].Text, "spr_Reportes_Edades");
            DataTable dt_Padron = Datosolicitud.Filtros("0", "0", "0", row.Cells[0].Text, "spr_Reportes");

            //Array que contendrá los parámetros
            ReportParameter[] parametros = new ReportParameter[4];
            parametros[0] = new ReportParameter("Par_Host", Server.HtmlDecode(row.Cells[2].Text));
            parametros[1] = new ReportParameter("Par_Jornada", row.Cells[1].Text);
            parametros[2] = new ReportParameter("Par_Colonia", Server.HtmlDecode(row.Cells[4].Text));
            parametros[3] = new ReportParameter("Par_Atendidos", row.Cells[6].Text);

            // CREAMOS DATASET
            Reporte_ReportViewer.LocalReport.DataSources.Clear();

            ReportDataSource VReportDataSource_Sexo = new ReportDataSource("Sexo_ds", dt_Sexo);
            ReportDataSource VReportDataSource_Edades = new ReportDataSource("Edades_ds", dt_edades);
            ReportDataSource VReportDataSource_Padron = new ReportDataSource("Padron_ds", dt_Padron);

            // AGREGAMOS DATASET AL REPORTE
            Reporte_ReportViewer.LocalReport.DataSources.Add(VReportDataSource_Sexo);
            Reporte_ReportViewer.LocalReport.DataSources.Add(VReportDataSource_Edades);
            Reporte_ReportViewer.LocalReport.DataSources.Add(VReportDataSource_Padron);
            Reporte_ReportViewer.LocalReport.SetParameters(parametros);

            // REFRESH AL REPORTE
            Reporte_ReportViewer.LocalReport.Refresh();


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Registro", "$('#Imprimir_Div').modal('show');", true);
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
      

    }

    protected void BinData()
    {
        dt = Datosolicitud.TraeJornadas("TotalJornadas");
        GridSol.DataSource = dt;
        GridSol.DataBind();
        lblJornadas.Text = dt.Rows.Count.ToString();
        lblbeneficiarios.Text = Convert.ToString(dt.Compute("SUM(Total)", string.Empty));

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