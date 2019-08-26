









CREATE PROCEDURE dbo.infRTObtenerControlesDetalle
	(
		@Establo char(7),
		@FPrevista smalldatetime
	)
AS

/* INFORME - REBAÑO TOTAL */
/* Procedimiento: OBtenerControlesDetalle */

IF @Establo<>''
	BEGIN
		SELECT RES_KLAM + RES_KLPM AS KGLeche, (RES_KLAM + RES_KLPM) * RES_TGR/100 AS KGGrasa, 
		(RES_KLAM + RES_KLPM) * RES_TPR/100 AS KGProteina,
		RES_FP, RES_FCTR,RES_TRC AS RCS,RES_CBA,res_gen,RES_NC,RES_NP,RES_AB
		FROM Control 
		WHERE RES_TIT = @Establo AND RES_FPREV = @FPrevista
		order by res_np,res_nc
	END
ELSE
	BEGIN
		SELECT RES_KLAM + RES_KLPM AS KGLeche, (RES_KLAM + RES_KLPM) * RES_TGR/100 AS KGGrasa, 
		(RES_KLAM + RES_KLPM) * RES_TPR/100 AS KGProteina,
		RES_FP, RES_FCTR,RES_TRC AS RCS,RES_CBA,res_gen,RES_NC,RES_NP,RES_AB
		FROM Control 
		WHERE  RES_FPREV >=@FPrevista
		AND RES_FPREV < DATEADD(MONTH,1,@FPrevista)
		order by res_np,res_nc
	END




