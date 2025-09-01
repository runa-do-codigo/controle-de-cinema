# Controle de Cinema

<div align="center">
  <img src="https://i.imgur.com/m5Ge8sr.gif" alt="Cliente" width="298" />
  <img src="https://i.imgur.com/K48XU9w.gif" alt="Empresa" width="300" />
</div>

## Introdução

O projeto **Controle de Cinema** tem como objetivo gerenciar filmes, sessões e ingressos de forma prática, organizada e eficiente.

Desenvolvido como projeto acadêmico, a aplicação utiliza **C# com .NET 8.0**, com interface web em **ASP.NET MVC**, seguindo uma **arquitetura em camadas**, garantindo separação de responsabilidades, manutenção facilitada e escalabilidade.

A solução permite que cinemas organizem sua programação, gerenciem salas e sessões, controlem a venda de ingressos e otimizem o atendimento ao público.

---

## Tecnologias

<p align="left"> <img src="https://skillicons.dev/icons?i=cs" height="50"/> <img src="https://skillicons.dev/icons?i=dotnet" height="50"/> <img src="https://skillicons.dev/icons?i=visualstudio" height="50"/> <img src="https://skillicons.dev/icons?i=html" height="50"/> <img src="https://skillicons.dev/icons?i=css" height="50"/> <img src="https://skillicons.dev/icons?i=js" height="50"/> <img src="https://skillicons.dev/icons?i=bootstrap" height="50"/> <img src="https://skillicons.dev/icons?i=git" height="50"/> <img src="https://skillicons.dev/icons?i=github" height="50"/> <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/postgresql/postgresql-original.svg" height="45"/> <img src="https://cdn.jsdelivr.net/gh/simple-icons/simple-icons/icons/render.svg" height="40"/> <img src="https://skillicons.dev/icons?i=azure" height="50"/> <img src="https://skillicons.dev/icons?i=docker" height="50"/> </p>

---

## Funcionalidades

### Para Empresas (Admin)
- **Filmes**: cadastrar, editar, excluir e visualizar informações dos filmes (nome, gênero, duração, classificação indicativa).  
- **Salas**: gerenciar salas de exibição (número, capacidade, tipo de sala).  
- **Sessões**: criar sessões de filmes, definindo data, horário e preço do ingresso; editar ou cancelar sessões.

### Para Clientes
- **Ingressos**: visualizar sessões disponíveis, escolher lugares e comprar ingressos.  

---

## Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

```
dotnet restore
```

4. Em seguida, compile a solução utilizando o comando:
   
```
dotnet build --configuration Release
```

5. Para executar o projeto compilando em tempo real
   
```
dotnet run --project ControleDeCinema.ConsoleApp
```

6. Para executar o arquivo compilado, navegue até a pasta `./ControleDeCinema.WebApp/bin/Release/net8.0/` e execute o arquivo:
   
```
ControleDeCinema.ConsoleApp.exe
```

## Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.

- Visual Studio 2022 ou superior (opcional, para desenvolvimento).
