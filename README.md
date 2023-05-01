# Projeto Myapp

Este é um projeto simples criado com Asp net core 6 e Elasticsearch para demonstração.

## Instruções de uso

### Com docker-compose

1. Clone o repositório em sua máquina:

   ```
   git clone https://github.com/osniantonio/dotnetchapter01.git
   ```

2. Acesse a pasta raíz do projeto onde o arquivo fica o arquivo docker-compose.yaml:

   ```
   cd dotnetchapter01
   ```

3. Execute o comando para subir os containers:

   ```
   docker-compose up -d --build
   ```

   Obs: Este comando pode levar alguns instantes para finalizar.

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

6. Acesse o projeto no navegador:

   ```
   http://localhost:8080/Test
   ```

7. Se preciso finalizar o container, execute o comando:

   ```
   docker stop <container-id>
   ```

   Obs: Para descobrir o `<container-id>`, execute o comando `docker ps` e encontre o id do container que está rodando o projeto.
