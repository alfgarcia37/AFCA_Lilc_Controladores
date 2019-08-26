-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CENSO_PARTOS_CALDERON] 
	(
		@strEstablo char(7),
		@Fecha_CensoDesde smalldatetime,
		@Fecha_CensoHasta smalldatetime,
		@usuario varchar(50)
	) 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DELETE  FROM Tmp_CensoVacas80
	WHERE ESTABLO=@strEstablo
	
	
	INSERT INTO Tmp_CensoVacas80 (ESTABLO,VACA)
	select distinct ESTABLO, vaca 
	from TMP_CensoAfca
	WHERE ESTABLO=@strEstablo
	AND  usuario=@usuario
    
  
    DELETE  FROM Tmp_Partos80 
    WHERE ESTABLO=@strEstablo
	
	  -- 2.A cojo todos los partos de ese establo (aunque se haya marchado la vaca de ese establo antes de la fecha de cálculo de censo)
	--INSERT INTO Tmp_Partos80 (ESTABLO,VACA,FPARTO)
	--select  distinct ESTABLO, id,FPARTO from Partos  
	--where fparto  between  @Fecha_CensoDesde and @Fecha_CensoHasta
	--AND ESTABLO=@strEstablo
	
	-- 2.B. Cojo los partos de ese Censo de ese establo. Opción RESTRICTIVA
	INSERT INTO Tmp_Partos80 (ESTABLO,VACA,FPARTO)
	select  distinct MAX(P.ESTABLO), MAX(id),MAX(FPARTO) from Partos P ,Tmp_CensoVacas80 T
	where fparto  between  @Fecha_CensoDesde and @Fecha_CensoHasta
	AND P.ESTABLO=@strEstablo
	and P.ESTABLO=T.Establo 
	and p.ID=t.Vaca 
	and NP>0
	GROUP BY P.ESTABLO,ID
	
	
	
	update Tmp_Partos80
	set fn=maes_fn
	from maestro
	where vaca=maes_gen
	AND ESTABLO=@strEstablo
	--borro los menores de xxx dias en la fecha del censo
	PRINT 'DELTE....'
	--DELETE FROM Tmp_Partos80 WHERE DATEdiff(DAY,fn,@Fecha_Censo)<822
	PRINT 'DELTE....'
	-- 13/02/2017 Borro todos los animales que sean de cruce
	delete from Tmp_CensoVacas80
	where vaca in (select maes_gen from maestro where  maes_raza='00'  AND ESTABLO=@strEstablo)
	delete  from Tmp_Partos80
	where vaca in (select maes_gen from maestro where  maes_raza='00' AND ESTABLO=@strEstablo)
	
		
	--ACTUALIZO PARTOS DE TABLA DE TOTALES
	--3.2 Porcentajes por Establo
	--DECLARE @NUMPARTOS REAL
	--DECLARE C1 CURSOR LOCAL SCROLL OPTIMISTIC FOR
	--PARTOS........................................
	--SELECT COUNT(*)
	--FROM Tmp_Partos80
	--WHERE ESTABLO=@strEstablo
	--GROUP BY ESTABLO
	--OPEN C1 
	--FETCH NEXT FROM C1 INTO @NUMPARTOS
	--WHILE (@@FETCH_STATUS=0)
	--BEGIN
	--	  UPDATE Tmp_TOTALES80 
	--	  SET TOT15=@NUMPARTOS
	--	  WHERE ESTABLO=@strEstablo
	--	  FETCH NEXT FROM C1 INTO @NUMPARTOS
	--END
	--CLOSE C1
	--DEALLOCATE C1
	
	
	DELETE  FROM Tmp_TOTALES80
	WHERE ESTABLO=@strEstablo
	
	
	INSERT INTO Tmp_TOTALES80 (ESTABLO,TOT15)
	select  ESTABLO, COUNT(*) 
	from Tmp_Partos80
	WHERE ESTABLO=@strEstablo
	GROUP BY ESTABLO
	
	--CENSO..........................................
	DECLARE @NUMcenso REAL
	DECLARE C2 CURSOR LOCAL SCROLL OPTIMISTIC FOR
	SELECT COUNT(*)
	FROM Tmp_CensoVacas80
	WHERE ESTABLO=@strEstablo
	GROUP BY ESTABLO
	OPEN C2 
	FETCH NEXT FROM C2 INTO @NUMcenso
	
	WHILE (@@FETCH_STATUS=0)
	BEGIN
		  UPDATE Tmp_TOTALES80 
		  SET TOTPARTOS=@NUMcenso
		  WHERE ESTABLO=@strEstablo
		  FETCH NEXT FROM C2 INTO @NUMcenso
	END
	CLOSE C2
	DEALLOCATE C2
	UPDATE Tmp_TOTALES80
	--..........................................
	
	SET PORCENTAJE=TOT15/TOTPARTOS
	WHERE ESTABLO=@strEstablo
-- FIN 3.2

update Tmp_CensoVacas80
set cea=numsanitario
from reggen..ganaderias_maestro
where establo=codgan
AND ESTABLO=@strEstablo

update Tmp_CensoVacas80
set Nombre=A.Nombre
from reggen..animales A
where vaca COLLATE Modern_Spanish_CI_AS=A.codigoidentif


update Tmp_CensoVacas80
set fn=TMP_CensoAfca.fn
from TMP_CensoAfca
where Tmp_CensoVacas80.vaca=TMP_CensoAfca.vaca

update Tmp_Partos80
set cea=numsanitario
from reggen..ganaderias_maestro
where establo=codgan

update Tmp_Partos80
set Tmp_Partos80.NP=Partos.NP
from Partos
where vaca=id
and  Tmp_Partos80.FPARTO=Partos.FParto

--update Tmp_Partos80
--set Tmp_Partos80.FPARTO=Partos.FParto
--from Partos
--where vaca=id
--and Tmp_Partos80.NP=Partos.NP

update Tmp_Partos80
set NOMBRE=maes_NOM
from maestro
where vaca=maes_gen


update Tmp_TOTALES80
set cea=numsanitario
from reggen..ganaderias_maestro
where establo=codgan
END
