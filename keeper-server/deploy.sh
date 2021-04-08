dotnet publish -c Release
docker build -t keeper-server ./
docker tag keeper-server registry.heroku.com/keeper-final/web
docker push registry.heroku.com/keeper-final/web
heroku container:release web -a keeper-final
echo press any key
read end
