# Proyecto Backend - BlogApp

Este es el proyecto backend de BlogApp, desarrollado en .NET Core.

## Requisitos

- .NET SDK (versión 7)
- SQL Server o una base de datos compatible

## Instalación

1. Clona el repositorio:

`git clone <URL_DEL_REPOSITORIO>`

2. Navega a la carpeta del proyecto:

`cd BlogApp`

3. Restaura las dependencias:

`dotnet restore`

5. Ejecuta las migraciones para configurar la base de datos:

`dotnet ef database update`

5. Inicia la aplicación:

`dotnet run`

## Uso

- La API estará disponible en `http://localhost:7143` por defecto.
- Asegúrate de que la base de datos esté configurada correctamente antes de ejecutar la aplicación.

## Pruebas

Para ejecutar las pruebas, utiliza el siguiente comando:
dotnet test
