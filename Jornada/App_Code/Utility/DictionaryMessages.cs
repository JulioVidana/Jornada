using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DictionaryMessages
/// </summary>
public class DictionaryMessages
{
    private static Dictionary<int, string> Messages = new Dictionary<int, string>()
    {
        {100, "Usuario ó Contraseña Incorrectos..."},
        {101, "El sistema valida letras mayúsculas y minúsculas"},
        {102, "Se le ha enviado un correo..."},
        {103, "El correo NO es valido..."},
        {104, "Cambio de Contraseña correcto..."},
        {105, "Las Contraseñas NO Coinciden..."},
        {106, "Debe capturar todos los campos..."},
        {108, "NO se puede eliminar, tiene registros relacionados..."},
        {200, "Se guardo con éxito."},
        {201, "Se elimino con éxito."},
        {202, "Se cancelo con éxito."},
        {500, "Ocurrió un error. Póngase en Contacto con Sistemas."},
        {507, "Error al enviar correo."},
    };

    public static string Get(int key)
    {
        return Messages.First(x => x.Key == key).Value;
    }
}