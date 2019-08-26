









CREATE PROCEDURE dbo.infRTObtenerControlesDIM
	(
		@Establo char(7),
		@FPrevista smalldatetime
	)
AS

/* INFORME - REBAÑO TOTAL */
/* Procedimiento: OBtenerControlesDetalle */
SELECT  AVG(DATEDIFF(day, RES_FP, RES_FCTR))
FROM Control 
WHERE RES_TIT = @Establo AND RES_FPREV = @FPrevista and res_ab='A'









