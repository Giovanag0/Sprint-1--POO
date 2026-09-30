# SISTEMA BANCÁRIO- DESAFIO POO
Sistema desenvolvido na linguagem c# que aplica os conceitos estudados em Programação Orientada a Objeto, sendo elas: Classes e objetos, construtores e propriedades, encapsulamento, herança, polimorfismo, interface e tratamento de exceções.
-------------------------------------------------------------------------------------------------

PROBLEMA: 
Um banco precisa gerenciar diferentes tipos de contas. Todas as contas têm saldo e titular, mas as regras de saque e taxas funcionam de forma diferente dependendo do tipo:
*ContaCorrente:* Tem uma taxa a cada saque. 
*ContaPoupanca:* Não tem taxa, mas pode ter rendimento. 
*ContaEmpresarial:* Tem um limite de empréstimo extra.
-------------------------------------------------------------------------------------------------

CLASSES DA SOLUÇÃO:
*ContaBancaria (classe pai, abstrata)*
Atributos: NumeroConta do tipo int; Titular do tipo string; Saldo do tipo decimal; Tipo do tipo string (abstrato, cada filha define o próprio valor).
Métodos: Depositar (decimal valor): adiciona valor ao saldo; Sacar (decimal valor): abstrato, cada filha implementa a própria regra; ValidarValor (decimal valor): verifica se o valor é maior que zero; ToString(): monta a linha de exibição da conta na listagem.
----------------------------------------------------------------------------------------------------

*ContaCorrente (herda de ContaBancaria)*
Atributos: TaxaSaque do tipo decimal, valor fixo cobrado a cada saque.
Métodos: Sacar (decimal valor): desconta o valor pedido mais a taxa. Lança SaldoInsuficienteException se o saldo não cobrir os dois.
-------------------------------------------------------------------------------------------------------

*ContaPoupança (herda de ContaBancaria, implementa IRentavel):*
Atributos: TaxaRendimento do tipo decimal, percentual aplicado sobre o saldo.
Métodos: Sacar (decimal valor): desconta o valor direto do saldo, sem taxa. Lança SaldoInsuficienteException se o saldo for menor que o valor pedido; AplicarRendimento(): soma ao saldo o rendimento calculado pela taxa.
-------------------------------------------------------------------------------------------------------

*ContaEmpresarial (herda de ContaBancaria):*
Atributos: LimiteEmprestimo do tipo decimal, valor extra que a conta pode usar além do saldo; LimiteDisponivel do tipo decimal (calculado), soma do saldo com o limite de empréstimo.
Métodos: Sacar (decimal valor): permite saldo negativo até o limite de empréstimo. Lança SaldoInsuficienteException se o valor ultrapassar o limite disponível.
-------------------------------------------------------------------------------------------------------

*IRentavel (interface):*  
Contrato para contas que rendem. Só a ContaPoupanca implementa.
Membros: TaxaRendimento (propriedade que a classe implementadora deve expor); AplicarRendimento() (método que a classe implementadora deve definir).
-------------------------------------------------------------------------------------------------------

*Banco (classe de controle):*
Guarda a lista de contas e concentra as operações do sistema.
Atributos: lista de ContaBancaria com todas as contas abertas.
Métodos: AbrirConta: cria uma conta do tipo escolhido e adiciona à lista; BuscarConta (int numeroConta): retorna a conta com o número informado, ou lança ContaNaoEncontradaException; Transferir (int origem, int destino, decimal valor): saca da conta de origem e deposita na de destino; AplicarRendimentos(): aplica rendimento em todas as contas que implementam IRentavel.
-------------------------------------------------------------------------------------------------------

RELACIONAMENTO ENTRE AS CLASSES:
*Herança:* ContaCorrente, ContaPoupança e ContaEmpresarial herdam de ContaBancaria.
*Interface:* ContaPoupança implementa IRentavel.
*Composição:* Banco é composto por uma lista de ContaBancaria.
*Polimorfismo:* O método Sacar() é chamado da mesma forma em qualquer conta, mas se comporta de acordo com o tipo real do objeto (taxa na Corrente, sem taxa na Poupança, limite extra na Empresarial).
