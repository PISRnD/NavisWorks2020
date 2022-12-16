using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Timeliner;
using PiNavisworks.PiNavisworksSupport;
using PivdcNavisworksSupportModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;
using Nw = Autodesk.Navisworks.Api;
using Tl = Autodesk.Navisworks.Api.Timeliner;


namespace TimelineMerger
{
    public partial class Navisworks_Timeliner : Form
    {
        public static DateTime dtS = new DateTime();
        public static DateTime dtE = new DateTime();
        public static int sec = 0; 
        Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
        
        public string VPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public Navisworks_Timeliner()
        {
            InitializeComponent();

            TimelinerData timeliner = (oDoc.Timeliner as DocumentTimeliner).Value;
        }

        private static double DoWork(Progress progress, double stage, double maxValue, double increment)
        {
            for (; stage < maxValue; stage += increment)
            {
                progress.Update(stage);
                System.Threading.Thread.Sleep(100);
            }
            return stage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();

            Progress progress = Autodesk.Navisworks.Api.Application.BeginProgress();
            double stage = 0.0;

            progress.BeginSubOperation(0.99, "Exporting....");
            System.Threading.Thread.Sleep(100);

            stage = DoWork(progress, stage, 0.99, 0.01);

                //int Tcnt = TxmlString.Count();
                //int Scnt = SxmlString.Count();
                progress.EndSubOperation(true);                
                Autodesk.Navisworks.Api.Application.EndProgress();
        }
        
        public Object XMLToObject(string XMLString, Object oObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(oObject.GetType());
            oObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            XmlWriter tsk = XmlWriter.Create("GeneratedFile");
            tsk.Dispose();
            return oObject;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DatabaseManager.SetTime();
            openFileDialog1.ShowDialog();
            Progress progress1 = Autodesk.Navisworks.Api.Application.BeginProgress();
            double stage1 = 0.0;
            progress1.BeginSubOperation(0.99, "Processing....");
            System.Threading.Thread.Sleep(500);
            stage1 = DoWork(progress1, stage1, 0.99, 0.01);
            progress1.EndSubOperation(true);
            Autodesk.Navisworks.Api.Application.EndProgress();
            button4.Enabled = true;
            ToolSupport.InsertUsage(oDoc, 1);          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DatabaseManager.SetTime();
            openFileDialog2.ShowDialog();
            Progress progress1 = Autodesk.Navisworks.Api.Application.BeginProgress();
            double stage1 = 0.0;
            progress1.BeginSubOperation(0.99, "Processing....");
            System.Threading.Thread.Sleep(500);
            stage1 = DoWork(progress1, stage1, 0.99, 0.01);
            progress1.EndSubOperation(true);
            Autodesk.Navisworks.Api.Application.EndProgress();
            Navisworks_Timeliner frm = new Navisworks_Timeliner();
            ToolSupport.InsertUsage(oDoc, 1);           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DatabaseManager.SetTime();
            openFileDialog3.ShowDialog();
            Progress progress1 = Autodesk.Navisworks.Api.Application.BeginProgress();
            double stage1 = 0.0;
            progress1.BeginSubOperation(0.99, "Processing....");
            System.Threading.Thread.Sleep(500);
            stage1 = DoWork(progress1, stage1, 0.99, 0.01);
            progress1.EndSubOperation(true);
            Autodesk.Navisworks.Api.Application.EndProgress();
            ToolSupport.InsertUsage(oDoc, 1);            
        }
                                
        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void Navisworks_Timeliner_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var SAppear = XDocument.Load(openFileDialog1.OpenFile());

            if(SAppear!=null)
            {
                var ResultSet = from v1 in SAppear.Root.Descendants("SimAppearence")
                                select new
                                {
                                    t1 = v1.Element("DisplayName").Value,
                                    R = Convert.ToDouble(v1.Element("colrR").Value),
                                    G = Convert.ToDouble(v1.Element("colrG").Value),
                                    B = Convert.ToDouble(v1.Element("colrB").Value),
                                    t3 = Convert.ToDouble(v1.Element("Transperancy").Value)
                                };

                Nw.Document doc = Nw.Application.ActiveDocument;
                Nw.DocumentParts.IDocumentTimeliner tl = doc.Timeliner;

                Tl.DocumentTimeliner tl_doc = (Tl.DocumentTimeliner)tl;
                //Tl.SimulationAppearance newSimAppearence;
                Tl.SimulationAppearance newSimAppearence = null;
                int cnt = 0;
                ResultSet.ToList().ForEach((r) =>
                {
                    cnt++;
                    try
                    {
                        newSimAppearence = new Tl.SimulationAppearance(r.t1, new Autodesk.Navisworks.Api.Color(r.R, r.G, r.B), r.t3);

                        tl_doc.SimulationAppearanceAddCopy(newSimAppearence);
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }
                });
                button4.Enabled = true;
            }
            else
            {
                var MRes = MessageBox.Show("File Content empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            SimulationStatus sm = new SimulationStatus();

            var TType = XDocument.Load(openFileDialog3.OpenFile());

            if (TType != null)
            {
                var ResultSet3 = from v14 in TType.Root.Descendants("SimTaskType")
                                 select new
                                 {
                                     DisplayName = v14.Element("DisplayName").Value,
                                     EndAStatus = v14.Element("EndAStatus").Value,
                                     EndAMode = v14.Element("EndAMode").Value,
                                     OverrunAStatus = v14.Element("OverrunAStatus").Value,
                                     OverrunAMode = v14.Element("OverrunAMode").Value,
                                     SimulationStartAStatus = v14.Element("SimulationStartAStatus").Value,
                                     SimulationStartAMode = v14.Element("SimulationStartAMode").Value,
                                     StartAStatus = v14.Element("StartAStatus").Value,
                                     StartAMode = v14.Element("StartAMode").Value,
                                     UnderrunAStatus = v14.Element("UnderrunAStatus").Value,
                                     UnderrunAMode = v14.Element("UnderrunAMode").Value
                                 };

                Nw.Document doc = Nw.Application.ActiveDocument;
                Nw.DocumentParts.IDocumentTimeliner tl = doc.Timeliner;

                Tl.DocumentTimeliner tl_doc = (Tl.DocumentTimeliner)tl;

                int cnt = 0;
                Dictionary<string, SimulationAppearanceMode> simDic = new Dictionary<string, SimulationAppearanceMode>();
                simDic.Add("Default", SimulationAppearanceMode.Default);
                simDic.Add("Hidden", SimulationAppearanceMode.Hidden);
                simDic.Add("None", SimulationAppearanceMode.None);
                simDic.Add("UserAppearance", SimulationAppearanceMode.UserAppearance);
                ResultSet3.ToList().ForEach((r) =>
                {
                    cnt++;
                    try
                    {
                        Tl.SimulationTaskType TTaskType = new SimulationTaskType();
                        TTaskType.DisplayName = r.DisplayName;// +"_" + cnt;

                        SimulationAppearanceMode sam = SimulationAppearanceMode.None;
                        simDic.TryGetValue(r.StartAMode, out sam);

                        TTaskType.StartStatus.AppearanceMode = sam;// Tl.SimulationAppearanceMode.UserAppearance;
                        if (sam == SimulationAppearanceMode.UserAppearance)
                            TTaskType.StartStatus.SimulationAppearanceName = r.StartAStatus;

                        simDic.TryGetValue(r.EndAMode, out sam);
                        TTaskType.EndStatus.AppearanceMode = sam;
                        if (sam == SimulationAppearanceMode.UserAppearance)
                            TTaskType.EndStatus.SimulationAppearanceName = r.EndAStatus;

                        simDic.TryGetValue(r.OverrunAMode, out sam);
                        TTaskType.OverrunStatus.AppearanceMode = sam;
                        if (sam == SimulationAppearanceMode.UserAppearance)
                            TTaskType.OverrunStatus.SimulationAppearanceName = r.OverrunAStatus;

                        simDic.TryGetValue(r.SimulationStartAMode, out sam);
                        TTaskType.SimulationStartStatus.AppearanceMode = sam;
                        if (sam == SimulationAppearanceMode.UserAppearance)
                            TTaskType.SimulationStartStatus.SimulationAppearanceName = r.SimulationStartAStatus;

                        simDic.TryGetValue(r.UnderrunAMode, out sam);
                        TTaskType.UnderrunStatus.AppearanceMode = sam;
                        if (sam == SimulationAppearanceMode.UserAppearance)
                            TTaskType.UnderrunStatus.SimulationAppearanceName = r.UnderrunAStatus;
                        tl_doc.SimulationTaskTypeAddCopy(TTaskType);
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }

                    button3.Enabled = true;
                });
            }
            else
            {
                var MRes = MessageBox.Show("File Content empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            var TList = XDocument.Load(openFileDialog2.OpenFile());

            if(TList!=null)
            {
                var ResultSet1 = from v12 in TList.Root.Descendants("TLTask")
                                 select new
                                 {
                                     TYpe = v12.Element("TaskType").Value,
                                     DName = v12.Element("TaskName").Value,
                                     PeD = v12.Element("PlannedEndDate").Value,
                                     PsD = v12.Element("PlannedStartDate").Value,
                                 };

                Nw.Document doc = Nw.Application.ActiveDocument;
                Nw.DocumentParts.IDocumentTimeliner tl = doc.Timeliner;

                Tl.DocumentTimeliner tl_doc = (Tl.DocumentTimeliner)tl;

                Tl.TimelinerTask newTaskType = new Tl.TimelinerTask();

                int cnt = 0;
                ResultSet1.ToList().ForEach((r) =>
                {
                    cnt++;
                    try
                    {
                        newTaskType.DisplayName = r.DName;
                        newTaskType.PlannedEndDate = DateTime.Parse(r.PeD);
                        newTaskType.PlannedStartDate = DateTime.Parse(r.PsD);
                        newTaskType.SimulationTaskTypeName = r.TYpe;
                        tl_doc.TaskAddCopy(newTaskType);
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }
                });
            }
            else
            {
                var MRes = MessageBox.Show("File Content empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string FilePath = saveFileDialog1.FileName;

            try
            {
                TimelinerData timeliner = (oDoc.Timeliner as DocumentTimeliner).Value;
                List<SimAppearence> SimAppear = new List<SimAppearence>();
                List<SimAppearence> SimAppear1 = new List<SimAppearence>();
               
               
                List<SimAppearenceRoot> simuApperRootLst = new List<SimAppearenceRoot>();

                List<SimAppearenceRoot> ShowAppearenceList = new List<SimAppearenceRoot>();

                List<SimAppearence> simuApperLst = new List<SimAppearence>();
                   

                timeliner.SimulationAppearancesRoot.Children.ToList().ForEach((x) =>
                {
                    SimulationAppearance sSppr = x as SimulationAppearance;
                    SimAppearence sAppear = new SimAppearence();
                    //sAppear.Comments = sSppr.Comments;
                    sAppear.DisplayName = sSppr.DisplayName;
                    //sAppear.colr = Convert.ToString(sSppr.Color.R) + "#" + Convert.ToString(sSppr.Color.G) + "#" + Convert.ToString(sSppr.Color.B);
                    sAppear.colrR = Convert.ToString(sSppr.Color.R);
                    sAppear.colrG = Convert.ToString(sSppr.Color.G);
                    sAppear.colrB = Convert.ToString(sSppr.Color.B);
                    simuApperLst.Add(sAppear);

                    SimAppearenceRoot sRoot = new SimAppearenceRoot();
                    //sRoot.Comments = x.Comments;
                    sRoot.DisplayName = x.DisplayName;
                    simuApperRootLst.Add(sRoot);
                });

                string SxmlString = ToXML1(simuApperLst, FilePath);

                Object SimulatorAppearanceList = XMLToObject(SxmlString, SimAppear as Object);
                
                Navisworks_Timeliner fr = new Navisworks_Timeliner();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }

        public string ToXML1(Object oObject, string FPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                xmlDoc.Save(FPath + ".xml");
                return xmlDoc.InnerXml;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();

            Progress progress = Autodesk.Navisworks.Api.Application.BeginProgress();
            double stage = 0.0;

            progress.BeginSubOperation(0.99, "Exporting....");
            System.Threading.Thread.Sleep(100);

            stage = DoWork(progress, stage, 0.99, 0.01);

            //int Tcnt = TxmlString.Count();
            //int Scnt = SxmlString.Count();
            progress.EndSubOperation(true);
            Autodesk.Navisworks.Api.Application.EndProgress();
        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

            string FileName = saveFileDialog2.FileName;

            try
            {
                TimelinerData timeliner = (oDoc.Timeliner as DocumentTimeliner).Value;
                List<SimTaskType> TaskType = new List<SimTaskType>();
                List<SimTaskType> SimTaskTy = new List<SimTaskType>();                
                timeliner.SimulationTaskTypes.ToList().ForEach((x) =>
                {
                    SimTaskType sTaskTyp = new SimTaskType();
                    SimulationTaskType stt = x as SimulationTaskType;
                    sTaskTyp.DisplayName = stt.DisplayName;

                    sTaskTyp.EndAStatus = new ClsSimulationStatus1() { SimulationAppearanceName = Convert.ToString(stt.EndStatus.SimulationAppearanceName) };
                    sTaskTyp.EndAMode = new ClsSimulationStatus2() { AppearanceMode = stt.EndStatus.AppearanceMode };

                    sTaskTyp.OverrunAStatus = new ClsSimulationStatus1() { SimulationAppearanceName = Convert.ToString(stt.OverrunStatus.SimulationAppearanceName) };
                    sTaskTyp.OverrunAMode = new ClsSimulationStatus2() { AppearanceMode = stt.OverrunStatus.AppearanceMode };

                    sTaskTyp.SimulationStartAStatus = new ClsSimulationStatus1() { SimulationAppearanceName = Convert.ToString(stt.SimulationStartStatus.SimulationAppearanceName) };
                    sTaskTyp.SimulationStartAMode = new ClsSimulationStatus2() { AppearanceMode = stt.SimulationStartStatus.AppearanceMode };

                    sTaskTyp.StartAStatus = new ClsSimulationStatus1() { SimulationAppearanceName = Convert.ToString(stt.StartStatus.SimulationAppearanceName) };
                    sTaskTyp.StartAMode = new ClsSimulationStatus2() { AppearanceMode = stt.StartStatus.AppearanceMode };

                    sTaskTyp.UnderrunAStatus = new ClsSimulationStatus1() { SimulationAppearanceName = Convert.ToString(stt.UnderrunStatus.SimulationAppearanceName) };
                    sTaskTyp.UnderrunAMode = new ClsSimulationStatus2() { AppearanceMode = stt.UnderrunStatus.AppearanceMode };

                    string name = stt.DisplayName;
                    TaskType.Add(sTaskTyp);
                });            
                string SmTaskTypexmlString = ToXML2(TaskType, FileName);
                Object SimulatorTaskType = XMLToObject(SmTaskTypexmlString, SimTaskTy as Object);               
                Autodesk.Navisworks.Api.Application.EndProgress();
                Navisworks_Timeliner fr = new Navisworks_Timeliner();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }

        public string ToXML2(Object oObject, string FName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                xmlDoc.Save(FName + ".xml");
                return xmlDoc.InnerXml;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            saveFileDialog3.ShowDialog();

            Progress progress = Autodesk.Navisworks.Api.Application.BeginProgress();
            double stage = 0.0;

            progress.BeginSubOperation(0.99, "Exporting....");
            System.Threading.Thread.Sleep(100);

            stage = DoWork(progress, stage, 0.99, 0.01);
            progress.EndSubOperation(true);
            Autodesk.Navisworks.Api.Application.EndProgress();
        }

        private void saveFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            string FilePath = saveFileDialog3.FileName;

            try
            {
                TimelinerData timeliner = (oDoc.Timeliner as DocumentTimeliner).Value;
                List<TLTask> taskLst = new List<TLTask>();              
                List<TLTask> taskLstFromFile = new List<TLTask>();                
                List<SimAppearenceRoot> simuApperRootLst = new List<SimAppearenceRoot>();
                List<SimAppearenceRoot> ShowAppearenceList = new List<SimAppearenceRoot>();
                timeliner.Tasks.ToList().ForEach((itm) =>
                {
                    TimelinerTask parentTtask = itm as TimelinerTask;
                    taskLst.Add(new TLTask()
                    {
                        ActualDuration = parentTtask.ActualDuration,
                        ActualEndDate = parentTtask.ActualEndDate,
                        ActualStartDate = parentTtask.ActualStartDate,
                        PlannedDuration = parentTtask.PlannedDuration,
                        PlannedEndDate = parentTtask.PlannedEndDate,
                        PlannedStartDate = parentTtask.PlannedStartDate,
                        ProgressPercent = parentTtask.ProgressPercent,
                        TotalCost = parentTtask.TotalCost,
                        LaborCost = parentTtask.LaborCost,
                        MaterialCost = parentTtask.MaterialCost,
                        TaskStatus = parentTtask.TaskStatus,
                        TaskType = parentTtask.SimulationTaskTypeName,
                        IsParent = true,
                        TaskName = parentTtask.DisplayName
                    });

                    parentTtask.Children.ToList().ForEach(x =>
                    {
                        TimelinerTask child = x as TimelinerTask;
                        taskLst.Add(new TLTask()
                        {
                            ActualDuration = (child).ActualDuration,
                            ActualEndDate = (child).ActualEndDate,
                            ActualStartDate = (child).ActualStartDate,
                            PlannedDuration = (child).PlannedDuration,
                            PlannedEndDate = (child).PlannedEndDate,
                            PlannedStartDate = (child).PlannedStartDate,
                            ProgressPercent = (child).ProgressPercent,
                            TotalCost = (child).TotalCost,
                            LaborCost = (child).LaborCost,
                            MaterialCost = (child).MaterialCost,
                            TaskStatus = (child).TaskStatus,
                            IsParent = false,
                            TaskName = (child).DisplayName,
                            TaskType = (child).SimulationTaskTypeName
                        });
                    });
                });


                string TxmlString = ToXML(taskLst, FilePath);
                Object taskLstFromFile1 = XMLToObject(TxmlString, taskLstFromFile as Object);
                Autodesk.Navisworks.Api.Application.EndProgress();
                Navisworks_Timeliner fr = new Navisworks_Timeliner();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }

        }

        public string ToXML(Object oObject, string FPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                xmlDoc.Save(FPath + ".xml");
                return xmlDoc.InnerXml;
            }
        }

    }
}
