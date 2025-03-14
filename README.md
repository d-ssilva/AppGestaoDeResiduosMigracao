# AppGestaoDeResiduos

Este projeto é uma API para gestão de resíduos, desenvolvida em **ASP.NET Core**, que permite o rastreamento de caminhões de coleta, agendamento de coletas e notificação de usuários. Inicialmente desenvolvido para trabalhar com **Oracle**, o projeto está em processo de migração para o **MongoDB**.

---

## 🚀 Funcionalidades

- **Rastreamento de Caminhões**: Monitora a localização dos caminhões de coleta em tempo real.
- **Agendamento de Coletas**: Agenda coletas de resíduos com base na capacidade dos caminhões.
- **Notificação de Usuários**: Envia notificações aos moradores sobre dias de coleta e separação adequada de resíduos.
- **Migração para MongoDB**: O projeto está sendo migrado do Oracle para o MongoDB, aproveitando as vantagens de um banco de dados NoSQL.

---

## 🛠️ Tecnologias Utilizadas

- **Backend**:
  - ASP.NET Core
  - Entity Framework Core (para Oracle)
  - MongoDB.Driver (para MongoDB)
  - Autenticação JWT

- **Banco de Dados**:
  - Oracle (legado)
  - MongoDB (em migração)

- **Ferramentas**:
  - Docker (para containerização)
  - Swagger (para documentação da API)
  - XUnit (para testes unitários)

---

## 📋 Pré-requisitos

Antes de executar o projeto, certifique-se de ter instalado:

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker](https://www.docker.com/) (opcional, para rodar o MongoDB em um contêiner)
- [MongoDB](https://www.mongodb.com/try/download/community) (ou use o MongoDB Atlas)