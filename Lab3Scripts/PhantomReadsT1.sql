BEGIN TRAN
WAITFOR DELAY '00:00:04'
INSERT Planes VALUES ('Boeing','Lufthansa')
COMMIT TRAN