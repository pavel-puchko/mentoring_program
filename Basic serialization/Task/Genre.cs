using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Task
{
    public enum Genres
    {
        [XmlEnum("Computer")]
        Computer,
        [XmlEnum("Fantasy")]
        Fantasy,
        [XmlEnum("Romance")]
        Romance,
        [XmlEnum("Horror")]
        Horror,
        [XmlEnum("Science Fiction")]
        ScienceFiction
    }
}
