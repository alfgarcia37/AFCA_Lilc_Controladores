CREATE PROCEDURE PR_GET_ANIMAL_CRUCE
(
		@strEstablo char(7),
		@strRES CHAR(14)
	)
AS


SELECT  CIB,NOMBRE as det,AÑONAC,MESNAC,DIANAC
FROM RegGen..Cruces_Hembras
Where GANPRO = @strEstablo
AND CIB=@strRES