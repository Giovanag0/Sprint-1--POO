public class SaldoInsuficienteException : Exception
{
    public decimal ValorDisponivel { get; }
    public decimal ValorNecessario { get; }

    public SaldoInsuficienteException(decimal valorDisponivel, decimal valorNecessario)
        : base($"Saldo insuficiente. Disponível: {valorDisponivel:C2} | Necessário: {valorNecessario:C2}")
    {
        ValorDisponivel = valorDisponivel;
        ValorNecessario = valorNecessario;
    }
}