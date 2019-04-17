SET DEADLOCK_PRIORITY HIGH
begin tran
update Technicians set name='Hanna T2' where id>10
-- this transaction has exclusively lock on table Technicians
waitfor delay '00:00:10'
update Planes set company='FlyDubaiT2' where id>25
-- this transaction will be blocked because transaction 1 has already blocked our lock on table Planes, so, both of the transactions are blocked
commit tran