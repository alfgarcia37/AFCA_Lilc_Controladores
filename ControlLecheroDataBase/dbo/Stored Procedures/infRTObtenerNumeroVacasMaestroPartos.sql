






CREATE PROCEDURE dbo.infRTObtenerNumeroVacasMaestroPartos
	(
		@Establo char(7),
		@NumParto int
	)
AS

/* INFORME - REBAÑO TOTAL */
/* Procedimiento: ObtenerNumeroVacasMAESTRO-PARTOS*/
if @NumParto=3 
BEGIN
	SELECT COUNT(*) AS NumeroVacas FROM Maestro,Partos 
	WHERE MAES_TIT = @Establo 
	AND MAES_GEN=ID
	AND NP >2
END
IF @NumParto<>99 AND  @NumParto<>3 
BEGIN
	SELECT COUNT(*) AS NumeroVacas FROM Maestro,Partos 
	WHERE MAES_TIT = @Establo 
	AND MAES_GEN=ID
	AND NP =@numparto
END

IF @NumParto=99
BEGIN
	SELECT COUNT(*) AS NumeroVacas FROM Maestro,Partos 
	WHERE MAES_TIT = @Establo 
	AND MAES_GEN=ID
END






