CREATE OR ALTER PROCEDURE AddData @type VARCHAR(30), @company VARCHAR(30), @name VARCHAR(30) AS
BEGIN
DECLARE @planeId INT, @techId INT;
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
			IF (dbo.ValidateName(@name)<>1)
				BEGIN
					RAISERROR('Name can not be null or empty!',14,1);
				END
			INSERT Technicians VALUES (@name);
			SET @planeId=IDENT_CURRENT('Planes');
			SET @techId=IDENT_CURRENT('Technicians');
			INSERT PlanesTechnicians VALUES (@planeId,@techId);
			COMMIT TRAN
			PRINT 'Transaction committed'
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			PRINT 'Transaction rollbacked'
		END CATCH
END