# Electronic-Information-Security-Technology-Assignment

# Requirement

- .NET 7.0
- docker

<img src="/readme_asset/image/powered.png" style="margin:auto; display:block;" />

`This App runed by docker container by nginx reverse proxy`

## start up

```bash
docker-compose up -d
```

## shutdown
```bash
docker-compose down
```

## Security

### SSL

if you want to apply ssl in this application, remove comments in docker-compose.yml
```yml
  certbot:
    container_name: certbot
    image: certbot/certbot:latest
    restart: unless-stopped
    volumes:
      - ./data/certbot/conf:/etc/letsencrypt
      - ./data/certbot/www:/var/www/certbot
    depends_on:
      - nginx
   entrypoint: "/bin/sh -c 'trap exit TERM; while :; do certbot renew; sleep 12h & wait $${!}; done;'"

```
then, edit config in `init-letsencrypt.sh`.

and excute `init-letsencrypt.sh`.

### database
It powered by `EntityFrameWorkCore`.
That Provides SQL Injection defense.

and I Apply AES Encryption In `mysql connection strings` in `application.json`

```json
"connectionStrings": {
    "mysqlconnection": "QW+I7n1OGO6SmTglQs096dhn1Lv4PHVzxgG1GxxxZpvEjx+7cw4wKrmV+FMgifeNt8qZcw6JjJ9Ktuu23Nj41FVws9sC4Wvus/pS2Mu3EAE=",
    "sqliteConnection": "Data Source=.\\sqlites\\LocalDatabase.db",
    "devMysql": "Server=localhost;Port=32912;Database=neko_hat;Uid=neko_hat;Pwd=neko_hat;"
  },
```

## Sample

![image]("/readme_asset/image/docker_test.png")
