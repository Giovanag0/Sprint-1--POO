public class ContaCorrente : ContaBancaria
{
    public decimal TaxaSaque { get; } = 2.50m;

    public override string Tipo => "Corrente";

    public ContaCorrente(int numeroConta, string titular, decimal saldoInicial)
        : base(numeroConta, titular, saldoInicial)
    {
    }

    public override void Sacar(decimal valor)
    {
        ValidarValor(valor);

        decimal totalDescontado = valor + TaxaSaque;

        if (totalDescontado > Saldo)
            throw new SaldoInsuficienteException(Saldo, totalDescontado);

        Saldo -= totalDescontado;
    }
}
