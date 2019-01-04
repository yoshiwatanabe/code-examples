using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Settings
{
    public class ParentSettings
    {
        public string ParentProperty1 { get; set; }
        public List<ArrayChildSettings> ChildSettings { get; set; }
    }
}
