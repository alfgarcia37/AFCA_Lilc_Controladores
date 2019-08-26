-- Batch submitted through debugger: SQLQuery4.sql|7|0|C:\Users\alfongar\AppData\Local\temp\~vsB059.sql
-- Batch submitted through debugger: SQLQuery17.sql|7|0|C:\Documents and Settings\ana\Configuración local\Temp\~vs12B.sql
CREATE PROCEDURE [dbo].[PR_DictamenLactaciones] 
(@strESTABLO CHAR(7),
@strRES char(14),
@intNP int)
AS
--SON BUENAS LOS DICTAMENES: 0,1,2
DECLARE @ESTABLO CHAR(7),@RES CHAR(14), @NP INT
DECLARE @wRES_TIT CHAR(7),@wID CHAR(14), @wNP INT, @wNC INT, @wFP SMALLDATETIME, @wFC SMALLDATETIME
DECLARE @WKILOSi REAL, @WKILOSi1 REAL, @wGRASAi REAL, @wGRASAi1 REAL, @PROTi REAL, @PROTi1 REAL, @wPR REAL,@wCBA As CHAR(2)
DECLARE @WKILOSGrasa REAL, @WKILOSProt REAL
DECLARE @wTOTALKILOS REAL, @wTOTALKILOS305 REAL, @wTOTALGRASA REAL, @wTOTALPROT REAL, @wTOTALGRASA305 REAL, @wTOTALPROT305 REAL, @wDIASLac INT
DECLARE @wDIAS INT, @wFECHAi SMALLDATETIME, @wFechaSecado SMALLDATETIME
DECLARE @wFECHAi1 SMALLDATETIME, @wAB CHAR(1), @wFinalizada CHAR(1)
DECLARE @WKILOS REAL, @wGrasa REAL, @wProt REAL, @wCALCULADO CHAR(1)
DECLARE @RES_TIT CHAR(7),@RES_GEN CHAR(14),@RES_FP SMALLDATETIME,@RES_NP INT,@RES_NC INT
DECLARE @RES_AB CHAR(1),@RES_KL REAL,@RES_TGR REAL,@RES_TPR REAL,@RES_FBA SMALLDATETIME,@RES_CBA CHAR(2),@RES_FCTR SMALLDATETIME
DECLARE @RES_RCS REAL,@TOTALRCS INT,@GRASAFALTANTE REAL,@PROTEINAFALTANTE REAL
DECLARE @CONTADOR INT,@NC1 CHAR(1),@DIFDIAS INT

--set @strESTABLO = '*'
--set	@strRES = 'ES040604074573'
--set	@intNP = 0

IF @strEstablo='*' and @strRes='*'
BEGIN
	DECLARE CL1 CURSOR FOR
	SELECT ESTABLO,RES,NP
	FROM LACTACIONES
	wHERE  dictamen is null
	--and RAZA='62'
	--AND FSECADO>'01/01/2008'
	ORDER BY RES,NP
	OPEN  CL1 
	FETCH NEXT FROM CL1 INTO  @ESTABLO,@RES,@NP
END
ELSE
BEGIN
	IF @strRes<>'*'
	BEGIN
		DECLARE CL1 CURSOR FOR
		SELECT ESTABLO,RES,NP
		FROM LACTACIONES
		WHERE  RES=@strRES
		--AND NP=@intNP
		ORDER BY RES,NP
		OPEN  CL1 
		FETCH NEXT FROM CL1 INTO  @ESTABLO,@RES,@NP
	END
	ELSE
	BEGIN
		DECLARE CL1 CURSOR FOR
		SELECT ESTABLO,RES,NP
		FROM LACTACIONES
		WHERE  ESTABLO=@strEstablo
		AND FPARTO>'01/01/2003'
		--AND RES=@strRES
		--AND NP=@intNP
		ORDER BY RES,NP
		OPEN  CL1 
		FETCH NEXT FROM CL1 INTO  @ESTABLO,@RES,@NP
	END
END



WHILE (@@FETCH_STATUS=0 )  --..........................................................................WHILE 1.........................................................................................
BEGIN
	DECLARE C2 CURSOR FOR
	SELECT RES_TIT,RES_GEN,RES_FP,RES_NP,RES_NC,RES_AB,(RES_KLAM+RES_KLPM),RES_TGR,RES_TPR,RES_FBA,RES_CBA,RES_FCTR,RES_TRC
	FROM CONTROL
	--agc 	WHERE  RES_TIT=@ESTABLO
 	WHERE RES_GEN=@RES
	AND RES_NP=@NP
	ORDER BY RES_GEN,RES_NP,RES_NC
		
	OPEN  C2 
	FETCH NEXT FROM C2 INTO  @RES_TIT,@RES_GEN,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_FBA,@RES_CBA,@RES_FCTR,@RES_RCS

	SET @Wid = @RES_GEN
	SET @wFECHAi = @RES_FP
	SET @wFECHAi1 = @RES_FCTR
	SET @wFP = @RES_FP
	SET @CONTADOR=0;SET @NC1='0'
	
	
	UPDATE LACTACIONES SET DICTAMEN='0' --correcta
	WHERE ESTABLO=@ESTABLO
	AND RES=@RES
	AND NP=@NP
	
	
	-- @RES_CBA = '09' @RES_CBA = '10' VENTA O BAJA.
  WHILE (@@FETCH_STATUS=0 AND  (@RES_AB = 'A' OR (@RES_AB = 'B' AND (@RES_CBA = '09' OR @RES_CBA = '10'))  AND @RES_GEN<>'ZZZ' )) --...........................WHILE .........2..................................................................................................................................................................
		BEGIN
	      SET @wDIAS = DateDiff(DAY, @wFECHAi, @wFECHAi1)
	     	IF @NC1='0' -- ES EL PRIMER CONTROL...	TOMO FP Y FCTR DEL PRIMER CONTROL
			BEGIN--primer control
		      IF DATEDIFF(DAY, @wFECHAi, @wFECHAi1) <5  
				BEGIN
				SET @CONTADOR=@CONTADOR+1
				END
		    ELSE
			BEGIN--controles >1
				SET @DIFDIAS=DATEDIFF(DAY, @wFECHAi, @wFECHAi1)
				IF @DIFDIAS>=38   AND @DIFDIAS <=71  SET @CONTADOR=@CONTADOR+1 
				IF @DIFDIAS>=72 AND @DIFDIAS <=105  SET @CONTADOR=@CONTADOR+2
				IF @DIFDIAS>=106   SET @CONTADOR=@CONTADOR+3
			END
		SET @NC1='1'

		END
		ELSE
		BEGIN--solo para los primeros 305 dias. Aqui entra a partir del 2º control QUE NO TIENE POR QUE SER NC=2
			--IF (DATEDIFF(DAY, @wFECHAi, @wFECHAi1) < 26 OR  DATEDIFF(DAY, @wFECHAi, @wFECHAi1) >34 ) and  DATEDIFF(DAY, @wFP, @wFECHAi1) <=305
			--IF   DATEDIFF(DAY, @wFECHAi, @wFECHAi1) >33  AND  DATEDIFF(DAY, @wFP, @wFECHAi1) <=305 16/04/15....nuevos limites
			IF   DATEDIFF(DAY, @wFECHAi, @wFECHAi1) >37  AND  DATEDIFF(DAY, @wFP, @wFECHAi1) <=305 --16/04/15....nuevos limites
			BEGIN
				SET @DIFDIAS=DATEDIFF(DAY, @wFECHAi, @wFECHAi1)
				IF @DIFDIAS<22   SET @CONTADOR=@CONTADOR+1
				--IF @DIFDIAS>=34 AND @DIFDIAS <=67  SET @CONTADOR=@CONTADOR+1 16/04/15....nuevos limites
				IF @DIFDIAS>37 AND @DIFDIAS <=67  SET @CONTADOR=@CONTADOR+1
				IF @DIFDIAS>=68 AND @DIFDIAS <=101  SET @CONTADOR=@CONTADOR+2
				IF @DIFDIAS>=102   SET @CONTADOR=@CONTADOR+3
				--SET @CONTADOR=@CONTADOR+1
			END
			 
		END

        	IF   DATEDIFF(DAY, @wFECHAi, @wFECHAi1) >67 and  DATEDIFF(DAY, @wFP, @wFECHAi1) <=305
			BEGIN
				SET @CONTADOR=67
			END
		
	SET @wFP = @RES_FP
      	SET @wFECHAi = @RES_FCTR
	SET @Wid = @RES_GEN
      	IF @RES_FCTR IS NULL
		BEGIN
			SET @wFECHAi = null
		END
	ELSE
		BEGIN
			SET @wFECHAi = @RES_FCTR
		END
	
      	FETCH NEXT FROM C2 INTO  @RES_TIT,@RES_GEN,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_FBA,@RES_CBA,@RES_FCTR,@RES_RCS
      	SET @wFECHAi1 = @RES_FCTR
	----PRUEBA----
IF @RES_CBA='09' or @RES_CBA='10'
	BEGIN
		--prueba 27/04/2017...........
			IF DateDiff(DAY, @RES_FP, @RES_FBA)<240
			BEGIN
				UPDATE LACTACIONESTEMP SET DICTAMEN='10' --MENOS DE 240 DIAS
				WHERE ESTABLO=@ESTABLO
				AND RES=@RES
				AND NP=@NP
			END
			IF @CONTADOR>2  AND @CONTADOR<10
			BEGIN
				UPDATE LACTACIONESTEMP SET DICTAMEN='20'   -- MAS DE DOS CONTROLES FALTANTES
				WHERE ESTABLO=@ESTABLO
				AND RES=@RES
				AND NP=@NP
			END
			IF @CONTADOR>66  AND @CONTADOR<70   -- EMPIEZA TARDE
			BEGIN
				UPDATE LACTACIONESTEMP SET DICTAMEN='67'  
				WHERE ESTABLO=@ESTABLO
				AND RES=@RES
				AND NP=@NP
			END
	-- fin .....24/04/2017
	
		FETCH NEXT FROM C2 INTO  @RES_TIT,@RES_GEN,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_FBA,@RES_CBA,@RES_FCTR,@RES_RCS
		SET @wFECHAi1 = @RES_FCTR
	END
	--FIN PRUEBA----
	
    END --WHILE ...................................2.................2.................2.........................................................................................................................................................................................................................................
	CLOSE C2
	DEALLOCATE C2

	--UPDATE LACTACIONES SET DICTAMEN='0' --correcta
	--WHERE ESTABLO=@ESTABLO
	--AND RES=@RES
	--AND NP=@NP

	IF @CONTADOR>2  AND @CONTADOR<10
	BEGIN
		UPDATE LACTACIONES SET DICTAMEN='20'   -- MAS DE DOS CONTROLES FALTANTES
		WHERE ESTABLO=@ESTABLO
		AND RES=@RES
		AND NP=@NP
	END
	IF @CONTADOR>66  AND @CONTADOR<70   -- EMPIEZA TARDE
	BEGIN
		UPDATE LACTACIONES SET DICTAMEN=@CONTADOR
		WHERE ESTABLO=@ESTABLO
		AND RES=@RES
		AND NP=@NP
	END
	UPDATE LACTACIONES SET DICTAMEN=@CONTADOR -- SI DICTAMEN=0 =1=2  -> CORRECTA 81 significa una falta, posiblemente de vacaciones)
	WHERE ESTABLO=@ESTABLO
	AND RES=@RES
	AND NP=@NP
	and DICTAMEN<>'10'

	 IF  @RES_AB = 'B' And   @RES_FBA IS NOT NULL  --SI hay fsecado--->se hace con fsecado
	BEGIN--SI hay fsecado--->se hace con fsecado
	        SET @wFechaSecado = @RES_FBA
	         SET @wDIAS = DateDiff(day, @wFECHAi, @RES_FBA)
	        SET @wDIASLac = DateDiff(day, @RES_FP, @RES_FBA)
	        
	END

	 IF   @RES_AB ='B' AND  @RES_FBA IS  NULL
	BEGIN
	          SET @wDIAS = 14
	          SET @wFechaSecado = DateAdd(day, 14, @wFECHAi)
	          SET @wDIASLac = DateDiff(day, @wFP, @wFECHAi) + 14
	 END
-- esto esta sin probar al 100%
 	IF   @RES_AB ='A' AND  @RES_FBA IS  NULL
	BEGIN
	          SET @wDIAS = 14
	          SET @wFechaSecado = DateAdd(day, 14, @wFECHAi)
	          SET @wDIASLac = DateDiff(day, @wFP, @wFECHAi) + 14
	 END
-- FIN esto esta sin probar al 100%

	-------GUARDAR LACTACION
	IF @wDIASLac<240
	BEGIN
		UPDATE LACTACIONES SET DICTAMEN='10' --MENOS DE 240 DIAS
		WHERE ESTABLO=@ESTABLO
		AND RES=@RES
		AND NP=@NP
	END

     FETCH NEXT FROM CL1 INTO  @ESTABLO,@RES,@NP

END -- WHILE  .........1.............................1..............1...............1..............................................................................................................................
CLOSE CL1
DEALLOCATE CL1

UPDATE LACTACIONES 
SET DICTAMEN='10' --MENOS DE 240 DIAS
WHERE diasLac<240


UPDATE LACTACIONES
SET DICTAMEN='22'
WHERE FPARTO>'01/01/2009'
AND RES + CONVERT(varchar(10), FPARTO, 103)
IN (
SELECT AUD_GEN + CONVERT(varchar(10), CL_FP, 103)
  FROM [controllechero].[dbo].[ControlAud]
  WHERE    (AUD_KL>0 AND CL_KL>=0)
  AND (AUD_KL/CL_KL<0.6 OR AUD_KL/CL_KL>1.4)
  and ( (aud_INC='' OR aud_INC='00') and(  CL_INC='' OR CL_INC='00'  OR CL_INC='0')) 
  )
 -- para las vaca que solo tienen un control de secado
 UPDATE LACTACIONES
SET DICTAMEN='23'
WHERE FPARTO>'01/01/2009'
and finalizada='S'
and kilos=0