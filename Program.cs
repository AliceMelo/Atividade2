using System;
using System.Linq;

namespace Atividade2EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var context = new BancoContext();
            InitOperator(context);
            int menu = 0;
            int opcao = 1;
            for (; opcao != 0;)
            {

                if (menu == 0) menu = Menu(opcao);
                opcao = menu;
                if (menu == 1) menu = selcionarConta(opcao, context);
                else if (menu == 2) menu = abrirConta(context);
                if (opcao == 100) menu = 0;

            }
            Console.Clear();
            Console.WriteLine("Ate mais!");
        }

        public static void InitOperator(BancoContext context)
        {
            if (!context.Bancos.Any())
            {
                
                var newBanco = new Banco() { Nome = "Banco  Teste" };
                context.Add(newBanco);
                context.SaveChanges();

                if (!context.Agencias.Any())
                {
                    var newAgencia = new Agencia() { Numero = "01", Banco = newBanco };
                    context.Add(newAgencia);
                    context.SaveChanges();
                }


            }

        }

        public static int Menu(int opcao)
        {
            
            Console.Clear();
            Console.WriteLine("-----MENU----*");
            Console.WriteLine("1---ACESSAR CONTA");
            Console.WriteLine("2---CRIAR CONTA");
            Console.WriteLine("3---SAIR");

            
            try
            {
                opcao = Int32.Parse(Console.ReadLine());
            }
            catch (Exception erro)
            {
                Console.Clear();
                erro.ToString();
                Console.WriteLine("Atencao! OPÇÃO INVÁLIDA");
                opcao = 10;
            }
            return opcao;
        }

        static int selcionarConta(int opcao, BancoContext context)
        {
             
            Conta conta = new Conta();
            bool access = false;
            for (; opcao != 0;)
            {
                Console.Clear();
                Console.WriteLine("---MENU---");
                Console.WriteLine("1---CONTRA CORRENTE");
                Console.WriteLine("2---CONTA POUPANCA");
                Console.WriteLine("0---SAIR");

                try
                {
                    opcao = Int32.Parse(Console.ReadLine());
                }
                catch (Exception erro)
                {
                    erro.ToString();
                    opcao = 10;
                }
                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        conta = verificarContaCorrente(opcao, context);
                        if (conta == null)
                        {
                            access = false;
                        }
                        else if (conta != null)
                        {
                            access = true;
                        }
                        if (access == true) menuContaCorrente(opcao, context, conta);
                        break;

                    case 2:
                        Console.Clear();
                        conta = verificarContaPoupanca(opcao, context);
                        if (conta == null)
                        {
                            access = false;
                        }
                        else if (conta != null)
                        {
                            access = true;
                        }
                        if (access == true) menuContaPoupanca(opcao, context, conta);
                        break;

                    case 0: break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Atencao! OPÇÃO INVÁLIDA");
                        break;
                }
            }

            return opcao;
        }

        static void menuContaCorrente(int opcao, BancoContext context, Conta conta)
        {
            Console.Clear();
            Console.WriteLine("BEM VINDO(A) " + conta.Titular);
            for (; opcao != 0;)
            {
                
                Console.WriteLine("CONTA CORRENTE           ");
                Console.WriteLine("            Opcoes        ");
                Console.WriteLine("1---SACAR");
                Console.WriteLine("2---DEPOSITAR");
                Console.WriteLine("3---SALDO");
                Console.WriteLine("4---ATUALIZAR DADOS");
                Console.WriteLine("5---EXCLUIR CONTA");
                Console.WriteLine("0---SAIR");
                Console.WriteLine(" ");
                try
                {
                    opcao = Int32.Parse(Console.ReadLine());
                }
                catch (Exception erro)
                {
                    erro.ToString();
                    opcao = 10;
                }
                Console.Clear();
                switch (opcao)
                {
                    case 1:
                        sacarCorrente(conta, context);
                        break;

                    case 2:
                        depositarCorrente(conta, context);
                        break;

                    case 3:
                        Saldo(conta);
                        break;

                    case 4:
                        AtualizarDados(conta, context, 1);
                        break;

                    case 5:
                        opcao = deletarConta(context, conta, 1);
                        break;

                    case 6:
                        opcao = 0;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Atencao! Opção Invalida");
                        Console.WriteLine(" ");
                        opcao = 1;
                        break;
                }
            }
        }

        static void menuContaPoupanca(int opcao, BancoContext context, Conta conta)
        {
            
            Console.Clear();
            Console.WriteLine("BEM VINDO(A) " + conta.Titular);
            for (; opcao != 0;)
            {
                Console.Clear();
                Console.WriteLine(" ");
                Console.WriteLine("CONTA POUPANÇA           ");
                Console.WriteLine("        Opcoes      ");
                Console.WriteLine("1---SACAR");
                Console.WriteLine("2---DEPOSITAR");
                Console.WriteLine("3---SALDO");
                Console.WriteLine("4-ATUALIZAR DADOS");
                Console.WriteLine("5-EXCLUIR CONTA");
                Console.WriteLine("0---VOLTAR");
                Console.WriteLine(" ");

                
                try
                {
                    opcao = Int32.Parse(Console.ReadLine());
                }
                catch (Exception erro)
                {
                    erro.ToString();
                    opcao = 10;
                }
                Console.Clear();
                switch (opcao)
                {
                    case 1:
                        sacarPoupanca(conta, context);
                        break;

                    case 2:
                        depositarPoupanca(conta, context);
                        break;

                    case 3:
                        Saldo(conta);
                        break;

                    case 4:
                        AtualizarDados(conta, context, 2);
                        break;

                    case 5:
                        opcao = deletarConta(context, conta, 2);
                        break;

                    case 6:
                        opcao = 0;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("OPÇÃO INVÁLIDA");
                        opcao = 1;
                        break;
                }
            }
        }
        public static int abrirConta(BancoContext context)
        {
            
            string cpf;
            int idade;
            string agencia;
            Agencia agenciaCliente = new Agencia();
            string nome;
            Console.WriteLine("DIGITE SEU CPF");
            cpf = Console.ReadLine();

            
            try
            {
                var clienteCadastrado = context.Clientes.Where(b => b.Cpf == cpf).FirstOrDefault();
                if (clienteCadastrado != null)
                {
                    Console.WriteLine("Atencao! ESTE CPF JÁ ESTÁ CADASTRADO");
                    return 0;
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("TENTE MAIS TARDE");
                return 0;

            }
            Console.Clear();
            Console.WriteLine("DIGITE O NOME");
            nome = Console.ReadLine();
            Console.WriteLine("DIGITE A IDADE");
            
            try
            {
                idade = Int32.Parse(Console.ReadLine());
            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.WriteLine("IDADE INVÁLIDA");
                return 0;
            }
            Console.WriteLine("SELECIONE UMA DAS AGÊNCIAS");
            var bancos = context.Set<Banco>();
            foreach (var b in bancos)
            {
                if (b.Nome == "Banco Teste")
                    Console.WriteLine("--------" + b.Nome + "--------");
            }
            var agencias = context.Set<Agencia>();
            foreach (var a in agencias)
            {
                Console.WriteLine(a.Numero);
            }
            Console.WriteLine(" ");
            bool error = true;
            for (; error != false;)
            {
                Console.WriteLine("DIGITE A AGÊNCIA QUE DESEJA");
                agencia = Console.ReadLine();
                try
                {
                    var agenciaSelecionada = context.Agencias.Where(b => b.Numero == agencia).FirstOrDefault();
                    agenciaCliente = agenciaSelecionada;
                    error = false;
                }
                catch (Exception erro)
                {
                    erro.ToString();
                    Console.Clear();
                    Console.WriteLine("AGÊNCIA NÃO ENCONTRADA");
                    error = true;
                }
            }
            int contaType = 0;
            for (; contaType != 1 && contaType != 2;)
            {
                Console.WriteLine("ESCOLHA UM TIPO DE CONTA:");
                Console.WriteLine(" ");
                Console.WriteLine("1---CONTA CORRENTE");
                Console.WriteLine("2---CONTA POUPANCA");
                try
                {
                    contaType = Int32.Parse(Console.ReadLine());
                }
                catch (Exception erro)
                {
                    erro.ToString();
                    contaType = 100;
                }
                switch (contaType)
                {
                    case 1:
                        abrirContaCorrente(cpf, nome, idade, agenciaCliente, context);
                        break;

                    case 2:
                        abrirContaPoupanca(cpf, nome, idade, agenciaCliente, context);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("TIPO DE CONTA INVÁLIDA");
                        Console.WriteLine(" ");
                        break;
                }
            }
            return 0;
        }

        public static void abrirContaCorrente(string cpf, string nome, int idade, Agencia agencia, BancoContext context)
        {
            try
            {
                var newCliente = new Cliente() { Nome = nome, Cpf = cpf, Idade = idade };
                context.Add(newCliente);
                context.SaveChanges();
                decimal saldo = 0;
                var newConta = new Conta() { Agencia = agencia, Cliente = newCliente, Saldo = saldo, Titular = newCliente.Nome };
                context.Add(newConta);
                context.SaveChanges();
                var newContaCorrente = new ContaCorrente() { Conta = newConta, Taxa = 0.10M };
                context.Add(newContaCorrente);
                context.SaveChanges();
                Console.Clear();
                Console.WriteLine("CONTA CADASTRADA COM SUCESSO!");
            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.WriteLine(erro);
                Console.WriteLine("NÃO FOI POSSÍVEL EXECUTAR ESTA AÇÃO!");

            }

        }

        public static void abrirContaPoupanca(string cpf, string nome, int idade, Agencia agencia, BancoContext context)
        {
            try
            {
                var newCliente = new Cliente() { Nome = nome, Cpf = cpf, Idade = idade };
                context.Add(newCliente);
                context.SaveChanges();
                decimal saldo = 0;
                var newConta = new Conta() { Agencia = agencia, Cliente = newCliente, Saldo = saldo, Titular = newCliente.Nome };
                context.Add(newConta);
                context.SaveChanges();
                decimal taxaJuros = 0;
                var newContaPoupanca = new ContaPoupanca() { Conta = newConta, TaxaJuros = taxaJuros };
                context.Add(newContaPoupanca);
                context.SaveChanges();
                Console.Clear();
                Console.WriteLine("OPERAÇÃO REALIZADA COM SUCESSO!");

            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.Clear();
                Console.WriteLine("NÃO FOI POSSÍVEL REALIZAR ESTA AÇÃO!");

            }

        }

        public static Conta verificarContaCorrente(int opcao, BancoContext context)
        {
            
            Conta conta = new Conta();
            string nome;
            string cpf;
            Console.WriteLine("DIGITE O NOME DO TITULAR");
            nome = Console.ReadLine();
            Console.WriteLine("DIGITE O CPF DO TITULAR");
            cpf = Console.ReadLine();
            try
            {
                var cliente = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                conta = context.Contas.Where(b => b.Titular == nome && b.Cliente == cliente).FirstOrDefault();
                var contaCorrente = context.ContasCorrente.Where(b => b.Conta == conta).FirstOrDefault();
                if (contaCorrente == null)
                {
                    Console.Clear();
                    Console.WriteLine("CONTA NÃO ENCONTRADA");
                    return null;
                }
            }
            catch (Exception erro)
            {
                Console.Clear();
                erro.ToString();
                Console.WriteLine("CONTA NÃO ENCONTRADA");
                return null;
            }
            return conta;
        }

        public static Conta verificarContaPoupanca(int opcao, BancoContext context)
        {
            
            Conta conta = new Conta();
            string nome;
            string cpf;
            Console.WriteLine("DIGITE O NOME DO TITULAR DA CONTA");
            nome = Console.ReadLine();
            Console.WriteLine("DIGITE O CPF DO TITUTLAR DA CONTA");
            cpf = Console.ReadLine();
            try
            {
                var cliente = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                conta = context.Contas.Where(b => b.Titular == nome && b.Cliente == cliente).FirstOrDefault();
                var contaPoupanca = context.ContasPoupanca.Where(b => b.Conta == conta).FirstOrDefault();

                if (contaPoupanca == null)
                {
                    Console.Clear();
                    Console.WriteLine("CONTA NÃO ENCONTRADA");
                    return null;
                }
            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.WriteLine("CONTA NÃO ENCONTRADA");
                return null;
            }
            return conta;
        }

        static void sacarCorrente(Conta conta, BancoContext context)
        {
            Console.WriteLine("DIGITE A QUANTIDADE DO SAQUE");
            decimal saque;
            
            try
            {
                saque = Decimal.Parse(Console.ReadLine());
                conta.Sacar(saque, conta, context, 1);
            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.WriteLine("aTENCAO! VALOR INVÁLIDO");
            }
        }

        static void depositarCorrente(Conta conta, BancoContext context)
        {
            Console.WriteLine("Digite a quantidade a ser Depositada");
            decimal deposito;
            try
            {
                deposito = Decimal.Parse(Console.ReadLine());
                conta.Depositar(deposito, conta, context, 1);
            }
            catch (Exception e)
            {
                e.ToString();
                Console.WriteLine("Valor Invalido");
            }
        }

        static void sacarPoupanca(Conta conta, BancoContext context)
        {
            Console.WriteLine("Digite a quantidade a ser Sacada");
            decimal saque;
            try
            {
                saque = Decimal.Parse(Console.ReadLine());
                conta.Sacar(saque, conta, context, 2);
            }
            catch (Exception e)
            {
                e.ToString();
                Console.WriteLine("Valor Invalido");
            }
        }

        static void depositarPoupanca(Conta conta, BancoContext context)
        {
            Console.WriteLine("DIGITE O VALOR DO DEPÓSITO");
            decimal deposito;
            
            try
            {
                deposito = Decimal.Parse(Console.ReadLine());
                conta.Depositar(deposito, conta, context, 2);
            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.WriteLine("VALOR INVÁLIDO");
            }
        }

        static void Saldo(Conta conta)
        {
            Console.Clear();
            Console.WriteLine("SALDO DISPONÍVEL: " + conta.Saldo);
        }

        static void AtualizarDados(Conta conta, BancoContext context, int opcao)
        {
            Console.Clear();
            Console.WriteLine("DIGITE NOVAMENTE O NOME DO TITULAR DA CONTA");
            string nome = Console.ReadLine();
            Console.WriteLine("DIGITE NOVAMENTE O CPF DO TITULAR DA CONTA");
            string cpf = Console.ReadLine();
            if (opcao == 1)
            {
                try
                {
                    var clienteC = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                    var contaCorrente = context.ContasCorrente.Where(b => b.Conta == conta).FirstOrDefault();
                    clienteC.Atualizar(conta, clienteC, context);
                }
                catch (Exception erro)
                {
                    Console.Clear();
                    erro.ToString();
                    Console.WriteLine("CREDENCIAIS INCORRETAS");

                }
            }
            else if (opcao == 2)
            {
                try
                {
                    var clienteP = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                    var contaPoupanca = context.ContasPoupanca.Where(b => b.Conta == conta).FirstOrDefault();
                    clienteP.Atualizar(conta, clienteP, context);
                }
                catch (Exception erro)
                {
                    Console.Clear();
                    erro.ToString();
                    Console.WriteLine("CREDENCIAIS INCORRETAS");
                }
            }
        }

        static int deletarConta(BancoContext context, Conta conta, int opcao)
        {
            
            Cliente cliente = new Cliente();
            ContaCorrente contaC = new ContaCorrente();
            ContaPoupanca contaP = new ContaPoupanca();
            int option = 0;
            Console.WriteLine("DIGITE NOVAMENTE O NOME DO TITULAR DA CONTA");
            string nome = Console.ReadLine();
            Console.WriteLine("DIGITE NOVAMENTE O CPF DO TITULAR DA CONTA");
            string cpf = Console.ReadLine();
            try
            {
                if (opcao == 1)
                {
                    cliente = context.Set<Cliente>().Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                    contaC = context.Set<ContaCorrente>().Where(b => b.Conta == conta).FirstOrDefault();
                }
                else if (opcao == 2)
                {
                    cliente = context.Set<Cliente>().Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                    contaP = context.Set<ContaPoupanca>().Where(b => b.Conta == conta).FirstOrDefault();
                }
                for (; option != 2;)
                {
                    Console.Clear();
                    Console.WriteLine("DESEJA DELETAR ESSA CONTA?");
                    Console.WriteLine("1---SIM");
                    Console.WriteLine("2---Não");
                    try
                    {
                        option = Int32.Parse(Console.ReadLine());
                    }
                    catch (Exception erro)
                    {
                        erro.ToString();
                        option = 10;
                    }
                    switch (option)
                    {
                        case 1:
                            if (opcao == 1)
                            {
                                var solicitacao = context.Set<Solicitacao>();
                                foreach (var s in solicitacao)
                                {
                                    if (s.Conta == conta)
                                    {
                                        context.Remove(s);
                                    }
                                }
                                context.Remove(contaC);
                                context.Remove(conta);
                                context.Remove(cliente);
                                context.SaveChanges();
                            }
                            else if (opcao == 2)
                            {
                                var solicitacao = context.Set<Solicitacao>();
                                foreach (var s in solicitacao)
                                {
                                    if (s.Conta == conta)
                                    {
                                        context.Remove(s);
                                    }
                                }
                                context.Remove(contaP);
                                context.Remove(conta);
                                context.Remove(cliente);
                                context.SaveChanges();
                            }
                            Console.WriteLine("OPERAÇÃO REALIZADA COM SUCESSO!");


                            break;

                        case 2:
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("OPÇÃO INVÁLIDA");
                            break;
                    }
                    if (option == 1) return 0;
                }

            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.Clear();
                Console.WriteLine("CREDENCIAIS INCORRETAS");
            }

            return 5;
        }
    }
}
