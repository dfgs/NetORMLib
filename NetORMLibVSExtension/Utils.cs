using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLibVSExtension.Models;

namespace NetORMLibVSExtension
{
    public static class Utils
    {
        public static readonly string databaseFileName = "database.tbx";
        public static ProjectItem GetProjectItem(Project Project,string Name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (ProjectItem item in Project.ProjectItems)
            {
                if (item.Name == Name) return item;
            }
            return null;
        }
        public static ProjectItem GetProjectItem(ProjectItem Parent, string Name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (ProjectItem item in Parent.ProjectItems)
            {
                if (item.Name == Name) return item;
            }
            return null;
        }

        public static DTE GetDTE(IServiceProvider ServiceProvider)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return ServiceProvider.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
        }

        public static Project GetActiveProject(IServiceProvider ServiceProvider)
        {
            object[] activeSolutionProjects;
            EnvDTE.Project project;
            EnvDTE.DTE dte;

            ThreadHelper.ThrowIfNotOnUIThread();
            dte = GetDTE(ServiceProvider);
            if (dte == null) return null;

            activeSolutionProjects = dte.ActiveSolutionProjects as object[];
            if (activeSolutionProjects == null) return null;

            foreach (object activeSolutionProject in activeSolutionProjects)
            {
                project = activeSolutionProject as EnvDTE.Project;
                if (project != null) return project;
                
            }
            return null;

        }

        public static ProjectItem CreateFile(ProjectItem Parent,string Name)
        {
            string fileName;

            ThreadHelper.ThrowIfNotOnUIThread();

            fileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Parent.ContainingProject.FullName), Parent.Name, Name);
            using (Stream stream = System.IO.File.Create(fileName))
            {

            }
            return Parent.ProjectItems.AddFromFile(fileName);
        }

	

		public static Stream OpenFile(Project Parent, string Name)
		{
			string fileName;

			ThreadHelper.ThrowIfNotOnUIThread();

			fileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Parent.FullName), Name);
			return new FileStream(fileName, FileMode.Open);
		}

		public static Stream OpenFile(ProjectItem Parent, string Name)
		{
			string fileName;

			ThreadHelper.ThrowIfNotOnUIThread();

			fileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Parent.ContainingProject.FullName), Parent.Name, Name);
			return new FileStream(fileName, FileMode.Open);
		}

		public static void CreateDatabaseFile(IServiceProvider ServiceProvider)
        {
            string fileName;
            ThreadHelper.ThrowIfNotOnUIThread();
            Project project;
            ProjectItem file;
			CreateTable createTable;

            project = Utils.GetActiveProject(ServiceProvider);
            if (project == null) return;


            file = Utils.GetProjectItem(project, Utils.databaseFileName);
            if (file != null)
            {
                VsShellUtilities.ShowMessageBox(ServiceProvider, "Database file already exists", "Information", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            Database database = new Database();
            Revision revision = new Revision();
            database.Revisions.Add(revision);

			createTable = new CreateTable() { Name = "Table1" }; createTable.Columns.Add(new Column() { Name = "ID", Type = "DBInt" });
			revision.Changes.Add(createTable);
			createTable = new CreateTable() { Name = "Table2" }; createTable.Columns.Add(new Column() { Name = "ID", Type = "DBInt" });
			revision.Changes.Add(createTable);


			fileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(project.FullName), Utils.databaseFileName);
            database.SaveToFile(fileName);
            project.ProjectItems.AddFromFile(fileName);

            project.Save();

            VsShellUtilities.ShowMessageBox(ServiceProvider, "Database file created", "Information", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

        }

		private static void GenerateTableFile(Stream Stream,string NameSpace,string Table,IEnumerable<Column> Columns)
		{
			StreamWriter writer;

			writer = new StreamWriter(Stream);

			writer.WriteLine("using NetORMLib.Columns;");
			writer.WriteLine("using NetORMLib.DbTypes;");
			writer.WriteLine();
			writer.WriteLine($"namespace {NameSpace}");
			writer.WriteLine("{");
			writer.WriteLine($"	public static class {Table}");
			writer.WriteLine("	{");
			foreach (Column column in Columns)
			{
				writer.Write($"		public static readonly IColumn<{Table}, {column.Type}> {column.Name} = new Column<{Table}, {column.Type}>()");
				if (column.IsPrimaryKey || column.IsIdentity)
				{
					writer.Write("{ ");
					if (column.IsPrimaryKey) writer.Write("IsPrimaryKey = true;");
					if (column.IsPrimaryKey) writer.Write("IsPrimaryKey = true;");
					writer.Write("} ");
				}
				writer.WriteLine(";");
			}
			writer.WriteLine("	}");
			writer.WriteLine("}");

			writer.Flush();
		}
	


		public static void CreateORMFiles(IServiceProvider ServiceProvider)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Project project;
            ProjectItem folder, file,databaseFile;
			Database database;

            project = Utils.GetActiveProject(ServiceProvider);
            if (project == null) return;

			databaseFile = Utils.GetProjectItem(project, Utils.databaseFileName);
			if (databaseFile == null)
			{
				VsShellUtilities.ShowMessageBox(ServiceProvider, "No Database file found, you must create it first", "Information", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
				return;
			}

			folder = Utils.GetProjectItem(project, "Tables");
			if (folder == null) folder = project.ProjectItems.AddFolder("Tables");

			using (Stream stream = Utils.OpenFile(project, Utils.databaseFileName))
			{
				database = Database.LoadFromStream(stream);
			}
			
			foreach (CreateTable table in database.EnumerateCreateTables())
			{
				file = Utils.GetProjectItem(folder, $"{table.Name}.cs");
				if (file == null) file = Utils.CreateFile(folder, $"{table.Name}.cs");
				using (Stream stream = Utils.OpenFile(folder, $"{table.Name}.cs"))
				{
					Utils.GenerateTableFile(stream, $"{project.Name}.Tables", table.Name, database.EnumerateColumns(table.Name));
				}
			}


            project.Save();

            VsShellUtilities.ShowMessageBox(ServiceProvider, "ORM files created", "Information", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

        }

		
	}
}
