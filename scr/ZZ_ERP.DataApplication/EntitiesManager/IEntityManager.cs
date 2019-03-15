using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;

namespace ZZ_ERP.DataApplication.EntitiesManager
{
    public interface IEntityManager
    {
         Task<Command> GetAll(Command command);
         Task<Command> Add(Command command);
         Task<Command> Edit(Command command);
         Task<Command> Delete(Command command);
    }
}
