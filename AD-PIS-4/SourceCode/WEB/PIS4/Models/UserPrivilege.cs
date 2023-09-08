namespace PIS4.Models
{
    public class UserPrivilege
    {
        public string ActionType { get; set; }
        public string GroupIcon { get; set; }
        public string GroupName { get; set; }
        public string GroupID { get; set; }
        public string MenuID { get; set; }
        public string MenuDesc { get; set; }
        public string AllowAccess { get; set; }
        public string AllowUpdate { get; set; }
        public string AllowDelete { get; set; }
        public string RegisterUser { get; set; }
		public string UserGroupID { get; set; }
	}
}
