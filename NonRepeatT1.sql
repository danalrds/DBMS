
BEGIN TRAN
WAITFOR DELAY '00:00:05'
UPDATE Projects SET name='PROJECT' WHERE pid<10
COMMIT TRAN