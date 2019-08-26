
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <05/01/2011>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[IM_Dim]
	
	(
		@Establo char(7),
		@FPrevista smalldatetime,
		@NP1 smallint,
		@NP2 smallint,
		@Tamano char(2)
	)
as


/* Lo hago sólo  */
IF @Establo=''
BEGIN
	IF @TAMANO<>''
		BEGIN
			SELECT  AVG(DATEDIFF(day, RES_FP,RES_FCTR))
			FROM Control,LecturasControl lc
			WHERE  
			RES_TIT=lc.Establo
			AND LC.tamanoestablo=@TAMANO
			AND RES_FPREV >=@FPrevista
			AND RES_FPREV < DATEADD(MONTH,1,@FPrevista)
			and RES_AB='A'
			AND RES_NP BETWEEN @NP1 AND @NP2
		END
		ELSE
		BEGIN
			SELECT  AVG(DATEDIFF(day, RES_FP, RES_FCTR))
			FROM Control
			WHERE RES_FPREV >=@FPrevista
			AND RES_FPREV < DATEADD(MONTH,1,@FPrevista)
			and RES_AB='A'
			AND RES_NP BETWEEN @NP1 AND @NP2
		END
END
else
BEGIN
	SELECT  AVG(DATEDIFF(day, RES_FP,RES_FCTR))
	FROM Control
	WHERE  
	RES_TIT=@Establo
	AND RES_FPREV >=@FPrevista
	AND RES_FPREV < DATEADD(MONTH,1,@FPrevista)
	and RES_AB='A'
	AND RES_NP BETWEEN @NP1 AND @NP2
END
