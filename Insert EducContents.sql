DELETE FROM EducationalContents; -- Limpia tabla si quieres reiniciar

-- Reiniciar identidad (SQL Server)
DBCC CHECKIDENT ('EducationalContents', RESEED, 0);


INSERT INTO EducationalContents (Title, Type, Description, Url, CategoryId) VALUES
('Guía para manejar la ansiedad', 'Artículo', 
 'Este artículo proporciona técnicas prácticas para manejar la ansiedad en la vida diaria.', 
 'https://www.mentalhealth.org.uk/a-to-z/a/anxiety', 1),

('Técnicas para superar la depresión', 'Artículo', 
 'Descripción de métodos efectivos para el tratamiento y manejo de la depresión.', 
 'https://www.nimh.nih.gov/health/topics/depression', 2),

('Cómo reducir el estrés diario', 'Artículo', 
 'Estrategias para identificar y reducir el estrés cotidiano.', 
 'https://www.helpguide.org/articles/stress/stress-management.htm', 3),

('Ejercicios de mindfulness', 'Video', 
 'Tutorial con ejercicios prácticos de mindfulness para la concentración y relajación.', 
 'https://www.mindful.org/mindfulness-how-to-do-it/', 7),

('Estrategias para mejorar la autoestima', 'Artículo', 
 'Guía para aumentar la autoestima y promover pensamientos positivos.', 
 'https://www.psychologytoday.com/us/basics/self-esteem', 4),

('Reconociendo y tratando el burnout', 'Artículo', 
 'Información sobre el síndrome de burnout y cómo prevenirlo en el trabajo.', 
 'https://www.apa.org/topics/burnout', 15);
