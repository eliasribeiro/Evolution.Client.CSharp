#!/bin/bash

# Verifica se o arquivo de configuração de desenvolvimento existe
if [ ! -f "tests/Evolution.Client.CSharp.IntegrationTests/appsettings.Development.json" ]; then
    echo "ERRO: O arquivo appsettings.Development.json não foi encontrado."
    echo "Por favor, crie o arquivo com as configurações corretas conforme a documentação."
    exit 1
fi

# Verifica se o projeto está na solução
if ! dotnet sln list | grep -q "Evolution.Client.CSharp.IntegrationTests"; then
    echo "Adicionando o projeto de testes de integração à solução..."
    dotnet sln add tests/Evolution.Client.CSharp.IntegrationTests/Evolution.Client.CSharp.IntegrationTests.csproj
fi

# Restaura os pacotes
echo "Restaurando pacotes..."
dotnet restore tests/Evolution.Client.CSharp.IntegrationTests/Evolution.Client.CSharp.IntegrationTests.csproj

# Executa os testes de integração
echo "Executando testes de integração..."
dotnet test tests/Evolution.Client.CSharp.IntegrationTests/Evolution.Client.CSharp.IntegrationTests.csproj -v n

# Verifica o resultado
if [ $? -eq 0 ]; then
    echo "\n✅ Testes de integração executados com sucesso!"
else
    echo "\n❌ Falha na execução dos testes de integração."
    echo "Verifique as configurações no arquivo appsettings.Development.json."
fi