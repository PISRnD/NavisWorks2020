using Autodesk.Navisworks.Api;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using PivdcSupportModel;
using System;
using System.Reflection;

namespace PivdcSupportModule
{
    //get and set Project information
    public static class ProjectInformationReadWrite
    {
        internal readonly static Guid schemaGuidProjectInformation = new Guid("F09CC6A6-232F-4BD1-B8B7-D0509CB655C9");

        /// <summary>
        /// Read the project information based on schema read
        /// </summary>
        /// <param name="document">The current Revit document</param>
        /// <returns>The project id from the schema based on Guid id</returns>
        public static int ReadProjectInformation(Document document)
        {
            int existanceValue = 0;
            string domainName = string.Empty;
            Entity settingsEntity = GetSettingsEntity(document);
            if (settingsEntity == null || !settingsEntity.IsValid())
            {
                return existanceValue;
            }
            existanceValue = settingsEntity.Get<int>("Parameter1");
            domainName = settingsEntity.Get<string>("Parameter2");
            return existanceValue;
        }

        /// <summary>
        /// Write project id information as external information
        /// </summary>
        /// <param name="document">The current Revit document</param>
        /// <param name="projectId">The project ID</param>
        /// <param name="domainName">The domain to incorporate with the project id</param>
        public static void WriteProjectInformation(Document document, int projectId)
        {
            //To find from which domain was enter the project information
            string domainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            var settingDs = GetSettingsDataStorage(document);
            if (settingDs == null)
            {
                settingDs = DataStorage.Create(document);
            }
            Entity settingsEntity = new Entity(MyProjectSettingsSchema.GetSchema());
            settingsEntity.Set("Parameter1", projectId);
            settingsEntity.Set("Parameter2", domainName);
            // Identify settings data storage
            Entity idEntity = new Entity(DataStorageUniqueIdSchema.GetSchema());
            idEntity.Set("Id", schemaGuidProjectInformation);
            settingDs.SetEntity(idEntity);
            settingDs.SetEntity(settingsEntity);
        }

        /// <summary>
        /// Getting setting entity from the Revit document
        /// </summary>
        /// <param name="document">The current Revit document</param>
        /// <returns>The external entity which have the datastorage according to the schema</returns>
        private static Entity GetSettingsEntity(Document document)
        {
            FilteredElementCollector collector = new FilteredElementCollector(document);
            var dataStorages = collector.OfClass(typeof(DataStorage));
            // Find setting data storage
            foreach (var dataStorage in dataStorages)
            {
                Entity settingEntity = dataStorage.GetEntity(MyProjectSettingsSchema.GetSchema());
                // If a DataStorage contains 
                // setting entity, we found it
                if (!settingEntity.IsValid()) continue;
                return settingEntity;
            }
            return null;
        }

        /// <summary>
        /// Getting the setting datastorage from the Revit by schema guid
        /// </summary>
        /// <param name="document">The current Revit document</param>
        /// <returns>The datastorage to find out the further information</returns>
        private static DataStorage GetSettingsDataStorage(Document document)
        {
            // Retrieve all data storages from project
            FilteredElementCollector collector = new FilteredElementCollector(document);
            var dataStorages = collector.OfClass(typeof(DataStorage));
            // Find setting data storage 
            foreach (var dataStorage in dataStorages)
            {
                Entity settingIdEntity
                  = dataStorage.GetEntity(DataStorageUniqueIdSchema.GetSchema());
                if (!settingIdEntity.IsValid()) continue;
                var id = settingIdEntity.Get<Guid>("Id");
                if (!id.Equals(schemaGuidProjectInformation)) continue;

                return (dataStorage as DataStorage);
            }
            return null;
        }
    }

    /// <summary>
    /// The unique schema guid and operation to save the prject information entity in the Revit document
    /// </summary>
    public static class DataStorageUniqueIdSchema
    {
        //The project unique id for project information
        static readonly Guid schemaGuid = new Guid("9958F968-2F66-4A6C-A411-6BDA066F2844");
        public static Schema GetSchema()
        {
            Schema schema = Schema.Lookup(schemaGuid);

            if (schema != null)
                return schema;

            SchemaBuilder schemaBuilder = new SchemaBuilder(schemaGuid);

            schemaBuilder.SetSchemaName("DataStorageUniqueId");
            schemaBuilder.SetReadAccessLevel(AccessLevel.Vendor); // allow Vendor to read the object
            schemaBuilder.SetWriteAccessLevel(AccessLevel.Vendor); // restrict writing to this vendor only
            schemaBuilder.SetVendorId("PISD"); // required because of restricted write-access
            schemaBuilder.AddSimpleField("Id", typeof(Guid));
            return schemaBuilder.Finish();
        }
    }

    /// <summary>
    /// Project information settings schema
    /// </summary>
    public static class MyProjectSettingsSchema
    {
        //readonly static Guid schemaGuid = new Guid("F09CC6A6-232F-4BD1-B8B7-D0509CB655C9")
        /// <summary>
        /// Getting the schema based on vendor id and schema name
        /// </summary>
        /// <returns></returns>
        public static Schema GetSchema()
        {
            Schema schema = Schema.Lookup(ProjectInformationReadWrite.schemaGuidProjectInformation);

            if (schema != null) return schema;

            SchemaBuilder schemaBuilder = new SchemaBuilder(ProjectInformationReadWrite.schemaGuidProjectInformation);
            schemaBuilder.SetReadAccessLevel(AccessLevel.Vendor); // allow Vendor to read the object
            schemaBuilder.SetWriteAccessLevel(AccessLevel.Vendor); // restrict writing to this vendor only
            schemaBuilder.SetVendorId("PISD"); // required because of restricted write-access
            schemaBuilder.SetSchemaName("PISProjectInfo");
            schemaBuilder.AddSimpleField("Parameter1", typeof(int));
            schemaBuilder.AddSimpleField("Parameter2", typeof(string));
            return schemaBuilder.Finish();
        }
    }

    /// <summary>
    /// Validate the project information and write the project information if required
    /// </summary>
    public static class ProjectIdValidation
    {
        /// <summary>
        /// Getting project information by read and or write into the Revit document
        /// </summary>
        /// <param name="document">The current Revit document</param>
        /// <param name="toolConnectionString">The project information connection string of database</param>
        /// <returns>return true if the project id is successfully retrieve from the Revit document</returns>
        public static bool IsProjectInformationValidate(Document document, string toolConnectionString)
        {
            bool isWrittenProjectInfo = false;
            if (document != null)
            {
                string projCode = string.Empty;
                int projectId = ProjectInformationReadWrite.ReadProjectInformation(document);
                if (projectId > 0)
                {
                    SupportDatas.ProjectId = projectId;
                    SupportDatas.ProjectCode = DatabaseInformation.GetProjectCodeFromId(projectId, toolConnectionString);
                }
                else
                {
                    projectId = DatabaseInformation.GetProjectIdNCode(toolConnectionString, out projCode);
                    if (projectId > 0)
                    {
                        projCode = DatabaseInformation.GetProjectCodeFromId(projectId, toolConnectionString);
                        using (Transaction transaction = new Transaction(document, "WriteProjectInformation"))
                        {
                            try
                            {
                                transaction.Start();
                                ProjectInformationReadWrite.WriteProjectInformation(document, projectId);
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                if (transaction.GetStatus() == TransactionStatus.Started) transaction.RollBack();
                                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ProjectInformationToolId);
                                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                            }
                        }
                        SupportDatas.ProjectId = ProjectInformationReadWrite.ReadProjectInformation(document);
                        SupportDatas.ProjectCode = projCode;
                    }
                }
                if (SupportDatas.ProjectId > 0)
                {
                    isWrittenProjectInfo = true;
                }
            }
            return isWrittenProjectInfo;
        }
    }
}