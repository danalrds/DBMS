SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRAN
SELECT * FROM Classes
WAITFOR DELAY '00:00:05'
SELECT * FROM Classes
COMMIT TRAN