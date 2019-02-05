using System;
using System.Collections.Generic;
using System.Text;

namespace Atividade2EFCore
{
    public class Agencia
    {
        public int Id { get; set; }
        public Agencia()
        {
            Contas = new List<Conta>();
        }
        public string Numero { get; set; }
        public virtual Banco Banco { get; set; }

        public virtual ICollection<Conta> Contas { get; set; }
    }
}