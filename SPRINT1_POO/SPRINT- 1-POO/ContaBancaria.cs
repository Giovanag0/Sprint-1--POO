public abstract class ContaBancaria
{
    private string _titular = string.Empty;

    public int NumeroConta { get; }

    public string Titular
    {
        get => _titular;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("O titular não pode ser vazio.");
            _titular = value.Trim();
        }
    }

    public decimal Saldo { get; protected set; }

    public abstract string Tipo { get; }

    protected ContaBancaria(int numeroConta, string titular, decimal saldoInicial)
    {
        if (saldoInicial < 0)
            throw new ArgumentException("O saldo inicial não pode ser negativo.");

        NumeroConta = numeroConta;
        Titular = titular;
        Saldo = saldoInicial;
    }

    public virtual void Depositar(decimal valor)
    {
        ValidarValor(valor);
        Saldo += valor;
    }

    public abstract void Sacar(decimal valor);

    protected static void ValidarValor(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor deve ser maior que zero.");
    }

    public override string ToString()
    {
        return $"Conta {NumeroConta} | {Tipo,-11} | Titular: {Titular,-15} | Saldo: {Saldo:C2}";
    }
}