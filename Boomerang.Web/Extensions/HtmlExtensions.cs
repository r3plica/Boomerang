using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Boomerang.Web.Extensions
{
    public static class HtmlExtensions
    {
        public static string decodeText(string text)
        {
            return HttpUtility.UrlDecode(text);
        }

        public static MvcHtmlString decodeEscapedText(string text)
        {
            return encodeText(decodeText(text));
        }

        public static MvcHtmlString encodeText(string text)
        {
            if (text != null && text != "")
            {
                StringBuilder builder = new StringBuilder();
                string[] lines = text.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i > 0)
                        builder.Append("<br/>");
                    builder.Append(HttpUtility.HtmlEncode(lines[i]));
                }
                return MvcHtmlString.Create(builder.ToString());
            }
            else
            {
                return MvcHtmlString.Create("");
            }
        }

        public static string ToNearestFileSize(this int bytes)
        {
            string[] Suffix = { " B", " KB", " MB", " GB", " TB" };
            int i = 0;
            double dblSByte = bytes;
            if (bytes > 1024)
                for (i = 0; (bytes / 1024) > 0; i++, bytes /= 1024)
                    dblSByte = bytes / 1024.0;
            
            return String.Format("{0:0.##}{1}", dblSByte, Suffix[i]);
        }
    }
}
