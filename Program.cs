using System.Collections;
using System.Diagnostics;

internal class Program
{
    public static List<Tag> listaTags = new List<Tag>();
    public static List<Veiculo> listaVeiculos = new List<Veiculo>();

    private static void Main(string[] args)
    {
        Cadastro();
        Menu();
        Resumo();
    }

    static void Cadastro()
    {
        try
        {
            int opcao;

            Console.WriteLine("=========================================");

            Console.WriteLine("      Bem-Vindos ao Programa de TAGs      ");

            Console.WriteLine("=========================================");

            do
            {
                Console.WriteLine("O que você deseja fazer? \n 1- Criar TAG e Veículo \n 2- Mostrar Lista \n 0- Sair");
                opcao = int.Parse(Console.ReadLine());

                switch ((CadastroEnumeradores)opcao)
                {
                    case CadastroEnumeradores.CriarTagVeiculo:
                        var tag = CriarTag();
                        CadastrarVeiculo(tag);
                        break;

                    case CadastroEnumeradores.MostrarLista:
                        ExibirLista();
                        break;

                    case CadastroEnumeradores.Sair:
                        Console.WriteLine("Saindo...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            } while (opcao != 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static Tag CriarTag()
    {
        try
        {
            Console.WriteLine("Informe o nome da TAG que deseja criar:");
            string identificacao = Console.ReadLine();

            var tag = new Tag(identificacao);

            var verificandoTag = listaTags.FirstOrDefault(x => x.Identificacao == identificacao);

            if (verificandoTag != null)
            {
                throw new Exception("TAG já cadastrada.");
            }
            else
            {
                Console.WriteLine("TAG cadastrada com sucesso.");
                listaTags.Add(tag);
                return tag;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
            CriarTag();
        }
        return null;
    }

    static void CadastrarVeiculo(Tag tag)
    {
        try
        {
            Console.WriteLine("Informe a placa do veículo");
            string placa = Console.ReadLine();

            var verificandoPlaca = listaVeiculos.FirstOrDefault(x => x.Placa == placa);

            if (verificandoPlaca != null)
            {
                throw new Exception("Veículo já cadastrado");
            }
            else
            {
                Console.WriteLine("Informe o tipo do veículo: \n 1- Moto \n 2- Carro \n 3- Caminhao");
                //Enum.TryParse(Console.ReadLine(), out TiposDeVeiculoEnumeradores tipoDeVeiculo);

                //var tipoDeVeiculo = Enum.Parse<TiposDeVeiculoEnumeradores>(Console.ReadLine());

                if (Enum.TryParse(Console.ReadLine(), out TiposDeVeiculoEnumeradores tipoDeVeiculo) && (tipoDeVeiculo == TiposDeVeiculoEnumeradores.Moto || tipoDeVeiculo == TiposDeVeiculoEnumeradores.Carro || tipoDeVeiculo == TiposDeVeiculoEnumeradores.Caminhao))
                {
                    Console.WriteLine("Informe o modelo do veículo:");
                    string modelo = Console.ReadLine();

                    Console.WriteLine("Informe a marca do veículo:");
                    string marca = Console.ReadLine();

                    var veiculo = new Veiculo(placa, modelo, marca, tipoDeVeiculo);

                    listaVeiculos.Add(veiculo);

                    veiculo.AssociarTAG(tag);

                    Console.WriteLine("Veiculo cadastrado com sucesso!");
                }
                else
                {
                    throw new Exception("Opção inválida!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
            CadastrarVeiculo(tag);
        }

    }

    static void ExibirLista()
    {
        try
        {
            if (listaVeiculos.Count() == 0)
            {
                throw new Exception("A lista está vazia.");
            }
            else
            {
                Console.WriteLine("Lista de veículos com suas respectivas TAGS:");
                foreach (var item in listaVeiculos)
                {
                    Console.WriteLine($"Tag: {item.Tag.Identificacao} | Modelo: {item.Modelo} | Marca: {item.Marca} | Placa: {item.Placa} | Tipo: {item.TipoDeVeiculo}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void Menu()
    {
        try
        {
            int opcao;

            do
            {
                Console.WriteLine("Informe a opção desejada: \n 1- Carregar Saldo \n 2- Exibir Saldo \n 3- Passagem pelo Pedágio \n 4- Alterar Placa \n 5- Informação \n 6- Relatório \n 7- Cadastro \n 0- Sair");
                opcao = int.Parse(Console.ReadLine());

                switch ((MenuEnumeradores)opcao)
                {
                    case MenuEnumeradores.CarregarSaldo:
                        CarregarSaldo();
                        break;

                    case MenuEnumeradores.ExibirSaldo:
                        ExibirSaldo();
                        break;

                    case MenuEnumeradores.PassagemPeloPedagio:
                        PassarPeloPedagio();
                        break;

                    case MenuEnumeradores.AlterarPlaca:
                        AlterarPlaca();
                        break;

                    case MenuEnumeradores.Informacao:
                        Informacao();
                        break;

                    case MenuEnumeradores.Relatorio:
                        Relatorio();
                        break;

                    case MenuEnumeradores.Cadastro:
                        Cadastro();
                        break;

                    case MenuEnumeradores.Sair:
                        Console.WriteLine("Obrigado e volte sempre!");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            } while (opcao != 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void CarregarSaldo()
    {
        try
        {
            Console.WriteLine("Em qual TAG deseja fazer a recarga?");
            string identificacao = Console.ReadLine();

            var verificandoTag = listaTags.FirstOrDefault(x => x.Identificacao == identificacao);

            if (verificandoTag != null)
            {
                Console.WriteLine("Informe o valor que deseja carregar:");
                decimal valorRecarga = decimal.Parse(Console.ReadLine());

                if (valorRecarga > 0)
                {
                    verificandoTag.RecarregarTag(valorRecarga);
                    Console.WriteLine("Recarca realizada com sucesso!");
                    Console.WriteLine($"Saldo atual: {verificandoTag.ObterSaldo()}");
                }
                else
                {
                    throw new Exception("Informe um valor válido.");
                }
            }
            else
            {
                throw new Exception("TAG não encontrada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void ExibirSaldo()
    {
        try
        {
            Console.WriteLine("Em qual TAG deseja saber o saldo?");
            string identificacao = Console.ReadLine();

            var verificandoTag = listaTags.FirstOrDefault(x => x.Identificacao == identificacao);

            if (verificandoTag != null)
            {
                Console.WriteLine($"Saldo da {verificandoTag.Identificacao}: {verificandoTag.ObterSaldo()}");
            }
            else
            {
                throw new Exception("TAG não encontrada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void PassarPeloPedagio()
    {
        try
        {
            Console.WriteLine("Qual TAG deseja passar pelo pedágio?");
            string identificacao = Console.ReadLine();

            var verificandoTag = listaTags.FirstOrDefault(x => x.Identificacao == identificacao);

            if (verificandoTag != null)
            {
                var verificandoVeiculo = listaVeiculos.FirstOrDefault(x => x.Tag == verificandoTag);

                if (verificandoVeiculo != null)
                {
                    if (Pedagio.PassarPedagio(verificandoVeiculo) == true)
                    {
                        Console.WriteLine($"Boa viagem! Saldo atual: R$ {verificandoVeiculo.Tag.Saldo}");

                        var passagem = new Passagem(verificandoVeiculo);

                        Pedagio.passagensEfetivadas.Add(passagem);

                        passagem.DataHora = DateTime.Now;
                        passagem.Veiculo = verificandoVeiculo;

                        Console.WriteLine($"Valor cobrado: R$ {passagem.ValorCobrado}");
                        Console.WriteLine($"Data e Hora: {passagem.DataHora}");

                    }
                    else
                    {
                        var tentativaPassagem = new TentativaPassagem(verificandoVeiculo);

                        Console.WriteLine(tentativaPassagem.Motivo);

                        Pedagio.passagensNaoEfetuadas.Add(tentativaPassagem);

                        tentativaPassagem.DataHora = DateTime.Now;
                        tentativaPassagem.Veiculo = verificandoVeiculo;

                        Console.WriteLine($"Data e Hora: {tentativaPassagem.DataHora}");
                    }
                }
                else
                {
                    throw new Exception($"Veículo não associado a TAG {verificandoTag.Identificacao}.");
                }
            }
            else
            {
                throw new Exception("TAG não encontrada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void AlterarPlaca()
    {
        try
        {
            Console.WriteLine("Qual placa deseja alterar?");
            string placa = Console.ReadLine();

            var verificandoPlaca = listaVeiculos.FirstOrDefault(x => x.Placa == placa);

            if (verificandoPlaca != null)
            {
                Console.WriteLine("Informe a placa nova:");
                string placaNova = Console.ReadLine();

                verificandoPlaca.AlterarPlaca(placaNova);

                Console.WriteLine($"Placa alterada com sucesso. Nova placa: {verificandoPlaca.Placa}");
            }
            else
            {
                throw new Exception("Placa não encontrada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void Informacao()
    {
        try
        {
            Console.WriteLine("Qual TAG deseja saber informação?");
            string identificacao = Console.ReadLine();

            var verificandoTag = listaTags.FirstOrDefault(x => x.Identificacao == identificacao);

            if (verificandoTag != null)
            {
                var verificandoVeiculo = listaVeiculos.FirstOrDefault(x => x.Tag == verificandoTag);

                if (verificandoVeiculo != null)
                {
                    Console.WriteLine($"Placa: {verificandoVeiculo.Placa} | Marca: {verificandoVeiculo.Marca} | Modelo: {verificandoVeiculo.Modelo} | Tag: {verificandoVeiculo.Tag.Identificacao} | Tipo: {verificandoVeiculo.TipoDeVeiculo} | Saldo: {verificandoVeiculo.Tag.ObterSaldo()}");
                }
                else
                {
                    throw new Exception($"Veículo não associado a TAG {identificacao}");
                }
            }
            else
            {
                throw new Exception("TAG não encontrada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void Relatorio()
    {
        try
        {
            Console.WriteLine("Qual TAG deseja saber o relatório?");
            string identificacao = Console.ReadLine();

            var verificandoTag = listaTags.FirstOrDefault(x => x.Identificacao == identificacao);

            if (verificandoTag != null)
            {
                var verificandoVeiculo = listaVeiculos.FirstOrDefault(x => x.Tag == verificandoTag);

                var verificandoPassagem = Pedagio.passagensEfetivadas.Where(x => x.Veiculo == verificandoVeiculo);

                var verificandoTentativaPassagem = Pedagio.passagensNaoEfetuadas.Where(x => x.Veiculo == verificandoVeiculo);

                if (verificandoVeiculo != null)
                {
                    Console.WriteLine($"Passagens COM sucesso pelo pedágio: {verificandoPassagem.Count()}");
                    Console.WriteLine($"Passagens SEM sucesso pelo pedágio: {verificandoTentativaPassagem.Count()}");
                }
                else
                {
                    throw new Exception($"Veículo não associado a TAG {identificacao}");
                }
            }
            else
            {
                throw new Exception("TAG não encontrada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void Resumo()
    {
        try
        {
            if (listaVeiculos.Count() == 0)
            {
                throw new Exception("A lista está vazia.");
            }
            else
            {
                foreach (var item in listaVeiculos)
                {
                    Console.WriteLine($"Tag: {item.Tag.Identificacao} | Modelo: {item.Modelo} | Marca: {item.Marca} | Placa: {item.Placa} | Tipo: {item.TipoDeVeiculo} | Saldo: R$ {item.Tag.Saldo}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }
}

