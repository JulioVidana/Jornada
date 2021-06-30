using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

public class ObjetoBase
{
    public enum enumObjectType
    {
        strType = 0,
        intType = 1,
        dblType = 2,
        dateType = 3,
        boolType = 4,
        charType = 5
    }

    public void Log(string s)
    {
        System.IO.StreamWriter ArchivoW;
        string strLineaTMP;
        string Minuto;
        //DateTime dt = DateTime.Now;

        Minuto = DateTime.Now.Minute.ToString();
        if (Minuto.Length == 1)
            Minuto = "0" + Minuto;

        strLineaTMP = "\n" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour + ":" + Minuto + ". " + s + "\n";
        try
        {
            ArchivoW = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true, System.Text.Encoding.UTF8);

            try
            {
                ArchivoW.Write(strLineaTMP);
            }
            catch (Exception)
            {
            }
            ArchivoW.Close();
        }
        catch (UnauthorizedAccessException)
        {

        }
    }

    protected void LogError(string sError)
    {
        System.IO.StreamWriter ArchivoW;
        string strLineaTMP;
        string Minuto;
        //DateTime dt = DateTime.Now;

        Minuto = DateTime.Now.Minute.ToString();
        if (Minuto.Length == 1)
            Minuto = "0" + Minuto;

        strLineaTMP = "\n" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour + ":" + Minuto + ". " + sError + "\n";
        try
        {
            ArchivoW = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true, System.Text.Encoding.UTF8);

            try
            {
                ArchivoW.Write(strLineaTMP);
            }
            catch (Exception)
            {
            }
            ArchivoW.Close();
        }
        catch (UnauthorizedAccessException)
        {

        }
    }

    public void WriteFile(string path, string fileName, string xml)
    {
        System.IO.StreamWriter ArchivoW;

        try
        {
            DateTime date = DateTime.Now;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(path))
                File.Delete(path + fileName);

            ArchivoW = new System.IO.StreamWriter(path + fileName, true, System.Text.Encoding.UTF8);
            ArchivoW.Write(xml);
            ArchivoW.Close();
        }
        catch (Exception ex)
        {
            LogError(ex.Message + ex.StackTrace + ex.InnerException);
        }
    }

    public string WriteBytes(string path, string fileName, byte[] bytes)
    {
        string strBase64 = string.Empty;

        try
        {
            DateTime date = DateTime.Now;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(path))
                File.Delete(path + fileName);

            File.WriteAllBytes(path + fileName, bytes);
        }
        catch (Exception ex)
        {
            LogError(ex.Message + ex.StackTrace + ex.InnerException);
        }

        return strBase64;
    }

    public void DownloadFile(byte[] toBytes, string fileName)
    {
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
        HttpContext.Current.Response.ContentType = "application/octet-stream";
        HttpContext.Current.Response.BinaryWrite(toBytes); 
        HttpContext.Current.Response.End();
    }

    public Object CheckDbNull(Object obj, enumObjectType ObjectType)
    {
        Object objReturn;
        objReturn = obj;
        if (ObjectType == enumObjectType.strType && DBNull.Value == obj)
            return "";
        else if (ObjectType == enumObjectType.intType && DBNull.Value == obj)
            return 0;
        else if (ObjectType == enumObjectType.dblType && DBNull.Value == obj)
            return 0.0;
        else if (ObjectType == enumObjectType.dateType && DBNull.Value == obj)
            return null;
        else if (ObjectType == enumObjectType.boolType && DBNull.Value == obj)
            return false;
        else if (ObjectType == enumObjectType.charType && DBNull.Value == obj)
            return 0;

        return objReturn;
    }

    public T SetNullStringToDefault<T>(T entity, string _default)
    {
        List<PropertyInfo> properties = entity.GetType().GetProperties().ToList();
        foreach (var property in properties)
        {
            //if property is string
            if (property.PropertyType == typeof(string))
            {
                string value = property.GetValue(entity, null) as string;
                if (string.IsNullOrEmpty(value))
                    property.SetValue(entity, _default, null);
            }
        }
        return entity;
    }
}