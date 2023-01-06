using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderCMS.Models.Entities
{
    [Table("UserRoles")]
    public class UserRole
    {
        private int _UserId;
        private int _RoleId;

        [Key, Column(Order = 0)]
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        [Key, Column(Order = 1)]
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
