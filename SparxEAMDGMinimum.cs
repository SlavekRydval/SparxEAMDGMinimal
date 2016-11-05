using System;
using System.Windows.Forms;

namespace MDGMinimum
{
    public class SparxEAMDGMinimum
    {

        public String EA_Connect(EA.Repository Repository) => "MDG";

        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        const string menuHeader = "-MDG Minimum";
        const string MDGConnectExternalProject = "&Connect External Project";
        const string menuItemAbout = "&About…";
        
        public object EA_GetMenuItems(EA.Repository Repository, string Location, string MenuName)
        {
            switch (MenuName)
            {
                case "":
                    return menuHeader;
                case menuHeader:
                    string[] subMenus = { menuItemAbout };
                    return subMenus;
            }
            return "";
        }

        bool IsProjectOpen(EA.Repository Repository)
        {
            try
            {
                EA.Collection c = Repository.Models;
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected EA.ObjectType emergencyGetContextItemType(EA.Repository Repository)
        {
            EA.ObjectType vOT = Repository.GetContextItemType();
            if (vOT == EA.ObjectType.otNone)
            {
                try
                {
                    if (((EA.Package)(Repository.GetContextObject())).ParentID == 0)
                        return EA.ObjectType.otModel;
                }
                catch
                {
                    return vOT;
                }
            }
            return vOT;
        }

        public void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            bool vIsProjectOpen = IsProjectOpen(Repository);
            EA.ObjectType vOT = vIsProjectOpen ? emergencyGetContextItemType(Repository) : EA.ObjectType.otNone;

            switch (ItemName)
            {
                case menuItemAbout:
                    IsEnabled = true;
                    break;
                case MDGConnectExternalProject:
                    IsEnabled = vIsProjectOpen && (vOT == EA.ObjectType.otPackage);
                    break;
                default:
                    IsEnabled = false;
                    break;
            }
        }

        public void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                case menuItemAbout:
                    MessageBox.Show("Sparx EA MDG Minimum");
                    break;

                default:
                    MessageBox.Show($"Unhandled menu item '{ItemName}'!");
                    break;
            }
        }



        ///-----------------
        ///MGD
        ///-----------------
        public void MDG_BuildProject(EA.Repository Repository, EA.Package Package) { }

        public int MDG_Connect(EA.Repository Repository, int PackageID, string PackageGuid) => PackageID;

        public int MDG_Disconnect(EA.Repository Repository, string PackageGuid) => 1;

        public string[] MDG_GetConnectedPackages(EA.Repository Repository) => null; //new string[] { "{895AC636-D120-45fe-861B-55A3EC22DB7F}" };

        public object MDG_GetProperty(EA.Repository Repository, string PackageGuid, string PropertyName)
        {
            switch (PropertyName)
            {
                case "IconID": return System.Reflection.Assembly.GetExecutingAssembly().Location + "#ico";
                case "Language": return "";
                case "HiddenMenus": return EA.MDGMenus.mgBuildProject | EA.MDGMenus.mgMerge | EA.MDGMenus.mgRun;
                default: throw new NotImplementedException($"MDG_GetProperty (name=={PropertyName})");
            }
        }

        public int MDG_Merge(EA.Repository Repository, string PackageGuid, object SynchObjects, string SynchType, 
            object ExportObjects, object ExportFiles, object ImportFiles, string IgnoreLocked, string Language) => 1;

        public string MDG_NewClass(EA.Repository Repository, string PackageGuid, string CodeID, string Language) => null;

        public int MDG_PostGenerate(EA.Repository Repository, string PackageGuid, string FilePath, string FileContents) => 0;

        public int MDG_PostMerge(EA.Repository Repository, string PackageGuid) => 0;

        public int MDG_PreGenerate(EA.Repository Repository, string PackageGuid) => 0;

        public int MDG_PreMerge(EA.Repository Repository, string PackageGuid) => 0;

        public void MDG_PreReverse(EA.Repository Repository, string PackageGuid, object FilePaths) { }

        public void MDG_RunExe(EA.Repository Repository, string PackageGuid) { }

        public int MDG_View(EA.Repository Repository, string PackageGuid, string CodeID) => 0;

    }
}
