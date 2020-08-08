using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Collections.ObjectModel;

namespace iTuner
{
    public class DriveContentCollection : ObservableCollection<DriveContent>
    {
        public bool Contains(string name)
        {
            return this.AsQueryable<DriveContent>().Any(d => d.Name == name) == true;
        }

        public bool ContainsFullPath(string FullPath)
        {
            return this.AsQueryable<DriveContent>().Any(d => d.FullPath == FullPath) == true;
        }
        /// <summary>
        /// Remove the named disk from the collection.
        /// </summary>
        /// <param name="name">The Windows name, or drive letter, of the disk to remove.</param>
        /// <returns>
        /// <b>True</b> if the item is removed; otherwise <b>false</b>.
        /// </returns>

        public bool Remove(string name)
        {
            DriveContent disk =
                (this.AsQueryable<DriveContent>()
                .Where(d => d.Name == name)
                .Select(d => d)).FirstOrDefault<DriveContent>();

            if (disk != null)
            {
                return this.Remove(disk);
            }

            return false;
        }

        public bool RemoveFullPath(string FullPath)
        {
            DriveContent disk =
                (this.AsQueryable<DriveContent>()
                .Where(d => d.FullPath == FullPath)
                .Select(d => d)).FirstOrDefault<DriveContent>();

            if (disk != null)
            {
                return this.Remove(disk);
            }

            return false;
        }


        public DriveContent TraverseFullPath(string FullPath)
        {
            DriveContent disk =
                (this.AsQueryable<DriveContent>()
                .Where(d => d.FullPath == FullPath)
                .Select(d => d)).FirstOrDefault<DriveContent>();

            if (disk != null)
            {
                return disk;
            }
            else if (disk  != null && disk.driveContentCollection != null && disk.driveContentCollection.Count > 0)
            {
                foreach (DriveContent diskd in disk.driveContentCollection)
                {
                 disk=  TraverseFullPath(diskd.FullPath);

                 if (disk == null)
                     continue;
                 else
                     break;



                }

            }

            return disk;
        }
    }
}
