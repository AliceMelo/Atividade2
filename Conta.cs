using System;
using System.Linq;

namespace Atividade2EFCore
{
    public class Conta
   {
    public int Id { get; set; }

        public decimal Saldo { get; set; }
        public string Titular { get; set; }
        public virtual Agencia Agencia { get; set; }

        public virtual Cliente Cliente { get; set; }

        public void Sacar(decimal valor, Conta conta, BancoContext context, int type)
        {
            string solicitacao;
            decimal desconto;
            if (conta.Saldo >= valor)
            {
                try
                {
                    if (type == 1)
                    {

                        var contaC = context.ContasCorrente.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                        desconto = valor * contaC.Taxa;
                        solicitacao = DateTime.Now.ToString() + " CONTACORRENTE SAQUE- SALDO:" + conta.Saldo + " VALOR:" + valor + " DESCONTO:" + desconto;
                        conta.Saldo = conta.Saldo - (valor + desconto);
                        solicitacao = solicitacao + " SALDOATUAL:" + conta.Saldo;
                        context.SaveChanges();
                        var newSolicitacao = new Solicitacao() { Conta = conta, Movimentacao = solicitacao };
                        context.Add(newSolicitacao);
                        context.SaveChanges();
                    }
                    else if (type == 2)
                    {
                        var contaC = context.ContasPoupanca.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                        solicitacao = DateTime.Now.ToString() + " CONTAPOUPANCA SAQUE- SALDO:" + conta.Saldo + " VALOR:" + valor;
                        conta.Saldo = conta.Saldo - valor;
                        solicitacao = solicitacao + " SALDOATUAL:" + conta.Saldo;
                        context.SaveChanges();
                        var newSolicitacao = new Solicitacao() { Conta = conta, Movimentacao = solicitacao };
                        context.Add(newSolicitacao);
                        context.SaveChanges();
                    }
                    Console.WriteLine("Operação Realizada com sucesso!");
                    Console.WriteLine(" ");
                }
                catch (Exception error)
                {
                    error.ToString();
                    Console.WriteLine("Não foi possivel efetuar esta ação");
                    Console.WriteLine(" ");
                }
            }
            else
            {
                Console.WriteLine("A conta não possui saldo suficiente");
                Console.WriteLine(" ");
            }
        }

        public void Depositar(decimal valor, Conta conta, BancoContext context, int type)
        {
            string solicitacao;
            decimal desconto;
            try
            {
                if (type == 1)
                {
                    var contaC = context.ContasCorrente.Where(b => b.Conta == conta)
                                          .FirstOrDefault();
                    desconto = valor * contaC.Taxa;
                    solicitacao = DateTime.Now.ToString() + " CONTACORRENTE DEPOSITO- SALDO:" + conta.Saldo + " VALOR:" + valor + " DESCONTO:" + desconto;
                    conta.Saldo = conta.Saldo + (valor - desconto);
                    solicitacao = solicitacao + " SALDOATUAL:" + conta.Saldo;
                    context.SaveChanges(); var newSolicitacao = new Solicitacao() { Conta = conta, Movimentacao = solicitacao };
                    context.Add(newSolicitacao);
                    context.SaveChanges();
                }
                else if (type == 2)
                {
                    var contaC = context.ContasPoupanca.Where(b => b.Conta == conta)
                                          .FirstOrDefault();
                    solicitacao = DateTime.Now.ToString() + " CONTAPOUPANCA DEPOSITO- SALDO:" + conta.Saldo + " VALOR:" + valor;
                    conta.Saldo = conta.Saldo + valor;
                    solicitacao = solicitacao + " SALDOATUAL:" + conta.Saldo;
                    context.SaveChanges();
                    context.SaveChanges(); var newSolicitacao = new Solicitacao() { Conta = conta, Movimentacao = solicitacao };
                    context.Add(newSolicitacao);
                    context.SaveChanges();
                }
                Console.WriteLine("Operação Realizada com sucesso!");
                Console.WriteLine(" ");
            }
            catch (Exception error)
            {
                error.ToString();
                Console.WriteLine("Não foi possivel efetuar esta ação");
                Console.WriteLine(" ");
            }

        }
    }
}