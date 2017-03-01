FROM mono:4

RUN \
	apt-get update \
	&& apt-get install -y git \
	&& rm -rf /var/cache/apt/* /var/lib/apt/lists/* \
	&& mkdir -p /app/src/

COPY . /app/src/

RUN \
	/app/src/build.sh \
	&& mv /app/src/bin/Release/ /app-2/ \
	&& rm -rf /app \
	&& mv /app-2 /app

CMD ["bash", "-c", "sleep 5; mono /app/SteamDatabaseBackend.exe"]
