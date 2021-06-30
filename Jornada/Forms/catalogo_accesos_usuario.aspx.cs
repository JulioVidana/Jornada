using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_catalogo_accesos_usuario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(Session["Session_Id_Usuario"]) <= 0)
        //    Response.Redirect(ResolveUrl("~/Login.aspx"), true);

        if (!Page.IsPostBack)
        {
            DD_Usrs.DataBind();
            llena_treeview();
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ejecuta_javascript", "ejecuta_javascript();", true);
    }

    protected void Usuarios_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        llena_treeview();
    }

    private void llena_treeview()
    {
        // LIMPIMAMOS TREEVIEW
        Accesos_TreeView.Nodes.Clear();

        // LLENAMOS TREEVIEW CON LOS ACCESOS
        DataView dv_Accesos = (DataView)Accesos_SqlDataSource.Select(DataSourceSelectArguments.Empty);
        int wnivel_1 = -1;
        int wnivel_2 = -1;
        int wnivel_3 = -1;
        int wnivel_4 = -1;
        int wnivel_5 = -1;
        int wcont_nivel_1 = -1;
        int wcont_nivel_2 = -1;
        int wcont_nivel_3 = -1;
        int wcont_nivel_4 = -1;

        foreach (DataRowView drv_accesos in dv_Accesos)
        {
            TreeNode wnodo = new TreeNode();
            wnodo.Text = " " + drv_accesos["nombre_acceso"].ToString();
            wnodo.Value = drv_accesos["id_acceso"].ToString();
            string worden = (drv_accesos["orden"].ToString() + "00000000000000").Substring(0, 14);

            if (Convert.ToInt32(wnivel_1) != Convert.ToInt32(worden.Substring(0, 2)))
            {
                wnivel_1 = Convert.ToInt32(worden.Substring(0, 2));
                wnivel_2 = -1;
                wnivel_3 = -1;
                wnivel_4 = -1;
                wnivel_5 = -1;
                wcont_nivel_1 = wcont_nivel_1 + 1;
                wcont_nivel_2 = -1;
                wcont_nivel_3 = -1;
                wcont_nivel_4 = -1;
                Accesos_TreeView.Nodes.Add(wnodo);
            }
            else
            if (Convert.ToInt32(wnivel_2) != Convert.ToInt32(worden.Substring(3, 2)))
            {
                wnivel_2 = Convert.ToInt32(worden.Substring(3, 2));
                wnivel_3 = -1;
                wnivel_4 = -1;
                wnivel_5 = -1;
                wcont_nivel_2 = wcont_nivel_2 + 1;
                wcont_nivel_3 = -1;
                wcont_nivel_4 = -1;
                Accesos_TreeView.Nodes[wcont_nivel_1].ChildNodes.Add(wnodo);
            }
            else
            if (Convert.ToInt32(wnivel_3) != Convert.ToInt32(worden.Substring(6, 2)))
            {
                wnivel_3 = Convert.ToInt32(worden.Substring(6, 2));
                wnivel_4 = -1;
                wnivel_5 = -1;
                wcont_nivel_3 = wcont_nivel_3 + 1;
                wcont_nivel_4 = -1;
                Accesos_TreeView.Nodes[wcont_nivel_1].ChildNodes[wcont_nivel_2].ChildNodes.Add(wnodo);
            }
            else
            if (Convert.ToInt32(wnivel_4) != Convert.ToInt32(worden.Substring(9, 2)))
            {
                wnivel_4 = Convert.ToInt32(worden.Substring(9, 2));
                wnivel_5 = -1;
                wcont_nivel_4 = wcont_nivel_4 + 1;
                Accesos_TreeView.Nodes[wcont_nivel_1].ChildNodes[wcont_nivel_2].ChildNodes[wcont_nivel_3].ChildNodes.Add(wnodo);
            }
            else
            if (Convert.ToInt32(wnivel_5) != Convert.ToInt32(worden.Substring(12, 2)))
            {
                wnivel_5 = Convert.ToInt32(worden.Substring(12, 2));
                Accesos_TreeView.Nodes[wcont_nivel_1].ChildNodes[wcont_nivel_2].ChildNodes[wcont_nivel_3].ChildNodes[wcont_nivel_4].ChildNodes.Add(wnodo);
            }

        }

        // MARCAMOS LOS ACCESOS POR USUARIO
        DataView dvSql = (DataView)Accesos_Usuario_SqlDataSource.Select(DataSourceSelectArguments.Empty);
        foreach (DataRowView drvSql in dvSql)
        {
            try
            {
                foreach (TreeNode wnodo in Accesos_TreeView.Nodes)
                {
                    if (wnodo.Value == drvSql["id_acceso"].ToString())
                    {
                        wnodo.Checked = true;
                        break;
                    }
                    int i = 0;
                    for (i = 0; i <= wnodo.ChildNodes.Count - 1; i++)
                    {
                        if (wnodo.ChildNodes[i].Value == drvSql["id_acceso"].ToString())
                        {
                            wnodo.ChildNodes[i].Checked = true;
                            break;
                        }
                        int j = 0;
                        for (j = 0; j <= wnodo.ChildNodes[i].ChildNodes.Count - 1; j++)
                        {
                            if (wnodo.ChildNodes[i].ChildNodes[j].Value == drvSql["id_acceso"].ToString())
                            {
                                wnodo.ChildNodes[i].ChildNodes[j].Checked = true;
                                break;
                            }
                            int k = 0;
                            for (k = 0; k <= wnodo.ChildNodes[i].ChildNodes[j].ChildNodes.Count - 1; k++)
                            {
                                if (wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].Value == drvSql["id_acceso"].ToString())
                                {
                                    wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].Checked = true;
                                    break;
                                }
                                int l = 0;
                                for (l = 0; l <= wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count - 1; l++)
                                {
                                    if (wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value == drvSql["id_acceso"].ToString())
                                    {
                                        wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }

            // EXPANDIMOS TREEVIEW
            Accesos_TreeView.ExpandAll();
        }
    }

    protected void Guardar_Button_Click(object sender, EventArgs e)
    {
        // PRIMERO MARCAMOS COMO CHECADOS LOS MENUS
        foreach (TreeNode wnodo in Accesos_TreeView.Nodes)
        {
            int i = 0;
            for (i = 0; i <= wnodo.ChildNodes.Count - 1; i++)
            {
                if (wnodo.ChildNodes[i].Checked)
                {
                    wnodo.Checked = true;
                }
                int j = 0;
                for (j = 0; j <= wnodo.ChildNodes[i].ChildNodes.Count - 1; j++)
                {
                    if (wnodo.ChildNodes[i].ChildNodes[j].Checked)
                    {
                        wnodo.ChildNodes[i].Checked = true;
                        wnodo.Checked = true;
                    }
                    int k = 0;
                    for (k = 0; k <= wnodo.ChildNodes[i].ChildNodes[j].ChildNodes.Count - 1; k++)
                    {
                        if (wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].Checked)
                        {
                            wnodo.ChildNodes[i].ChildNodes[j].Checked = true;
                            wnodo.ChildNodes[i].Checked = true;
                            wnodo.Checked = true;
                        }
                        int l = 0;
                        for (l = 0; l <= wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count - 1; l++)
                        {
                            if (wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked)
                            {
                                wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].Checked = true;
                                wnodo.ChildNodes[i].ChildNodes[j].Checked = true;
                                wnodo.ChildNodes[i].Checked = true;
                                wnodo.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        // BORRAMOS LOS ACCESOS POR USUARIO
        borra_acceso_por_usuario();

        // BARREMOS EL TREEVIEW PARA GUARDAR
        foreach (TreeNode wnodo in Accesos_TreeView.Nodes)
        {
            if (wnodo.Checked)
            {
                agrega_acceso_por_usuario(Convert.ToInt32(wnodo.Value));
            }
            int i = 0;
            for (i = 0; i <= wnodo.ChildNodes.Count - 1; i++)
            {
                if (wnodo.ChildNodes[i].Checked)
                {
                    agrega_acceso_por_usuario(Convert.ToInt32(wnodo.ChildNodes[i].Value));
                }
                int j = 0;
                for (j = 0; j <= wnodo.ChildNodes[i].ChildNodes.Count - 1; j++)
                {
                    if (wnodo.ChildNodes[i].ChildNodes[j].Checked)
                    {
                        agrega_acceso_por_usuario(Convert.ToInt32(wnodo.ChildNodes[i].ChildNodes[j].Value));
                    }
                    int k = 0;
                    for (k = 0; k <= wnodo.ChildNodes[i].ChildNodes[j].ChildNodes.Count - 1; k++)
                    {
                        if (wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].Checked)
                        {
                            agrega_acceso_por_usuario(Convert.ToInt32(wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].Value));
                        }
                        int l = 0;
                        for (l = 0; l <= wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count - 1; l++)
                        {
                            if (wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked)
                            {
                                agrega_acceso_por_usuario(Convert.ToInt32(wnodo.ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value));
                            }
                        }
                    }
                }
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "mensaje", "despliega_aviso('success','Se guardaron los cambios...','','txt_btn_no','txt_btn_si','txt_msj_no','txt_msj_si');", true);
    }

    private void borra_acceso_por_usuario()
    {
        var sql_connectionstring = ConfigurationManager.ConnectionStrings["db_ConnectionString"].ConnectionString;
        using (var connection = new SqlConnection(sql_connectionstring))
        {
            SqlCommand sql_command = new SqlCommand("spr_portal_lee_accesos", connection);
            sql_command.CommandType = CommandType.StoredProcedure;

            sql_command.Parameters.AddWithValue("@funcion", "Borra");
            sql_command.Parameters.Add("@id_usuario", SqlDbType.Int);
            sql_command.Parameters["@id_usuario"].Value = DD_Usrs.SelectedValue;

            connection.Open();
            sql_command.ExecuteReader();
            connection.Close();
        }
    }

    private void agrega_acceso_por_usuario(int wacceso)
    {
        var sql_connectionstring = ConfigurationManager.ConnectionStrings["db_ConnectionString"].ConnectionString;
        using (var connection = new SqlConnection(sql_connectionstring))
        {
            SqlCommand sql_command = new SqlCommand("spr_portal_lee_accesos", connection);
            sql_command.CommandType = CommandType.StoredProcedure;

            sql_command.Parameters.AddWithValue("@funcion", "Agrega");
            sql_command.Parameters.Add("@id_usuario", SqlDbType.Int);
            sql_command.Parameters["@id_usuario"].Value = DD_Usrs.SelectedValue;
            sql_command.Parameters.Add("@id_acceso", SqlDbType.Int);
            sql_command.Parameters["@id_acceso"].Value = wacceso;

            connection.Open();
            sql_command.ExecuteReader();
            connection.Close();
        }
    }
}