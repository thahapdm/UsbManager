using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace iTuner
{
    public class DriveOperations : IDisposable
    {


        public void Dispose()
        {
           
            GC.SuppressFinalize(this);
        }


        public DriveOperations()
		{
			
		}
        ~DriveOperations()
		{
			Dispose();
		}

        public DriveContent GetFullDirectoryFileList(UsbDiskCollection ObjUsbList)
        {
           // DriveContentCollection driveContentColl = new DriveContentCollection();
            DriveContent Contents = new DriveContent();
            Contents.Name = "My Computer";
            Contents.FullPath = "MyComputer";
            Contents.OperationAllowed = false;
            Contents.FileTypes = FileType.Folder;

            

            if (ObjUsbList != null && ObjUsbList.Count > 0)
            {
                Contents.driveContentCollection = new DriveContentCollection();
                foreach (UsbDisk usbobj in ObjUsbList)
                {

                    DriveContent ContentsDrive = new DriveContent();
                    ContentsDrive.Name = usbobj.DeviceId+"\\";
                    ContentsDrive.FullPath = usbobj.DeviceId+"\\";
                    ContentsDrive.OperationAllowed = false;
                    ContentsDrive.FileTypes = FileType.Folder;

                   // ContentsDrive = GetSubDirectoryFiles(ContentsDrive);

                    Contents.driveContentCollection.Add(ContentsDrive);
                }

            }




           // driveContentColl.Add(Contents);

            return Contents;
        }


        public  DriveContent GetSubDirectoryFiles(DriveContent ObjDriveContent)
        {

            if (ObjDriveContent == null)

                return ObjDriveContent;

            DirectoryInfo Dir = new DirectoryInfo(ObjDriveContent.FullPath);
            ObjDriveContent.driveContentCollection = new DriveContentCollection();

            try
            {

                foreach (DirectoryInfo Di in Dir.GetDirectories())
                {

                    DriveContent DriveContentNew = new DriveContent();
                    DriveContentNew.Name = Di.Name;
                    DriveContentNew.FullPath = Di.FullName + "\\";
                    DriveContentNew.OperationAllowed = true;
                    DriveContentNew.FileTypes = FileType.Folder;

                   // DriveContentNew = GetSubDirectoryFiles(DriveContentNew);
                    ObjDriveContent.driveContentCollection.Add(DriveContentNew);
                }
            }
            catch (Exception Ex)
            {
            }

              try
            {

            foreach (FileInfo Fi in Dir.GetFiles())
            {

                DriveContent DriveContentNew = new DriveContent();
                DriveContentNew.Name = Fi.Name;
                DriveContentNew.FullPath =  Fi.FullName;
                DriveContentNew.OperationAllowed = true;
                DriveContentNew.FileTypes = FileType.File;
                ObjDriveContent.driveContentCollection.Add(DriveContentNew);
            }
            }
              catch (Exception Ex1)
              {
              }

            return ObjDriveContent;
        }



    }


  
}
