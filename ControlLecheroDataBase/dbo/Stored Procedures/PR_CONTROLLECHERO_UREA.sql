CREATE PROCEDURE [dbo].[PR_CONTROLLECHERO_UREA] 
	
	(
		@Establo char(7),
		@FPrevista smalldatetime
	)
AS
DECLARE @ESTABLOLECTURAS AS CHAR(7),@FLECTURAS AS SMALLDATETIME
SELECT max(res_fctr) 'FechaControl',avg(res_klam+res_klpm) 'kilos',sum((res_klam+res_klpm)*res_tgr)/sum(res_klam+res_klpm) 'grasa',sum((res_klam+res_klpm)*res_tpr)/sum(res_klam+res_klpm) 'proteina'
  FROM [controllechero].[dbo].control
  where   res_tit=@Establo
  and res_fprev=@FPrevista
  and res_ab='A'
  --and Datediff(day,res_fprev,@FPrevista) between -16 and 16
  