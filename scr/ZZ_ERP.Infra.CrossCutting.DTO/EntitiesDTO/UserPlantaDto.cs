using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class UserPlantaDto : EntityDto
    {
        public string UserId { get; set; }

        public long PlantaId { get; set; }
        public UserDadosDto PlantaDto { get; set; }
    }
}
