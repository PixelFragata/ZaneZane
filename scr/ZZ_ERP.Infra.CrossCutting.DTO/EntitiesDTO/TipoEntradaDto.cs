﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class TipoEntradaDto : EntityDto
    {
        public string Description { get; set; }
        public string NomeEntity { get; set; }
        public bool ControlaEstoque { get; set; }
    }
}
 