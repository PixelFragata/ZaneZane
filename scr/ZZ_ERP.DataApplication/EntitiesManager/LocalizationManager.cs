using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Repositories;

namespace ZZ_ERP.DataApplication.EntitiesManager
{
    public class LocalizationManager
    {
        private const string EstadosUrl = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";

        public static async Task<Command> UpdateEstados(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                using (var context = new ZZContext())
                {
                    var rep = new Repository<Estado>(context);
                    var entities = await rep.Get();
                    var list = entities.ToList();
                    var ibgeList = await SearchAsync();

                    if (list.Any() && ibgeList.Any())
                    {
                        foreach (var estado in ibgeList)
                        {
                            if (list.Exists(e => e.Ibge == estado.Ibge))
                            {
                                var est = list.Find(e => e.Ibge == estado.Ibge);
                                est.Ibge = estado.Ibge;
                                est.Descricao = estado.Descricao;
                            }
                            
                        }
                        await rep.InsertList(list);
                        await rep.Save();
                        cmd.Cmd = ServerCommands.LogResultOk;
                        var dtos = list.Select(t => t.ConvertDto()).ToList();
                        cmd.Json = await SerializerAsync.SerializeJsonList(dtos);
                    }
                    else if(ibgeList.Any())
                    {
                        await rep.InsertList(ibgeList);
                        await rep.Save();
                        cmd.Cmd = ServerCommands.LogResultOk;
                        var dtos = ibgeList.Select(t => t.ConvertDto()).ToList();
                        cmd.Json = await SerializerAsync.SerializeJsonList(dtos);
                    }
                    else
                    {
                        cmd.Cmd = ServerCommands.LogResultDeny;
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);

            }

            return cmd;
        }

        public static async Task<Command> UpdateCidades(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                using (var context = new ZZContext())
                {
                    var repEstado = new Repository<Estado>(context);
                    var repCity = new Repository<Cidade>(context);
                    var entities = await repEstado.Get();
                    var list = entities.ToList();
                    var cidades = await repCity.Get();
                    var cidadeList = cidades.ToList();

                    if (list.Any() && cidadeList.Any())
                    {
                        foreach (var estado in list)
                        {
                            var ibgeCities = await SearchMunicipioAsync(estado.Ibge, estado.Id);
                            foreach (var city in ibgeCities)
                            {
                                if (cidadeList.Exists(c => c.Ibge == city.Ibge))
                                {
                                    var cidade = list.Find(c => c.Ibge == city.Ibge);
                                    cidade.Ibge = city.Ibge;
                                    cidade.Descricao = city.Descricao;
                                }
                            }
                        }
                        await repCity.InsertList(cidadeList);
                        await repCity.Save();
                        cmd.Cmd = ServerCommands.LogResultOk;
                        var dtos = cidadeList.Select(t => t.ConvertDto()).ToList();
                        cmd.Json = await SerializerAsync.SerializeJsonList(dtos);
                    }
                    else if (list.Any())
                    {
                        foreach (var estado in list)
                        {
                            var ibgeCities = await SearchMunicipioAsync(estado.Ibge, estado.Id);
                            cidadeList.AddRange(ibgeCities);
                        }
                        await repCity.InsertList(cidadeList);
                        await repCity.Save();
                        cmd.Cmd = ServerCommands.LogResultOk;
                        var dtos = cidadeList.Select(t => t.ConvertDto()).ToList();
                        cmd.Json = await SerializerAsync.SerializeJsonList(dtos);
                    }
                    else
                    {
                        cmd.Cmd = ServerCommands.LogResultDeny;
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);

            }

            return cmd;
        }


        private static async Task<List<Cidade>> SearchMunicipioAsync(int uf, long ufId)
        {
            using (var client = new HttpClient())
            {
                var url = EstadosUrl + "/" + uf+"/municipios";
                var response = await client.GetAsync(url, CancellationToken.None).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var jsonContent = await response.Content.ReadAsStringAsync();
                var jsonList = await SerializerAsync.DeserializeJsonList<CidadeIbge>(jsonContent);
                var cidades = new List<Cidade>();
                foreach (var ibge in jsonList)
                {
                    cidades.Add(new Cidade
                        { Ibge = ibge.Id, Descricao = ibge.Nome, EstadoId = ufId, IsActive = true });
                }

                return cidades;
            }
        }
        private static async Task<List<Estado>> SearchAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(EstadosUrl, CancellationToken.None).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var jsonContent = await response.Content.ReadAsStringAsync();
                var jsonList = await SerializerAsync.DeserializeJsonList<EstadoIbge>(jsonContent);
                var estadoList = new List<Estado>();
                foreach (var ibge in jsonList)
                {
                    estadoList.Add(new Estado
                        {Ibge = ibge.Id, Descricao = ibge.Nome, Sigla = ibge.Sigla, IsActive = true});
                }

                return estadoList;
            }
        }


        public class EstadoIbge
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Sigla { get; set; }
            public RegiaoIbge Regiao { get; set; }
        }

        public class CidadeIbge
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public MicrorregiaoIbge Microrregiao { get; set; }
        }

        public class RegiaoIbge
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Sigla { get; set; }
        }

        public class MesorregiaoIbge
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public EstadoIbge UF { get; set; }
        }

        public class MicrorregiaoIbge
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public MesorregiaoIbge Mesorregiao { get; set; }
        }
    }
}
