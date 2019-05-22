using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Identity;
using ZZ_ERP.Infra.Data.Repositories;

namespace ZZ_ERP.DataApplication.EntitiesManager
{
    public class UserPlantaManager : EntityManager<UserPlanta>
    { 

        public override async Task<Command> Add(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                 var dto = await SerializerAsync.DeserializeJson<UserPlantaDto>(command.Json);

                if (!string.IsNullOrEmpty(dto.UserId) && dto.PlantaId > 0)
                {
                    var userPlantas = await MyRepository.Get(t => t.PlantaId == dto.PlantaId
                                                                          && t.UserId.Equals(dto.UserId));

                    var userDbSet = Context.Set<UserAccount>();
                    var user = await userDbSet.FindAsync(dto.UserId);
                    
                    var plantaRep = new Repository<Planta>(Context);
                    var planta = await plantaRep.GetById(dto.PlantaId);

                    if (userPlantas != null && !userPlantas.Any() && user != null && planta != null)
                    {
                        var entity = new UserPlanta();
                        entity.UpdateEntity(dto);
                        entity.Codigo = dto.UserId + dto.PlantaId + DateTime.Now.ToString();
                        entity.Planta = planta;

                        var insertEntity = await MyRepository.Insert(entity);
                        if (insertEntity != null)
                        {
                            cmd.Cmd = ServerCommands.LogResultOk;
                            cmd.Json = await SerializerAsync.SerializeJson(true);
                            await MyRepository.Save();
                            cmd.EntityId = entity.Id;
                        }
                        else
                        {
                            cmd.Cmd = ServerCommands.RepeatedHumanCode;
                            ConsoleEx.WriteLine(ServerCommands.RepeatedHumanCode);
                        }
                    }
                    else
                    {
                        cmd.Cmd = ServerCommands.LogResultDeny;
                    }
                }
                else
                {
                    cmd.Cmd = ServerCommands.LogResultDeny;
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);

            }

            return cmd;
        }

        public override async Task<Command> Edit(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                cmd.Cmd = ServerCommands.LogResultDeny;
                cmd.Json = await SerializerAsync.SerializeJson(false);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);

            }

            return cmd;
        }

    }
}
