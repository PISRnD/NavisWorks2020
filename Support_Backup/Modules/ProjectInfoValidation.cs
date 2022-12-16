using System;
using Autodesk.Navisworks.Api.Data;
using System.Data;
using Autodesk.Navisworks.Api.DocumentParts;
using PivdcNavisworksSupportModel;

namespace PivdcNavisworksSupportModule
{
    public static class ProjectInfoValidation
    {
        public static bool IsHasProjectInfo(out int projectId)
        {
            projectId = 0;
            bool getProjectInfo = false;

            if (SupportDatas.CurrentLoginInformation.LoginStatus)
            {
                try
                {
                    //get document database
                    DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;

                    //create adaptor to retrieve data from the data source.
                    NavisworksDataAdapter dataAdapter = new NavisworksDataAdapter("SELECT PISEmpName, PISProjectID FROM pinnacleRndNavisDatabase", database.Value);

                    //An empty DataTable instance is passed as an argument to the Fill method. When the method returns,
                    //the DataTable instance will be populated with the queried data
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (int.TryParse(Convert.ToString(row["PISProjectID"]), out projectId))
                            {
                                getProjectInfo = true;
                            }

                        }

                    }
                    else
                    {
                        getProjectInfo = false;
                    }

                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }

            }
            else if (!SupportDatas.CurrentLoginInformation.LoginStatus)
            {
                getProjectInfo = false;
            }

            return getProjectInfo;
        }
    }
}