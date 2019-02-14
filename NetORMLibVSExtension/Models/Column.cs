using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NetORMLibVSExtension.Models
{
	[Serializable]
	public class Column
	{
		[XmlAttribute]
		public string Name { get; set; }
		[XmlAttribute]
		public string Type { get; set; }
		[XmlAttribute]
		public bool AllowNull { get; set; }
		[XmlAttribute]
		public bool IsIdentity { get; set; }
		[XmlAttribute]
		public bool IsPrimaryKey { get; set; }
	}
}
