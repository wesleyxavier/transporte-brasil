using System;

namespace Transporte.Entidades
{
    public class Veiculo
    {
        public string Nome { get; private set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public Veiculo Summary { get; set; }

        public Veiculo(string nome)
        {
            this.Nome = nome;
        }
    }
}
