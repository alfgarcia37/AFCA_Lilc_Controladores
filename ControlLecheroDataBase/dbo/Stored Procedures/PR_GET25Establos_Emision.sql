
-- =============================================
-- Create date: 26/10/2015
-- Description:	Obtiene los 25 peores establos de F.Control hasta F.Emisión Informe
-- =============================================
CREATE PROCEDURE [dbo].[PR_GET25Establos_Emision]
	(@DESDE CHAR(10),@HASTA CHAR(10))

AS
set nocount on
--AHORA ACTUALIZO LOS VALORES DE TODAS LAS TABLAS
--DECLARE @DESDE CHAR(10),@HASTA CHAR(10)
--SET @DESDE='01/01/2015'
--SET @HASTA='31/01/2015'
		
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tmp_25Establos]') AND type in (N'U'))
	DROP TABLE [dbo].Tmp_25Establos
	cREATE TABLE [dbo].Tmp_25Establos([establo] [char](7) NOT NULL,FPrevista [smalldatetime] null,[Fcontrol_am] [date] NULL,[Fcontrol_pm] [date] NULL,[Fcontrol] [date] NULL,
	FDescarga [smalldatetime] null,FEntrega [smalldatetime] null,FCorregido [smalldatetime] null,FAnalisis [smalldatetime] null,FInforme [smalldatetime] null,Numcontroler [int],Motivo [char](2) NULL,Fichero [varchar](20) NULL,ControlInforme [int] )

	INSERT INTO Tmp_25Establos(ESTABLO,FPREVISTA,Fcontrol_am,Fcontrol_Pm,FDescarga,FEntrega,FCorregido,FAnalisis,FInforme,Numcontroler,Motivo,Fichero)
	SELECT TOP 1000 establo ,fprevista,[ActualOrdenoM],[ActualOrdenoT],FDescarga,FEntrega,FCorregido,FAnalisis,FInforme,Numcontroler,MotivoCorregido,Fichero FROM LecturasFinalizadas 
	Where fprevista BETWEEN @DESDE AND @HASTA
	AND analitica='1' and datediff(day,fcorregido,getdate())>5 ORDER BY  datediff(day,fcorregido,finforme) DESC
	
	UPDATE Tmp_25Establos
	SET [Fcontrol]=[Fcontrol_am] WHERE [Fcontrol_am] IS NOT NULL
	UPDATE Tmp_25Establos
	SET [Fcontrol]=[Fcontrol_pm] WHERE [Fcontrol_pm] IS NOT NULL
	--AHORA ELIMINO SABADOS,DOMINGOS Y FESTIVOS 8ME QUEDO LABORABLES)
	UPDATE Tmp_25Establos
	SET ControlInforme=datediff(day,Fcontrol,FInforme)
	
/*	
	
DECLARE @wDiasTotal smallint,@Fecha1 smalldatetime,@Fecha2 smalldatetime,@NumFestivos smallint,@ControlInforme smallint
declare @wFechaIni smalldatetime,@i smallint

DECLARE C1 CURSOR FOR
SELECT Fcontrol,FInforme,ControlInforme FROM Tmp_25Establos 
FOR UPDATE OF ControlInforme
OPEN  C1
FETCH NEXT FROM C1 INTO @Fecha1,@Fecha2,@ControlInforme
WHILE (@@FETCH_STATUS=0)
BEGIN
	SET @wDiasTotal = DateDiff(DAY, @Fecha1, @Fecha2)
	DECLARE C2 CURSOR FOR 
	SELECT count(*) FROM Festivos where id between @Fecha1 and @Fecha2
	OPEN  C2
	FETCH NEXT FROM C2 INTO @NumFestivos
		WHILE (@@FETCH_STATUS=0)
		BEGIN
			SET @wDiasTotal = @wDiasTotal - @NumFestivos
			FETCH NEXT FROM C2 INTO @NumFestivos
		END
		CLOSE C2
		DEALLOCATE C2
	--QUITO SABADOS Y DOMINGOS
	SET @wFechaIni=@Fecha1;SET @i=1
	WHILE (@i<= DATEDIFF(DAY ,@Fecha1, @Fecha2))
	BEGIN
		IF (datepart(weekday,@wFechaIni) = 6 Or datepart(weekday,@wFechaIni) = 7 )
		BEGIN
			SET @wDiasTotal = @wDiasTotal - 1
		END
	 SET @wFechaIni = DateAdd(DAY, 1, @wFechaIni)
	 SET @i=@i+1
	END
			
	UPDATE Tmp_25Establos
	SET ControlInforme=@wDiasTotal
	WHERE CURRENT OF C1
	FETCH NEXT FROM C1 INTO @Fecha1,@Fecha2,@ControlInforme	
END
CLOSE C1
DEALLOCATE C1
*/	 
--order by datediff(day,fcorregido,finforme) desc

--SELECT Establo as ID,NumControler as DET,Fprevista,Freal,Fentrega,Fanalisis,Fcarga,FDescarga,FCorregido,MotivoCorregido,RazonCorregido,FInforme,
--Generatxt,Analitica,PrecioEstablo,Informe,subido,Corregido,TipoControl,Tanque 
--FROM LecturasFinalizadas Where fprevista>'01/10/2015' and (analitica='1' and datediff(day,fcorregido,getdate())>5)  

