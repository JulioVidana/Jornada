using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

/// <summary>
/// Descripción breve de Conexion
/// </summary>
public class Conexion
{
    public SqlConnection SqlJORNADA = new SqlConnection(WebConfigurationManager.ConnectionStrings["db_ConnectionString"].ConnectionString);
    //public SqlConnection SqlGEN = new SqlConnection(WebConfigurationManager.ConnectionStrings["db_ConnectionString2"].ConnectionString);
    //public SqlConnection SqlPORTAL = new SqlConnection(WebConfigurationManager.ConnectionStrings["db_ConnectionString3"].ConnectionString);

    /// <summary>
    /// Abre conexion con SQL: 1=DB Jornada, 2=DB General, 3=DB Portal
    /// </summary>
    public SqlConnection AbrirConn(string cnxa)
    {
        SqlConnection result = null;
        // SqlConn.Open();
        switch (cnxa)
        {
            case "1": SqlJORNADA.Open(); result = SqlJORNADA; break; //DB Jornada
            //case "2": SqlGEN.Open(); result = SqlGEN; break; //DB General
            //case "3": SqlGEN.Open(); result = SqlPORTAL; break; //DB Portal
        }

        return result;
        // return SqlConn;
    }

    public void CerrarConn()
    {
        SqlJORNADA.Close(); /*SqlGEN.Close();*/
    }
}