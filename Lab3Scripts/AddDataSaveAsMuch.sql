CREATE OR ALTER PROCEDURE AddDataSeparate @type VARCHAR(30), @company VARCHAR(30), @name VARCHAR(30) AS
BEGIN
DECLARE @planeId INT, @techId INT, @count1 INT, @count2 INT;
	BEGIN TRAN
		BEGIN TRY
			IF (dbo.ValidateCompany(@company)<>1)
				BEGIN
					RAISERROR('Company must be Lufthansa, Emirates or Qantas',14,1);
				END
			IF (dbo.ValidateType(@type)<>1)
				BEGIN
					RAISERROR('Type of the aircraft must be Airbus% or Boeing%',14,1);
				END
			INSERT Planes VALUES (@type, @company);			
			COMMIT TRAN
			PRINT 'Transaction Plane committed!'
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			PRINT 'Transaction Plane rollbacked'
		END CATCH
	BEGIN TRAN
		BEGIN TRY
			IF (dbo.ValidateName(@name)<>1)
				BEGIN
					RAISERROR('Name can not be null or empty!',14,1);
				END
			INSERT Technicians VALUES (@name);
			COMMIT TRAN
			PRINT 'Transaction Technician committed!'			
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			PRINT 'Transaction Technician rollbacked'
		END CATCH
	BEGIN TRAN
		BEGIN TRY
			SET @planeId=(SELECT id FROM Planes WHERE type=@type AND company=@company);
			SET @techId=(SELECT id FROM Technicians WHERE name=@name);
			SET @count1=(SELECT COUNT(*) FROM Planes WHERE type=@type AND company=@company);
			SET @count2=(SELECT COUNT(*) FROM Technicians WHERE name=@name);
			PRINT @count1;
			PRINT @count2;
			IF (@count1>0) AND (@count2>0)  
				BEGIN
					INSERT PlanesTechnicians VALUES (@planeId,@techId);
					COMMIT TRAN
					PRINT 'Transaction PlanesTechnicians committed'
				END
				ELSE
				BEGIN
					PRINT 'dsgvbhgjhj'
					RAISERROR('Plane or technician was not added!',14,1);
				END
			
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			PRINT 'Transaction Planestechnicians rollbacked'
		END CATCH
END