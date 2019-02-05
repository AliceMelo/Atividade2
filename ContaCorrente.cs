using System;
using System.Collections.Generic;
using System.Text;

 namespace Atividade2EFCore
{
    public class ContaCorrente
    {
        public int Id { get; set; }
        public decimal Taxa { get; set; }
        public virtual Conta Conta { get; set; }
    }
}