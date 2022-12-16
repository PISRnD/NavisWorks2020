using Autodesk.Navisworks.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levelwise_Viewpoint_Creater.Model
{
    /// <summary>
    /// This is the MyItem class
    /// </summary>
    public class MyItem
    {
        private ModelItem _mi;
        public ModelItem mi
        {
            get
            {
                return _mi;
            }
            set
            {
                _mi = value;
                bbx = value.BoundingBox();
            }
        }
     
        public BoundingBox3D bbx { get; set; }
        public string level { get; set; }

        public double LevelElevation { get; set; }
    }
}
