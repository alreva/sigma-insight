Useful commands:

```bash

find . -type f \( -name "*.cs" -o -name "*.json" -o -name "*.razor" -o -name "*.csproj" -o -name "*.sln" -o -name "*.css" \) ! -name "*.min.css" -exec cat {} + > all_code.txt

cd src/IaC/

COMMIT_HASH=$(git rev-parse --short HEAD)
docker build -t localhost:5000/sigma/sigma-insight-web:0.1-$COMMIT_HASH ../../src/SigmaInsight.Web/.
docker push localhost:5000/sigma/sigma-insight-web:0.1-$COMMIT_HASH

docker build -t localhost:5000/sigma/sigma-insight-api:0.1-$COMMIT_HASH ../../src/SigmaInsight.Api/.
docker push localhost:5000/sigma/sigma-insight-api:0.1-$COMMIT_HASH


docker build -t localhost:5000/sigma/sigma-insight-web:latest ../../src/SigmaInsight.Web/.
docker push localhost:5000/sigma/sigma-insight-web:latest
docker pull localhost:5000/sigma/sigma-insight-web:latest

docker build -t localhost:5000/sigma/sigma-insight-api:latest ../../src/SigmaInsight.Api/.
docker push localhost:5000/sigma/sigma-insight-api:latest
docker pull localhost:5000/sigma/sigma-insight-api:latest


pulumi up --yes
pulumi destroy --yes

docker rm $(docker ps --filter status=exited -q)
docker rm $(docker ps --filter status=created -q)

pulumi destroy --yes
pulumi up --yes

docker stop sigma-web
docker rm sigma-web
docker image rm localhost:5000/sigma/sigma-insight-web:latest

docker build -t localhost:5000/sigma/sigma-insight-web:latest ../../src/SigmaInsight.Web/.
docker push localhost:5000/sigma/sigma-insight-web:latest
docker pull localhost:5000/sigma/sigma-insight-web:latest

docker run -d -p 8081:80 --name sigma-web localhost:5000/sigma/sigma-insight-api:latest

docker stop sigma-api
docker rm sigma-api
docker image rm localhost:5000/sigma/sigma-insight-api:latest

docker build -t localhost:5000/sigma/sigma-insight-api:latest ../../src/SigmaInsight.Api/.
docker push localhost:5000/sigma/sigma-insight-api:latest
docker pull localhost:5000/sigma/sigma-insight-api:latest

docker run -d -p 8081:80 --name sigma-web localhost:5000/sigma/sigma-insight-api:latest

```