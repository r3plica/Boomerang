using Boomerang.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Boomerang.Components
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        private readonly JavaScriptSerializer js;

        private int Id { get; set; }
        private string UserId { get; set; }
        private QueryType Type { get; set; }
        private Document Document { get; set; }

        private string StorageRoot
        {
            get 
            {
                string FilePath = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/"); //Path should! always end with '/'
                if (Id > 0)
                    FilePath = Path.Combine(FilePath, Type.ToString() + "/" + Id + "/");

                return FilePath;
            } 
        }

        public UploadHandler()
        {
            js = new JavaScriptSerializer();
            js.MaxJsonLength = 41943040;
        }

        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.AddHeader("Pragma", "no-cache");
            context.Response.AddHeader("Cache-Control", "private, no-cache");

            HandleMethod(context);
        }

        // Handle request based on method
        private void HandleMethod(HttpContext context)
        {
            Id = Convert.ToInt32(context.Request["Id"]);
            Type = (QueryType)Convert.ToInt32(context.Request["Type"]);
            UserId = context.Request["UserId"];
            string Method = context.Request["Method"];

            if (Method == null)
            {
                switch (context.Request.HttpMethod)
                {
                    case "HEAD":
                    case "GET":
                        DeliverFile(context);
                        break;

                    case "POST":
                    case "PUT":
                        UploadFile(context);
                        break;

                    case "DELETE":
                        if (GivenFilename(context))
                            DeleteFile(context);
                        else
                            context.Response.ClearHeaders();
                            context.Response.StatusCode = 404;
                        break;

                    case "OPTIONS":
                        ReturnOptions(context);
                        break;

                    default:
                        context.Response.ClearHeaders();
                        context.Response.StatusCode = 405;
                        break;
                }
            }
            else
            {
                switch (Method.ToLower())
                {
                    case "delete": DeleteFile(context); break;
                }
            }
        }

        private static void ReturnOptions(HttpContext context)
        {
            context.Response.AddHeader("Allow", "DELETE,GET,HEAD,POST,PUT,OPTIONS");
            context.Response.StatusCode = 200;
        }

        // Delete file from the server
        private void DeleteFile(HttpContext context)
        {
            Document Document = new Document(Convert.ToInt32(context.Request["Id"]));

            string filePath = Document.FilePath + Document.FileName;
            if (File.Exists(filePath))
                File.Delete(filePath);

            Document.Delete(); // Delete our document
        }

        // Upload file to the server
        private void UploadFile(HttpContext context)
        {
            var statuses = new List<FilesStatus>();
            var headers = context.Request.Headers;

            if (string.IsNullOrEmpty(headers["X-File-Name"]))
            {
                UploadWholeFile(context, statuses);
            }
            else
            {
                UploadPartialFile(headers["X-File-Name"], context, statuses);
            }

            WriteJsonIframeSafe(context);
        }

        // Upload partial file
        private void UploadPartialFile(string fileName, HttpContext context, List<FilesStatus> statuses)
        {
            if (context.Request.Files.Count != 1) 
                throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");

            var inputStream = context.Request.Files[0].InputStream;
            if (!Directory.Exists(StorageRoot))
                Directory.CreateDirectory(StorageRoot);

            var fullName = StorageRoot + Path.GetFileName(fileName);

            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }

                fs.Flush();
                fs.Close();
            }
            statuses.Add(new FilesStatus(new FileInfo(fullName)));
        }

        private string AppendVersion(string FilePath, string FileName)
        {
            int s = FileName.LastIndexOf("(");
            if (s > 0)
            {
                int output;
                int e = FileName.LastIndexOf(")");
                var Append = FileName.Substring(s + 1, e - s - 1);
                if (Int32.TryParse(Append, out output))
                {
                    int Version = output + 1;

                    if (File.Exists(Path.Combine(FilePath, FileName)))
                        return AppendVersion(FilePath, FileName.Replace("(" + output + ")", "(" + Version + ")"));
                    else
                        return FileName;
                }
            } 
            
            if (File.Exists(Path.Combine(FilePath, FileName)))
                return AppendVersion(FilePath, FileName + " (1)");
            else
                return FileName + " (1)";
        }

        // Upload entire file
        private void UploadWholeFile(HttpContext context, List<FilesStatus> statuses)
        {
            if (!Directory.Exists(StorageRoot))
                Directory.CreateDirectory(StorageRoot);

            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                var file = context.Request.Files[i];
                var fullPath = StorageRoot + Path.GetFileName(file.FileName);

                if (file.FileName != null && file.FileName != "")
                {
                    bool Exists = File.Exists(fullPath);
                    fullPath = (Exists) ? fullPath.Replace(file.FileName, AppendVersion(StorageRoot, file.FileName)) : fullPath;

                    Document = new Document()
                    {
                        CandidateId = (Type == QueryType.Candidate) ? this.Id : 0,
                        ClientId = (Type == QueryType.Client) ? this.Id : 0,
                        FileName = (Exists) ? AppendVersion(StorageRoot, file.FileName) : file.FileName,
                        FileSize = file.ContentLength,
                        FilePath = this.StorageRoot,
                        UserId = this.UserId
                    };

                    file.SaveAs(fullPath);
                    Document.Save();

                    string fullName = Path.GetFileName(file.FileName);
                    statuses.Add(new FilesStatus(fullName, file.ContentLength, fullPath));
                }
            }
        }

        private void WriteJsonIframeSafe(HttpContext context)
        {
            context.Response.AddHeader("Vary", "Accept");
            try
            {
                if (context.Request["HTTP_ACCEPT"].Contains("application/json"))
                    context.Response.ContentType = "application/json";
                else
                    context.Response.ContentType = "text/plain";
            }
            catch
            {
                context.Response.ContentType = "text/plain";
            }

            var jsonObj = js.Serialize(Document);
            context.Response.Write(jsonObj);
        }

        private static bool GivenFilename(HttpContext context)
        {
            return !string.IsNullOrEmpty(context.Request["Id"]);
        }

        private void DeliverFile(HttpContext context)
        {
            int Id = Convert.ToInt32(context.Request["Id"]);
            Document d = new Document(Id);
            string FilePath = Path.Combine(d.FilePath, d.FileName);

            if (File.Exists(FilePath))
            {
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + d.FileName + "\"");
                context.Response.ContentType = "application/octet-stream";
                context.Response.ClearContent();
                context.Response.WriteFile(FilePath);
            }
            else
                context.Response.StatusCode = 404;
        }

        private void ListCurrentFiles(HttpContext context)
        {
            var files =
                new DirectoryInfo(StorageRoot)
                    .GetFiles("*", SearchOption.TopDirectoryOnly)
                    .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))
                    .Select(f => new FilesStatus(f))
                    .ToArray();

            string jsonObj = js.Serialize(files);
            context.Response.AddHeader("Content-Disposition", "inline; filename=\"files.json\"");
            context.Response.Write(jsonObj);
            context.Response.ContentType = "application/json";
        }

    }
}