CREATE OR ALTER FUNCTION ValidateType(@type VARCHAR(30))
RETURNS INT AS
  BEGIN
	DECLARE @ok INT
	SET @ok=0
	IF (@type LIKE 'Boeing%') OR (@type LIKE 'Airbus%')
		SET @ok=1
	RETURN  @ok
  END


