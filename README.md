# Basic Authentication API
This repository contains a basic authentication API implemented in ASP.NET Core. The API provides endpoints for user authentication and registration, as well as user management functionalities.

## Getting Started
To get started with this API, follow these steps:

Clone the repository to your local machine:

```

git clone git@github.com:R-uan/BasicAuthenticationAPI.git

```
Open the project in your preferred IDE or text editor.

Build and run the project using the .NET CLI or your IDE's built-in tools.

# Endpoints
## Authentication
### Endpoint: /auth (GET)

Description: Tries to authenticate a user based on the Basic Authorization header.


Returns:

- 200 (Ok) with a JWT token if authentication is successful.
- 400 (Bad Request) if no authorization header is found.
- 500 (Internal Server Error) with error message in case of an unexpected error.

### Endpoint: /auth (POST)

Description: Validates the data received from the request body and attempts to register a new user.

Body: JSON object representing the user to be registered.

Returns:

- 200 (Ok) with the saved user entity if successful.
- 500 (Internal Server Error) if unable to register.

### Endpoint: /enduser/{username} (GET)

Description: Finds the user entity associated with the provided username.

Parameters: username - The username of the user to be retrieved.

Returns:

- 200 (Ok) with the associated user entity.
- 404 (Not Found) if no user is found with the provided username.
- 500 (Internal Server Error) with the error message in case of an unexpected error.

### Endpoint: /enduser (GET)

Description: Lists all registered users.

Returns:

- 200 (Ok) with a list of user entities.
- 500 (Internal Server Error) with the error message in case of an unexpected error.
- Additional Components
- 
## JWT Helper Class

The API includes a JWTHelper class responsible for generating JSON Web Tokens (JWTs) for user authentication. This class utilizes the configured JWT settings to create secure tokens based on user credentials.

## Logging and Configuration
During startup, the application logs the current environment and the configured JWT settings for debugging and informational purposes. Additionally, the JWT settings are retrieved from the application configuration, and an exception is thrown if they are missing.

## Dependency Injection
The application utilizes ASP.NET Core's built-in dependency injection container to configure and manage various services and components. Notable services include validators, repositories, services related to end users, authentication, and the JWT helper.

## Authentication and Authorization
Authentication and authorization are configured using JWT bearer authentication scheme, ensuring secure access to protected endpoints. JWT tokens are validated based on the configured issuer and secret key, providing a robust security mechanism.
