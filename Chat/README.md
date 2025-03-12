# Chat con Sockets en C#

Este proyecto implementa un chat utilizando sockets en C# con una arquitectura cliente-servidor.

## Funcionalidades

### Servidor
El servidor es el encargado de gestionar las solicitudes de los clientes. Sus principales funciones incluyen:
- Verificar si un usuario es nuevo o si ya está registrado.
- Gestionar el envío de mensajes entre usuarios.
- Proporcionar la lista de usuarios conectados.

### Cliente
El cliente tiene una interfaz creada con Windows Forms que permite a los usuarios interactuar con el chat. Sus funcionalidades incluyen:
- Pantalla inicial donde el usuario debe ingresar:
  - Su nombre.
  - La dirección IP y el puerto del servidor al que se conectará.
- Interfaz principal para:
  - Enviar mensajes a otros usuarios.
  - Recibir mensajes en tiempo real.
  - Ver la lista de usuarios conectados.

## Requisitos
- .NET Framework (.Net 9)
- Conexión de red para la comunicación entre cliente y servidor.

## Instalación y Ejecución
1. **Servidor:** Ejecutar la aplicación del servidor para iniciar la gestión de conexiones.
2. **Cliente:** Abrir la aplicación cliente e ingresar los datos requeridos para conectarse al servidor.

