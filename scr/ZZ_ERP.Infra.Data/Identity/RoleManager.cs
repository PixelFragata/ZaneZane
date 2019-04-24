using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;
using ZZ_ERP.Infra.Data.Contexts;
using System.Linq;
using System.Security.Claims;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.DTO.IdentityDTO;
using ZZ_ERP.Infra.Data.Repositories;

namespace ZZ_ERP.Infra.Data.Identity
{
    public class RoleManager : IRoleManager
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ZZContext _dbContext;
        private readonly Repository<TipoPermissao> _tiposPermissaoRepository;
        private readonly Repository<PermissaoTela> _permissaoTelasRepository;

        public RoleManager(RoleManager<IdentityRole> roleManager, ZZContext dbContext)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _tiposPermissaoRepository = new Repository<TipoPermissao>(_dbContext);
            _permissaoTelasRepository = new Repository<PermissaoTela>(_dbContext);
        }

        public async Task<bool> CreateAsync(string roleName)
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole(roleName);
                    await _roleManager.CreateAsync(role);
                    await _dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return false;
            }
        }

        public List<IdentityRole> ListAll()
        {
            var roles = _dbContext.Roles;
            return roles.Any() ? roles.ToList() : new List<IdentityRole>();
        }

        public async Task<bool> AddRoleClaim(string roleName, string nomeTela, string tipoPermissao)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                if (_permissaoTelasRepository.Get(p => p.NomeTela == nomeTela).Result.Any() &&
                    _tiposPermissaoRepository.Get(t => t.Descricao == tipoPermissao).Result.Any())
                {
                    var result = await _roleManager.AddClaimAsync(role, new Claim(nomeTela, tipoPermissao));
                    if (result.Succeeded)
                    {
                        return result.Succeeded;
                    }
                }
            }

            return false;
        }

        public async Task<MatrizRolePermission> GetRolePermissions(string roleName)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var matriz = new MatrizRolePermission();
                    matriz.RoleName = roleName;
                    var telas = await _permissaoTelasRepository.Get();
                    matriz.Telas = telas.Select(t => t.NomeTela).ToList();

                    var permissoes = await _tiposPermissaoRepository.Get();
                    matriz.Permissoes = permissoes.Select(t => t.Descricao).ToList();

                    matriz.TelasPermissoes = new bool[matriz.Telas.Count][];

                    var myClaims = await _roleManager.GetClaimsAsync(role);
                    int i = 0, j = 0;

                    foreach (var tela in matriz.Telas)
                    {
                        matriz.TelasPermissoes[i] = new bool[matriz.Permissoes.Count];
                        j = 0;
                        foreach (var permissao in matriz.Permissoes)
                        {
                            if (myClaims.Where(c => c.Type.Equals(tela) && c.Value.Equals(permissao)).Any())
                            {
                                matriz.TelasPermissoes[i][j] = true;
                            }
                            else
                            {
                                matriz.TelasPermissoes[i][j] = false;
                            }

                            j++;
                        }

                        i++;
                    }
                    return matriz;
                }

                return null;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return null;
            }
        }

        public async Task InitializeAdminRole()
        {
            var permissaoRep = new Repository<TipoPermissao>(_dbContext);
            var telasRep = new Repository<PermissaoTela>(_dbContext);

            var permissaoList = permissaoRep.Get().Result;
            var telasList = telasRep.Get().Result;
            var tipoPermissaos = permissaoList.ToList();

  
            foreach (var tela in telasList)
            {
                foreach (var permissao in tipoPermissaos)
                {
                    await AddRoleClaim("Admin", tela.NomeTela, permissao.Descricao);
                }
            }
            
        }
    }
}
