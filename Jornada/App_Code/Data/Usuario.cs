using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Usuario
/// </summary>
public class Usuario
{
    public Usuario()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string IdUsuario { get; set; }
    public string NombreUsuario { get; set; }
    public string Area { get; set; }
    public string ID_Area { get; set; }
    public string TipoUsuario { get; set; }
    public string Activo { get; set; }
    public string Correo { get; set; }
    //public string funcion { get; set; }
    //public Byte[] Imagen { get; set; }
    //public string TipoImagen { get; set; }
    //public string Estatus { get; set; }

}