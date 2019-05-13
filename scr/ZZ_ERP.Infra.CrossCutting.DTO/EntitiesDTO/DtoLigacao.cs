using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class DtoLigacao : EntityDto
    {
        public long FirstDtoId { get; set; }
        public EntityDto FirstDto { get; set; }

        public long SecondDtoId { get; set; }
        public EntityDto SecondDto { get; set; }
    }
}
