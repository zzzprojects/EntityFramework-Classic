#if NETSTANDARD
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Z.EntityFramework.Classic
{
    /// <summary>Manager for UseDatabaseFirst option</summary>
    internal static class UseDatabaseFirstManager
    {
        /// <summary>Executes to convert the model in conceptual, mapping and storage file.</summary>
        /// <param name="modelName">Convert the model in conceptual, mapping and storage file.</param>
        internal static void Execute(string modelName)
        {
            var path = System.IO.Directory.GetCurrentDirectory() + "\\" + modelName;
            var fileInfo = new FileInfo(path);

            using (StreamReader reader = new StreamReader(fileInfo.FullName))
            {
                // GET model element
                XmlElement conceptualSchemaElement;
                XmlElement mappingElement;
                XmlElement storageSchemaElement;
                string processingValue;
                EntityDesignerUtils.ExtractConceptualMappingAndStorageNodes(reader, out conceptualSchemaElement, out mappingElement, out storageSchemaElement, out processingValue);

                // SAVE model element
                var outputDirectory = fileInfo.Directory.FullName + @"\";
                string modelWithoutExtensions = fileInfo.Name.Replace(".edmx", string.Empty);

                OutputXml(outputDirectory + modelWithoutExtensions + ".csdl", conceptualSchemaElement);
                OutputXml(outputDirectory + modelWithoutExtensions + ".msl", mappingElement);
                OutputXml(outputDirectory + modelWithoutExtensions + ".ssdl", storageSchemaElement);
            }
        }
        private static void OutputXml(string outputPath, XmlElement xmlElement)
        {
            FileInfo info = new FileInfo(outputPath);
            Stream stream = null;
            try
            {
                if (info.Exists)
                {
                    stream = new FileStream(outputPath, FileMode.Truncate, FileAccess.Write);
                }
                else
                {
                    stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
                }

                using (stream)
                {
                    EntityDesignerUtils.OutputXmlElementToStream(xmlElement, stream);
                }
                    
            }
            catch (Exception exception)
            {
                throw new Exception("Fail to save UseDatabaseFirst steam: " + info.FullName, exception);
            }
        }

        private static void OutputXmlElementToStream(XmlElement xmlElement, Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
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
    }
}
#endif