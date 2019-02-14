using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NetORMLibVSExtension.Models
{
    [Serializable]
    public class Database
    {
        public List<Revision> Revisions
        {
            get;
            set;
        }
        public Database()
        {
            Revisions = new List<Revision>();
        }

        public void SaveToFile(string FileName)
        {
            using (FileStream stream = new FileStream(FileName, FileMode.Create))
            {
                SaveToStream(stream);
            }
        }

        public void SaveToStream(Stream Stream)
        {
            XmlSerializer serializer;

            serializer = new XmlSerializer(typeof(Database));
            serializer.Serialize(Stream, this);
        }

        public static Database LoadFromFile(string FileName)
        {
            using (FileStream stream = new FileStream(FileName, FileMode.Open))
            {
                return LoadFromStream(stream);
            }
        }
        public static Database LoadFromStream(Stream Stream)
        {
            XmlSerializer serializer;

            serializer = new XmlSerializer(typeof(Database));
            return (Database)serializer.Deserialize(Stream);
        }

		public IEnumerable<Change> EnumerateChanges()
		{
			foreach(Revision revision in Revisions)
			{
				foreach(Change change in revision.Changes)
				{
					yield return change;
				}
			}
		}

		public IEnumerable<CreateTable> EnumerateCreateTables()
		{
			foreach(Change change in EnumerateChanges())
			{
				if (change is CreateTable createTable) yield return createTable;
			}
		}

		public IEnumerable<Column> EnumerateColumns(string Table)
		{
			List<Column> items;

			items = new List<Column>();
			foreach (Change change in EnumerateChanges())
			{
				if ((change is CreateTable createTable) && (createTable.Name==Table)) items.AddRange(createTable.Columns);
				//TODO: Implement add and delete column
			}
			return items;
		}





	}
}
