using System;

namespace Secao1
{
    class Program
    {
        static void Main(string[] args)
        {
            Pessoa pessoa1 = new Pessoa();
            pessoa1.nome = "Lucas Antunes";
            pessoa1.idade = 29;
            pessoa1.genero = "Masculino";
            pessoa1.Identificar();
        }
    }
}