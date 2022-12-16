using PivdcSupportModel;
using System;
using System.IO;

namespace PivdcSupportModule
{
    /// <summary>
    /// To create the journal files for any exceptions or details of the working
    /// </summary>
    public static class PivdcToolsJournal
    {
        public static void JournalLog(this Exception exception, string methodName)
        {
            string errorLogDirectory = string.Format("{0}\\PivdcChronicle", Path.GetDirectoryName(SupportDatas.RefFilesLocation));
            if (!Directory.Exists(errorLogDirectory))
            {
                Directory.CreateDirectory(errorLogDirectory);
            }
            string errorLogFileFullPath = string
                .Format("{0}\\{1}{2}{3}Memoir.txt",
                errorLogDirectory,
                Convert.ToString(DateTime.Now.Date.Year),
                Convert.ToString(DateTime.Now.Date.Month),
                Convert.ToString(DateTime.Now.Date.Day));
            if (!File.Exists(errorLogFileFullPath))
            {
                FileStream fileStream = File.Create(errorLogFileFullPath);
                fileStream.Close();
            }
            if (File.Exists(errorLogFileFullPath))
            {
                string separator = "#pi#";
                JournalData journalData = new JournalData();
                if (!(SupportDatas.RvtDocument is null))
                {
                    journalData.FilePath = SupportDatas.RvtDocument.GetSavedPath();
                }
                else
                {
                    journalData.FilePath = "Unknown";
                }
                if (!(exception.Message is null))
                {
                    journalData.ErrorMessage = exception.Message;
                }
                else
                {
                    journalData.ErrorMessage = "Unknown";
                }
                if (!(exception.Source is null))
                {
                    journalData.ErrorSource = Convert.ToString(exception.Source);
                }
                else
                {
                    journalData.ErrorSource = "Unknown";
                }
                if (!(exception.InnerException is null))
                {
                    journalData.ErrorInnerException = Convert.ToString(exception.InnerException);
                }
                else
                {
                    journalData.ErrorInnerException = "Unknown";
                }
                if (!(exception.Data is null))
                {
                    journalData.ErrorData = Convert.ToString(exception.Data);
                }
                else
                {
                    journalData.ErrorData = "Unknown";
                }
                journalData.ErrorHyperResult = Convert.ToString(exception.HResult);
                if (!(exception.TargetSite is null))
                {
                    if (!(exception.TargetSite.Name is null))
                    {
                        journalData.ErrorTargetSiteName = Convert.ToString(exception.TargetSite.Name);
                    }
                    else
                    {
                        journalData.ErrorTargetSiteName = "Unknown";
                    }
                    if (!(exception.TargetSite.Module is null))
                    {
                        journalData.ErrorTargetSiteModule = Convert.ToString(exception.TargetSite.Module);
                    }
                    else
                    {
                        journalData.ErrorTargetSiteName = "Unknown";
                    }
                    journalData.ErrorTargetSiteMemberType = Convert.ToString(exception.TargetSite.MemberType);
                }
                else
                {
                    journalData.ErrorTargetSiteName = "Unknown";
                    journalData.ErrorTargetSiteMemberType = "Unknown";
                    journalData.ErrorTargetSiteModule = "Unknown";
                }
                if (!(exception.StackTrace is null))
                {
                    journalData.ErrorStackTrace = Convert.ToString(exception.StackTrace);
                }
                else
                {
                    journalData.ErrorStackTrace = "Unknown";
                }
                journalData.DateTime = DateTime.Now.ToLongDateString();
                File.AppendAllText(errorLogFileFullPath,
                            string.Format("{0}Error={1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}\n\n",
                            journalData.FilePath,
                            separator,
                            journalData.ErrorMessage,
                            separator,
                            journalData.ErrorSource,
                            separator,
                            journalData.ErrorInnerException,
                            separator,
                            journalData.ErrorData,
                            separator,
                            journalData.ErrorHyperResult,
                            separator,
                            journalData.ErrorTargetSiteName,
                            separator,
                            journalData.ErrorTargetSiteModule,
                            separator,
                            journalData.ErrorTargetSiteMemberType,
                            separator,
                            journalData.ErrorStackTrace,
                            separator,
                            journalData.DateTime,
                            separator,
                            methodName));
            }
        }
    }
}