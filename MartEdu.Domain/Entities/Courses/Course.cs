using MartEdu.Domain.Commons;
using MartEdu.Domain.Entities.Authors;
using MartEdu.Domain.Entities.Users;
using MartEdu.Domain.Enums;
using MartEdu.Domain.Enums.Courses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MartEdu.Domain.Entities.Courses
{
    public class Course : IAuditable
    {
        public Course()
        {
            Participants = new List<User>();
        }

        public Author Author { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Hashtag Teg { get; set; }
        public Level Level { get; set; }
        public Section Section { get; set; }
        public virtual ICollection<User> Participants { get; set; }
        public string Image { get; set; }


        [JsonIgnore]
        public long Score { get; set; }

        [JsonIgnore]
        public int CountOfVotes { get; set; }

        [NotMapped]
        public float VoteScore { get => Score / (float)this.CountOfVotes; }



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
