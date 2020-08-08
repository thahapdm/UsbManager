//************************************************************************************************
// Copyright © 2010 Steven M. Cohn. All Rights Reserved.
//
//************************************************************************************************

namespace UsbManagerDemo
{
	using System;
	using System.Windows.Forms;
	using iTuner;
    using System.Drawing;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

	public partial class MainForm : Form
	{
		private static readonly string CR = Environment.NewLine;
	
		private UsbManager manager;
        private DriveContent LeftDriveContent = null, RightDriveContent = null, 
            driveContent = null, SelectedDriveContent = null, CompareSourceDriveContent = null, CompareDestinationDriveContent=null;
        private DriveOperations ObjDriveOP = new DriveOperations();

        private FileOperationAllowed FileOP;

		public MainForm ()
		{
			InitializeComponent();
            if (File.Exists(@AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\m_bg.jpg"))
                        {
                            this.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\m_bg.jpg");
              }

            if (File.Exists(@AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\Home.png"))
            {
                BtnHome.Image = Image.FromFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\Home.png");
                btnLeftLoad.Image = Image.FromFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\Home.png");
                BtnRightLoad.Image = Image.FromFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\Home.png");
                btnLeftrefresh.Image = Image.FromFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\Home.png");
                btnRightRefresh.Image = Image.FromFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\Home.png");
            }

            ContextMenu cmLPaste = new ContextMenu();
            MenuItem MenuLPaste = new MenuItem("Past");
            MenuLPaste.Click += new System.EventHandler(this.MenuLPasteleft1_Click);
            cmLPaste.MenuItems.Add(MenuLPaste);
            PnlLeft.ContextMenu = cmLPaste;



            ContextMenu cmRPaste = new ContextMenu();
            MenuItem MenuRPaste = new MenuItem("Past");
            MenuRPaste.Click += new System.EventHandler(this.MenuRPasteleft1_Click);
            cmRPaste.MenuItems.Add(MenuRPaste);
            pnlRight.ContextMenu = cmRPaste;



            LoadHomeData();
          
		}
        private void MenuLPasteleft1_Click(object sender, EventArgs e)
        {
            if (SelectedDriveContent != null)
            {
                if (LeftDriveContent != null)
                {

                    if (LeftDriveContent.OperationAllowed)
                    {

                        FileOPerations(SelectedDriveContent, LeftDriveContent, FileOP);
                        LeftDriveContent = ObjDriveOP.GetSubDirectoryFiles(LeftDriveContent);
                        PostFolders(LeftDriveContent, PnlLeft);
                    }

                    else
                    {
                        MessageBox.Show(" Paste will Not Allow here", "My File", MessageBoxButtons.OK);

                    }


                }


            }

        }

        private void MenuRPasteleft1_Click(object sender, EventArgs e)
        {
            if (SelectedDriveContent != null)
            {
                if (RightDriveContent != null)
                {

                    if (RightDriveContent.OperationAllowed)
                    {
                        FileOPerations(SelectedDriveContent, RightDriveContent, FileOP);
                        RightDriveContent = ObjDriveOP.GetSubDirectoryFiles(RightDriveContent);
                        PostFolders(RightDriveContent, pnlRight);
                       

                    }

                    else
                    {
                        MessageBox.Show(" Paste will Not Allow here", "My File", MessageBoxButtons.OK);

                    }


                }


            }

        }

        private void FileOPerations(DriveContent Source, DriveContent Destination, FileOperationAllowed FileOP)
        {

            if (Source.FileTypes == FileType.Folder)
            {
                if (Directory.Exists(Destination.FullPath + Source.Name))
                {
                    MessageBox.Show(" Paste will Not Allow here bacause Folder already exists", "My File", MessageBoxButtons.OK);
                    return;
                }

            }

            else
            {

                if (File.Exists(Destination.FullPath + Source.Name))
                {
                    MessageBox.Show(" Paste will Not Allow here bacause File already exists ", "My File", MessageBoxButtons.OK);
                    return;
                }

            }



            try
            {
                switch (FileOP)
                {
                    case FileOperationAllowed.Copy:
                        if (Source.FileTypes == FileType.Folder)
                        {
                            CopyFolder(Source.FullPath, Destination.FullPath + Source.Name);
                        }
                        else
                        {
                            File.Copy(Source.FullPath, Destination.FullPath + Source.Name);
                            System.Threading.Thread.Sleep(500);

                        }
                        break;

                    case FileOperationAllowed.Cut:

                        if (Source.FileTypes == FileType.Folder)
                        {
                            CopyFolder(Source.FullPath, Destination.FullPath + Source.Name);
                            //Directory.Delete(Source.FullPath);

                            System.IO.DirectoryInfo di = new DirectoryInfo(Source.FullPath);

                            foreach (FileInfo file in di.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo dir in di.GetDirectories())
                            {
                                dir.Delete(true);
                            }
                            di.Delete(true);
                        }
                        else
                        {
                            File.Copy(Source.FullPath, Destination.FullPath + Source.Name);
                            File.Delete(Source.FullPath);
                            System.Threading.Thread.Sleep(500);

                        }

                        break;
                }
            }
            catch(Exception ex)
            {
            }

        }


        static public void CopyFolder(string sourceFolder, string destFolder)
        {


            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);

                System.Threading.Thread.Sleep(500);
            }
            
           
           

            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                if (!File.Exists(dest))
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

        private void LoadHomeData()
        {


            manager = new UsbManager();
            UsbDiskCollection disks = manager.GetAvailableDisks();

            txtLeftPanel.Text = "My Computer";
            txtRightPanel.Text = "My Computer";
            pnlRight.Controls.Clear();
            PnlLeft.Controls.Clear();


            DriveContent objDrvCont = ObjDriveOP.GetFullDirectoryFileList(disks);

            PostFolders(objDrvCont, PnlLeft);
            LeftDriveContent = objDrvCont;
            PostFolders(objDrvCont, pnlRight);
            RightDriveContent = objDrvCont;
        }

        private void PostFolders(DriveContent objDrvCont, Panel Pnl)
        {
            int i = 3;
            int x = 14;
            int itemCount = 1;
            if (objDrvCont != null)
            {
                foreach (DriveContent Objg in objDrvCont.driveContentCollection)
                {
                    ToolTip toolTip1 = new ToolTip();
                    Button btn = new Button();
                    btn.Name = Objg.FullPath;
                    btn.Text = Objg.Name;
                    if (Objg.FileTypes == FileType.Folder)
                    {
                        if (File.Exists(@AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\folder.png"))
                        {
                            btn.Image = Image.FromFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\folder.png");
                           
                        }
                    }

                    else
                    {
                        if (File.Exists(@AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\file.png"))
                        {
                            btn.Image = Image.FromFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "images\\file.png");
                            
                        }
                    }
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.ImageAlign = ContentAlignment.MiddleRight;
                    btn.TextAlign = ContentAlignment.MiddleLeft; 
                    toolTip1.SetToolTip(btn, Objg.Name);
                    btn.Location = new Point(i, x);
                    btn.BackColor = System.Drawing.Color.White;
                    btn.ForeColor = System.Drawing.Color.Blue;
                    btn.Click += new System.EventHandler(this.btn_Click);

                    ContextMenu cm = new ContextMenu();
                    MenuItem miOne = new MenuItem("Copy");
                    miOne.Name = Objg.FullPath;
                    MenuItem miTwo = new MenuItem("Cut");
                    miTwo.Name = Objg.FullPath;

                    MenuItem mithree = new MenuItem("Compare");
                    mithree.Name = Objg.FullPath;
                  
                    if (Pnl.Name == "PnlLeft")
                    {

                        miOne.Click += new System.EventHandler(this.menuItemleft1_Click);
                        miTwo.Click += new System.EventHandler(this.menuItemleft2_Click);
                        mithree.Click += new System.EventHandler(this.menuItemleft3_Click);
                    }

                    else
                    {
                        miOne.Click += new System.EventHandler(this.menuItemright1_Click);
                        miTwo.Click += new System.EventHandler(this.menuItemright2_Click);
                        mithree.Click += new System.EventHandler(this.menuItemright3_Click);
                    }

                    cm.MenuItems.Add(miOne);
                    cm.MenuItems.Add(miTwo);
                    cm.MenuItems.Add(mithree);
                    btn.ContextMenu = cm;

                    Pnl.Controls.Add(btn);
                    i += 140;

                    itemCount = itemCount + 1;
                    if (itemCount == 4)
                    {

                        x += 80;
                        i = 3;
                        itemCount = 1;
                    }
                }
            }

        }

		private void DoStateChanged (UsbStateChangedEventArgs e)
		{
			
		}

        private void menuItemleft1_Click(object sender, EventArgs e)
        {
            MenuItem BtnMenuItem = ((MenuItem)(sender));

             if( BtnMenuItem != null)
            {
                driveContent = LeftDriveContent.driveContentCollection.TraverseFullPath(BtnMenuItem.Name);
                if (driveContent != null)
                {
                    if (driveContent.OperationAllowed)
                    {
                        FileOP = FileOperationAllowed.Copy;
                        SelectedDriveContent = driveContent;
                    }
                    else
                    {
                        MessageBox.Show(" Cut and Copy will Not Allow", "My File", MessageBoxButtons.OK);
                    }

                }

            }
          
        }

        private void menuItemleft2_Click(object sender, EventArgs e)
        {
            MenuItem BtnMenuItem = ((MenuItem)(sender));

            if (BtnMenuItem != null)
            {
                driveContent = LeftDriveContent.driveContentCollection.TraverseFullPath(BtnMenuItem.Name);
                if (driveContent != null)
                {
                    if (driveContent.OperationAllowed)
                    {
                        FileOP = FileOperationAllowed.Cut;
                        SelectedDriveContent = driveContent;
                    }
                    else
                    {
                        MessageBox.Show(" Cut and Copy will Not Allow","My File",MessageBoxButtons.OK);

                    }
                }

            }
        }

        private void menuItemleft3_Click(object sender, EventArgs e)
        {
            MenuItem BtnMenuItem = ((MenuItem)(sender));

            if (BtnMenuItem != null)
            {
                driveContent = LeftDriveContent.driveContentCollection.TraverseFullPath(BtnMenuItem.Name);
                if (driveContent != null)
                {
                    if (driveContent.OperationAllowed)
                    {

                        if (CompareSourceDriveContent == null)
                            CompareSourceDriveContent = driveContent;

                        else
                        {
                            CompareDestinationDriveContent = driveContent;
                            List<string> OutPut = GetDiffOfSubfolders(CompareSourceDriveContent.FullPath, CompareDestinationDriveContent.FullPath);
                            CompareSourceDriveContent = null;
                            CompareSourceDriveContent = null;
                            var str = String.Join(",", OutPut.ToArray());
                            if (str == string.Empty)
                            {
                                str = "both are Same";
                            }
                            MessageBox.Show(str,"My File", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show(" Compare Will not  Allow","My File",MessageBoxButtons.OK);

                    }
                }

            }
        }


        

        private void menuItemright1_Click(object sender, EventArgs e)
        {
            MenuItem BtnMenuItem = ((MenuItem)(sender));

            if (BtnMenuItem != null)
            {
                driveContent = RightDriveContent.driveContentCollection.TraverseFullPath(BtnMenuItem.Name);
                if (driveContent != null)
                {
                    if (driveContent.OperationAllowed)
                    {
                        FileOP = FileOperationAllowed.Copy;
                        SelectedDriveContent = driveContent;
                    }
                    else
                    {
                        MessageBox.Show(" Cut and Copy will Not Allow", "My File", MessageBoxButtons.OK);
                    }

                }

            }
        }

        private void menuItemright2_Click(object sender, EventArgs e)
        {
            MenuItem BtnMenuItem = ((MenuItem)(sender));

            if (BtnMenuItem != null)
            {
                driveContent = RightDriveContent.driveContentCollection.TraverseFullPath(BtnMenuItem.Name);
                if (driveContent != null)
                {
                    if (driveContent.OperationAllowed)
                    {
                        FileOP = FileOperationAllowed.Cut;
                        SelectedDriveContent = driveContent;
                    }
                    else
                    {
                        MessageBox.Show(" Cut and Copy will Not Allow", "My File", MessageBoxButtons.OK);
                    }

                }

            }
        }

        private void menuItemright3_Click(object sender, EventArgs e)
        {
            MenuItem BtnMenuItem = ((MenuItem)(sender));

            if (BtnMenuItem != null)
            {
                driveContent = RightDriveContent.driveContentCollection.TraverseFullPath(BtnMenuItem.Name);
                if (driveContent != null)
                {
                    if (driveContent.OperationAllowed)
                    {

                        if (CompareSourceDriveContent == null)
                            CompareSourceDriveContent = driveContent;

                        else
                        {
                            CompareDestinationDriveContent = driveContent;
                        List<string> OutPut=   GetDiffOfSubfolders(CompareSourceDriveContent.FullPath, CompareDestinationDriveContent.FullPath);
                            CompareSourceDriveContent = null;
                            CompareSourceDriveContent = null;
                            var str = String.Join(",", OutPut.ToArray());
                            if (str == string.Empty)
                            {
                                str = "both are Same";
                            }
                            MessageBox.Show(str, "My File", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show(" Compare Will not  Allow", "My File", MessageBoxButtons.OK);
                    }

                }

            }
        }



       

private List<string> GetDiffOfSubfolders(string source, string dest)
        {
            List<string> notMatchedSubFolders = new List<string>();
            try
            {

                if (!Directory.Exists(source))
                {
                    MessageBox.Show(" Source Directory Doesn't exists ", "My File", MessageBoxButtons.OK);

                    return notMatchedSubFolders;
                }

                if (!Directory.Exists(dest))
                {
                    MessageBox.Show(" Destination Directory Doesn't exists ", "My File", MessageBoxButtons.OK);

                    return notMatchedSubFolders;
                }

                DirectoryInfo sourceDir = new DirectoryInfo(source);

                DirectoryInfo destinationDir = new DirectoryInfo(dest);


             


                var subDirsSrc = sourceDir.GetDirectories();
                var subDirsDesc = destinationDir.GetDirectories();
                var subDirsDescFolderNames = subDirsDesc.Select(x => x.Name).ToList();

              

                foreach (var folder in subDirsSrc)
                {
                    if (subDirsDescFolderNames.Contains(folder.Name))
                    {
                        DirectoryInfo sourceSubDir = new DirectoryInfo(folder.FullName);
                        var list1 = sourceSubDir.GetFiles("*", SearchOption.AllDirectories).Select(x => Path.GetFileName(x.FullName));

                        string destinationSubFolderName = subDirsDesc.FirstOrDefault(x => x.Name == folder.Name).FullName;
                        DirectoryInfo destSubDir = new DirectoryInfo(destinationSubFolderName);
                        var list2 = destSubDir.GetFiles("*", SearchOption.AllDirectories).Select(x => Path.GetFileName(x.FullName));

                        var diff = list1.Except(list2);

                        if (diff.Any())
                        {
                            notMatchedSubFolders.Add(folder.FullName);
                        }
                    }
                    else
                    {
                        notMatchedSubFolders.Add(folder.FullName);
                    }
                }

            }
            catch( Exception  ex)
            { }
    return notMatchedSubFolders;
}


        private void btn_Click(object sender, EventArgs e)
        {
           string  Operation="";
            Button BtnDrive= ((Button)(sender));
           if( BtnDrive != null)
          {
           Panel PnlParent = (Panel)BtnDrive.Parent;
           if (PnlParent != null)
           {


              

               if (PnlParent.Name == "PnlLeft")
               {
                   Operation = "Left";
                  
                 driveContent=  LeftDriveContent.driveContentCollection.TraverseFullPath(BtnDrive.Name);
                 if (driveContent != null)
                 {
                 if (driveContent.FileTypes == FileType.Folder)
                 {

                     txtLeftPanel.Text = BtnDrive.Name;
                         PnlParent.Controls.Clear();
                         driveContent = ObjDriveOP.GetSubDirectoryFiles(driveContent);
                         PostFolders(driveContent, PnlParent);
                         LeftDriveContent = driveContent;
                     }
                 }
               }

               else if (PnlParent.Name == "pnlRight")
               {

                   Operation = "Right";
                  driveContent= RightDriveContent.driveContentCollection.TraverseFullPath(BtnDrive.Name);
                  if (driveContent != null)
                  {
                  if (driveContent.FileTypes == FileType.Folder)
                  {
                      txtRightPanel.Text = BtnDrive.Name;
                          PnlParent.Controls.Clear();
                          driveContent = ObjDriveOP.GetSubDirectoryFiles(driveContent);
                          PostFolders(driveContent, PnlParent);
                          RightDriveContent = driveContent;
                      }
                  }
               }


           }
          }

        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            LoadHomeData();
        }

        

        private void BtnRightLoad_Click(object sender, EventArgs e)
        {
            SpecificLoadHomeData(pnlRight, txtRightPanel, ref RightDriveContent);
        }

        private void btnLeftLoad_Click(object sender, EventArgs e)
        {
            SpecificLoadHomeData(PnlLeft, txtLeftPanel, ref LeftDriveContent);
        }


        private void SpecificLoadHomeData(Panel pnl,TextBox   txtBox ,  ref DriveContent  ProDriveContent)
        {


            manager = new UsbManager();
            UsbDiskCollection disks = manager.GetAvailableDisks();

            txtBox.Text = "My Computer";
            
            pnl.Controls.Clear();
           


            DriveContent objDrvCont = ObjDriveOP.GetFullDirectoryFileList(disks);

            PostFolders(objDrvCont, pnl);
            ProDriveContent = objDrvCont;
            
        }

        private void btnLeftrefresh_Click(object sender, EventArgs e)
        {

            PnlLeft.Controls.Clear();
            if (RightDriveContent.Name == "My Computer")
            {
                driveContent = RightDriveContent;
            }

            else
            {
                driveContent = ObjDriveOP.GetSubDirectoryFiles(RightDriveContent);
            }
            PostFolders(driveContent, PnlLeft);
            LeftDriveContent = driveContent;

        }

        private void btnRightRefresh_Click(object sender, EventArgs e)
        {
            pnlRight.Controls.Clear();
            if (RightDriveContent.Name == "My Computer")
            {
                driveContent = RightDriveContent;
            }

            else
            {
                driveContent = ObjDriveOP.GetSubDirectoryFiles(RightDriveContent);
            }
            PostFolders(driveContent, pnlRight);
            RightDriveContent = driveContent;

        }


      
	}
}
