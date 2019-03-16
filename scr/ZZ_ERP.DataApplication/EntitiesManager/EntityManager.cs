using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Repositories;

namespace ZZ_ERP.DataApplication.EntitiesManager
{
    public abstract class EntityManager <TEntity> : IEntityManager where TEntity : Entity
    {
        public ZZContext Context { get; private set; }
        public Repository<TEntity> MyRepository { get; private set; }

        public EntityManager()
        {
            Context = new ZZContext();
            MyRepository = new Repository<TEntity>(Context);
        }



        public async Task<Command> GetAll(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var entities = await MyRepository.Get();
                var list = entities.ToList();

                if (list.Any())
                {
                    cmd.Cmd = ServerCommands.LogResultOk;
                    var dtos = list.Select(t => t.ConvertDto()).ToList();
                    cmd.Json = await SerializerAsync.SerializeJsonList(dtos);
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
       

         public async Task<Command> Delete(Command command)
         {
             Command cmd = new Command(command);
             try
             {
                 var entity = await MyRepository.GetById(cmd.EntityId);

                 if (entity != null)
                 {
                     entity.IsActive = false;
                     cmd.Cmd = ServerCommands.LogResultOk;
                     cmd.Json = await SerializerAsync.SerializeJson(true);
                     await MyRepository.Save();
                 }
                 else
                 {
                     cmd.Cmd = ServerCommands.LogResultDeny;
                     cmd.Json = await SerializerAsync.SerializeJson(false);
                 }
             }
             catch (Exception e)
             {
                 ConsoleEx.WriteError(e);

             }

             return cmd;
         }

        public abstract Task<Command> Add(Command command);
        public abstract Task<Command> Edit(Command command);
    }
}
