CREATE FUNCTION ValidateCompany(@company VARCHAR(30))
RETURNS INT AS
  BEGIN
	DECLARE @ok INT
	SET @ok=0
	IF (@company IN ('Lufthansa', 'Emirates', 'Qantas'))
		SET @ok=1
	RETURN  @ok
  END

