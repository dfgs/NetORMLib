using System;
using System.Xml.Serialization;

namespace NetORMLibVSExtension.Models
{
    [Serializable,XmlInclude(typeof(CreateTable))]
    public abstract class Change
    {
    }
}