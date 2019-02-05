using System;
using System.Collections.Generic;
using System.Text;
 
namespace Atividade2EFCore
{
    public class Solicitacao
    {
        public int Id { get; set; }
        public virtual Conta Conta { get; set; }
        public string Movimentacao { get; set; }
    }
}