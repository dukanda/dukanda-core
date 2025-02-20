#!/bin/bash

# Valida o número de argumentos
if [ "$#" -lt 1 ]; then
  echo "Uso: $0 <NomeDoRecurso> [NomeDoPlural]"
  echo "Exemplo: $0 TodoList TodoLists"
  exit 1
fi

# Nome do recurso passado como argumento
RESOURCE=$1
FEATURE_NAME=${2:-"${RESOURCE}s"}  # Usa o segundo argumento como plural ou adiciona "s" por padrão

# Verifica se o comando dotnet existe
if ! command -v dotnet &> /dev/null; then
  echo "Erro: O comando 'dotnet' não foi encontrado. Certifique-se de que o .NET SDK está instalado."
  exit 1
fi

# Verifica se está na pasta src/Application ou muda para lá
if [ ! -d "src/Application" ]; then
  echo "A pasta src/Application não foi encontrada no diretório atual."
  echo "Certifique-se de estar na raiz do projeto antes de rodar o script."
  exit 1
else
  cd src/Application || exit 1
fi

# Função para criar um use case
create_usecase() {
  local NAME=$1
  local USECASE_TYPE=$2
  local RETURN_TYPE=$3

  if [ -d "$NAME" ]; then
    echo "Aviso: O use case '$NAME' já existe. Pulando criação."
  else
    echo "Gerando use case: $NAME"
    dotnet new ca-usecase --name "$NAME" --feature-name "$FEATURE_NAME" --usecase-type "$USECASE_TYPE" --return-type "$RETURN_TYPE"
  fi
}

echo "Iniciando a criação dos use cases para a feature '$FEATURE_NAME'..."

# Operações CRUD + GetList
create_usecase "Create${RESOURCE}" command int
create_usecase "Get${RESOURCE}ById" query "${RESOURCE}Vm"
create_usecase "Update${RESOURCE}" command void
create_usecase "Delete${RESOURCE}" command void
create_usecase "Get${FEATURE_NAME}" query "IEnumerable<${RESOURCE}Vm>"

echo "Todos os use cases para '$FEATURE_NAME' foram criados com sucesso!"
