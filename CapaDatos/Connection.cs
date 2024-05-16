using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Connection
    {
        private OdbcCommand cmd;
        private OdbcConnection conex;
        private OdbcDataAdapter da;
        private DataTable dt;

        public string conectar()
        {
            string mensaje = "Conexión exitosa";
            conex = new OdbcConnection("Driver={MySQL ODBC 8.4 Unicode Driver};database=dbworkshoppanel;db=dbworkshoppanel;no_schema=1;port=3306;server=127.0.0.1;uid=root;user=root");
            conex.Open();
            return mensaje;
        }

        public void cerrar_Conexion()
        {
            conex.Close();
        }

        public DataTable consultar_Tabla(string consulta)
        {
            conectar();
            dt = new DataTable();
            if (conex.State == ConnectionState.Open)
            {
                cmd = new OdbcCommand(consulta, conex);
                da = new OdbcDataAdapter(cmd);
                da.Fill(dt);
                cerrar_Conexion();
            }
            return dt;
        }
          
        public bool generar_Sentencias(string sentencia_SQL)
        {
            bool respuesta = false;
            conectar();
            if (conex.State == ConnectionState.Open)
            {
                
                cmd = new OdbcCommand(sentencia_SQL, conex);
                try
                {
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
                catch
                {
                    
                }
                cerrar_Conexion();
            }
            return respuesta;
        }

        public OdbcDataAdapter generar_DataAdapter(string consulta)
        {
            conectar();
            if (conex.State == ConnectionState.Open)
            {
                cmd = new OdbcCommand(consulta, conex);
                da = new OdbcDataAdapter(cmd);
                conex.Close();
            }
            return da;
        }

        
    }
}
