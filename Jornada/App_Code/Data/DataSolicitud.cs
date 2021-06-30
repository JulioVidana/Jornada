using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

/// <summary>
/// conexiones para consulta capturas padrón
/// </summary>
public class DataSolicitud
{

    Conexion conect = new Conexion();
    
    /// <summary>
    /// Llenar grid de consultas para Capturistas
    /// </summary>
    public DataTable llenarSolicitudes(string idarea, string tipo, string idjornada)
    {
        DataSet dset = new DataSet("count");
        string query = "spr_Capturas";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", tipo);
        comm.Parameters.AddWithValue("@id_area", Convert.ToInt32(idarea));
        comm.Parameters.AddWithValue("@id_jornada", Convert.ToInt32(idjornada));
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "count");
        DataTable table = dset.Tables["count"];
        conect.CerrarConn();
        return table;
    }

    /// <summary>
    /// Inserta nuevo padrón de persona
    /// </summary>
    public void NuevaCaptura(Dictionary<string, string> datosPersona)
    {

        string query = "spr_Capturas";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", "Nuevo");
        comm.Parameters.AddWithValue("@nombre", datosPersona["Nombre"]);
        comm.Parameters.AddWithValue("@aPaterno", datosPersona["aPaterno"]);
        comm.Parameters.AddWithValue("@aMaterno", datosPersona["aMaterno"]);
        comm.Parameters.AddWithValue("@celular", datosPersona["Celular"]);
        comm.Parameters.AddWithValue("@direccion", datosPersona["Direccion"]);
        comm.Parameters.AddWithValue("@id_colonia", Convert.ToInt32(datosPersona["Colonia"]));
        comm.Parameters.AddWithValue("@email", datosPersona["email"]);
        comm.Parameters.AddWithValue("@asunto", datosPersona["Asunto"]);
        comm.Parameters.AddWithValue("@fechaCaptura", DateTime.Now.ToString("yyyy-MM-dd"));
        comm.Parameters.AddWithValue("@id_area", Convert.ToInt32(datosPersona["id_area"]));
        comm.Parameters.AddWithValue("@edad", datosPersona["edad"]);
        comm.Parameters.AddWithValue("@sexo", datosPersona["sexo"]);
        comm.Parameters.AddWithValue("@id_jornada", Convert.ToInt32(datosPersona["id_jornada"]));
        //comm.Parameters.AddWithValue("@ApoyoId", Convert.ToInt32(datosPersona["ApoyoId"]));
        comm.ExecuteNonQuery();
        conect.CerrarConn();
    }
    /// <summary>
    /// Trae último ID registrado en Padrón
    /// </summary>
    public DataTable PadronIdentity()
    {
        DataSet dset = new DataSet("count");
        string query = "spr_Capturas";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", "identity");
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "identity");
        DataTable table = dset.Tables["identity"];
        conect.CerrarConn();
        return table;
    }

    /// <summary>
    /// Inserta Nuevos Apoyos
    /// </summary>
    public void InsertaApoyos(string idPersona,string ApoyoId)
    {

        string query = "spr_Capturas";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", "InsertaApoyos");
        comm.Parameters.AddWithValue("@id_persona", Convert.ToInt32(idPersona));
        comm.Parameters.AddWithValue("@ApoyoId", Convert.ToInt32(ApoyoId));
        comm.ExecuteNonQuery();
        conect.CerrarConn();
    }

    /// <summary>
    /// Actualizada datos generales de Persona
    /// </summary>
    public void ActualizaPersona(Dictionary<string, string> datosPersona)
    {

        string query = "spr_Capturas";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", "UPpadron");
        comm.Parameters.AddWithValue("@nombre", datosPersona["Nombre"]);
        comm.Parameters.AddWithValue("@aPaterno", datosPersona["aPaterno"]);
        comm.Parameters.AddWithValue("@aMaterno", datosPersona["aMaterno"]);
        comm.Parameters.AddWithValue("@celular", datosPersona["Celular"]);
        comm.Parameters.AddWithValue("@direccion", datosPersona["Direccion"]);
        comm.Parameters.AddWithValue("@id_colonia", Convert.ToInt32(datosPersona["Colonia"]));
        comm.Parameters.AddWithValue("@email", datosPersona["email"]);
        comm.Parameters.AddWithValue("@asunto", datosPersona["Asunto"]);
        comm.Parameters.AddWithValue("@id_persona", datosPersona["idPersona"]);
        comm.Parameters.AddWithValue("@edad",datosPersona["edad"]);
        comm.Parameters.AddWithValue("@sexo", datosPersona["sexo"]);
        //comm.Parameters.AddWithValue("@ApoyoId", Convert.ToInt32(datosPersona["ApoyoId"]));
        comm.ExecuteNonQuery();
        conect.CerrarConn();
    }


    /// <summary>
    /// Borra Apoyos/Persona
    /// </summary>
    public void BorraApoyos(string idPersona)
    {

        string query = "spr_Capturas";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", "BorraApoyos");
        comm.Parameters.AddWithValue("@id_persona", Convert.ToInt32(idPersona));
        comm.ExecuteNonQuery();
        conect.CerrarConn();
    }

    /// <summary>
    /// Borra Persona y su seguimiento
    /// </summary>
    public void BorraPerona(string idPersona)
    {

        string query = "spr_Capturas";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", "BorraPersona");
        comm.Parameters.AddWithValue("@id_persona", Convert.ToInt32(idPersona));
        comm.ExecuteNonQuery();
        conect.CerrarConn();
    }

    /// <summary>
    /// Acutualiza seguimiento seguimiento
    /// </summary>
    public void ActualizaSeguimiento(string idPersona,string estatus, string descripcion)
    {

        string query = "spr_Capturas";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", "UPSeguimiento");
        comm.Parameters.AddWithValue("@id_persona", Convert.ToInt32(idPersona));
        comm.Parameters.AddWithValue("@estatus", Convert.ToInt32(estatus));
        comm.Parameters.AddWithValue("@descripcion", descripcion);
        comm.Parameters.AddWithValue("@fechaSegui", DateTime.Now.ToString("yyyy-MM-dd"));
        comm.ExecuteNonQuery();
        conect.CerrarConn();
    }

    /// <summary>
    /// Trae catálogos para los DropDownList de Módulo Capturas
    /// </summary>
    public DataTable llenaCombos(string tipo, string idArea)
    {
        DataSet dset = new DataSet("count");
        string query = "spr_Capturas";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", tipo);
        comm.Parameters.AddWithValue("@id_area", Convert.ToInt32(idArea));
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "count");
        DataTable table = dset.Tables["count"];
        conect.CerrarConn();
        return table;
    }

   

    /// ****************************************************************    ACCESOS y USUARIOS

    public DataTable Login(string usuario, string contra)
    {

        DataSet dset = new DataSet("count");
        string query = "spr_portal_valida_acceso";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@usuario", usuario);
        comm.Parameters.AddWithValue("@password", csSecurityHandler.Encriptar(contra));
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "count");
        DataTable table = dset.Tables["count"];
        conect.CerrarConn();
        return table;
    }


    /// <summary>
    /// Trae a todos los usuarios
    /// </summary>
    public DataTable llenarUsuarios()
    {
        DataSet dset = new DataSet("count");
        string query = "spr_catalogo_usuarios";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@funcion", "leetodos");
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "count");
        DataTable table = dset.Tables["count"];
        conect.CerrarConn();
        return table;
    }

    /// <summary>
    /// Trae datos del usuario al logearse
    /// </summary>
    public Usuario DatosUsuario(string usuario, string tipousuario)
    {
        Usuario InfoUsuario = new Usuario();
        DataSet dset = new DataSet("count");
        string query = "spr_catalogo_usuarios";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@funcion", "datos_usr");
        comm.Parameters.AddWithValue("@nombre", usuario);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "datos");
        DataTable tabla = dset.Tables["datos"];
        conect.CerrarConn();

        if (tabla.Rows.Count > 0)
        {
            InfoUsuario.IdUsuario = tabla.Rows[0]["id_usuario"].ToString();
            InfoUsuario.ID_Area = tabla.Rows[0]["area"].ToString();
            InfoUsuario.Area = tabla.Rows[0]["Areas"].ToString();
            InfoUsuario.Activo = tabla.Rows[0]["activo"].ToString();
            InfoUsuario.Correo = tabla.Rows[0]["correo"].ToString();
        }
        InfoUsuario.NombreUsuario = usuario;
        InfoUsuario.TipoUsuario = tipousuario; //los permisos para tipo de usuario están en la tabla de "Acceso" y "Usuarios_Acceso" (Nombre_acceso) "editor" "Propietario" 

        return InfoUsuario;

    }

    /// <summary>
    /// Trae los accesos del usuario
    /// </summary>
    public DataTable Menu(int idusuario)
    {

        DataSet dset = new DataSet("count");
        string query = "spr_master_lee_accesos_usuario";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@id_usuario", idusuario);
        comm.Parameters.AddWithValue("@ver_no_visualizar", Convert.ToBoolean("True"));
        comm.Parameters.AddWithValue("@sistema", "00");
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "count");
        DataTable table = dset.Tables["count"];
        conect.CerrarConn();
        return table;
    }


    /// <summary>
    /// Agrega Nuevo o Actualiza Usuario
    /// </summary>
    public void AgregaUsuario(Dictionary<string, string> datosUsuario)
    {

        string query = "spr_catalogo_usuarios"; 
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@funcion", datosUsuario["Funcion"]);
        comm.Parameters.AddWithValue("@nombre", datosUsuario["Nombre"]);
        comm.Parameters.AddWithValue("@correo", datosUsuario["Correo"]);
        comm.Parameters.AddWithValue("@area", Convert.ToInt32(datosUsuario["idArea"]));
        comm.Parameters.AddWithValue("@id_usuario", datosUsuario["id_usuario"]);
        comm.Parameters.AddWithValue("@activo", Convert.ToBoolean(datosUsuario["activo"]));
        comm.ExecuteNonQuery();
        conect.CerrarConn();
    }

    /// <summary>
    /// Borra Usuario y accesos?
    /// </summary>
    public void BorraUsuario(string idPersona)
    {

        string query = "spr_catalogo_usuarios";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@funcion", "elimina");
        comm.Parameters.AddWithValue("@id_usuario", Convert.ToInt32(idPersona));
        comm.ExecuteNonQuery();
        conect.CerrarConn();
    }


    /// ****************************************************************    FILTROS

    /// <summary>
    /// Trae catálogo de Áreas
    /// </summary>
    public DataTable llenarAreas()
    {
        DataSet dset = new DataSet("count");
        string query = "spr_Reportes";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", "SelectAreas");
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "count");
        DataTable table = dset.Tables["count"];
        conect.CerrarConn();
        return table;
    }

    /// <summary>
    /// Busquedas por Area, sexo y rango de edad
    /// </summary>
    public DataTable Filtros(string Area, string sexo, string Colonia,string jornada,string store)
    {
        DataSet dset = new DataSet("count");
        string query = store;
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", "Filtros");
        comm.Parameters.AddWithValue("@id_colonia", Convert.ToInt32(Colonia));
        comm.Parameters.AddWithValue("@id_area", Convert.ToInt32(Area));
        comm.Parameters.AddWithValue("@id_jornada", Convert.ToInt32(jornada));
        comm.Parameters.AddWithValue("@sexo", sexo);

        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "count");
        DataTable table = dset.Tables["count"];
        conect.CerrarConn();
        return table;
    }

    /// <summary>
    /// Trae Jornadas con sus totales
    /// </summary>
    public DataTable TraeJornadas(string tipo)
    {
        DataSet dset = new DataSet("count");
        string query = "spr_Reportes";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@tipo", tipo);

        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "count");
        DataTable table = dset.Tables["count"];
        conect.CerrarConn();
        return table;
    }

    /// <summary>
    /// Agrega o Actualizada Catalogo de Jornadas
    /// </summary>
    public void AgregaJornada(Dictionary<string, string> datosUsuario)
    {

        string query = "spr_catalogo_usuarios"; 
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@funcion", datosUsuario["Funcion"]);
        comm.Parameters.AddWithValue("@id_usuario", datosUsuario["id_jornada"]);

        comm.Parameters.AddWithValue("@Jornada", datosUsuario["Jornada"]);
        comm.Parameters.AddWithValue("@activo", Convert.ToBoolean(datosUsuario["activo"]));
        comm.Parameters.AddWithValue("@Telefono", datosUsuario["telefono"]);
        comm.Parameters.AddWithValue("@nombre", datosUsuario["host"]);
        comm.Parameters.AddWithValue("@Direccion", datosUsuario["direccion"]);
        comm.Parameters.AddWithValue("@id_colonia", datosUsuario["id_colonia"]);
        comm.Parameters.AddWithValue("@fecha", datosUsuario["fecha"]);

        comm.ExecuteNonQuery();
        conect.CerrarConn();
    }

    /// <summary>
    /// Trae catálogo de Jornadas
    /// </summary>
    public DataTable llenaJornada()
    {
        DataSet dset = new DataSet("count");
        string query = "spr_catalogo_usuarios";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@funcion", "SEL_jor");
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dset, "count");
        DataTable table = dset.Tables["count"];
        conect.CerrarConn();
        return table;
    }

    /// <summary>
    /// Actualiza estatus a positivos de la jornada
    /// </summary>
    public void Positivos(string idarea, string tipo, string idjornada)
    {

        string query = "spr_Capturas";
        SqlCommand comm = new SqlCommand(query, conect.AbrirConn("1"));
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@Tipo", tipo);
        comm.Parameters.AddWithValue("@id_area", Convert.ToInt32(idarea));
        comm.Parameters.AddWithValue("@id_jornada", Convert.ToInt32(idjornada));
        comm.ExecuteNonQuery();
        conect.CerrarConn();
    }

    public DataSolicitud()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    
}