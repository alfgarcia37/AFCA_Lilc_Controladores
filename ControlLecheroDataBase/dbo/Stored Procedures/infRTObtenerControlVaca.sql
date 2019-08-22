









CREATE PROCEDURE dbo.infRTObtenerControlVaca
	(
		@Establo char(7),
		@NP int,
		@NC int
	)
AS

/* INFORME - REBAÑO TOTAL */
/* Procedimiento: OBtenerControlesDetalle */
SELECT RES_FBA
FROM CONTROL
WHERE RES_TIT = @Establo AND 
RES_NP = @NP AND
RES_NC=@NC









