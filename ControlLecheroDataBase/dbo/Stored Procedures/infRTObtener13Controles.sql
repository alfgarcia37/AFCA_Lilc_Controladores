









CREATE PROCEDURE dbo.infRTObtener13Controles
	(
		@Establo char(7)
	)
AS

/* INFORME - REBAÑO TOTAL */
/* Procedimiento: Obtener13Controles */
IF @Establo<>"" 
BEGIN
	SELECT TOP 13 FPrevista, FReal FROM LecturasFinalizadas WHERE Establo = @Establo ORDER BY fprevista DESC
END





