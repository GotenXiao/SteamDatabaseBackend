# Running SteamDatabaseBackend in Docker
First, you will need to clone this repository to your local environment:

`$ git clone https://github.com/SteamDatabase/SteamDatabaseBackend`

Then run the following to build an image:

`$ docker build -t steamdb-backend .`

Once that's done, you need a MySQL/MariaDB database configured for use by this
image. The easiest way to do that is by using [docker-compose](https://docs.docker.com/compose/),
with a `docker-compose.yml` like the following:

```yaml
version: "3"

services:
  backend:
    image: steamdb-backend
    volumes:
      - "./settings.json:/app/settings.json:ro"
      - "./files:/app/files"
    networks:
      - frontend
      - backend
    depends_on:
      - db

  db:
    image: mariadb
    environment:
      MYSQL_ROOT_PASSWORD: "replacemereplacemereplaceme"
      MYSQL_DATABASE: "steamcachedb"
      MYSQL_USER: "steamcachedb"
      MYSQL_PASSWORD: "replacemereplacemereplaceme"
    volumes:
      - "./db:/var/lib/mysql"
      - "./Database.sql:/docker-entrypoint-initdb.d/init.sql"
    networks:
      - backend

networks:
  frontend:
  backend:
    internal: true
```

This compose file provides two networks, one with internet access, and the
other isolated to prevent access to the database server. If you have any
child services that require access to the database, they should be attached to
the backend network as well.

You will need to download the [database schema file](https://github.com/SteamDatabase/steamdb.info/blob/master/Database.sql)
from the main repository and place it alongside the `docker-compose.yml` file
to initialise the database. One thing to be aware of when first starting the
stack is that the database will take some time to setup on first run, so you
will need to restart the backend after the database is ready. Alternatively,
when starting for the first time, invoke `docker-compose` as follows:

```
$ docker-compose up -d db
$ sleep 2m
$ docker-compose up -d backend
```
