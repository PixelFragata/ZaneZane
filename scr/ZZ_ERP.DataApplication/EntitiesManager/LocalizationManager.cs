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
        private const String BaseUrl = "https://viacep.com.br";

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
                    var ibgeList = await SearchEstadoAsync();

                    if (list.Any() && ibgeList.Any())
                    {
                        foreach (var estado in ibgeList)
                        {
                            if (list.Exists(e => e.Ibge == estado.Ibge))
                            {
                                var est = list.Find(e => e.Ibge == estado.Ibge);
                                est.Ibge = estado.Ibge;
                                est.Descricao = estado.Descricao;
                                est.Sigla = estado.Sigla;
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


        public static async Task<Command> GetAllStates(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                using (var context = new ZZContext())
                {
                    var rep = new Repository<Estado>(context);
                    var entities = await rep.Get();
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



        public static async Task<Command> GetCityByUf(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                using (var context = new ZZContext())
                {
                    var dto = await SerializerAsync.DeserializeJson<TipoSiglaDto>(cmd.Json); 
                    var repEstado = new Repository<Estado>(context);
                    var repCity = new Repository<Cidade>(context);
                    var list = await repEstado.Get(e => e.Sigla.Equals(dto.Sigla));
                    var entity = list.FirstOrDefault();
                    var cidades = await repCity.Get(c => c.EstadoId == entity.Id);
                    var cidadeList = cidades.ToList();

                    if (cidadeList.Any())
                    {
                        cmd.Cmd = ServerCommands.LogResultOk;
                        var dtos = cidadeList.Select(t => t.ConvertDto(dto.Sigla)).ToList();
                        
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

        public static async Task<Command> GetAddressByZipCode(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                using (var context = new ZZContext())
                {
                    var dto = await SerializerAsync.DeserializeJson<EnderecoDto>(cmd.Json);

                    var repCity = new Repository<Cidade>(context);
                    var address = await SearchAddressAsync(dto.Cep, CancellationToken.None);
                    var cidades = await repCity.Get(c => c.Descricao.Contains(address.City) && c.Estado.Sigla.Equals(address.StateInitials),null, "Estado");
                    var cidade = cidades.FirstOrDefault();

                    if (cidade != null)
                    {
                        cmd.Cmd = ServerCommands.LogResultOk;
                        var entity = new EnderecoDto
                        {
                            Cep = address.ZipCode,
                            Bairro = address.Neighborhood,
                            CidadeId = cidade.Id,
                            Cidade = cidade.Descricao,
                            Estado = cidade.Estado.Sigla,
                            Logradouro = address.Street,
                            Complemento = address.Complement,
                            Ibge = address.IBGECode,
                            GIACode = address.GIACode,
                            Numero = dto.Numero
                        };
                        cmd.Json = await SerializerAsync.SerializeJson(entity);
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

        public static async Task<Command> GetAddress(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                using (var context = new ZZContext())
                {
                    var dto = await SerializerAsync.DeserializeJson<EnderecoDto>(cmd.Json);
                    var addressList = await SearchAddressAsync(dto.Estado, dto.Cidade, dto.Logradouro, CancellationToken.None);

                    var viaCepResults = addressList.ToList();
                    if (viaCepResults.Any())
                    {
                        cmd.Cmd = ServerCommands.LogResultOk;
                        var dtos = viaCepResults.Select(d => new EnderecoDto{
                            Cep = d.ZipCode,
                            Bairro = d.Neighborhood,
                            Cidade = d.City,
                            Estado = d.StateInitials,
                            Logradouro = d.Street,
                            Complemento = d.Complement,
                            Ibge = d.IBGECode,
                            GIACode = d.GIACode,
                            Numero = dto.Numero
                        });
                        cmd.Json = await SerializerAsync.SerializeJsonList(dtos.ToList());
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

        public static async Task<Command> SaveAddress(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                using (var context = new ZZContext())
                {
                    var dto = await SerializerAsync.DeserializeJson<EnderecoDto>(cmd.Json);
                    var repAddress = new Repository<Endereco>(context);
                    var repCity = new Repository<Cidade>(context);
                    var cidades = await repCity.Get(c => c.Descricao.Contains(dto.Cidade) && c.Estado.Sigla.Equals(dto.Estado), null, "Estado");
                    var cidade = cidades.FirstOrDefault();
                    if (cidade != null)
                    {
                        dto.CidadeId = cidade.Id;
                        if (dto.Ibge <= 0)
                        {
                            var viacep = await SearchAddressAsync(dto.Cep, CancellationToken.None);
                            dto.Ibge = viacep.IBGECode;
                            dto.GIACode = viacep.GIACode;
                        }

                        if (String.IsNullOrWhiteSpace(dto.Codigo))
                        {
                            dto.Codigo = dto.Ibge.ToString() + dto.Numero.ToString() + dto.Logradouro;
                        }
                        var address = await repAddress.Get(e =>
                            e.Logradouro.Equals(dto.Logradouro) && e.Numero == dto.Numero);

                        if (!address.Any())
                        {

                            var entity = new Endereco();
                            entity.UpdateEntity(dto);

                            var insertEntity = await repAddress.Insert(entity);
                            if (insertEntity != null)
                            {
                                cmd.Cmd = ServerCommands.LogResultOk;
                                cmd.Json = await SerializerAsync.SerializeJson(true);
                                await repAddress.Save();
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
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);

            }

            return cmd;
        }

        public static async Task<Command> EditAddress(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                using (var context = new ZZContext())
                {
                    var dto = await SerializerAsync.DeserializeJson<EnderecoDto>(cmd.Json);
                    var repAddress = new Repository<Endereco>(context);
                    var repCity = new Repository<Cidade>(context);
                    var cidades = await repCity.Get(c => c.Descricao.Contains(dto.Cidade) && c.Estado.Sigla.Equals(dto.Estado), null, "Estado");
                    var cidade = cidades.FirstOrDefault();
                    if (cidade != null)
                    {
                        dto.CidadeId = cidade.Id;
                        var address = await repAddress.GetById(dto.Id);
                        if (address != null)
                        {
                            cmd.Cmd = ServerCommands.LogResultOk;
                            address.UpdateEntity(dto);
                            await repAddress.Save();
                        }
                        else
                        {
                            cmd.Cmd = ServerCommands.LogResultDeny;
                        }
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
                var url = EstadosUrl + "/" + uf + "/municipios";
                var response = await client.GetAsync(url, CancellationToken.None).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var jsonContent = await response.Content.ReadAsStringAsync();
                var jsonList = await SerializerAsync.DeserializeJsonList<CidadeIbge>(jsonContent);
                var cidades = new List<Cidade>();
                foreach (var ibge in jsonList)
                {
                    cidades.Add(new Cidade
                        { Ibge = ibge.Id, Descricao = ibge.Nome, EstadoId = ufId, IsActive = true, Codigo = ibge.Id.ToString()});
                }

                return cidades;
            }
        }
        private static async Task<List<Estado>> SearchEstadoAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(EstadosUrl, CancellationToken.  None).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var jsonContent = await response.Content.ReadAsStringAsync();
                var jsonList = await SerializerAsync.DeserializeJsonList<EstadoIbge>(jsonContent);
                var estadoList = new List<Estado>();
                foreach (var ibge in jsonList)
                {
                    estadoList.Add(new Estado
                        {Ibge = ibge.Id, Descricao = ibge.Nome, Sigla = ibge.Sigla, IsActive = true, Codigo = ibge.Id.ToString()});
                }

                return estadoList;
            }
        }

        /// <summary>
        /// Searches the asynchronous.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static async Task<ViaCEPResult> SearchAddressAsync(String zipCode, CancellationToken token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var response = await client.GetAsync($"/ws/{zipCode}/json", token).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var jsonContent = await response.Content.ReadAsStringAsync();
                return await SerializerAsync.DeserializeJson<ViaCEPResult>(jsonContent);
            }
        }

        /// <summary>
        /// Searches the asynchronous.
        /// </summary>
        /// <param name="stateInitials">The state initials.</param>
        /// <param name="city">The city.</param>
        /// <param name="address">The address.</param> 
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<ViaCEPResult>> SearchAddressAsync(
            String stateInitials,
            String city,
            String address,
            CancellationToken token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var response = await client.GetAsync($"/ws/{stateInitials}/{city}/{address}/json", token).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var jsonContent = await response.Content.ReadAsStringAsync();
                return await SerializerAsync.DeserializeJsonList<ViaCEPResult>(jsonContent);
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

        public sealed class ViaCEPResult
        {
            [JsonProperty("cep")]
            public String ZipCode { get; set; }

            [JsonProperty("logradouro")]
            public String Street { get; set; }

            [JsonProperty("complemento")]
            public String Complement { get; set; }

            [JsonProperty("bairro")]
            public String Neighborhood { get; set; }

            [JsonProperty("localidade")]
            public String City { get; set; }

            [JsonProperty("uf")]
            public String StateInitials { get; set; }

            [JsonProperty("unidade")]
            public String Unit { get; set; }

            [JsonProperty("ibge")]
            public Int32 IBGECode { get; set; }

            [JsonProperty("gia")]
            public Int32 GIACode { get; set; }
        }
    }
}
