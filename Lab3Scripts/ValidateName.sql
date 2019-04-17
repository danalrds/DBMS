CREATE OR ALTER FUNCTION ValidateName(@name VARCHAR(30))
RETURNS INT AS
  BEGIN
	DECLARE @ok INT
	SET @ok=1
	IF (@name IS NULL) OR (@name NOT LIKE '_%')
		SET @ok=0
	RETURN  @ok
  END
