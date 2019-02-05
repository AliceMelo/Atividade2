using System;
using System.Collections.Generic;
using System.Text;

namespace Atividade2EFCore
{
    public class Banco
    {
        public int Id { get; set; }
        public Banco()
        {
            Agencias = new List<Agencia>();
        }
        public string Nome { get; set; }

        public virtual ICollection<Agencia> Agencias { get; set; }
    }
}