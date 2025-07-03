DELETE FROM EducationalContents; -- Limpia tabla si quieres reiniciar

-- Reiniciar identidad (SQL Server)
DBCC CHECKIDENT ('EducationalContents', RESEED, 0);


INSERT INTO EducationalContents (Title, Type, Description, Url, CategoryId) VALUES
('Gu�a para manejar la ansiedad', 'Art�culo', 
 'Este art�culo proporciona t�cnicas pr�cticas para manejar la ansiedad en la vida diaria.', 
 'https://www.mentalhealth.org.uk/a-to-z/a/anxiety', 1),

('T�cnicas para superar la depresi�n', 'Art�culo', 
 'Descripci�n de m�todos efectivos para el tratamiento y manejo de la depresi�n.', 
 'https://www.nimh.nih.gov/health/topics/depression', 2),

('C�mo reducir el estr�s diario', 'Art�culo', 
 'Estrategias para identificar y reducir el estr�s cotidiano.', 
 'https://www.helpguide.org/articles/stress/stress-management.htm', 3),

('Ejercicios de mindfulness', 'Video', 
 'Tutorial con ejercicios pr�cticos de mindfulness para la concentraci�n y relajaci�n.', 
 'https://www.mindful.org/mindfulness-how-to-do-it/', 7),

('Estrategias para mejorar la autoestima', 'Art�culo', 
 'Gu�a para aumentar la autoestima y promover pensamientos positivos.', 
 'https://www.psychologytoday.com/us/basics/self-esteem', 4),

('Reconociendo y tratando el burnout', 'Art�culo', 
 'Informaci�n sobre el s�ndrome de burnout y c�mo prevenirlo en el trabajo.', 
 'https://www.apa.org/topics/burnout', 15);
