using System;
using System.Collections.Generic;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Connections;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Infra.CrossCutting.DTO.Interfaces
{
    public interface IConnectionManager
    {
        ServerConnection Zz { get; }
        LoginResultDto LoginResultDto { get; set; }
    }
}
