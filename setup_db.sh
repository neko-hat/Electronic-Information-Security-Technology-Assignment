docker-compose -f ./DevDB/docker-compose.yml up -d
mysql -h localhost -P 32912 -u neko_hat -p neko_hat --database neko_hat < ./neko_hat.sql
docker-compose -f ./DevDB/docker-compose.yml down