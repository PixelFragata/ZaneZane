using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;

namespace ZZ_ERP.Domain.Entities
{
    public class UserPlanta : Entity
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public long PlantaId { get; set; }
        public virtual Planta Planta { get; set; }

        public UserPlanta()
        {
            Planta = new Planta();
        }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new UserPlantaDto
                {
                    Id = Id,
                    Codigo = Codigo,
                    UserId = UserId,
                    PlantaId = PlantaId
                };

                if (Planta != null)
                {
                    dto.PlantaDto = (UserDadosDto)Planta.ConvertDto();
                }

                return dto;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return null;
            }
        }

        public override void UpdateEntity(EntityDto dto)
        {
            try
            {
                var userPlantaDto = (UserPlantaDto)dto;
                Codigo = userPlantaDto.Codigo;
                UserId = userPlantaDto.UserId;
                PlantaId = userPlantaDto.PlantaId;

                if (userPlantaDto.PlantaDto != null)
                {
                    Planta.UpdateEntity(userPlantaDto.PlantaDto);
                }

            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
