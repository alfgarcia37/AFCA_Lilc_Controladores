-- =============================================
-- Author:		<Author,,Name>
-- Create date: 27/01/2015
-- Description:	Calcula Censo Establos,Altas y bajas
-- =============================================
CREATE PROCEDURE [dbo].[PR_HistoricoCensoEstablos] 
	@FechaCenso smalldatetime,@TipoCenso char(1)
AS
DECLARE @FechaSgte smalldatetime
--set @FechaCenso='31/12/2012'
set @FechaSgte=dateadd(year,1,@FechaCenso)
IF @TipoCenso='C'
	BEGIN--CENSO EN @FechaCenso 
		SELECT COUNT(*) FROM [controllechero].[dbo].[HistoricoGanaderias]
		where ((falta < @FechaCenso and fbaja is null) or (falta < @FechaCenso and fbaja >@FechaCenso))
	END
ELSE
	BEGIN
		IF @TipoCenso='B' --BAJAS LAS QUE ESTAN EN 2010 Y NO ESTAN EN 2011
			begin
				SELECT COUNT(*)	FROM [controllechero].[dbo].[HistoricoGanaderias]
				where year(fbaja) =year(@FechaCenso) 
				and ESTABLO not in (SELECT establo FROM [controllechero].[dbo].[HistoricoGanaderias]
				where year(falta)=YEAR(@FechaCenso))
				--SELECT COUNT(*)	FROM [controllechero].[dbo].[HistoricoGanaderias]
				--where ((falta < @FechaCenso and fbaja is null) or (falta < @FechaCenso and fbaja >@FechaCenso))
				--and establo not in  (
			 --      SELECT establo
				--   FROM [controllechero].[dbo].[HistoricoGanaderias]
				--   where ((falta < @FechaSgte and fbaja is null) or (falta < @FechaSgte and fbaja >@FechaSgte)))
			end
			else--ALTAS.. LAS QUE  ESTAN EN 2011 Y NO ESTABAN EN 2010
			begin
				SELECT  COUNT(*) FROM [controllechero].[dbo].[HistoricoGanaderias]
				where year(falta)=YEAR(@FechaCenso)
				and ESTABLO not in (SELECT ESTABLO 	FROM [controllechero].[dbo].[HistoricoGanaderias]
				where year(fbaja) =year(@FechaCenso) )
				--SELECT  COUNT(*) FROM [controllechero].[dbo].[HistoricoGanaderias]
				--where ((falta < @FechaSgte and fbaja is null) or (falta < @FechaSgte and fbaja >@FechaSgte))
				--and establo not in  (
				--	SELECT [ESTABLO]   FROM [controllechero].[dbo].[HistoricoGanaderias]
				--	where ((falta < @FechaCenso and fbaja is null) or (falta < @FechaCenso and fbaja >@FechaCenso)))
  			end
	END




  
 
  
