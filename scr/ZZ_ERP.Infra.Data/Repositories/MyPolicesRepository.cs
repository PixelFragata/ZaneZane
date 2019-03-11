using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;
using ZZ_ERP.Infra.Data.Contexts;

namespace ZZ_ERP.Infra.Data.Repositories
{
    public class MyPolicesRepository : IPolicies
    {
        public List<IPolicy> MyPolicies { get; set; }
        private static IPolicies _instance;

        public static IPolicies Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MyPolicesRepository();
                }

                return _instance;
            }

        }

        public MyPolicesRepository()
        {
            MyPolicies = new List<IPolicy>();
            using (var context = new ZZContext())
            {
                var permissaoTelaRep = new Repository<PermissaoTela>(context);
                var tipoPermissaoRep = new Repository<TipoPermissao>(context);

                var allTelas = permissaoTelaRep.Get().Result;
                var allPermissoes = tipoPermissaoRep.Get().Result;

                foreach (var tela in allTelas)
                {
                    foreach (var permissao in allPermissoes)
                    {
                        MyPolicies.Add(new Policy(tela,permissao));
                    }
                }
            }

            _instance = this;
        }
    }
}
