using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Exceptions;

namespace bytebank_ATENDIMENTO.bytebank.Atendimento
{
#nullable disable
    public class BytebankAtendimento
    {
    
        private List<ContaCorrente> _listaDeContas = new List<ContaCorrente>()
        {
            new ContaCorrente(96) {
                Saldo = 1000,
                Titular = new Cliente()
                {
                    Nome = "Fulano",
                    Cpf = "1234",
                    Profissao = "Auxiliar"
                }
            },
            new ContaCorrente(95) {
                Saldo = 1300,
                Titular = new Cliente()
                {
                    Nome = "Ciclano",
                    Cpf = "4321",
                    Profissao = "Auxiliar"
                }
            },
            new ContaCorrente(94) {
                Saldo = 1200,
                Titular = new Cliente()
                {
                    Nome = "Beltrano",
                    Cpf = "1324",
                    Profissao = "Auxiliar"
                }
            },
        };

        public void AtendimentoCliente()
        {
            try
            {
                char opcao = '0';
                while (opcao != '6')
                {
                    Console.Clear();
                    Console.WriteLine("=======================================");
                    Console.WriteLine("=====         Atendimento         =====");
                    Console.WriteLine("=====  1 - Cadastrar Conta        =====");
                    Console.WriteLine("=====  2 - Listar Conta           =====");
                    Console.WriteLine("=====  3 - Remover Conta          =====");
                    Console.WriteLine("=====  4 - Ordenar Conta          =====");
                    Console.WriteLine("=====  5 - Pesquisar Conta        =====");
                    Console.WriteLine("=====  6 - Sair do sistema        =====");
                    Console.WriteLine("=======================================");
                    Console.WriteLine("\n \n");
                    Console.Write("Digite a opção desejada: ");

                    try
                    {
                        opcao = Console.ReadLine()[0];
                    }
                    catch (Exception e)
                    {
                        throw new ByteBankException(e.Message);
                    }


                    switch (opcao)
                    {
                        case '1':
                            CadastrarConta();
                            break;
                        case '2':
                            ListarContas();
                            break;
                        case '3':
                            RemoverConta();
                            break;
                        case '4':
                            OrdenarContas();
                            break;
                        case '5':
                            PesquisaConta();
                            break;
                        default:
                            Console.WriteLine("opção não implementada.");
                            break;
                    }
                }
            }
            catch (ByteBankException excecao)
            {

                Console.WriteLine($"{excecao.Message}");
            }

        }

        private void PesquisaConta()
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("=====       Pesquisar Conta       =====");
            Console.WriteLine("=======================================");
            Console.WriteLine("\n");
            Console.Write("Deseja pesquisar por (1) Número da Conta ou (2) Cpf Titular ou (3) Numero da Agencia ?");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    {
                        Console.Write("Informe o número da conta: ");
                        string _numeroConta = Console.ReadLine();
                        ContaCorrente consultaConta = ConsultaPorNumeroConta(_numeroConta);
                        Console.WriteLine(consultaConta.ToString());
                        Console.ReadKey();
                        break;
                    }
                case 2:
                    {
                        Console.Write("Informe o Cpf do titular: ");
                        string _cpf = Console.ReadLine();
                        ContaCorrente consultaCpf = ConsultaPorCPFTitular(_cpf);
                        Console.WriteLine(consultaCpf.ToString());
                        Console.ReadKey();
                        break;
                    }
                case 3:
                    {
                        Console.Write("Informe o Numero da Agencia: ");
                        int _numeroDaAgencia = int.Parse(Console.ReadLine());
                        List<ContaCorrente> contaPorAgencia = ConsultaPorAgencia(_numeroDaAgencia);
                        ExibirListaDeContas(contaPorAgencia);
                        Console.ReadKey();
                        break;
                    }
                default:
                    Console.WriteLine("opção não implementada");
                    return;


            }

        }
        
        private List<ContaCorrente> ConsultaPorAgencia(int numeroDaAgencia)
        {
            var consulta = (
                from conta in _listaDeContas
                where conta.Numero_agencia == numeroDaAgencia
                select conta
                ).ToList();

            return consulta;
        }

        private ContaCorrente ConsultaPorCPFTitular(string cpf)
        {
            return _listaDeContas.FirstOrDefault(conta => conta.Titular.Cpf.Equals(cpf));
        }

        private ContaCorrente ConsultaPorNumeroConta(string numeroConta)
        {
            return _listaDeContas.FirstOrDefault(conta => conta.Conta.Equals(numeroConta));
        }

        private void OrdenarContas()
        {
            _listaDeContas.Sort();
            Console.WriteLine("... Lista de Contas ordenadas");
            Console.ReadKey();
        }

        private void RemoverConta()
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("=====        Remover Conta        =====");
            Console.WriteLine("=======================================");
            Console.WriteLine("\n");

            Console.Write("Informe o número da Conta: ");
            string numeroConta = Console.ReadLine();
            ContaCorrente conta = null;

            foreach (ContaCorrente item in _listaDeContas)
            {
                if (item.Conta.Equals(numeroConta))
                {
                    conta = item;
                }
            }

            if (conta != null)
            {
                _listaDeContas.Remove(conta);
                Console.WriteLine("... Conta removida da Lista! ...");
            }
            else
            {
                Console.WriteLine("... Conta não encontrada ...");
            }
            Console.ReadKey();
        }

        private void ListarContas()
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("=====       Lista de Contas       =====");
            Console.WriteLine("=======================================");
            Console.WriteLine("\n");

            if (_listaDeContas.Count <= 0)
            {
                Console.WriteLine("... Não há contas cadastradas! ...");
                Console.ReadKey();
                return;
            }

            ExibirListaDeContas(_listaDeContas);
        }

        private void CadastrarConta()
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("=====      Cadastro de conta      =====");
            Console.WriteLine("=======================================");
            Console.WriteLine("\n");

            Console.WriteLine("=====  1 - Informe dados da conta =====");

            Console.Write("Número da Agência: ");
            int numeroAgencia = int.Parse(Console.ReadLine());

            ContaCorrente conta = new ContaCorrente(numeroAgencia);

            Console.WriteLine($"Número da conta [Nova] : {conta.Conta}");

            Console.Write("Informe o saldo inicial: ");
            conta.Saldo = double.Parse(Console.ReadLine());

            Console.Write("Informe o nome do titular: ");
            conta.Titular.Nome = Console.ReadLine();

            Console.Write("Informe o CPF do titular: ");
            conta.Titular.Cpf = Console.ReadLine();

            Console.Write("Informe a profissão do titular: ");
            conta.Titular.Profissao = Console.ReadLine();

            _listaDeContas.Add(conta);
            Console.Write("... Conta cadastrada com sucesso");
            Console.ReadKey();
        }

        private void ExibirListaDeContas(List<ContaCorrente> contaPorAgencia)
        {
            if (contaPorAgencia == null)
            {
                Console.WriteLine("... A consulta não retornou dados ...");
                return;
            }
            else
            {
                foreach (ContaCorrente item in contaPorAgencia)
                {
                    Console.WriteLine(item.ToString());
                    Console.WriteLine("=======================================\n");
                }
            }
        }
    }
}
