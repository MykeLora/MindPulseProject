-- Borrar datos relacionados
DELETE FROM Recommendations;
DELETE FROM EducationalContents;

-- Borrar categorías
DELETE FROM Categories;

-- Reiniciar identidad (SQL Server)
DBCC CHECKIDENT ('Categories', RESEED, 0);

-- Insertar nuevas categorías
INSERT INTO Categories (Name, Description) VALUES
('Ansiedad', 'Estado de preocupación o miedo excesivo'),
('Depresión', 'Estado de tristeza profunda y persistente'),
('Estrés', 'Respuesta física y emocional a situaciones demandantes'),
('Autoestima alta', 'Valoración positiva y confianza en uno mismo'),
('Autoestima baja', 'Falta de confianza y valoración negativa de uno mismo'),
('Trastorno Bipolar', 'Cambios extremos en el estado de ánimo'),
('Mindfulness', 'Conciencia plena y atención al momento presente'),
('Ansiedad Social', 'Miedo intenso a situaciones sociales'),
('Resiliencia', 'Capacidad de recuperarse ante la adversidad'),
('Fobia', 'Miedo irracional a objetos o situaciones específicas'),
('Trastorno Obsesivo-Compulsivo', 'Pensamientos obsesivos y comportamientos compulsivos'),
('Optimismo', 'Tendencia a esperar resultados positivos'),
('Pérdida', 'Experiencia de duelo o tristeza por la ausencia'),
('Motivación', 'Impulso para realizar acciones hacia una meta'),
('Burnout', 'Agotamiento emocional por estrés laboral prolongado');
