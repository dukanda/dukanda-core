# DukandaCore - Contexto do Projeto

## Visão Geral
DukandaCore é uma solução backend desenvolvida em .NET que serve como infraestrutura central da plataforma Dukanda. O projeto utiliza os princípios da Clean Architecture para garantir uma base de código organizada, testável e manutenível.

## Domínio do Negócio
O sistema é estruturado para gerenciar as operações principais da plataforma Dukanda. 

### Principais Entidades
*(Esta seção deve ser preenchida com as entidades específicas do seu domínio)*
- Usuários
- Produtos
- Pedidos
- etc.

## Arquitetura

### Camadas da Aplicação

1. **Camada de Domínio (Domain Layer)**
   - Contém as regras de negócio centrais
   - Define as entidades principais
   - Implementa as regras de validação do domínio
   - Independente de frameworks externos

2. **Camada de Aplicação (Application Layer)**
   - Implementa os casos de uso do sistema
   - Orquestra o fluxo de dados entre as camadas
   - Gerencia as transformações de dados (DTOs)
   - Define interfaces para serviços externos

3. **Camada de Infraestrutura (Infrastructure Layer)**
   - Implementa a persistência de dados
   - Gerencia integrações com serviços externos
   - Implementa o acesso ao banco de dados
   - Fornece implementações concretas das interfaces definidas na camada de aplicação

4. **Camada Web**
   - Disponibiliza endpoints da API
   - Gerencia autenticação e autorização
   - Implementa controllers e rotas
   - Trata requisições HTTP

## Stack Tecnológica
- **Framework**: .NET 8.0
- **Banco de Dados**: SQL Server
- **Arquitetura**: Clean Architecture
- **Padronização de Código**: EditorConfig
- **Documentação API**: Swagger/OpenAPI

## Estratégia de Testes
O projeto implementa diferentes níveis de testes:
- **Testes Unitários**: Validação das regras de negócio
- **Testes de Integração**: Verificação da integração entre componentes
- **Testes Funcionais**: Validação de funcionalidades completas
- **Testes de Aceitação**: Cenários de ponta a ponta

## Fluxo de Desenvolvimento
1. Criação de branch feature/bugfix
2. Desenvolvimento com TDD quando aplicável
3. Code review via pull requests
4. Testes automatizados antes do merge
5. Integração contínua

## Configuração do Ambiente
### Pré-requisitos
- SDK .NET 8.0 ou superior
- SQL Server (LocalDB ou superior)
- IDE compatível com .NET (recomendado: Visual Studio 2022 ou Rider)

### Primeiros Passos
1. Clone o repositório
2. Configure a string de conexão do banco de dados
3. Execute as migrations do banco
4. Build e execute o projeto

## Observações
Este documento deve ser atualizado conforme o projeto evolui. Recomenda-se adicionar:
- Diagramas de arquitetura
- Modelos de domínio
- Fluxos de processos de negócio
- Decisões arquiteturais importantes

## Convenções e Boas Práticas
- Código em inglês
- Commits semânticos
- Documentação em português
- Clean Code
- SOLID Principles
- Domain-Driven Design (quando aplicável)

## Monitoramento e Logs
*(Adicionar informações sobre ferramentas e práticas de monitoramento quando implementadas)*

## Segurança
*(Adicionar informações sobre práticas de segurança, autenticação e autorização quando implementadas)* 