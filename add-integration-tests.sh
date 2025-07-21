#!/bin/bash

# Adiciona o projeto de testes de integração à solução
dotnet sln add tests/Evolution.Client.CSharp.IntegrationTests/Evolution.Client.CSharp.IntegrationTests.csproj

# Exibe mensagem de sucesso
echo "Projeto de testes de integração adicionado à solução com sucesso!"
echo "IMPORTANTE: O arquivo appsettings.Development.json contém informações sensíveis e não será versionado no GitHub."
echo "Certifique-se de que o arquivo .gitignore está configurado corretamente."