using System.Collections.Generic;

namespace PIS4.Models
{
    public class GroupMenu
    {
        public string ActionType { get; set; }
        public string GroupIcon { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupIndeks { get; set; }
        public string UserID { get; set; }
        public string SubGroupCount { get; set; }
        public string GroupURL { get; set; }
        public List<SubGroupMenu> SubGroupMenu { get; set; }

	}

    public class SubGroupMenu
    {
        public string ActionType { get; set; }
        public string GroupID { get; set; }
        public string SubGroupID { get; set; }
        public string SubGroupName { get; set; }
        public string SubGroupIndeks { get; set; }   
        public string UserID { get; set; }
        public List<UserMenu> Menu { get; set; }
    }

    public class UserMenu
    {
        public string ActionType { get; set; }
        public string GroupID { get; set; }
        public string SubGroupID { get; set; }
        public string MenuID { get; set; }
        public string MenuDesc { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string MenuIndeks { get; set; }
        public string UserID { get; set; }
    }

    public class UserMenuprm
    {
        public string UserGroupID { get; set; }
    }
}
