using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Task
{
    [Serializable]
    public class Book: IXmlSerializable
    {
        [XmlElement("author")]
        public string Author { get; set; }
        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("genre")]
        public Genres Genre { get; set; }
        [XmlElement("publisher")]
        public string Publisher { get; set; }
        [XmlIgnore]
        public DateTime PublishDate { get; set; }
        [XmlElement("publish_date")]
        public string PublishDateWrapper
        {
            get { return this.PublishDate.ToString("yyyy-MM-dd"); }
            set { this.PublishDate = DateTime.Parse(value); }
        }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlIgnore]
        public DateTime RegistrationDate { get; set; }
        [XmlElement("registration_date")]
        public string RegistraionDateWrapper
        {
            get { return this.RegistrationDate.ToString("yyyy-MM-dd"); }
            set { this.RegistrationDate = DateTime.Parse(value); }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadToFollowing("author");
            Author = reader.ReadElementContentAsString();
            Title = reader.ReadElementContentAsString();
            Genres temp;
            Enum.TryParse(reader.ReadElementContentAsString(), out temp);
            Genre = temp;
            Publisher = reader.ReadElementContentAsString();
            PublishDateWrapper = reader.ReadElementContentAsString();
            Description = reader.ReadElementContentAsString();
            RegistraionDateWrapper = reader.ReadElementContentAsString();
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("author");
            writer.WriteValue(Author);
            writer.WriteEndElement();

            writer.WriteStartElement("title");
            writer.WriteValue(Title);
            writer.WriteEndElement();

            writer.WriteStartElement("genre");
            writer.WriteValue(Genre.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("publisher");
            writer.WriteValue(Publisher);
            writer.WriteEndElement();

            writer.WriteStartElement("publish_date");
            writer.WriteValue(PublishDateWrapper);
            writer.WriteEndElement();

            writer.WriteStartElement("description");
            writer.WriteValue(Description);
            writer.WriteEndElement();

            writer.WriteStartElement("registration_date");
            writer.WriteValue(RegistraionDateWrapper);
            writer.WriteEndElement();
        }
    }
}
