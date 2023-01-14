using System;
using System.Collections.Generic;

namespace MeterReaderCMS.Models.Entities
{
    public class User
    {
        private int _UserId;
        private string _Username;
        private string _FirstName;
        private string _LastName;
        private string _Email;
        private string _Password;
        private bool _IsActive;
        private Guid _ActivationCode;

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public Guid ActivationCode
        {
            get { return _ActivationCode; }
            set { _ActivationCode = value; }
        }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual List<Track> Tracks { get; set; }
    }
}