
BEGIN TRAN
WAITFOR DELAY '00:00:05'
UPDATE Users SET name='nam1' WHERE uid=1;
COMMIT TRAN