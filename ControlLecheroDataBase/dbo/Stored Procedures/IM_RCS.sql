
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <05/01/2012>
-- Description:	<Calcula las RCS para asignar por estratos>
-- =============================================
CREATE PROCEDURE [dbo].[IM_RCS]
	
	(
		@Establo char(7),
		@FPrevista smalldatetime,
		@Tamano char(2)
	)
AS

IF @Establo<>''
	BEGIN
		SELECT avg(RES_KLAM + RES_KLPM) AS KGLeche, avg((RES_KLAM + RES_KLPM) * RES_TGR/100) AS KGGrasa, 
		sum((RES_KLAM + RES_KLPM) * RES_TPR/100) AS KGProteina
		FROM Control
		WHERE RES_TIT = @Establo AND RES_FPREV = @FPrevista
		
	END
ELSE
BEGIN
	IF @TAMANO<>''
		BEGIN
			SELECT RES_TRC 
			FROM Control,LecturasControl lc
			WHERE  
			RES_TIT=lc.Establo
			AND LC.tamanoestablo=@TAMANO
			AND RES_FPREV >=@FPrevista
			AND RES_FPREV < DATEADD(MONTH,1,@FPrevista)
			and RES_AB='A'
			and RES_Trc>0
		END
		ELSE
		BEGIN
			SELECT  RES_TRC 
			FROM Control
			WHERE RES_FPREV >=@FPrevista
			AND RES_FPREV < DATEADD(MONTH,1,@FPrevista)
			and RES_AB='A'
			and RES_Trc>0
		END
END
	






