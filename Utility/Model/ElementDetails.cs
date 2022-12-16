using Autodesk.Navisworks.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public static class ElementDetails
    {
        public static string CategoryName { get; set; }
        public static string PropertyName { get; set; }
        public static string PropertyValue { get; set; }
        public static DateTime DateTimeStart { get; set; }
        public static DateTime DateTimeEnd { get; set; }
        public static int TimeInSeconds { get; set; }
       public static Document Document { get; set; }
        public static ModelItemCollection ModelItemCollection { get; set; }
       
    }
}
