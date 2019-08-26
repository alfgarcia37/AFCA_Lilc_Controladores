






CREATE PROCEDURE dbo.infRTObtenerNumeroVacasMaestro
	(
		@Establo char(7),
		@NumParto int
	)
AS

IF @Establo<>''
BEGIN
	IF @NumParto=3 
	BEGIN
		SELECT COUNT(*) AS NumeroVacas FROM Maestro
		WHERE MAES_TIT = @Establo 
		AND MAES_NP >2
		AND MAES_ESTADO<>'0'
	END
	IF @NumParto<>99 AND  @NumParto<>3 
	BEGIN
		SELECT COUNT(*) AS NumeroVacas FROM Maestro
		WHERE MAES_TIT = @Establo 
		AND MAES_NP =@numparto
		AND MAES_ESTADO<>'0'
	END
	
	IF @NumParto=99
	BEGIN
		SELECT COUNT(*) AS NumeroVacas FROM Maestro
		WHERE MAES_TIT = @Establo 
		AND MAES_ESTADO<>'0'
	END
END
ELSE
BEGIN
	IF @NumParto=3 
	BEGIN
		SELECT COUNT(*) AS NumeroVacas FROM Maestro
		WHERE 
		MAES_NP >2
		AND MAES_ESTADO<>'0'
	END
	IF @NumParto<>99 AND  @NumParto<>3 
	BEGIN
		SELECT COUNT(*) AS NumeroVacas FROM Maestro
		WHERE 
		MAES_NP =@numparto
		AND MAES_ESTADO<>'0'
	END
	
	IF @NumParto=99
	BEGIN
		SELECT COUNT(*) AS NumeroVacas FROM Maestro
		WHERE 
		MAES_ESTADO<>'0'
	END
END





