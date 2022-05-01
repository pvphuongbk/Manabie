using Manabie.Togo.Data.Base;
using System;

namespace Manabie.Togo.Data.Entity
{
    public class UserTaskEntity : BaseData
    {
        public Guid UserId { get; set; }

        public string TaskName { get; set; }

        public string Description { get; set; }

        public DateTime TaskDate { get; set; }
    }
}
