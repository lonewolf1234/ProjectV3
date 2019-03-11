using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLGenerator.Models
{
    public class TreeViewData
    {
        public TreeViewData()
        {
            this.Items = new List<TreeViewData>();
        }
        public string Title { get; set; }
        public List<TreeViewData> Items { get; set; }
    }
}
