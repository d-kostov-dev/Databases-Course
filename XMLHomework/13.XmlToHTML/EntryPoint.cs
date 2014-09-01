namespace XmlToHTML
{
    using System.Xml.Xsl;

    /// <summary>
    /// Create an XSL stylesheet, which transforms the file catalog.xml into HTML document, 
    /// formatted for viewing in a standard Web-browser.
    /// </summary>
    public class EntryPoint
    {
        public static void Main()
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("../../catalog.xsl");
            xslt.Transform("../../catalog.xml", "../../catalog.html");
        }
    }
}
