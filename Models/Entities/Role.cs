using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class Role
    {
        private int _RoleId;
        private string _RoleName;

        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

        public virtual ICollection<User> Users { get; set; }
    }
}