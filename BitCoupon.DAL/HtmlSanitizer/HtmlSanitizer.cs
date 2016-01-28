using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.Xml;

namespace BitCoupon.DAL.HtmlSanitizer
{
    public class HtmlSanitizer
    {
         public HashSet<string> WhiteList = new HashSet<string>() 
        {
                { "p" },
                { "br" },
                { "strong" },
                { "b" },
                { "a" },
                { "i" },                
                { "h1" },
                { "h2" },
                { "h3" },
                { "h4" },
                { "h5" },
                { "span" },
                {"small"},
                {"em"},
                {"sup"},
                {"code"},
                { "br/" }
        };

        /// <summary>
        /// Cleans up an HTML string and removes HTML tags in blacklist
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string SanitizeHtml(string html, params string[] whiteList)
        {
            var sanitizer = new HtmlSanitizer();
            if (whiteList != null && whiteList.Length > 0)
            {
                sanitizer.WhiteList.Clear();
                foreach (string item in whiteList)
                    sanitizer.WhiteList.Add(item);
            }
            return sanitizer.Sanitize(html);
        }

        /// <summary>
        /// Cleans up an HTML string by removing elements
        /// on the blacklist and all elements that start
        /// with onXXX .
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string Sanitize(string html)
        {
            var doc = new HtmlDocument();

            doc.LoadHtml(html);
            SanitizeHtmlNode(doc.DocumentNode);

            //return doc.DocumentNode.WriteTo();

            string output = null;

            // Use an XmlTextWriter to create self-closing tags
            using (StringWriter sw = new StringWriter())
            {
                XmlWriter writer = new XmlTextWriter(sw);
                doc.DocumentNode.WriteTo(writer);
                output = sw.ToString();

                // strip off XML doc header
                if (!string.IsNullOrEmpty(output))
                {
                    int at = output.IndexOf("?>");
                    output = output.Substring(at + 2);
                }

                writer.Close();
            }
            doc = null;

            return output;
        }

        private void SanitizeHtmlNode(HtmlNode node)
        {
            if (node.NodeType == HtmlNodeType.Element)
            {
                // check for blacklist items and remove
                if (!WhiteList.Contains(node.Name))
                {
                    node.Remove();
                    return;
                }

                // remove CSS Expressions and embedded script links
                if (node.Name == "style")
                {
                    var val = node.InnerHtml;
                    if (string.IsNullOrEmpty(node.InnerText))
                    {
                        if (HasExpressionLinks(val) || HasScriptLinks(val) )
                            node.ParentNode.RemoveChild(node);
                    }
                }

                // remove script attributes
                if (node.HasAttributes)
                {
                    for (int i = node.Attributes.Count - 1; i >= 0; i--)
                    {
                        HtmlAttribute currentAttribute = node.Attributes[i];

                        var attr = currentAttribute.Name.ToLower();
                        var val = currentAttribute.Value.ToLower();

                        // remove event handlers
                        if (attr.StartsWith("on"))
                            node.Attributes.Remove(currentAttribute);

                        // Remove CSS Expressions
                        else if (attr == "style" &&
                                 val != null &&
                                 HasExpressionLinks(val) || HasScriptLinks(val))
                            node.Attributes.Remove(currentAttribute);

                        // remove script links from all attributes
                        else if (
                            //(attr == "href" || attr== "src" || attr == "dynsrc" || attr == "lowsrc") &&
                                 val != null &&
                                 HasScriptLinks(val) )                                 
                            node.Attributes.Remove(currentAttribute);
                    }
                }
            }

            // Look through child nodes recursively
            if (node.HasChildNodes)
            {
                for (int i = node.ChildNodes.Count - 1; i >= 0; i--)
                {
                    SanitizeHtmlNode(node.ChildNodes[i]);
                }
            }
        }

        private bool HasScriptLinks(string value)
        {
            return value.Contains("javascript:") || value.Contains("vbscript:");
        }

        private bool HasExpressionLinks(string value)
        {
            return value.Contains("expression");
        }
        
    }
}
