#!/bin/sh

# Ler a versão do terminal
BUILD_VERSION="$1"

echo "Building Docker image for Dukanda-Core"

docker image build -t evandrend/dukanda-core:"$BUILD_VERSION" -f src/Web/Dockerfile .\
  && docker image push evandrend/dukanda-core:"$BUILD_VERSION"