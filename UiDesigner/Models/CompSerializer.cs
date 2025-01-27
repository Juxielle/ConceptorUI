using System.Collections.Generic;


namespace UiDesigner.Models
{
    public class CompSerializer
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool HasChildren { get; set; }
        public bool IsVertical { get; set; }
        public int AddedChildrenCount { get; set; }
        public bool CanAddIntoChildContent { get; set; }
        public int ChildContentLimit { get; set; }
        public bool IsInComponent { get; set; }
        public bool IsOriginalComponent { get; set; }
        public bool IsForceAlignment { get; set; }
        public List<GroupProperties>? Properties { get; set; }
        public List<CompSerializer>? Children { get; set; }

        public GroupProperties GetGroup(GroupNames groupName)
        {
            foreach (var group in Properties!)
            {
                if (group.Name != groupName.ToString()) continue;
                return group;
            }
            
            return null!;
        }
    }
}
