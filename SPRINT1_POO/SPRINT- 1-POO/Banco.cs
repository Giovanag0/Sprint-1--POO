public class Banco
{
    private readonly List<ContaBancaria> _contas = new();
    private int _proximoNumero = 1001;

    public IReadOnlyList<ContaBancaria> Contas => _contas;

    public ContaBancaria AbrirConta(int tipo, string titular, decimal saldoInicial, decimal limite = 0)
    {
        int numero = _proximoNumero;

        ContaBancaria conta = tipo switch
        {
            1 => new ContaCorrente(numero, titular, saldoInicial),
            2 => new ContaPoupanca(numero, titular, saldoInicial),
            3 => new ContaEmpresarial(numero, titular, saldoInicial, limite),
            _ => throw new ArgumentException("Tipo de conta inválido.")
        };

        _contas.Add(conta);
        _proximoNumero++;
        return conta;
    }

    public ContaBancaria BuscarConta(int numeroConta)
    {
        foreach (ContaBancaria conta in _contas)
        {
            if (conta.NumeroConta == numeroConta)
                return conta;
        }

        throw new ContaNaoEncontradaException(numeroConta);
    }

    public void Transferir(int numeroOrigem, int numeroDestino, decimal valor)
    {
        if (numeroOrigem == numeroDestino)
            throw new ArgumentException("A conta de origem e a de destino devem ser diferentes.");

        ContaBancaria origem = BuscarConta(numeroOrigem);
        ContaBancaria destino = BuscarConta(numeroDestino);

        origem.Sacar(valor);
        destino.Depositar(valor);
    }

    public int AplicarRendimentos()
    {
        int quantidade = 0;

        foreach (ContaBancaria conta in _contas)
        {
            if (conta is IRentavel rentavel)
            {
                rentavel.AplicarRendimento();
                quantidade++;
            }
        }

        return quantidade;
    }
}