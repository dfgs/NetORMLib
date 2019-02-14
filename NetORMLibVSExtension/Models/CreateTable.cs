using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NetORMLibVSExtension.Models
{
    [Serializable]
    public class CreateTable:Change
    {
        [XmlAttribute]
        public string Name
        {
            get;
            set;
        }

		public List<Column> Columns
		{
			get;
			set;
		}

		public CreateTable()
		{
			Columns = new List<Column>();
		}
	}
}
