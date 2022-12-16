using Autodesk.Navisworks.Api;


namespace ClashOptimiser
{
    public class ClashItemInformation
    {
        public string PropertyValueOne { get; set; }
        public string PropertyValueTwo { get; set; }
        public ModelItem ModelItemOne { get; set; }
        public ModelItem ModelItemTwo { get; set; }
        public SavedItem SavedModelItem { get; set; }
        public string CategoryName { get; set; }
        public string PropertyName { get; set; }
        public string GroupName { get; set; }
        public string ReverseGroupName { get; set; }
        public bool IsSame { get; set; }
        public int countItems { get; set; }

    }
}
