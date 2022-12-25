using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Atendimento;
using bytebank_ATENDIMENTO.bytebank.Exceptions;
using bytebank_ATENDIMENTO.bytebank.Util;
using System.Collections;

Console.WriteLine("Boas Vindas ao ByteBank, Atendimento.");

#region Exemplos de Arrays no C#
void TestaArrayDeContasCorrentes()
{
    ListaDeContasCorrentes listaDeContas = new ListaDeContasCorrentes();
    listaDeContas.Adicionar(new ContaCorrente(817));
    listaDeContas.Adicionar(new ContaCorrente(817));
    listaDeContas.Adicionar(new ContaCorrente(817));

    ContaCorrente contaDoFulano = new ContaCorrente(901);
    listaDeContas.Adicionar(contaDoFulano);

    listaDeContas.Adicionar(new ContaCorrente(817));
    listaDeContas.Adicionar(new ContaCorrente(817));
    listaDeContas.Adicionar(new ContaCorrente(817));

    listaDeContas.ExibeLista();

    listaDeContas.Remover(contaDoFulano);
    listaDeContas.ExibeLista();

    for (int i = 0; i < listaDeContas.Tamanho; i++)
    {
        ContaCorrente contaAtual = listaDeContas[i];
        Console.WriteLine($"Indice {i} - Conta: {contaAtual.Conta}/{contaAtual.Numero_agencia}");
    }
}

TestaArrayDeContasCorrentes();

#endregion

new BytebankAtendimento().AtendimentoCliente();

