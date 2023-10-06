# PontaAgro
### Pré requisitos
1. Possuir a  versão donet 6.0 ou superior e visual studio 2022 ou superior instalado no ambiente de desenvolvimento. (https://visualstudio.microsoft.com/pt-br/vs/community/)
2. Instalar posgresSql [https://www.enterprisedb.com/downloads/postgres-postgresql-downloads].
3. Seguir passo a passo da configuração do ambiente de desenvolvimento que se encontra no repositório.
4. Ler com atenção a configuração inicial do projeto,documento Manual de configuração do ambiente de desenvolvimento.pdf
   
## Estrutura do projeto

## ⚙️ Mecanismos Arquiteturiais

|Análise            |	Design                                |	Implementação      |
|-------------------|-----------------------------------------|------------------|
|Persistência       |	ORM	                                  | Entity Framework   |
|Persistência       |	Banco de dados relacional             | Postgres           |
|Back-end	        |  Arquitetura em camadas                 |	.Net Core 6        |
|Versionamento      |	Versionamento do código das aplicações|	GitHub             |
|Documentação de API|Solução para documentação das APIs da solução|	    Swagger  |
|Teste de Software| 	Teste unitários	                      |NUnit             |

## ⚙️ Estrutura Backend

<p align="justify">Desenvolvido em DDD (Domain Driven Design) é uma modelagem de software na qual o objetivo é facilitar a implementação de regras e processos, onde visa a divisão de responsabilidades por camadas e é independente da tecnologia utilizada. Ou seja, o DDD é uma filosofia voltado para o domínio do negócio.</p>

* Aplicação: Porta de entrada, responsável por receber as requisições e direcioná-las para camadas mais internas.
* Domínio: Responsável pelo Core do projeto, contendo classes, enums e interfaces que poderão ser utilizadas para compor as regras de negócio.
* Serviço: Um dos responsáveis pelo Core do projeto, onde é utilizado o que há na camada Domínio para realizar, de fato, as regras de negócio.
* Infra: Camada para comunicação externa, ou seja, pela comunicação com banco de dados, realizando operações.
## ⚙️ Estrutura do projeto
* Ponta.Aplicação – Projeto responsável pela organização e exposição das rotas da API.
* Ponta.Servico – Projeto que contém toda a regra de negócio da API.
* Ponta.Infra – Projeto responsável por centralizar a comunicação com o banco de dados.
* Ponta.Dominio– Projeto responsável por centralizar todo o Core do projeto.
* Ponta.TesteUnitario – Projeto com os testes unitários da API.
  
### ⚙️ Migrations no Backend

<p align="justify"> É uma forma de versionar o schema de sua aplicação. Migrations trabalha na manipulação da base de dados: criando, alterando ou removendo. Uma forma de controlar as alterações do seu banco juntamente com o versionamento de sua aplicação e compartilhar-la.</p>

* Exemplo: Sua aplicação necessita sofrer uma alteração, será necessário adicionar uma nova tabela na base de dados. 
* Porém neste cenário sua aplicação está em produção, você faz um commit alteração para homologação, depois do merge para produção, certo? 
* Porém sua base de dados sofreu alteração, como você irá criar a tabela? Conectando na base de dados.
Com as migrations não teremos esta necessidade de conectar na base para rodar as querys.
 
### ⚙️ ORM Entity Framework

O EF funciona com diversos tipos de banco de dados, e assim como todo e qualquer ORM, facilita o acesso ao banco de dados, mapeando suas tabelas e permitindo a manipulação dos registros sem muito esforço.
O que diferencia o EF de outros ORM é o uso do LINQ para montagem de queries no próprio C# e suporte da ferramenta e na comunidade.

### ⌨️ Principios Solid 

* S: Single Responsibility Principle
* O: Open-Closed Principle
* L: Liskov Substitution Principle
* I: Interface Segregation Principle
* D: Dependency Inversion Principle

S: Single Responsibility Principle

Uma classe deve ser responsável por fazer apenas um trabalho. Se a classe tem mais de uma responsabilidade, ela tende-se a ter um acoplamento. Uma mudança em uma responsabilidade resulta em modificação de outra responsabilidade.

O: Open-Closed Principle

Permite que o código seja extendido sem se preocupar com as classes, métodos legados. Uma vez que estas classes e métodos foram criados, eles não devem ser mais modificados, mas sim devem estar abertas para extensão.

L: Liskov Substitution Principle

Fundamental para a aplicação de heranças na orientação à objetos. Em outras palavras, este princípio também é enunciado da seguinte maneira: "uma classe base deve poder ser substituída por sua classe derivada"

I: Interface Segregation Principle

Melhor ter várias interfaces com alguns métodos que serão todos utilizados, do que uma única interface que tenha vários métodos mas várias implementações que não utilizam todos eles. 

D: Dependency Inversion Principle

Desacoplar as dependências do projeto, induzindo que módulos de alto e baixo nível dependam de uma mesma abstração.

Estes princípios tem como objetivo obter as vantagens na orientação a objetos através de um código de alta qualidade sendo assim nos permite ter:

 * Uma leitura, testes e manutenção fáceis.
 * Extensibilidade com o menor esforço possível.
 * Reaproveitamento.
 * Maximização do tempo de utilização do código.

### 🔧 Link acesso a API 
* http://localhost:5001/index.html
