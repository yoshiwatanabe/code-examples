using System.Collections.Generic;

namespace Project.Settings
{
    public class ParentSettings
    {
        public string ParentProperty1 { get; set; }
        public List<ArrayChildSettings> ChildSettings { get; set; }
    }
}
