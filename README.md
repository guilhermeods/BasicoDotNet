# 📘 **README.md — API de Avisos (Desafio Técnico)**

## 📌 **Descrição Geral**
Este projeto é uma API desenvolvida em .NET, seguindo o padrão arquitetural da Bernhoeft e utilizando MediatR, FluentValidation e Entity Framework Core (via camada de infraestrutura fornecida).  
O objetivo do desafio é evoluir a API existente para suportar operações completas de Avisos, mantendo a V1 funcionando e criando uma V2 aprimorada.

## 🚀 **Funcionalidades Implementadas**
### ✔️ **Versionamento**
- API agora expõe endpoints em **v1** e **v2**
- v1 permanece sem alterações (compatibilidade preservada)
- v2 inclui melhorias e novos campos no modelo Aviso

### ✔️ **Operações Disponíveis na V2**
- **GET /avisos** → Lista avisos ativos
- **GET /avisos/{id}** → Retorna aviso pelo ID
- **POST /avisos** → Cria um novo aviso
- **PUT /avisos/{id}** → Atualiza apenas a mensagem
- **DELETE /avisos/{id}** → Soft delete (desativa)

## 🧱 **Tecnologias Utilizadas**
- .NET 9.0  
- Entity Framework Core  
- MediatR  
- FluentValidation  
- Swagger/OpenAPI  
- API Versioning  

## 🧪 **Como Executar o Projeto**
### 1. Restaurar dependências:
dotnet restore

### 2. Rodar a API:
dotnet run --project 1-Presentation/Bernhoeft.GRT.Teste.Api

### 3. Acessar Swagger:
https://localhost:5001

## 🧩 **Validações**
Validações foram implementadas com FluentValidation:
- Título obrigatório (máx. 50 chars)
- Mensagem obrigatória
- Apenas mensagem pode ser alterada na edição
- ID deve ser válido (> 0)

## 🧠 **Decisões de Arquitetura**
O arquivo **DECISIONS.md** inclui:
- Motivações do versionamento
- Decisões sobre soft delete

## 🙋‍♂️ Autor
Guilherme Otávio
