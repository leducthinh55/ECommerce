using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class Helper
    {
        /* Binary Search Tree 
         * Make sure list is sorted 
         * */
        public static bool BST(this List<Guid> list, Guid? id)
        {
            if (id==null) return true;
            int l = 0, r = list.Count-1;
            while (l <= r)
            {
                int m = (l + r) / 2;
                if (list[m].Equals(id)) return true;
                if (list[m].CompareTo(id) > 0) r = m - 1; else l = m + 1;
            }
            return false;
        }
    }
}
