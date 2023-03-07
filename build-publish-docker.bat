docker build -t gos-web -f src/Gos.Web/Dockerfile .
docker build -t gos-console -f src/Gos.Console/Dockerfile .

docker login ghcr.io

docker tag gos-web ghcr.io/clarinsi/gos-web:latest
docker tag gos-web ghcr.io/clarinsi/gos-web:v1.0.5
docker tag gos-web ghcr.io/clarinsi/gos-web:v1.0
docker tag gos-web ghcr.io/clarinsi/gos-web:v1

docker tag gos-console ghcr.io/clarinsi/gos-console:latest
docker tag gos-console ghcr.io/clarinsi/gos-console:v1.0.5
docker tag gos-console ghcr.io/clarinsi/gos-console:v1.0
docker tag gos-console ghcr.io/clarinsi/gos-console:v1

docker push --all-tags ghcr.io/clarinsi/gos-web
docker push --all-tags ghcr.io/clarinsi/gos-console