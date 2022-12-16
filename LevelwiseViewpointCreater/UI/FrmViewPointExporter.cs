using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Interop;
using Levelwise_Viewpoint_Creater.Model;
using Levelwise_Viewpoint_Creater.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Levelwise_Viewpoint_Creater.UI
{
    public partial class FrmViewPointExporter : Form
    {
        public Document navisDocument { get; set; }
        List<string> viewPointList = new List<string>();
        List<SavedItemInfo> savedItemInfos = new List<SavedItemInfo>();

        public FrmViewPointExporter(Document document)
        {
            InitializeComponent();
            navisDocument = document;
        }

        private void FrmViewPointExporter_Load(object sender, EventArgs e)
        {
            navisDocument.SavedViewpoints.ToSavedItemCollection().Cast<SavedItem>()
               .ToList().Where(x => x.IsGroup).Cast<FolderItem>().ToList().ForEach(savedItem =>
               {
                   treeViewPointList.Nodes.Add(savedItem.DisplayName);
                  
               });

            navisDocument.SavedViewpoints.ToSavedItemCollection().Cast<SavedItem>()
               .ToList().Where(x => !x.IsGroup).ToList().ForEach(savedItem =>
               {
                   treeViewPointList.Nodes.Add(savedItem.DisplayName);
                   savedItemInfos.Add(new SavedItemInfo() 
                   { savedItem = savedItem, displayName = savedItem.DisplayName });

               });

            treeViewPointList.Nodes.Cast<TreeNode>().ToList().ForEach(treeNode =>
            {
                navisDocument.SavedViewpoints.ToSavedItemCollection().Cast<SavedItem>()
                .ToList()
                .Where(x => x.IsGroup && x.DisplayName == treeNode.Text).Cast<FolderItem>()
                .SelectMany(x => x.Children.ToList()).ToList().ForEach(childViewPoint =>
                {
                   

                    treeNode.Nodes.Add(childViewPoint.DisplayName);
                    savedItemInfos.Add(new SavedItemInfo() 
                    { savedItem = childViewPoint, displayName = childViewPoint.DisplayName });
                });

            });

            
        }
        //public void function1()
        //{
        //    exchange exchangeObj = XmlOperation.ReadXmlFile(typeof(exchange),
        //       txtImportViewpointFile.Text)
        //       as exchange;
        //    exchange temp = new exchange();

        //    temp.viewpoints = new exchangeViewpoints();

        //    temp.units = exchangeObj.units;
        //    temp.filename = exchangeObj.filename;
        //    temp.filepath = exchangeObj.filepath;


        //    string header = String.Format("<?xml version=\"1.0\" " +
        //         "encoding=\"UTF-8\" ?>\r\n<exchange xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        //         "xsi:noNamespaceSchemaLocation=\"http://download.autodesk.com/us/navisworks/schemas/nw-exchange-12.0.xsd\" " +
        //         "units=\"{0}\" filename=\"{1}\" filepath=\"{2}\">\r\n<viewpoints>",
        //         exchangeObj.units, exchangeObj.filename, exchangeObj.filepath);
        //    string footer = "</viewpoints>\r\n\t</exchange>";

        //    XElement root = XElement.Load(txtImportViewpointFile.Text);
        //    List<string> myElement1 = root.Elements("viewpoints").Descendants("view")
        //        .SelectMany(x => x.Parent.Nodes().ToList()).Select(x => x.ToString()).Distinct()
        //        .ToList();

        //    treeViewPointList.Nodes
        //        .Cast<TreeNode>().Where(x => x.Nodes.Count > 0)
        //        .SelectMany(x => x.Nodes.Cast<TreeNode>()
        //        .ToList()).Where(x => x.Checked).ToList().ForEach(checkednode =>
        //        {

        //            string body = myElement1.FirstOrDefault(x => x.Contains(checkednode.Text));

        //            StringBuilder stringBuilder = new StringBuilder();
        //            stringBuilder.Append(header);
        //            stringBuilder.Append(body);
        //            stringBuilder.Append(footer);
        //            File.WriteAllText(string.Format("{0}\\{1}.xml", txtExportLoc.Text, checkednode.Text),
        //                stringBuilder.ToString());


        //        });

        //    treeViewPointList.Nodes
        //        .Cast<TreeNode>().Where(x => x.Nodes.Count == 0 && x.Checked)
        //        .ToList().ForEach(checkednode =>
        //        {

        //            string body = myElement1.FirstOrDefault(x => x.Contains(checkednode.Text));

        //            StringBuilder stringBuilder = new StringBuilder();
        //            stringBuilder.Append(header);
        //            stringBuilder.Append(body);
        //            stringBuilder.Append(footer);


        //            File.WriteAllText(string.Format("{0}\\{1}.xml", txtExportLoc.Text, checkednode.Text),
        //                stringBuilder.ToString());


        //        });
        //}
        private void btnExport_Click(object sender, EventArgs e)
        {
            Export();
        }
        public void Export()
        {
            if (string.IsNullOrEmpty(txtExportLoc.Text))
            {
                MessageBox.Show("Please provide export location before process.");
                return;
            }


            treeViewPointList.Nodes
               .Cast<TreeNode>().Where(x => x.Nodes.Count > 0)
               .SelectMany(x => x.Nodes.Cast<TreeNode>()
               .ToList()).Where(x => x.Checked).ToList().ForEach(checkednode =>
               {

                   try
                   {
                       SavedViewpoint viewPoint = savedItemInfos
                                  .FirstOrDefault(x => x.displayName.Contains(checkednode.Text))
                                  .savedItem as SavedViewpoint;
                       string cameraString = viewPoint.Viewpoint.GetCamera();

                       File.WriteAllText(string.Format("{0}\\{1}.txt", txtExportLoc.Text,
                           checkednode.Text), cameraString);
                   }
                   catch (Exception ex)
                   {

                       string str = ex.ToString(); 
                   }


               });

            treeViewPointList.Nodes
                .Cast<TreeNode>().Where(x => x.Nodes.Count == 0 && x.Checked)
                .ToList().ForEach(checkednode =>
                {

                    try
                    {
                        SavedViewpoint viewPoint = savedItemInfos
                                .FirstOrDefault(x => x.displayName.Contains(checkednode.Text))
                                .savedItem as SavedViewpoint;
                        string cameraString = viewPoint.Viewpoint.GetCamera();

                        File.WriteAllText(string.Format("{0}\\{1}.txt", txtExportLoc.Text,
                            checkednode.Text), cameraString);
                    }
                    catch (Exception ex)
                    {

                        string str = ex.ToString(); 
                    }


                });

        }
      public void Import()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,
                Multiselect = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
               

                openFileDialog.FileNames.Cast<string>().ToList().ForEach(fileName =>
                {
                    FileInfo fileInfo = new FileInfo(fileName);



                    string cameraString = File.ReadAllText(fileName);

                    Viewpoint vPoint = navisDocument.CurrentViewpoint.CreateCopy();
                    vPoint.SetCamera(cameraString);
                    SavedViewpoint savedViewpoint = new SavedViewpoint(vPoint);
                    savedViewpoint.DisplayName = string.Format("{0}", fileInfo.Name.Replace(fileInfo.Extension,""));
                    navisDocument.SavedViewpoints.AddCopy(savedViewpoint);

                });


            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Import();
        }
    }
}
