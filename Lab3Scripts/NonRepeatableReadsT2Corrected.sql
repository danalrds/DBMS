SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
SELECT * FROM Planes
WAITFOR DELAY '00:00:05'
SELECT * FROM Planes
COMMIT TRAN