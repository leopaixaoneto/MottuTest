ECHO OFF
set NL=^^^%NLM%%NLM%^%NLM%%NLM%

echo:
echo Iniciando Docker Compose

cd ..
docker-compose up --force-recreate 