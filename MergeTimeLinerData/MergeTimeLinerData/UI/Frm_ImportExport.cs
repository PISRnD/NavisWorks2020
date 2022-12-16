using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Timeliner;
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

    public partial class Frm_ImportExport : Form
    {
        Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;       
        public string VPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //string VPath = string.Empty;
            //VPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
       
        //string assem=Assembly.GetAssembly().

        public Frm_ImportExport()
        {
            InitializeComponent();
                TimelinerData timeliner = (oDoc.Timeliner as DocumentTimeliner).Value;        
            timeliner.SimulationAppearancesRoot.Children.ToList().ForEach((x) =>
            {              
            });

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {               
                TimelinerData timeliner = (oDoc.Timeliner as DocumentTimeliner).Value;
                List<TLTask> taskLst = new List<TLTask>();
                List<SimTaskType> TaskType = new List<SimTaskType>();
                
                List<TLTask> taskLstFromFile = new List<TLTask>();
                List<SimAppearence> SimAppear = new List<SimAppearence>();
                List<SimAppearence> SimAppear1 = new List<SimAppearence>();
                List<SimTaskType> SimTaskTy = new List<SimTaskType>();
                List<SimTaskType> SimTaskTy1 = new List<SimTaskType>();
                List<SimAppearenceRoot> simuApperRootLst=new List<SimAppearenceRoot> ();

                List<SimAppearenceRoot> ShowAppearenceList=new List<SimAppearenceRoot>();

                List<SimAppearence> simuApperLst = new List<SimAppearence>();
                List<SimTaskType> simTaskTypList = new List<SimTaskType>();

               
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
                            TaskType=parentTtask.SimulationTaskTypeName,
                            IsParent=true,
                            TaskName=parentTtask.DisplayName
                        });

                        parentTtask.Children.ToList().ForEach(x =>
                            {
                                TimelinerTask child = x as TimelinerTask;
                                taskLst.Add(new TLTask()
                                {
                                    ActualDuration =   (child).ActualDuration,
                                    ActualEndDate =    (child).ActualEndDate,
                                    ActualStartDate =  (child).ActualStartDate,
                                    PlannedDuration =  (child).PlannedDuration,
                                    PlannedEndDate =   (child).PlannedEndDate,
                                    PlannedStartDate = (child).PlannedStartDate,
                                    ProgressPercent =  (child).ProgressPercent,
                                    TotalCost =        (child).TotalCost,
                                    LaborCost =        (child).LaborCost,
                                    MaterialCost =     (child).MaterialCost,
                                    TaskStatus =       (child).TaskStatus,
                                    IsParent = false,
                                    TaskName=(child).DisplayName,
                                    TaskType=(child).SimulationTaskTypeName

                                });

                            });
                    });
              
                timeliner.SimulationAppearancesRoot.Children.ToList().ForEach((x) =>
                {
                    SimulationAppearance sSppr = x as SimulationAppearance;
                    SimAppearence sAppear=new SimAppearence();
                    //sAppear.Comments = sSppr.Comments;
                    sAppear.DisplayName=sSppr.DisplayName;
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

                

                timeliner.SimulationTaskTypes.ToList().ForEach((x) =>
                {
                    SimTaskType sTaskTyp = new SimTaskType();
                    SimulationTaskType stt = x as SimulationTaskType;
                    
                    //sTaskTyp.Comments = stt.Comments.FirstOrDefault().;
                    sTaskTyp.DisplayName = stt.DisplayName;
                    
                    sTaskTyp.EndAStatus = new ClsSimulationStatus1() { SimulationAppearanceName = Convert.ToString(stt.EndStatus.SimulationAppearanceName)};
                    sTaskTyp.EndAMode = new ClsSimulationStatus2() { AppearanceMode = stt.EndStatus.AppearanceMode};
                    
                    sTaskTyp.OverrunAStatus = new ClsSimulationStatus1() { SimulationAppearanceName = Convert.ToString(stt.OverrunStatus.SimulationAppearanceName)};
                    sTaskTyp.OverrunAMode = new ClsSimulationStatus2() { AppearanceMode = stt.OverrunStatus.AppearanceMode};

                    sTaskTyp.SimulationStartAStatus = new ClsSimulationStatus1() { SimulationAppearanceName = Convert.ToString(stt.SimulationStartStatus.SimulationAppearanceName) };
                    sTaskTyp.SimulationStartAMode = new ClsSimulationStatus2() { AppearanceMode = stt.SimulationStartStatus.AppearanceMode };

                    sTaskTyp.StartAStatus = new ClsSimulationStatus1() { SimulationAppearanceName = Convert.ToString(stt.StartStatus.SimulationAppearanceName) };
                    sTaskTyp.StartAMode = new ClsSimulationStatus2() { AppearanceMode = stt.StartStatus.AppearanceMode };

                    sTaskTyp.UnderrunAStatus = new ClsSimulationStatus1() { SimulationAppearanceName = Convert.ToString(stt.UnderrunStatus.SimulationAppearanceName) };
                    sTaskTyp.UnderrunAMode = new ClsSimulationStatus2() { AppearanceMode = stt.UnderrunStatus.AppearanceMode };
                    
                    string name = stt.DisplayName;
                    TaskType.Add(sTaskTyp);
                    //simTaskTypList.Add(sTaskTyp);
                });

                

                string TxmlString = ToXML(taskLst);                

                Object taskLstFromFile1=  XMLToObject(TxmlString, taskLstFromFile as Object);

                string SxmlString = ToXML1(simuApperLst);

                Object SimulatorAppearanceList = XMLToObject(SxmlString, SimAppear as Object);

                string SmTaskTypexmlString = ToXML2(TaskType);

                Object SimulatorTaskType = XMLToObject(SmTaskTypexmlString, SimTaskTy as Object);

                //int Tcnt = TxmlString.Count();
                //int Scnt = SxmlString.Count();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }

        public string ToXML(Object oObject)
        {
            //string VPath = string.Empty;
            //VPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            XmlDocument xmlDoc = new XmlDocument();            
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);                
                xmlDoc.Save(VPath + "\\TaskList.xml");
                return xmlDoc.InnerXml;
            }
        }

        public string ToXML1(Object oObject)
        {            
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);                
                xmlDoc.Save(VPath + "\\SimulationAppearence.xml");
                return xmlDoc.InnerXml;
            }
        }

        public string ToXML2(Object oObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);                
                xmlDoc.Save(VPath + "\\TaskType.xml");
                return xmlDoc.InnerXml;
            }
        }


        public Object XMLToObject(string XMLString, Object oObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(oObject.GetType());
            oObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            XmlWriter tsk = XmlWriter.Create("TaskList");
            tsk.Dispose();
            return oObject;
        }


        private void Frm_ImportExport_Load(object sender, EventArgs e)
        {
            
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var SAppear = XDocument.Load(openFileDialog1.OpenFile());

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            var TList = XDocument.Load(openFileDialog2.OpenFile());

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

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {

            SimulationStatus sm = new SimulationStatus();

            var TType = XDocument.Load(openFileDialog3.OpenFile());

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
                    SimulationAppearanceMode sam=SimulationAppearanceMode.None;
                    simDic.TryGetValue(r.StartAMode,out sam);

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
                    TTaskType.UnderrunStatus.AppearanceMode =  sam;
                    if (sam == SimulationAppearanceMode.UserAppearance)
                    TTaskType.UnderrunStatus.SimulationAppearanceName = r.UnderrunAStatus;                   

                    tl_doc.SimulationTaskTypeAddCopy(TTaskType);
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            });           
        }
    }
}
