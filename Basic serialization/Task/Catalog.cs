using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Task
{
    [Serializable]
    public class Catalog: IXmlSerializable
    {
        private const string book = "book";

        [XmlArrayItem(book)]
        public List<Book> Books { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (!reader.EOF)
            {
                reader.ReadStartElement(book);
                this.Books.Add(reader.ReadContentAsObject() as Book);
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            for (int i = 0; i < this.Books.Count; i++)
            {
                writer.WriteStartElement(book);
                writer.WriteValue(this.Books[i]);
                writer.WriteEndElement();
            }
        }
    }
}
