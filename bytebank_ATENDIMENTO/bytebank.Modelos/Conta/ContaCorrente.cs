﻿namespace bytebank.Modelos.Conta
{
	public class ContaCorrente: IComparable<ContaCorrente>
	{
		private int _numero_agencia;

		private string _conta;

		private double saldo;

		public Cliente Titular { get; set; }

		public string Nome_Agencia { get; set; }

		public int Numero_agencia
		{
			get
			{
				return _numero_agencia;
			}
			set
			{
				if (value > 0)
				{
					_numero_agencia = value;
				}
			}
		}

		public string Conta
		{
			get
			{
				return _conta;
			}
			set
			{
				if (value != null)
				{
					_conta = value;
				}
			}
		}

		public double Saldo
		{
			get
			{
				return saldo;
			}
			set
			{
				if (!(value < 0.0))
				{
					saldo = value;
				}
			}
		}

		public static int TotalDeContasCriadas { get; set; }

		public bool Sacar(double valor)
		{
			if (saldo < valor)
			{
				return false;
			}
			if (valor < 0.0)
			{
				return false;
			}
			saldo -= valor;
			return true;
		}

		public void Depositar(double valor)
		{
			if (!(valor < 0.0))
			{
				saldo += valor;
			}
		}

		public bool Transferir(double valor, ContaCorrente destino)
		{
			if (saldo < valor)
			{
				return false;
			}
			if (valor < 0.0)
			{
				return false;
			}
			saldo -= valor;
			destino.saldo += valor;
			return true;
		}

		public ContaCorrente(int numero_agencia)
		{
			Numero_agencia = numero_agencia;
			Conta = Guid.NewGuid().ToString().Substring(0, 8);
			Titular = new Cliente();
			TotalDeContasCriadas++;
		}

        public int CompareTo(ContaCorrente? other)
        {
            if (other == null) return 1;

			return this.Numero_agencia.CompareTo(other.Numero_agencia);
        }

		public override string ToString()
		{
			return $"===  Dados da Contas  ===\n" +
				   $"Número da Conta: {this.Conta}\n" +
				   $"Número da Agencia: {this.Numero_agencia}\n" +
				   $"Saldo da Conta: {this.Saldo}\n" +
				   $"Titular da Conta: {this.Titular.Nome}\n" +
				   $"CPF da Conta: {this.Titular.Cpf}\n" +
				   $"Profissão do Titular: {this.Titular.Profissao}\n";
        }
    }

}