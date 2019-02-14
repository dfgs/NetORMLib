using System;
using System.Collections.Generic;

namespace NetORMLibVSExtension.Models
{
    [Serializable]
    public class Revision
    {
        public List<Change> Changes
        {
            get;
            set;
        }
        public Revision()
        {
            Changes = new List<Change>();
        }
    }

   
}