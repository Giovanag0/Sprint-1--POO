public class ContaPoupanca : ContaBancaria, IRentavel
{
    public decimal TaxaRendimento { get; } = 0.005m; // 0,5% ao mês

    public override string Tipo => "Poupança";

    public ContaPoupanca(int numeroConta, string titular, decimal saldoInicial)
        : base(numeroConta, titular, saldoInicial)
    {
    }

    public override void Sacar(decimal valor)
    {
        ValidarValor(valor);

        if (valor > Saldo)
            throw new SaldoInsuficienteException(Saldo, valor);

        Saldo -= valor;
    }

    public void AplicarRendimento()
    {
        Saldo += Saldo * TaxaRendimento;
    }
}