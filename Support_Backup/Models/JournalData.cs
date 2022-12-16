namespace PivdcNavisworksSupportModel
{
    public class JournalData
    {
        public string FilePath { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorInnerException { get; set; }
        public string ErrorData { get; set; }
        public string ErrorHyperResult { get; set; }
        public string ErrorTargetSiteName { get; set; }
        public string ErrorTargetSiteModule { get; set; }
        public string ErrorTargetSiteMemberType { get; set; }
        public string ErrorStackTrace { get; set; }
        public string DateTime { get; set; }
    }
}