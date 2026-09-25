using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

Banco banco = new Banco();
int opcao;

do
{
    MostrarMenu();
    opcao = Entrada.LerInteiro("Escolha uma opção: ");
    Console.WriteLine();

    try
    {
        switch (opcao)
        {
            case 1:
                CriarConta(banco);
                break;
            case 2:
                ListarContas(banco);
                break;
            case 3:
                Depositar(banco);
                break;
            case 4:
                Sacar(banco);
                break;
            case 5:
                Transferir(banco);
                break;
            case 6:
                AplicarRendimentos(banco);
                break;
            case 0:
                Console.WriteLine("Encerrando o sistema. Até logo!");
                break;
            default:
                Console.WriteLine("Opção inválida. Escolha um número do menu.");
                break;
        }
    }
    catch (SaldoInsuficienteException ex)
    {
        Console.WriteLine($"Operação negada: {ex.Message}");
    }
    catch (ContaNaoEncontradaException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Dado inválido: {ex.Message}");
    }

    if (opcao != 0)
    {
        Console.WriteLine();
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

} while (opcao != 0);


// ===================== MÉTODOS DO MENU =====================

static void MostrarMenu()
{
    Console.Clear();
    Console.WriteLine("=====================================");
    Console.WriteLine("         SISTEMA BANCÁRIO");
    Console.WriteLine("=====================================");
    Console.WriteLine("1 - Criar conta");
    Console.WriteLine("2 - Listar contas");
    Console.WriteLine("3 - Depositar");
    Console.WriteLine("4 - Sacar");
    Console.WriteLine("5 - Transferir");
    Console.WriteLine("6 - Aplicar rendimento (poupanças)");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("=====================================");
}

static void CriarConta(Banco banco)
{
    Console.WriteLine("--- Criar conta ---");
    Console.WriteLine("1 - Corrente (taxa de R$ 2,50 por saque)");
    Console.WriteLine("2 - Poupança (sem taxa, com rendimento)");
    Console.WriteLine("3 - Empresarial (limite de empréstimo extra)");
    int tipo = Entrada.LerInteiro("Tipo da conta: ");

    if (tipo < 1 || tipo > 3)
    {
        Console.WriteLine("Tipo de conta inválido.");
        return;
    }

    string titular = Entrada.LerTexto("Nome do titular: ");
    decimal saldoInicial = Entrada.LerDecimal("Saldo inicial: R$ ");

    decimal limite = 0;
    if (tipo == 3)
        limite = Entrada.LerDecimal("Limite de empréstimo: R$ ");

    ContaBancaria conta = banco.AbrirConta(tipo, titular, saldoInicial, limite);
    Console.WriteLine();
    Console.WriteLine("Conta criada com sucesso!");
    Console.WriteLine(conta);
}

static void ListarContas(Banco banco)
{
    Console.WriteLine("--- Contas cadastradas ---");

    if (banco.Contas.Count == 0)
    {
        Console.WriteLine("Nenhuma conta cadastrada.");
        return;
    }

    foreach (ContaBancaria conta in banco.Contas)
        Console.WriteLine(conta);
}

static void Depositar(Banco banco)
{
    Console.WriteLine("--- Depositar ---");
    int numero = Entrada.LerInteiro("Número da conta: ");
    ContaBancaria conta = banco.BuscarConta(numero);

    decimal valor = Entrada.LerDecimal("Valor do depósito: R$ ");
    conta.Depositar(valor);

    Console.WriteLine("Depósito realizado!");
    Console.WriteLine($"Novo saldo: {conta.Saldo:C2}");
}

static void Sacar(Banco banco)
{
    Console.WriteLine("--- Sacar ---");
    int numero = Entrada.LerInteiro("Número da conta: ");
    ContaBancaria conta = banco.BuscarConta(numero);

    decimal valor = Entrada.LerDecimal("Valor do saque: R$ ");

    conta.Sacar(valor);

    Console.WriteLine("Saque realizado!");
    Console.WriteLine($"Novo saldo: {conta.Saldo:C2}");
}

static void Transferir(Banco banco)
{
    Console.WriteLine("--- Transferir ---");
    int origem = Entrada.LerInteiro("Conta de origem: ");
    int destino = Entrada.LerInteiro("Conta de destino: ");
    decimal valor = Entrada.LerDecimal("Valor da transferência: R$ ");

    banco.Transferir(origem, destino, valor);

    Console.WriteLine("Transferência realizada!");
    Console.WriteLine($"Saldo origem : {banco.BuscarConta(origem).Saldo:C2}");
    Console.WriteLine($"Saldo destino: {banco.BuscarConta(destino).Saldo:C2}");
}

static void AplicarRendimentos(Banco banco)
{
    Console.WriteLine("--- Aplicar rendimento ---");
    int quantidade = banco.AplicarRendimentos();

    if (quantidade == 0)
        Console.WriteLine("Nenhuma conta com rendimento cadastrada.");
    else
        Console.WriteLine($"Rendimento aplicado em {quantidade} conta(s).");
}