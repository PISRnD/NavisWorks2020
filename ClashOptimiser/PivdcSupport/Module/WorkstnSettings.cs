using PivdcSupportModel;

namespace PivdcSupportModule
{
    public class WorkstnSettings
    {
        private string workstationSettingFileName { get; set; }

        public WorkstnSettings()
        {
            workstationSettingFileName = string.Format("{0}\\WorkstationSeetings.xml", SupportDatas.RefFilesLocation);
        }

        public WorkstationSettingsData GetWorkstationSettings()
        {
            return typeof(WorkstationSettingsData).ReadXmlFile(workstationSettingFileName) as WorkstationSettingsData;
        }
    }
}