using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Timeliner;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TimelineMerger
{
    public class TLTask 
    {
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public TimeSpan? ActualDuration { get; set; }
        public TimeSpan? PlannedDuration { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public double? ProgressPercent { get; set; }
        public double? TotalCost { get; set; }
        public double? LaborCost { get; set; }
        public double? MaterialCost { get; set; }
        public Autodesk.Navisworks.Api.Timeliner.TaskStatus TaskStatus { get; set; }
        public bool IsParent { get; set; }
    }
        
    public class SimAppearence
    {
        public string DisplayName { get; set; }
        public string colrR { get; set; }
        public string colrG { get; set; }
        public string colrB { get; set; }        
        public double Transperancy  { get; set; }
    }

    public class SimAppearenceRoot
    {

        public string DisplayName { get; set; }
    }
 

    public class SimTaskType
    {
        public string DisplayName { get; set; }

        public ClsSimulationStatus1 EndAStatus { get; set; }
        public ClsSimulationStatus2 EndAMode { get; set; }

        public ClsSimulationStatus1 OverrunAStatus { get; set; }
        public ClsSimulationStatus2 OverrunAMode { get; set; }

        public ClsSimulationStatus1 SimulationStartAStatus { get; set; }
        public ClsSimulationStatus2 SimulationStartAMode { get; set; }

        public ClsSimulationStatus1 StartAStatus { get; set; }
        public ClsSimulationStatus2 StartAMode { get; set; }

        public ClsSimulationStatus1 UnderrunAStatus { get; set; }
        public ClsSimulationStatus2 UnderrunAMode { get; set; }
    }

   public class ClsSimulationStatus1
   {
       public string SimulationAppearanceName { get; set; }  
   }

   public class ClsSimulationStatus2
   {
       public SimulationAppearanceMode AppearanceMode { get; set; }
   }

}
