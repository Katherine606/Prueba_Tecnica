# Sistema de Gestion de Salas

Aplicacion full-stack desarrollada con .NET 9 (Backend) y React + Vite + TypeScript (Frontend) para la gestion y reserva de salas de reuniones.

## Tecnologias Utilizadas

### Backend
* .NET 9 (Web API)
* Entity Framework Core (SQL Server)
* Autenticacion JWT (JSON Web Tokens)
* BCrypt.Net-Next (Hashing de contraseñas)

### Frontend
* React con TypeScript
* Vite
* React Router DOM
* Axios (con interceptores para token Bearer)
* Bootstrap

## Estructura del Proyecto

* `Prueba_Tecnica/` - API Backend
  * `Controllers/`: Endpoints de autenticacion (`AuthController`), salas (`RoomsController`) y usuarios (`UserController`).
  * `Services/`: Logica de negocio (Autenticacion, manejo de usuarios y salas).
  * `Repositories/`: Acceso a datos con EF Core.
* `front/` - Aplicacion Frontend
  * `src/components/`: Vistas de `Login` y `TablaSalas`.
  * `src/services/`: Configuracion de Axios (`api.ts`) y llamadas al backend.

## Levantamiento

### 1. Backend (.NET 9)
* Configura tu cadena de conexion a SQL Server y los parametros JWT en el archivo `appsettings.json`.
* Ejecuta la API mediante la terminal:

```bash
dotnet run
```
### 2. Frontend (React)

Entra a la carpeta del frontend e instala las dependencias:

```bash
cd front
npm install
```

### 3.Inicia el servidor de desarrollo:

```Bash
npm run dev
```
