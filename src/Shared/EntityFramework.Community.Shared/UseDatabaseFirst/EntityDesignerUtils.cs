#if NETSTANDARD
    
namespace Z.EntityFramework.Classic
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;

    internal static class EntityDesignerUtils
    {
        internal static readonly string _edmxFileExtension;
        internal const string EdmxNamespaceUriV1 = "http://schemas.microsoft.com/ado/2007/06/edmx";
        internal const string EdmxNamespaceUriV2 = "http://schemas.microsoft.com/ado/2008/10/edmx";
        internal const string EdmxNamespaceUriV3 = "http://schemas.microsoft.com/ado/2009/11/edmx";
        internal const string EdmxRootElementName = "Edmx";
        private static readonly EFNamespaceSet v1Namespaces;
        private static readonly EFNamespaceSet v2Namespaces;
        private static readonly EFNamespaceSet v3Namespaces;

        static EntityDesignerUtils()
        {
            EFNamespaceSet set = new EFNamespaceSet {
                Edmx = "http://schemas.microsoft.com/ado/2007/06/edmx",
                Csdl = "http://schemas.microsoft.com/ado/2006/04/edm",
                Msl = "urn:schemas-microsoft-com:windows:storage:mapping:CS",
                Ssdl = "http://schemas.microsoft.com/ado/2006/04/edm/ssdl"
            };
            v1Namespaces = set;
            set = new EFNamespaceSet {
                Edmx = "http://schemas.microsoft.com/ado/2008/10/edmx",
                Csdl = "http://schemas.microsoft.com/ado/2008/09/edm",
                Msl = "http://schemas.microsoft.com/ado/2008/09/mapping/cs",
                Ssdl = "http://schemas.microsoft.com/ado/2009/02/edm/ssdl"
            };
            v2Namespaces = set;
            set = new EFNamespaceSet {
                Edmx = "http://schemas.microsoft.com/ado/2009/11/edmx",
                Csdl = "http://schemas.microsoft.com/ado/2009/11/edm",
                Msl = "http://schemas.microsoft.com/ado/2009/11/mapping/cs",
                Ssdl = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl"
            };
            v3Namespaces = set;
            _edmxFileExtension = ".edmx";
        }

        internal static void ExtractConceptualMappingAndStorageNodes(StreamReader edmxInputStream, out XmlElement conceptualSchemaNode, out XmlElement mappingNode, out XmlElement storageSchemaNode, out string metadataArtifactProcessingValue)
        {
            XmlDocument document = new XmlDocument();
            using (XmlReader reader = XmlReader.Create(edmxInputStream))
            {
                document.Load(reader);
            }
            EFNamespaceSet set = v3Namespaces;
            if (document.DocumentElement.NamespaceURI == v2Namespaces.Edmx)
            {
                set = v2Namespaces;
            }
            else if (document.DocumentElement.NamespaceURI == v1Namespaces.Edmx)
            {
                set = v1Namespaces;
            }
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("edmx", set.Edmx);
            nsmgr.AddNamespace("edm", set.Csdl);
            nsmgr.AddNamespace("ssdl", set.Ssdl);
            nsmgr.AddNamespace("map", set.Msl);
            conceptualSchemaNode = (XmlElement) document.SelectSingleNode("/edmx:Edmx/edmx:Runtime/edmx:ConceptualModels/edm:Schema", nsmgr);
            storageSchemaNode = (XmlElement) document.SelectSingleNode("/edmx:Edmx/edmx:Runtime/edmx:StorageModels/ssdl:Schema", nsmgr);
            mappingNode = (XmlElement) document.SelectSingleNode("/edmx:Edmx/edmx:Runtime/edmx:Mappings/map:Mapping", nsmgr);
            metadataArtifactProcessingValue = string.Empty;
            XmlNodeList list = document.SelectNodes("/edmx:Edmx/edmx:Designer/edmx:Connection/edmx:DesignerInfoPropertySet/edmx:DesignerProperty", nsmgr);
            if (list != null)
            {
                foreach (XmlNode node in list)
                {
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        if (attribute.Name.Equals("Name", StringComparison.Ordinal) && attribute.Value.Equals("MetadataArtifactProcessing", StringComparison.OrdinalIgnoreCase))
                        {
                            foreach (XmlAttribute attribute2 in node.Attributes)
                            {
                                if (attribute2.Name.Equals("Value", StringComparison.Ordinal))
                                {
                                    metadataArtifactProcessingValue = attribute2.Value;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        internal static void OutputXmlElementToStream(XmlElement xmlElement, Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings {
                Encoding = Encoding.UTF8,
                Indent = true
            };
            XmlDocument document = new XmlDocument();
            XmlNode newChild = document.ImportNode(xmlElement, true);
            document.AppendChild(newChild);
            XmlWriter w = null;
            try
            {
                w = XmlWriter.Create(stream, settings);
                document.WriteTo(w);
            }
            finally
            {
                if (w != null)
                {
                    w.Close();
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct EFNamespaceSet
        {
            public string Edmx;
            public string Csdl;
            public string Msl;
            public string Ssdl;
        }
    }
}

#endif