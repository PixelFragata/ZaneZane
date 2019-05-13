using System;
using System.Collections.Generic;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class  Endereco : Entity
    {

        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int Ibge { get; set; }
        public int GIACode { get; set; }

        public string CidadeDescricao { get; set; }
        public string Uf { get; set; }


        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new EnderecoDto {Id = Id, Cep = Cep, Logradouro = Logradouro, Complemento = Complemento, Bairro = Bairro,
                    Cidade = CidadeDescricao, Estado = Uf, GIACode = GIACode, Ibge = Ibge, Numero = Numero, Codigo = Codigo};

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
                var tipoDto = (EnderecoDto)dto;
                Codigo = tipoDto.Codigo;
                Cep = tipoDto.Cep;
                Ibge = tipoDto.Ibge;
                Numero = tipoDto.Numero;
                Logradouro = tipoDto.Logradouro;
                Complemento = tipoDto.Complemento;
                Bairro = tipoDto.Bairro;
                GIACode = tipoDto.GIACode;
                CidadeDescricao = tipoDto.Cidade;
                Uf = tipoDto.Estado;
                if (tipoDto.Id > 0)
                {
                    Id = tipoDto.Id;
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }

        }
    }
}
