CREATE TABLE model (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Make VARCHAR(100) NOT NULL,
    Model VARCHAR(100) NOT NULL
);

INSERT INTO model 
(Make, Model) 
VALUES
('Toyota', 'Corolla'),
('Toyota', 'Camry'),
('Nissan', 'Duke'),
('Nissan', 'Duke'),
('Mazda', 'Mazda 3'),
('Mazda', 'CX5'),
('Toyota', 'Camry'),
('Ford', 'Raptor');


DELETE FROM model
WHERE ID NOT IN (
    SELECT MAX(ID)
    FROM model
    GROUP BY Make, Model
);

