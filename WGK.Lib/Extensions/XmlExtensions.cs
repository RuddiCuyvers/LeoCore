using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WGK.Lib.Extensions
{
    /// <summary>
    /// Static class containing XML extension methods
    /// </summary>
    /// <remarks>
    /// Sometimes you want to convert an XmlNode to an XElement and back again.  Some programming libraries define
    /// methods that take XmlNode objects as parameters. These libraries also may contain properties and methods that
    /// return XmlNode objects.  However, it is more convenient to work with LINQ to XML instead of the classes in
    /// System.Xml (XmlDocument, XmlNode, etc.)  This class contains extension methods to do these conversions.
    /// 
    /// When converting between XmlDocument and XDocument the only special case that we have to handle is for the
    /// XML declaration.  In System.Xml, an XmlDeclaration object is a child node of an XmlDocument.  In contrast,
    /// in LINQ to XML, an XDeclaration is a property of an XDocument.  When converting from XmlDocument to XDocument,
    /// and from XDocument to XmlDocument, the extension methods determine if there is an XML declaration, and then
    /// creates the same declaration in the destination object.
    /// 
    /// Note that per the XML specification, documents can also contain children text nodes so long as they contain only
    /// white space.  These text nodes never have any impact on the semantic meaning of the document.  The default
    /// behavior of LINQ to XML is to not preserve insignificant white space when de-serializing, and then to format or
    /// indent the XML when serializing.  The extension in this class have this same behavior with regards to white space
    /// in sibling text nodes to the root element of the document.
    /// 
    /// Code originally published by Eric White
    /// http://blogs.msdn.com/b/ericwhite/archive/2008/12/22/convert-xelement-to-xmlnode-and-convert-xmlnode-to-xelement.aspx
    /// http://blogs.msdn.com/ericwhite/archive/2010/03/05/convert-xdocument-to-xmldocument-and-convert-xmldocument-to-xdocument.aspx
    /// </remarks>
    public static class XmlExtensions
    {
        /// <summary>
        /// Creates a LINQ-to-XML XElement from an XmlNode
        /// </summary>
        public static XElement GetXElement(this XmlNode pXmlNode)
        {
            // To create an XElement from an XmlNode, you create an XDocument, create an XmlWriter using the
            // XContainer.CreateWriter method, write the XmlNode to the XDocument, and return the root element
            // of the XDocument.
            XDocument vXDocument = new XDocument();
            using (XmlWriter vXmlWriter = vXDocument.CreateWriter())
            {
                pXmlNode.WriteTo(vXmlWriter);                
            }
            return vXDocument.Root;
        }

        /// <summary>
        /// Creates an XmlNode from a LINQ-to-XML XElement
        /// </summary>
        public static XmlNode GetXmlNode(this XElement pXElement)
        {
            // To create an XmlNode from an XElement, you create an XmlReader using the XNode.CreateReader method,
            // create an XmlDocument, and load the document using the XmlReader.
            // Remark: XmlDocument inherits XmlNode
            using (XmlReader vXmlReader = pXElement.CreateReader())
            {
                XmlDocument vXmlDocument = new XmlDocument();
                vXmlDocument.Load(vXmlReader);
                return vXmlDocument;
            }
        }

        /// <summary>
        /// Creates a LINQ-to-XML XDocument from an XmlDocument
        /// </summary>
        public static XDocument GetXDocument(this XmlDocument pXmlDocument)
        {
            XDocument vXDocument = new XDocument();
            using (XmlWriter xmlWriter = vXDocument.CreateWriter())
            {
                pXmlDocument.WriteTo(xmlWriter);                
            }

            XmlDeclaration vXmlDeclaration = pXmlDocument
                .ChildNodes
                .OfType<XmlDeclaration>()
                .FirstOrDefault();
            if (vXmlDeclaration != null)
            {
                vXDocument.Declaration = new XDeclaration(
                    vXmlDeclaration.Version,
                    vXmlDeclaration.Encoding,
                    vXmlDeclaration.Standalone);
            }

            return vXDocument;
        }

        /// <summary>
        /// Creates an XmlDocument from a LINQ-to-XML XDocument
        /// </summary>
        public static XmlDocument GetXmlDocument(this XDocument pXDocument)
        {
            using (XmlReader vXmlReader = pXDocument.CreateReader())
            {
                XmlDocument vXmlDocument = new XmlDocument();
                vXmlDocument.Load(vXmlReader);
                if (pXDocument.Declaration != null)
                {
                    XmlDeclaration vXmlDeclaration = vXmlDocument.CreateXmlDeclaration(
                        pXDocument.Declaration.Version,
                        pXDocument.Declaration.Encoding,
                        pXDocument.Declaration.Standalone);
                    vXmlDocument.InsertBefore(vXmlDeclaration, vXmlDocument.FirstChild);
                }
                return vXmlDocument;
            }
        }
    }
}
