using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocios
{
    public class Validation
    {
        private Connection objConexion = new Connection(); 
        private string mensaje;
        
        public DataTable traer_Datos(string consulta)
        {
            return objConexion.consultar_Tabla(consulta);
        }

        public bool ejecutar_MySQL(string campos, string valores, string origen, string accion, string condicion)
        {
            bool respuesta;
            string sentencia = "";
            switch (accion)
            {
                case "INSERT":
                    if (validar_Campos(condicion) == "")
                    {
                        sentencia = "INSERT INTO " + origen + "(" + campos + ")" + "VALUES(" + valores + ")";
                    }
                    break;
                case "UPDATE":
                    sentencia = "UPDATE " + origen + " SET " + campos + " WHERE ID =+ " + valores;
                    break;
                case "DELETE":
                    if (validar_Campos(condicion) == "")
                    {
                        sentencia = "DELETE FROM " + origen + " WHERE " + valores;
                    }
                    break;
            }
            respuesta = objConexion.generar_Sentencias(sentencia);
            return respuesta;
        }                  
      
        public string validar_Campos(string condicion)
        {
            mensaje = "";
            DataTable dt = new DataTable();
            dt = objConexion.consultar_Tabla(condicion);
            if (dt.Rows.Count > 0)
            {
                mensaje = "Ya existe";
            }
            return mensaje;
        }

        public string validar_Usuario(string condicion)
        {
            mensaje = "";
            DataTable dt = new DataTable();
            dt = objConexion.consultar_Tabla(condicion);
            if (dt.Rows.Count == 0)
            {
                mensaje = "El nombre de usuario es incorrecto";
            }

            return mensaje;
        }

        public OdbcDataAdapter validar_DataAdapter(string consulta)
        {
            return objConexion.generar_DataAdapter(consulta);
        }
        
        public string enLetras(string valor)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;
            try
            {
                nro = Convert.ToDouble(valor);
            }
            catch
            {
                return "";
            }
            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }
            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        public string toText(double value)
        {
            string num2 = "";
            value = Math.Truncate(value);
            if (value == 0)
            { num2 = "CERO"; }
            else if (value == 1)
            { num2 = "UNO"; }
            else if (value == 2)
            { num2 = "DOS"; }
            else if (value == 3)
            { num2 = "TRES"; }
            else if (value == 4)
            { num2 = "CUATRO"; }
            else if (value == 5)
            { num2 = "CINCO"; }
            else if (value == 6)
            { num2 = "SEIS"; }
            else if (value == 7)
            { num2 = "SIETE"; }
            else if (value == 8)
            { num2 = "OCHO"; }
            else if (value == 9)
            { num2 = "NUEVE"; }
            else if (value == 10)
            { num2 = "DIEZ"; }
            else if (value == 11)
            { num2 = "ONCE"; }
            else if (value == 12)
            { num2 = "DOCE"; }
            else if (value == 13)
            { num2 = "TRECE"; }
            else if (value == 14)
            { num2 = "CATORCE"; }
            else if (value == 15)
            { num2 = "QUINCE"; }
            else if (value < 20)
            { num2 = "DIECI" + toText(value - 10); }
            else if (value == 20)
            { num2 = "VEINTE"; }
            else if (value < 30)
            { num2 = "VEINTI" + toText(value - 20); }
            else if (value == 30)
            { num2 = "TREINTA"; }
            else if (value == 40)
            { num2 = "CUARENTA"; }
            else if (value == 50)
            { num2 = "CINCUENTA"; }
            else if (value == 60)
            { num2 = "SESENTA"; }
            else if (value == 70)
            { num2 = "SETENTA"; }
            else if (value == 80)
            { num2 = "OCHENTA"; }
            else if (value == 90)
            { num2 = "NOVENTA"; }
            else if (value < 100)
            { num2 = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10); }
            else if (value == 100)
            { num2 = "CIEN"; }
            else if (value < 200)
            { num2 = "CIENTO" + toText(value - 100); }
            else if (value == 200)
            { num2 = toText(Math.Truncate(value / 100)) + "CIENTOS"; }
            else if (value == 300)
            { num2 = toText(Math.Truncate(value / 100)) + "CIENTOS"; }
            else if (value == 400)
            { num2 = toText(Math.Truncate(value / 100)) + "CIENTOS"; }
            else if (value == 500)
            { num2 = "QUINIENTOS"; }
            else if (value == 600)
            { num2 = toText(Math.Truncate(value / 100)) + "CIENTOS"; }
            else if (value == 700)
            { num2 = "SETECIENTOS"; }
            else if (value == 800)
            { num2 = toText(Math.Truncate(value / 100)) + "CIENTOS"; }
            else if (value == 900)
            { num2 = "NOVECIENTOS"; }
            else if (value < 1000)
            { num2 = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100); }
            else if (value == 1000)
            { num2 = "MIL"; }
            else if (value < 2000)
            { num2 = "MIL" + toText(value % 1000); }
            else if (value < 1000000)
            {
                num2 = toText(Math.Truncate(value / 1000)) + " MIL ";
                if ((value % 1000) > 0)
                { num2 = num2 + " " + toText(value % 1000); }
            }
            else if (value == 1000000)
            { num2 = " UN MILLON "; }
            else if (value < 2000000)
            { num2 = " UN MILLON " + toText(value % 1000000); }
            else if (value < 1000000000000)
            {
                num2 = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
                { num2 = num2 + " " + toText(value - Math.Truncate(value - 1000000) * 1000000); }
            }
            else if (value == 1000000000000)
            { num2 = "UN BILLON"; }
            else if (value < 2000000000000)
            { num2 = "UN BILLON" + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000); }
            else
            {
                num2 = toText(Math.Truncate(value / 1000000000000)) + " BILLONES ";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) num2 = num2 + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return num2;
        }
    }
}
