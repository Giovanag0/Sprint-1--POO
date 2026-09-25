using System.Globalization;

public static class Entrada
{
    private static readonly CultureInfo Cultura = new("pt-BR");

    public static string LerTexto(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string? texto = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(texto))
                return texto.Trim();

            Console.WriteLine("Entrada inválida: o texto não pode ser vazio.");
        }
    }

    public static int LerInteiro(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            try
            {
                return int.Parse(Console.ReadLine() ?? "", Cultura);
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada inválida: digite um número inteiro.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Entrada inválida: número muito grande.");
            }
        }
    }

    public static decimal LerDecimal(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            try
            {
                return decimal.Parse(Console.ReadLine() ?? "", Cultura);
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada inválida: digite um valor numérico (ex.: 150,75).");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Entrada inválida: valor muito grande.");
            }
        }
    }
}
