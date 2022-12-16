
using Autodesk.Navisworks.Api;
using System.Collections.Generic;


namespace PivdcSupportModel
{
    public static class SupportDatas
    {
        //public static string CentralLoginConnectionString = "Data Source=10.1.2.47;Initial Catalog=CentralLogin;User ID=pihd;pwd=p!$$@cle2017";
        //public static string RevitToolConnectionString = "Data Source=10.1.2.47;Initial Catalog=AppDatabase;User ID=PIHD;pwd=p!$$@cle2017";
        public static string CentralLoginConnectionString = "Data Source=10.25.2.171;Initial Catalog=CentralLogin;User ID=admin;pwd=^zJ-bz8uRYH=`B)=yjXWMew[3GXgaZ{<";
        public static string RevitToolConnectionString = "Data Source=10.25.2.171;Initial Catalog=AppDatabase;User ID=admin;pwd=^zJ-bz8uRYH=`B)=yjXWMew[3GXgaZ{<";
        public static string AssemblyDirectory { get; set; }
        public static string ToolWorkReportPath { get; set; }
        public static string SystemHostName { get; set; }
        public static string ToolNameInDatabase { get; set; }
        public static string RevitVersion { get; set; }
        public static string WindowIconPath { get; set; }
        public static string LoginToolId { get; set; }
        public static string ProjectInformationToolId { get; set; }
        public static string ToolUsageValidationToolId { get; set; }
        public static int ToolIdPerDatabase { get; set; }
        public static string RefFilesLocation { get; set; }
        public static string UserInterfaeTitle { get; set; }
        public static int ProjectId { get; set; }
        public static string ProjectCode { get; set; }
        public static int ToolUATStatus { get; set; }
        public static string LocalDBConnection { get; set; }
        public static string LocalDBLocation { get; set; }
        public static LoginInformation CurrentLoginInformation { get; set; }
        public static WorkstationSettingsData WorkstationSettingsDatas { get; set; }
        //public static List<PushButton> RvtPushButtons { get; set; }
        //public static List<PulldownButton> RvtPullButtons { get; set; }
        //public static List<SplitButton> RvtSplitButtons { get; set; }
        public static string TaskDialogInfoTitle { get; set; }
        public static string TaskDialogErrorTitle { get; set; }
        public static Document RvtDocument { get; set; }
        public static bool IsSpoolWindowActive { get; set; }
        public static string PinnacleLicenseFile { get; set; }
        public static int ProcessCount { get; set; }
        public static int TimeTakenInMilliSecond { get; set; }
        public static bool InsertUsageVaiAmazon { get; set; }
        public static string ToolLicenseAccessType { get; set; }
        public static bool CanLoginThroughAmazon { get; set; }
        //public static UpdaterId DimTextPosAdjusterUpdater { get; set; }
    }
}