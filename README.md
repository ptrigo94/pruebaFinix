# PruebaFinix
Estimado lector, espero que tenga un excelente día.
Este proyecto está escrito en .netCore 7 en formato API, usando clean architecture:
La solución se divide en 5 proyectos:
FinixBanks: el proyecto princial, donde estan los controladores y la API.
FinixBanks.BL: es donde se ejecuta la lógica del sistema.
FinixBanks.Core: donde se encuentra las clases de modelado del sistema .
FinixBanks.Infrastructure: el cual se tenia pensado como herramientas de utilidad.
FinixBanks.Persistence: que es donde se guardan las migraciones y el dbContext.

Cada una de estas capas es dependiente de las demás lo que garantiza una mejor consistencia, orden y entendimiento del proyecto
Se usó el patron MediaTr para simplificar la logica de negocio, mejorar la escalabilidad, etc.

Cada endpoint de la api trabaja con metodos y codigos de respuesta http.
