
CREATE PROCEDURE [dbo].[IM_Producciones]

	(
		@Establo char(7),
		@FPrevista smalldatetime,
		@NP1 smallint,
		@NP2 smallint,
		@Tamano char(2)
	)
AS

IF @Establo<>''
	BEGIN
		SELECT @Establo as Establo,avg(RES_KLAM + RES_KLPM) AS KGLeche, SUM((RES_KLAM + RES_KLPM) * RES_TGR/100) AS KGGrasa, 
		sum((RES_KLAM + RES_KLPM) * RES_TPR/100) AS KGProteina
		FROM Control
		WHERE RES_TIT = @Establo AND RES_FPREV = @FPrevista
		AND RES_NP BETWEEN @NP1 AND @NP2
	END
ELSE
BEGIN
	IF @TAMANO<>''
		BEGIN
			SELECT '*'  as Establo,COUNT(*) AS Muestras,sum(RES_KLAM + RES_KLPM) as Kilos,sum((RES_KLAM + RES_KLPM)*RES_TGR/100 )   AS GRASA, 
			sum((RES_KLAM + RES_KLPM)*RES_TpR/100 )  AS Proteina
			FROM Control,LecturasControl lc
			WHERE  
			RES_TIT=lc.Establo
			AND LC.tamanoestablo=@TAMANO
			AND RES_FPREV >=@FPrevista
			AND RES_FPREV < DATEADD(MONTH,1,@FPrevista)
			and RES_AB='A'
			AND RES_NP BETWEEN @NP1 AND @NP2
			and RES_TGR>0
		END
		ELSE
		BEGIN
			SELECT '*'  as Establo,COUNT(*) AS Muestras,sum(RES_KLAM + RES_KLPM) as Kilos,sum((RES_KLAM + RES_KLPM)*RES_TGR/100 )   AS GRASA, 
			sum((RES_KLAM + RES_KLPM)*RES_TpR/100 )  AS Proteina
			FROM Control,LecturasControl lc
			WHERE  
			RES_TIT=lc.Establo
			AND RES_FPREV >=@FPrevista
			AND RES_FPREV < DATEADD(MONTH,1,@FPrevista)
			and RES_AB='A'
			AND RES_NP BETWEEN @NP1 AND @NP2
			and RES_TGR>0
		END
END
	





