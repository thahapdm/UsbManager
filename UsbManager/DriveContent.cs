using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace iTuner
{
    public class DriveContent
    {

        internal DriveContent()
		{
			
            this.FullPath = String.Empty;
            this.Name = String.Empty;
            this.OperationAllowed = false;
            this.driveContentCollection = null;
            this.FileTypes = FileType.File;
		}

        public string Name
        {
            get;
            internal set;
        }
        public string FullPath
        {
            get;
            internal set;
        }
        public Boolean OperationAllowed
        {
            get;
            internal set;
        }

        public FileType FileTypes
        {
            get;
            internal set;
        }

        public DriveContentCollection driveContentCollection
        {
            get;
            internal set;
        }
        
    }
}
