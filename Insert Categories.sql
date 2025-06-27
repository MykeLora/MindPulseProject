-- Borrar datos relacionados
DELETE FROM Recommendations;
DELETE FROM EducationalContents;

-- Borrar categor�as
DELETE FROM Categories;

-- Reiniciar identidad (SQL Server)
DBCC CHECKIDENT ('Categories', RESEED, 0);

-- Insertar nuevas categor�as
INSERT INTO Categories (Name, Description) VALUES
('Ansiedad', 'Estado de preocupaci�n o miedo excesivo'),
('Depresi�n', 'Estado de tristeza profunda y persistente'),
('Estr�s', 'Respuesta f�sica y emocional a situaciones demandantes'),
('Autoestima alta', 'Valoraci�n positiva y confianza en uno mismo'),
('Autoestima baja', 'Falta de confianza y valoraci�n negativa de uno mismo'),
('Trastorno Bipolar', 'Cambios extremos en el estado de �nimo'),
('Mindfulness', 'Conciencia plena y atenci�n al momento presente'),
('Ansiedad Social', 'Miedo intenso a situaciones sociales'),
('Resiliencia', 'Capacidad de recuperarse ante la adversidad'),
('Fobia', 'Miedo irracional a objetos o situaciones espec�ficas'),
('Trastorno Obsesivo-Compulsivo', 'Pensamientos obsesivos y comportamientos compulsivos'),
('Optimismo', 'Tendencia a esperar resultados positivos'),
('P�rdida', 'Experiencia de duelo o tristeza por la ausencia'),
('Motivaci�n', 'Impulso para realizar acciones hacia una meta'),
('Burnout', 'Agotamiento emocional por estr�s laboral prolongado');
