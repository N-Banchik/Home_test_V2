# Home_test_V1 - ASP.NET Core 2.0 Server

API for performing Mathematical operations on two numbers via POST, with operation specified in HTTP header and JWT authorization.

## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```

## Run in Docker

```
cd src/Home_test_V1
docker build -t home_test_v1 .
docker run -p 5000:5000 home_test_v1
```
