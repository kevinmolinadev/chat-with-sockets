# Chat Peer-to-Peer con Sockets en C#

Este proyecto implementa un chat utilizando sockets en C# con una arquitectura **Peer-to-Peer (P2P)**, eliminando la necesidad de un servidor central.

## Funcionalidades

### Comunicación Directa entre Clientes
Cada cliente actúa tanto como **emisor** como **receptor**, estableciendo su propia conexión y escuchando en un puerto específico para recibir mensajes.

### Detección de Usuarios en la Red
Para descubrir qué clientes están activos en la red, se utiliza un mecanismo de **broadcast** con UDP:
- Cada cliente envía un **mensaje de presencia** en la red cada **2.5 segundos**.
- Al mismo tiempo, cada cliente está **escuchando** estos mensajes para detectar nuevos usuarios.
- Si un cliente recibe un mensaje de otro cliente anunciando su presencia, lo **agrega a su lista de contactos** automáticamente.

### Envío y Recepción de Mensajes
- Cada cliente inicia un **listener** en un puerto determinado para recibir mensajes directos de otros clientes.
- Para enviar un mensaje, un cliente simplemente se comunica con la IP y puerto del destinatario.

## Requisitos
- .NET Framework (.Net 9)
- Conexión de red que permita el envío y recepción de paquetes UDP y TCP.

## Instalación y Ejecución
1. **Ejecutar la aplicación en cada cliente**. No se requiere un servidor.
2. **Configurar el puerto de escucha** en cada cliente para recibir mensajes.
3. **La detección de usuarios es automática** gracias al mecanismo de broadcast UDP.
4. **Enviar mensajes a cualquier usuario** de la lista de contactos detectados.

