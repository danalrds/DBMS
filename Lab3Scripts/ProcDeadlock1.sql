CREATE OR ALTER PROCEDURE DeadlockT1 AS
BEGIN
begin tran
update Planes set company='FlyDubai' where id>25
-- this transaction has exclusively lock on table Planes
waitfor delay '00:00:10'
update Technicians set name='Hanna' where id>10
-- this transaction will be blocked because transaction 2 has already blocked our lock on table Technicians
-- so, transaction 1 is blocked on an exclusively block on table technicians
commit tran
END