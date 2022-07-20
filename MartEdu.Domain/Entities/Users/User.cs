using MartEdu.Domain.Commons;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MartEdu.Domain.Entities.Users
{
    public class User : IAuditable
    {
        public User()
        {
            Courses = new List<Course>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public virtual ICollection<Course> Courses { get; }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public ItemState State { get; set; }

        public void Create()
        {
            this.CreatedAt = DateTime.Now;
            this.State = ItemState.Created;
        }

        public void Update()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Updated;
        }

        public void Delete()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Deleted;
        }
    }
}
