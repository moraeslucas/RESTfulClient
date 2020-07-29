@ECHO OFF

ECHO Initializing the RestfulClient app...
start "" https://localhost:5001/api/Client/GetAll
ECHO (The web page may take some time to refresh and run properly)
ECHO -------------------------------------------------------------

cd "%CD%"\RESTfulClient
dotnet run