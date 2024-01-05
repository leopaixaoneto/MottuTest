ECHO OFF
set NL=^^^%NLM%%NLM%^%NLM%%NLM%

echo:
echo Aplicando migrations ao Banco de dados da API principal

cd ../MottuApi/

dotnet ef database update --connection "User ID=postgres;Password=postgres;Port=5432;Server=localhost;Database=mottutest-db;IntegratedSecurity=true;Pooling=true"

cd ../MottuKGS/

echo:
echo Aplicando migrations ao Banco de dados do KGS

dotnet ef database update --connection "User ID=postgres;Password=postgres;Port=5433;Server=localhost;Database=mottukgs-db;IntegratedSecurity=true;Pooling=true"

cd ../MottuAnalytics/

echo:
echo Aplicando migrations ao Banco de dados da API de Analytics

dotnet ef database update --connection "User ID=postgres;Password=postgres;Port=5434;Server=localhost;Database=mottuanalytics-db;IntegratedSecurity=true;Pooling=true"

echo:
PAUSE