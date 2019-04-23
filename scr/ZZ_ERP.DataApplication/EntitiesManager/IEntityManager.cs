using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Repositories;

namespace ZZ_ERP.DataApplication.EntitiesManager
{
    public interface IEntityManager
    {
        Task<Command> GetAll(Command command);
        Task<Command> GetById(Command command);
        Task<Command> GetByHumanCode(Command command);
        Task<Command> Add(Command command);
        Task<Command> Edit(Command command);
        Task<Command> Delete(Command command);
    }
}
