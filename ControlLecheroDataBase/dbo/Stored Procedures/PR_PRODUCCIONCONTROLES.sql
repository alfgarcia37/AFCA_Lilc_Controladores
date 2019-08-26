-- =============================================
-- Author:		<Author,,Name>
-- Create date: <C25/05/2012,,>
-- Description:	<Se hace pare el gráfico de lactaciones semestrales. Produccin agrupada por controles>
-- =============================================
CREATE PROCEDURE [dbo].[PR_PRODUCCIONCONTROLES] 
	
	(
		@Establo char(7),
		@Desde smalldatetime,
		@Hasta smalldatetime,
		@Tamano char(2),
		@TipoCon char(1)
	)
AS
IF @Establo<>''
	BEGIN
	 SELECT RES_NC AS ID,  DET=CASE WHEN @TipoCon='K' THEN AVG(RES_KLAM + RES_KLPM)
								    WHEN @TipoCon='G' THEN SUM(res_tGr*(RES_KLAM+RES_KLPM))/SUM(RES_KLAM+RES_KLPM)
								    WHEN @TipoCon='P' THEN SUM(res_tPr*(RES_KLAM+RES_KLPM))/SUM(RES_KLAM+RES_KLPM)
								    END  FROM Control
									WHERE RES_TIT = @Establo AND RES_FPREV between  @Desde and  @Hasta
									and RES_AB='A' AND RES_NC<13 GROUP BY RES_NC
	END
ELSE
	BEGIN
		IF @TAMANO<>''
			BEGIN
				SELECT RES_NC AS ID, DET=CASE WHEN @TipoCon='K' THEN AVG(RES_KLAM + RES_KLPM)
										WHEN @TipoCon='G' THEN SUM(res_tGr*(RES_KLAM+RES_KLPM))/SUM(RES_KLAM+RES_KLPM)
										WHEN @TipoCon='P' THEN SUM(res_tPr*(RES_KLAM+RES_KLPM))/SUM(RES_KLAM+RES_KLPM)
										END  FROM Control,LecturasControl lc
				WHERE  
				RES_TIT=lc.Establo
				AND LC.tamanoestablo=@TAMANO
				AND RES_FPREV between  @Desde and  @Hasta
				and RES_AB='A'
				AND RES_NC<13
				GROUP BY RES_NC
			END
			ELSE
			BEGIN
				SELECT RES_NC AS ID, DET=CASE WHEN @TipoCon='K' THEN AVG(RES_KLAM + RES_KLPM)
										WHEN @TipoCon='G' THEN SUM(res_tGr*(RES_KLAM+RES_KLPM))/SUM(RES_KLAM+RES_KLPM)
										WHEN @TipoCon='P' THEN SUM(res_tPr*(RES_KLAM+RES_KLPM))/SUM(RES_KLAM+RES_KLPM)
										END  FROM Control,LecturasControl lc
				WHERE  
				RES_TIT=lc.Establo
				AND RES_FPREV between  @Desde and  @Hasta
				and RES_AB='A'
				AND RES_NC<13
				GROUP BY RES_NC
			END
	END
	






