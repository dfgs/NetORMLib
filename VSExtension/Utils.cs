using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSExtension
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
    }
}
