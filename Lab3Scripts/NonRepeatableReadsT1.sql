UPDATE Planes
SET company='Emirates'; 

INSERT Planes VALUES ('Airbus570','Lufthansa')


BEGIN TRAN
WAITFOR DELAY '00:00:05'
UPDATE Planes SET company='Qantas' WHERE type  LIKE 'Airbus%'
COMMIT TRAN