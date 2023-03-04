using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;

namespace CrystalReportRenderWebAPI.Utility
{
    public static class CrystalReport
    {
        public static HttpResponseMessage RenderReport(DataTable dataTable, string reportPath, string reportFileName, string exportFilename)
        {
            var rd = new ReportDocument();

            rd.Load(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(reportPath), reportFileName));
            rd.SetDataSource(dataTable);
            MemoryStream ms = new MemoryStream();
            using (var stream = rd.ExportToStream(ExportFormatType.WordForWindows))
            {
                stream.CopyTo(ms);
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(ms.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = exportFilename
                };
            result.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/doc");
            return result;
        }
    }
}