# Projeto Myapp

Este é um simples projeto de demonstração de observabilidade em uma aplicação Asp net core 6 utilizando o Elasticsearch como ferramenta de armazenamento e análise de logs e métricas. Para isso, foram utilizadas as seguintes ferramentas:

- Serilog: biblioteca de logging utilizada para enviar logs para o Elasticsearch;

- OpenTelemetry: uma plataforma de observabilidade utilizada para gerar e enviar dados de tracing para o Elasticsearch;

- Jaeger: sistema de tracing utilizado em conjunto com o OpenTelemetry para visualizar e analisar traces;

- Docker: uma plataforma de contêineres que permitiu a criação de um ambiente isolado para rodar a aplicação e o Elasticsearch;

- Docker Compose: uma ferramenta que permitiu a definição e execução de múltiplos contêineres de forma integrada, facilitando a configuração do ambiente de desenvolvimento e demonstração.

## Instruções de uso

### Com docker-compose

1. Clone o repositório em sua máquina:

   ```
   git clone https://github.com/osniantonio/dotnetchapter01.git
   ```

2. Acesse a pasta raíz do projeto onde fica o arquivo docker-compose.yaml:

   ```
   cd dotnetchapter01
   ```

3. Execute o comando para subir os containers:

   ```
   docker-compose up -d --build
   ```

   Obs: Este comando pode levar algun tempo para finalizar.

4. Acesse o Kibana e o Jaeger no navegador para verificar se estão no ar:

   ```
   http://localhost:5601/app/home#/
   http://localhost:16686/
   ```

5. Se preciso finalizar, execute o comando:

   ```
   docker-compose down
   ```

### Com docker

1. Clonado o repositório em sua máquina;

2. Acesse a pasta myapp do projeto dentro de dotnetchapter01:

   ```
   cd myapp
   ```

3. Execute o comando para criar o pacote:

   ```
   dotnet publish -c Release
   ```

4. Execute o comando para criar a imagem Docker chamada myapp:latest:

   ```
   docker build -t myapp:latest .
   ```

5. Execute o comando para rodar o projeto na porta 8080 via imagem:

   ```
   docker run -p 8080:80 -e "ASPNETCORE_ENVIRONMENT=Development" myapp:latest
   ```

6. Acesse o projeto no navegador para testar:

   ```
   https://localhost:8080/Test/test1
   https://localhost:8080/Test/test2
   ```

7. Se preciso finalizar o container, execute o comando:

   ```
   docker stop <container-id>
   ```

   Obs: Para descobrir o `<container-id>`, execute o comando `docker ps` e encontre o id do container que está rodando o projeto.
