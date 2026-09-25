public class ContaEmpresarial : ContaBancaria
{
    public decimal LimiteEmprestimo { get; }

    public override string Tipo => "Empresarial";

    public decimal LimiteDisponivel => Saldo + LimiteEmprestimo;

    public ContaEmpresarial(int numeroConta, string titular, decimal saldoInicial, decimal limiteEmprestimo)
        : base(numeroConta, titular, saldoInicial)
    {
        if (limiteEmprestimo < 0)
            throw new ArgumentException("O limite de empréstimo não pode ser negativo.");

        LimiteEmprestimo = limiteEmprestimo;
    }

    public override void Sacar(decimal valor)
    {
        ValidarValor(valor);

        if (valor > LimiteDisponivel)
            throw new SaldoInsuficienteException(LimiteDisponivel, valor);

        Saldo -= valor;
    }

    public override string ToString()
    {
        return base.ToString() + $" | Limite: {LimiteEmprestimo:C2}";
    }
}