









CREATE PROCEDURE dbo.infRTObtenerNumeroVacasEnControlEstado
	(
		@Establo char(7),
		@FPrevista smalldatetime,
		@NumParto smalldatetime,
		@Estado char(1)
	)
AS

/* INFORME - REBAÑO TOTAL */
/* Procedimiento: ObtenerNumeroVacasEnControl */
/* 99= todos los partos */

if @NumParto=3 
	BEGIN
		IF @Establo<>"" 
			BEGIN
				SELECT COUNT(RES_TIT) AS NumeroVacas FROM Control 
				WHERE RES_TIT = @Establo 
				AND RES_FPREV = @FPrevista
				AND RES_NP>2
				AND RES_AB = @Estado
			END
		ELSE
			BEGIN
				SELECT COUNT(RES_TIT) AS NumeroVacas FROM Control 
				WHERE  
				RES_FPREV >= @FPrevista
				AND RES_FPREV <DATEADD(MONTH,1,@FPrevista)
				AND RES_NP>2
				AND RES_AB = @Estado
			END
		END
IF @NumParto<>99 AND  @NumParto<>3 
	BEGIN
		IF @Establo<>"" 
			BEGIN
				SELECT COUNT(RES_TIT) AS NumeroVacas FROM Control 
				WHERE RES_TIT = @Establo 
				AND RES_FPREV = @FPrevista
				AND RES_NP=@NumParto
				AND RES_AB = @Estado
			END
		ELSE
			BEGIN
				SELECT COUNT(RES_TIT) AS NumeroVacas FROM Control 
				WHERE  RES_FPREV >= @FPrevista
				AND RES_FPREV <DATEADD(MONTH,1,@FPrevista)
				AND RES_NP=@NumParto
				AND RES_AB = @Estado
			END
	END
IF @NumParto=99
	BEGIN
		IF @Establo<>"" 
			BEGIN
				SELECT COUNT(RES_TIT) AS NumeroVacas FROM Control 
				WHERE RES_TIT = @Establo 
				AND RES_FPREV = @FPrevista
				AND RES_AB = @Estado
			END
		ELSE
			BEGIN
				SELECT COUNT(RES_TIT) AS NumeroVacas FROM Control 
				WHERE  RES_FPREV >= @FPrevista
				AND RES_FPREV <DATEADD(MONTH,1,@FPrevista)
				AND RES_AB = @Estado
			END
	END




