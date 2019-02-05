using System;
using System.Collections.Generic;
using System.Text;
 
namespace Atividade2EFCore
{
    public class ContaPoupanca
    {
        public int Id { get; set; }
        public decimal TaxaJuros { get; set; }
         
        public virtual Conta Conta { get; set; }
    }
}