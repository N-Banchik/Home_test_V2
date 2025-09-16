# Home_test_V2 API

This is a .NET Core 8 REST API that performs arithmetic operations (add, subtract, multiply, divide) on two numbers provided via a POST request. The operation is specified in the `X-Operation` header, and the API requires JWT authorization. The project is containerized using Docker and includes unit tests, XML documentation, and Swagger integration.

## Prerequisites

- **Docker**: Install Docker Desktop or Docker CLI.
- **Postman**: For testing the API with JWT authentication.
- **.NET 8 SDK**: Optional, for local development or testing outside Docker.

## Project Structure

- `Home_test_V1/`: Main API project with controllers, services, and models.
- `Home_test_V1.Tests/`: Unit tests for `MathOpService`.
- `Dockerfile`: Defines the container build process.
- `docker-compose.yml`: Configures the API service.

## Building and Running with Docker

### 1. Build the Docker Image

In the project root directory (where `Dockerfile` and `docker-compose.yml` are located), run:

```bash
docker build -t Home_test_V1 .
```

This builds the Docker image named `Home_test_V1`.

### 2. Run with Docker Compose

Run the container using Docker Compose:

```bash
docker-compose up -d
```

- The `-d` flag runs the container in detached mode.
- The API will be available at `http://localhost:5000/api/calculate`.
- Swagger UI is available at `http://localhost:5000/swagger/index.html`.

To stop the container:

```bash
docker-compose down
```

### 3. Verify the Container

Check that the container is running:

```bash
docker ps
```

You should see a container named `Home_test_V1`.

## Testing the API with Postman

1. **Generate a JWT**:
   - Use a tool like `jwt.io` to create a JWT with:
     - Payload: `{  "iss": "Home_TestAPI", "exp": <Unix timestamp for expiration> }`
     - Example: `{  "iss": "Home_TestAPI", "exp": 1767225599 }` (expires 2025-12-31).
     - Signature: Any secret (not validated by the API).

2. **Send a POST Request**:
   - **URL**: `http://localhost:5000/api/calculate` (use HTTP, not HTTPS)
   - **Method**: POST
   - **Headers**:
     - `Authorization: Bearer <your-jwt>`
     - `X-Operation: add` (or `subtract`, `multiply`, `divide`, `+`, `-`, `*`, `/`)
   - **Body**: `multipart/form-data`
     - `Number1: 10.5`
     - `Number2: 5.2`
   - **Expected Response** (200 OK):
     ```json
     { "Result": 15.7 }
     ```
   - **Error Responses**:
     - 400: Invalid input, operation, or division by zero (e.g., `{ "error": "Invalid operation." }`).
     - 401: Invalid issuer or expired JWT (e.g., `{ "error": "Token expired" }`).

3. **Troubleshooting**:
   - **SSL Error**: If you get an SSL error (e.g., `WRONG_VERSION_NUMBER`), ensure the URL is `http://localhost:5000`, not `https`. Disable SSL verification in Postman settings if needed.
   - **MissingMethodException**: If you see `System.MissingMethodException` in `Microsoft.IdentityModel.JsonWebTokens.dll`, ensure NuGet packages are updated:
     ```bash
     dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.8
     dotnet add package System.IdentityModel.Tokens.Jwt --version 7.0.3
     dotnet add package Microsoft.IdentityModel.Tokens --version 7.0.3
     ```
     Rebuild the Docker image: `docker build -t arithmetic-api --no-cache .`
   - Check container logs: `docker logs <container-id>`.
   - Verify JWT includes `iss: your-issuer` and a valid `exp` (future timestamp).

## Running Unit Tests

Unit tests are located in `Home_test_V1.Tests`. To run tests locally (outside Docker):

```bash
cd Home_test_V1.Tests
dotnet test
```

## Notes

- **Authentication**: The API uses a custom `BearerAuthenticationHandler` (in `IO.Swagger.Security`) to validate JWTs, checking only the issuer (`iss`) and expiration (`exp`) claims.
- **Swagger**: Access Swagger UI at `[http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)` for API documentation.
- **XML Documentation**: All public types and members include XML comments, integrated with Swagger.
- **Port**: The API runs on port 5000. Update `docker-compose.yml` if a different port is needed.
