using NinthChevron.ComponentModel.DataAnnotations;
using NinthChevron.Data;
using NinthChevron.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    [Table("Users", "dbo", "Accounts")]
    public class User : Entity<User>
    {
        [Column("id", true, true, false), NotifyPropertyChanged]
        public int Id { get; set; }

        [Column("email", false), NotifyPropertyChanged]
        public string Email { get; set; }

        [Column("name", false), NotifyPropertyChanged]
        public string Name { get; set; }

        [Column("lastname", false), NotifyPropertyChanged]
        public string LastName { get; set; }

        [Column("lastaccess", true), NotifyPropertyChanged]
        public DateTime? LastAccess { get; set; }
    }

    [Table("Profiles", "dbo", "Accounts")]
    public class Profile : Entity<Profile>
    {
        [Column("id", true, true, false), NotifyPropertyChanged]
        public int Id { get; set; }

        [Column("userid", false), NotifyPropertyChanged]
        public int UserId { get; set; }

        [Column("pictureurl", false), NotifyPropertyChanged]
        public string PictureUrl { get; set; }
    }
}
