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

        public static void CreateDatabaseFile(IServiceProvider ServiceProvider)
        {
            string fileName;
            ThreadHelper.ThrowIfNotOnUIThread();
            Project project;
            ProjectItem file;


            project = Utils.GetActiveProject(ServiceProvider);
            if (project == null) return;


            file = Utils.GetProjectItem(project, "database.xml");
            if (file != null)
            {
                VsShellUtilities.ShowMessageBox(ServiceProvider, "Database file already exists", "Information", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            Database database = new Database();
            Revision revision = new Revision();
            database.Revisions.Add(revision);
            revision.Changes.Add(new CreateTable() { Name = "Table1" });
            revision.Changes.Add(new CreateTable() { Name = "Table2" });

            fileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(project.FullName), "database.xml");
            database.SaveToFile(fileName);
            project.ProjectItems.AddFromFile(fileName);

            project.Save();

            VsShellUtilities.ShowMessageBox(ServiceProvider, "Database file created", "Information", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

        }

        public static void CreateORMFiles(IServiceProvider ServiceProvider)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Project project;
            ProjectItem folder, file;
 

            project = Utils.GetActiveProject(ServiceProvider);
            if (project == null) return;


            folder = Utils.GetProjectItem(project, "Tables");
            if (folder == null) folder = project.ProjectItems.AddFolder("Tables");

            file = Utils.GetProjectItem(folder,"test.cs");
            if (file == null) file = Utils.CreateFile(folder, "test.cs");

            project.Save();

            VsShellUtilities.ShowMessageBox(ServiceProvider, "ORM files created", "Information", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

        }
    }
}
