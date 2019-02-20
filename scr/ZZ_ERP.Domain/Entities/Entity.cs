using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Domain.Entities
{
    public class Entity
    {
        public Entity()
        {
            IsActive = true;
        }

        public long Id { get; protected set; }

        public bool IsActive { get; set; }
    }
}
