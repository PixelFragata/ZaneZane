using System;
using System.Collections.Generic;
using System.Linq;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.Data.Contexts;

namespace ZZ_ERP.DataApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var context = new ZZContext())
            {
                var tipos = context.TipoServicos.ToList();
                Console.WriteLine(tipos.Count);

                foreach (var tipo in tipos)
                {
                    Console.WriteLine("Tipo serviço :  " + tipo.DescricaoServico);
                }
            }

            Console.ReadKey();
        }
    }
}
