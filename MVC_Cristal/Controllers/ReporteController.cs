using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using MVC_Cristal.Models;

namespace MVC_Cristal.Controllers
{
    public class ReporteController : Controller
    {
        //
        // GET: /Reporte/
        public ActionResult Index()
        {
            #region Datos dummy

            List<Persona> datos = new List<Persona>(){
                                            new Persona() { Nombre = "Sacarias", Apellido = "Piedras del Rio", Codigo = 1234 },
                                            new Persona() { Nombre = "Alan", Apellido = "Brito", Codigo = 4323 },
                                            new Persona() { Nombre = "Marcos", Apellido = "Pinto", Codigo = 9090 }
            };

            #endregion Datos dummy

            string DirectorioReportesRelativo = "~/";
            string urlArchivo = string.Format("{0}.{1}", "MiReporte", "rdlc");

            string FullPathReport = string.Format("{0}{1}",
                                    this.HttpContext.Server.MapPath(DirectorioReportesRelativo),
                                     urlArchivo);

            ReportViewer Reporte = new ReportViewer();

            Reporte.Reset();
            Reporte.LocalReport.ReportPath = FullPathReport;
            ReportDataSource DataSource = new ReportDataSource("DS_MiReporte", datos);
            Reporte.LocalReport.DataSources.Add(DataSource);
            Reporte.LocalReport.Refresh();
            byte[] file = Reporte.LocalReport.Render("PDF");

            return File(new MemoryStream(file).ToArray(),
                      System.Net.Mime.MediaTypeNames.Application.Octet,
                /*Esto para forzar la descarga del archivo*/
                      string.Format("{0}{1}", "archivoprueba.", "PDF"));
        }
    }
}