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
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Collections;

namespace PuedesPasARG.Controllers
{
    // ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

    public class ValidarDXController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(ValidarDXController));
        BaseDatos bd = new BaseDatos();
        // GET: Home
        

        public ActionResult Index()
        {
            BaseDatos bd = new BaseDatos();
            this.recuperarDiagnosticos();
            log.Debug("INDEX INICIO");
            this.recupearReglasDiagnosticos();
            return View(this.recuperarDiagnosticos());
        }

        
        public List<Dx> recuperarDiagnosticos()
        {
            List<Dx> lisDiagnosticos = new List<Dx>();
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SEHTA;Data Source=DESKTOP-KV23RRC\\SQL_SERVER";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "exec prc_obtener_dxs_nuevos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dx dx = new Dx()
                            {
                                id = reader.GetInt32(0),
                                edad = reader.GetInt32(1),
                                sistolica = reader.GetInt32(2),
                                diastolica = reader.GetInt32(3),
                                arp = reader.GetDouble(4),
                                conc_aldo = reader.GetDouble(5),
                                cromogranina_a = reader.GetDouble(6),
                                dopamina = reader.GetDouble(7),
                                epinefrina = reader.GetDouble(8),
                                igf1 = reader.GetDouble(9),
                                norepinefrina = reader.GetDouble(10),
                                serotonina = reader.GetDouble(11),
                                t4 = reader.GetDouble(12),
                                tsh = reader.GetDouble(13),
                                acido_5_hidrox = reader.GetDouble(14),
                                acido_vainillin_man = reader.GetDouble(15),
                                cortisol_libre = reader.GetDouble(16),
                                epinefrina_orina = reader.GetDouble(17),
                                norepinefrina_orina = reader.GetDouble(18),
                                proteinuria = reader.GetDouble(19),
                                sodio = reader.GetDouble(20),
                                supr_dexa = reader.GetDouble(21),
                                tfg = reader.GetDouble(22),
                                coartacion = reader.GetBoolean(23),
                                estenosis_renal = reader.GetBoolean(24),
                                quistes = reader.GetInt32(25),
                                iah = reader.GetInt32(26),
                                cortisol_nocturno = reader.GetDouble(27),
                                ant_poliquistosis = reader.GetBoolean(28),
                                cons_drogdas = reader.GetBoolean(29),
                                cons_otras = reader.GetBoolean(30),
                                cons_farmacos = reader.GetBoolean(31),
                                nivelPA = reader.GetString(32),
                                etiologia = reader.GetString(33),
                                estado = reader.GetString(34),
                                comentario = reader.GetString(35),
                            };

                            lisDiagnosticos.Add(dx);
                        }
                    }
                }
            }

            return lisDiagnosticos;
        }


        public int ValidarDX(string idDx , string comentario, string estado)
        {
            int op = 0;
            log.Debug("rechazar diagnostico funcion");
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SEHTA;Data Source=DESKTOP-KV23RRC\\SQL_SERVER";
           
            string updateQuery = "UPDATE diagnostico SET estado = @NuevoValor1, comentario = @NuevoValor2 WHERE id= " + idDx;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Parámetros para la consulta
                        command.Parameters.AddWithValue("@NuevoValor1", estado);
                        command.Parameters.AddWithValue("@NuevoValor2", comentario);
                        // Puedes agregar más parámetros según sea necesario

                        // Ejecutar la consulta de actualización
                        int rowsAffected = command.ExecuteNonQuery();

                        log.Debug($"Filas afectadas: {rowsAffected}");
                        op = rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug("Error: " + ex.Message);
            }
            return op;
        }

        public void recupearReglasDiagnosticos()
        {
            List<Regla> lisreglas = new List<Regla>();
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SEHTA;Data Source=DESKTOP-KV23RRC\\SQL_SERVER";

            string updateQuery = "select * from dbo.reglas_diagnostico";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Aquí, debes mapear los valores de las columnas a propiedades de tu clase
                                Regla r = new Regla()
                                {
                                    idDx = reader.GetInt32(1),
                                    regla = reader.GetString(2),
                                    
                                // Agrega más propiedades según tu tabla
                                };
                                log.Debug("Recuperado idDX:" + reader.GetInt32(1) + "rEGLA: " + reader.GetString(2));
                                lisreglas.Add(r);
                            }
                        }
                    }
                }
                //guardo el listado de reglas utilizadas para los diagnosticos
                System.Web.HttpContext.Current.Session["lisReglas"] = lisreglas;
            }
            catch (Exception ex)
            {
                log.Debug("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult obtenerReglas(string dx)
        {
            //recupero todas las reglas de todos los diagnosticos
            List<Regla> lisreglas = (List<Regla>)System.Web.HttpContext.Current.Session["lisReglas"];
            List<Regla> lr = new List<Regla>();

            if(lisreglas != null)
                foreach( Regla r in lisreglas)
                {
                        if (r.idDx == Int32.Parse(dx))
                            lr.Add(r);
                }
            log.Debug(Json(lr, JsonRequestBehavior.AllowGet));
            return Json(lr, JsonRequestBehavior.AllowGet);
        }
    } 
    
}