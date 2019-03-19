using System;
using System.Collections.Generic;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public abstract class Entity
    {
        public Entity()
        {
            IsActive = true;
        }

        public long Id { get; protected set; }

        public bool IsActive { get; set; }


        public abstract EntityDto ConvertDto();
        public abstract void UpdateEntity(EntityDto dto);
    }
}
