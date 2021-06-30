using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for csSecurity
/// </summary>
public static class csSecurityHandler
{
    /// Encripta una cadena
    public static string Encrypt(this string cadenaAencriptar)
    {
        string result = string.Empty;

        try
        {
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
        }
        catch(Exception ex) { }


        return result;
    }

    /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
    public static string Decrypting(this string cadenaAdesencriptar)
    {
        string result = string.Empty;

        try
        {
            byte[] decryted = Convert.FromBase64String(cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
        }
        catch(Exception ex) { }

        return result;
    }

    public static string Encriptar(string texto)
    {
        string key = "difson-sistemas"; //llave para encriptar datos

        byte[] keyArray;

        byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

        //Se utilizan las clases de encriptación MD5
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

        hashmd5.Clear();

        //Algoritmo TripleDES
        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();

        byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

        tdes.Clear();

        //se regresa el resultado en forma de una cadena
        texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);

        return texto;
    }

    public static string Desencriptar(string textoEncriptado)
    {
        string key = "difson-sistemas";
        byte[] keyArray;
        byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

        //algoritmo MD5
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();

        byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

        tdes.Clear();
        textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);

        return textoEncriptado;
    }

}