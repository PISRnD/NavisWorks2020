using System;
using System.IO;
using System.Xml.Serialization;

namespace PivdcNavisworksSupportModule
{
    public static class XmlOperation
    {
        public static object ReadXmlFile(this Type dataType, string xmlFullFilePath)
        {
            if (File.Exists(xmlFullFilePath))
            {
                FileStream fileStreamRead = new FileStream(xmlFullFilePath, FileMode.Open, FileAccess.Read);
                XmlSerializer xmlReadObject = new XmlSerializer(dataType);
                if (fileStreamRead.Length > 0)
                {
                    object availableData = xmlReadObject.Deserialize(fileStreamRead);
                    fileStreamRead.Close();
                    return availableData;
                }
                fileStreamRead.Close();
            }
            return null;
        }

        public static void CreateOrReplaceXml(this object dataToCreateXML, string xmlFullFilePath)
        {
            if (File.Exists(xmlFullFilePath))
            {
                File.Delete(xmlFullFilePath);
            }
            if (!Directory.Exists(Path.GetDirectoryName(xmlFullFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(xmlFullFilePath));
            }
            FileStream fileStream = new FileStream(xmlFullFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            XmlSerializer xmlSaveObject = new XmlSerializer(dataToCreateXML.GetType());
            xmlSaveObject.Serialize(fileStream, dataToCreateXML);
            fileStream.Close();
        }
    }
}