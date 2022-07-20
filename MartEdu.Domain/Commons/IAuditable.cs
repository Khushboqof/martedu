using MartEdu.Domain.Enums;
using System;

namespace MartEdu.Domain.Commons
{
    public interface IAuditable
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        Guid? UpdatedBy { get; set; }
        ItemState State { get; set; }

        public void Create();

        public void Update();

        public void Delete();
    }
}
