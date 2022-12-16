using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Clash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOptimiser
{
    /// <summary>
    /// This class is to compare the bounding box
    /// </summary>
     public class CompareBoundingBox : IEqualityComparer<SavedItem>
     {
        public bool Equals(SavedItem x, SavedItem y)
        {
            if (x == null)
                return y == null;
            else if (y == null)
                return false;
            else
                return CompareBoundingBx((x as ClashResult).BoundingBox, (y as ClashResult).BoundingBox);
        }

        public int GetHashCode(SavedItem obj)
        {
            return obj.GetHashCode();
        }
        public bool CompareBoundingBx(BoundingBox3D bb1, BoundingBox3D bb2)
        {
            double bbMinXres = Math.Abs(bb1.Min.X - bb2.Min.X);
            double bbMinYres = Math.Abs(bb1.Min.Y - bb2.Min.Y);
            double bbMinZres = Math.Abs(bb1.Min.Z - bb2.Min.Z);
            double bbMaxXres = Math.Abs(bb1.Max.X - bb2.Max.X);
            double bbMaxYres = Math.Abs(bb1.Max.Y - bb2.Max.Y);
            double bbMaxZres = Math.Abs(bb1.Max.Z - bb2.Max.Z);
            if (bbMinXres <= 20 && bbMinYres <= 20 && bbMinZres <= 20 && bbMaxXres <= 20 && bbMaxYres <= 20 && bbMaxZres <= 20)
            {
                return true;
            }
            return false;
        }
    }
}
