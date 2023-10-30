using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Dynamic;
using System.Text.Json;
using System.Web.Script.Serialization;
using System.Security.Policy;
using Ubiety.Dns.Core;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Infrastructure;
using log4net;
using System.Data.Entity.Core.Objects;
using PuedesPasARG.Models;
using System.Web.Helpers;
using System.Globalization;
using MySqlX.XDevAPI.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace PuedesPasARG.Controllers
{
    public class HomeController : Controller
    {
        Administradora adm = new Administradora();
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        BaseDatos bd = new BaseDatos();

        // GET: Home
      
        public ActionResult Index()
        {
            BaseDatos bd = new BaseDatos();
            return View();
        }

        [HttpPost]
        public String calcularPA()
        {
            List<string> listReglasPresionArterial = new List<string>();
            
            var resolveRequest = HttpContext.Request;
            resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
            string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create("http://localhost:8080/pa");
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
            streamWriter.Write(jsonString);
            log.Debug("Peticion: " + jsonString);
            streamWriter.Close();
            string result;

            WebResponse resp = httpRequest.GetResponse();
            using (var reader = new StreamReader(resp.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            log.Debug("Respuesta: " + result);

            JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            JsonElement reglasArray = root.GetProperty("reglas");
            if (reglasArray.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement regla in reglasArray.EnumerateArray())
                {
                    listReglasPresionArterial.Add(regla.GetString());
                }

                //guardo el listado de reglas utilizadas para calcular la PA en la sesion
                System.Web.HttpContext.Current.Session["listReglasPA"] = listReglasPresionArterial;
            }
            return result;
        }

        [HttpPost]
        public string determinarEtiologia()
        {
         List<string> listReglasEtiologia = new List<string>();

        var resolveRequest = HttpContext.Request;
        resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
            string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create("http://localhost:8080/etiologia");
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
            streamWriter.Write(jsonString);
            log.Debug("Peticion: " + jsonString);
            streamWriter.Close();
            string result;
            WebResponse resp = httpRequest.GetResponse();
            using (var reader = new StreamReader(resp.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            log.Debug("Respuesta: " + result);

            JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            JsonElement reglasArray = root.GetProperty("reglas");
            
            if (reglasArray.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement regla in reglasArray.EnumerateArray())
                {
                    listReglasEtiologia.Add(regla.GetString());
                }
                //guardo el listado de reglas utilizadas para calcular la Etiologia en la sesion
                System.Web.HttpContext.Current.Session["listReglasET"] = listReglasEtiologia;
            }
            return result;
        }

        public int agregarPersonaDB(string dni, string edad, string nombre, string apellido)
        {
            BaseDatos bd = new BaseDatos();
            ObjectParameter id_operacion = new ObjectParameter("idInsertado", 0);
            bd.prc_insert_paciente(Int32.Parse(dni), nombre, apellido, edad, id_operacion);
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["idPaciente"] = Int32.Parse(id_operacion.Value.ToString());
            System.Web.HttpContext.Current.Application.UnLock();
            return Int32.Parse(id_operacion.Value.ToString());
        }

        public int agregarPresionArterial(string sistolica, string diastolica)
        {
            BaseDatos bd = new BaseDatos();
            ObjectParameter id_operacion = new ObjectParameter("idInsertado", 0);
            bd.prc_insert_presion_arterial(Int32.Parse(sistolica), Int32.Parse(diastolica), id_operacion);
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["idPresionArterial"] = Int32.Parse(id_operacion.Value.ToString());
            System.Web.HttpContext.Current.Application.UnLock();
            return Int32.Parse(id_operacion.Value.ToString());
        }

        public int agregarDatosOrina(string supresion_dexametasona, string cortisol_libre, string tfg, string proteinuria, string epinefrina_orina, string norepinefrina_orina, string metanefrina_orina, string acido_vainillin_mandelico, string acido_hidroxindolacetico, string sodio)
        {
            BaseDatos bd = new BaseDatos();
            ObjectParameter id_operacion = new ObjectParameter("idInsertado", 0);
            bd.prc_insert_analisis_orina(casteoDecimal(acido_hidroxindolacetico), casteoDecimal(acido_vainillin_mandelico), casteoDecimal(cortisol_libre), casteoDecimal(epinefrina_orina), casteoDecimal(metanefrina_orina),
                casteoDecimal(norepinefrina_orina), casteoDecimal(proteinuria), casteoDecimal(sodio), casteoDecimal(supresion_dexametasona), casteoDecimal(tfg), id_operacion);
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["idOrina"] = Int32.Parse(id_operacion.Value.ToString());
            System.Web.HttpContext.Current.Application.UnLock();
            return Int32.Parse(id_operacion.Value.ToString());
        }

        private void cambiarCultureInfo(string cInfo) //en-US or es-AR
        {
            log.Debug("Se cambiara la configuracion de cultura a:" + cInfo);
            log.Debug("CurrentCulture actual: " + CultureInfo.CurrentCulture.Name.ToString());
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(cInfo);
            log.Debug("CurrentCulture nueva:" + CultureInfo.CurrentCulture.Name.ToString());
        }


        public int agregarDatosSangre(string arp, string concentracion_aldosterona, string cromogranina, string dopamina_sangre, string epinefrina_sangre, string igf1, string norepinefrina_sangre, string serotonina, string t4, string tsh)
        {
           
            BaseDatos bd = new BaseDatos();
            ObjectParameter id_operacion = new ObjectParameter("idInsertado", 0);
            bd.prc_insert_analisis_sangre(casteoDecimal(arp), casteoDecimal(concentracion_aldosterona), casteoDecimal(cromogranina), casteoDecimal(dopamina_sangre), casteoDecimal(epinefrina_sangre), casteoDecimal(igf1), casteoDecimal(norepinefrina_sangre), casteoDecimal(serotonina), casteoDecimal(t4), casteoDecimal(tsh), id_operacion);
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["idSangre"] = Int32.Parse(id_operacion.Value.ToString());
            System.Web.HttpContext.Current.Application.UnLock();
            return Int32.Parse(id_operacion.Value.ToString());
        }

        private double casteoDecimal(string n)
        {
            this.cambiarCultureInfo("en-US");
            double res = Math.Round((float)decimal.Parse(n), 2);
            this.cambiarCultureInfo("es-AR");
            return res;
        }

        public int agregarOtrosEstudios(string cortisol_nocturno, string coartacion, string estenosis_renal, string quistes, string indice_apnea_hipopnea)
        {
            BaseDatos bd = new BaseDatos();
            ObjectParameter id_operacion = new ObjectParameter("idInsertado", 0);
            bd.prc_insert_analisis_salival(casteoDecimal(cortisol_nocturno), id_operacion);
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["idSaliva"] = Int32.Parse(id_operacion.Value.ToString());
            System.Web.HttpContext.Current.Application.UnLock();
            bd.prc_insert_polisomnografia(Int32.Parse(indice_apnea_hipopnea), id_operacion);
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["idPoliso"] = Int32.Parse(id_operacion.Value.ToString());
            System.Web.HttpContext.Current.Application.UnLock();
            bd.prc_insert_estudio_imagen(Convert.ToBoolean(coartacion), Convert.ToBoolean(estenosis_renal), Int32.Parse(quistes), id_operacion);
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["idImagen"] = Int32.Parse(id_operacion.Value.ToString());
            System.Web.HttpContext.Current.Application.UnLock();
            return Int32.Parse(id_operacion.Value.ToString());
        }

        public int agregarAnamnesis(string ant_poliquistosis_renal, string consumo_drogas, string consumo_otras_sustancias, string consumo_farmacos)
        {
            BaseDatos bd = new BaseDatos();
            ObjectParameter id_operacion = new ObjectParameter("idInsertado", 0);
            bd.prc_insert_anamnesis( Convert.ToBoolean(ant_poliquistosis_renal), Convert.ToBoolean(consumo_drogas), Convert.ToBoolean(consumo_farmacos), Convert.ToBoolean(consumo_otras_sustancias), id_operacion);
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["idAnamnesis"] = Int32.Parse(id_operacion.Value.ToString());
            System.Web.HttpContext.Current.Application.UnLock();
            return Int32.Parse(id_operacion.Value.ToString());

        }

        public int agregarDiagnostico(string npa , string et)
        {
            BaseDatos bd = new BaseDatos();
            ObjectParameter id_operacion = new ObjectParameter("idInsertado", 0);
            System.Web.HttpContext.Current.Application.Lock();
            int idPaciente = Int32.Parse(System.Web.HttpContext.Current.Application["idPaciente"].ToString());
            int idOrina = Int32.Parse(System.Web.HttpContext.Current.Application["idOrina"].ToString());
            int idSangre = Int32.Parse(System.Web.HttpContext.Current.Application["idSangre"].ToString());
            int idSaliva = Int32.Parse(System.Web.HttpContext.Current.Application["idSaliva"].ToString());
            int idPoliso = Int32.Parse(System.Web.HttpContext.Current.Application["idPoliso"].ToString());
            int idImagen = Int32.Parse(System.Web.HttpContext.Current.Application["idImagen"].ToString());
            int idAnamnesis = Int32.Parse(System.Web.HttpContext.Current.Application["idAnamnesis"].ToString());
            int idPresionArterial = Int32.Parse(System.Web.HttpContext.Current.Application["idPresionArterial"].ToString());
            System.Web.HttpContext.Current.Application.UnLock();

            bd.prc_insert_diagnostico(DateTime.Now , npa, et, idPaciente , idOrina, idSangre, idPresionArterial, idPoliso, idImagen ,idSaliva, idAnamnesis,"NUEVO", "", id_operacion);


            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["idDiagnostico"] = Int32.Parse(id_operacion.Value.ToString());
            System.Web.HttpContext.Current.Application.UnLock();

//            List<string> ls = (List<string>)System.Web.HttpContext.Current.Session["listReglasPA"];
            guardarReglasDx(id_operacion.Value.ToString());
            return Int32.Parse(id_operacion.Value.ToString());
        }
        private void guardarReglasDx(string idDx)
        {
            
            List<string> ls1 = (List<string>)System.Web.HttpContext.Current.Session["listReglasPA"];
            if(ls1 != null)
            foreach (string s in ls1)
            {
                guardarReglas(idDx, s);
            }
            
            List<string> ls2 = (List<string>)System.Web.HttpContext.Current.Session["listReglasET"];
            if(ls2 != null)
            foreach (string s in ls2)
            {
                guardarReglas(idDx, s);
            }
        }

        public void guardarReglas(string idDx, string regla)
        {
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SEHTA;Data Source=DESKTOP-KV23RRC\\SQL_SERVER";

            string updateQuery = "INSERT INTO reglas_diagnostico(idDx,regla) VALUES(@NuevoValor1,@NuevoValor2)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NuevoValor1", idDx);
                        command.Parameters.AddWithValue("@NuevoValor2", regla);
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug("Error: " + ex.Message);
            }
        }

        public void validarDiagnostico(string comentario, string validar)
        {
            BaseDatos bd = new BaseDatos();
            ObjectParameter id_operacion = new ObjectParameter("idInsertado", 0);
            System.Web.HttpContext.Current.Application.Lock();
            int idDiagnostico = Int32.Parse(System.Web.HttpContext.Current.Application["idDiagnostico"].ToString());

            if ( validar.ToLower().Equals("true") )
            {
                bd.prc_actualizar_diagnostico(idDiagnostico, "CONFIRMADO", comentario);
            }
        else
            {
                bd.prc_actualizar_diagnostico(idDiagnostico, "RECHAZADO", comentario);
            }
        }

        
    } 

    
}