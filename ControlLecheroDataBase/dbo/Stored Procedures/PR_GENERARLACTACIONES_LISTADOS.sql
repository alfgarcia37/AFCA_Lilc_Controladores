-- Batch submitted through debugger: SQLQuery30.sql|7|0|C:\Users\alfongar\AppData\Local\temp\~vsC634.sql

CREATE PROCEDURE [dbo].[PR_GENERARLACTACIONES_LISTADOS] 

(@strRES char(14),
@intNP int,
@datFPREV smalldatetime,
@usuario varchar(20))


AS

DECLARE @wRES_TIT CHAR(7),@wID CHAR(14), @wNP INT, @wNC INT, @wFP SMALLDATETIME, @wFC SMALLDATETIME,@wRES_COL CHAR(5),@RES_COL CHAR(5)
DECLARE @WKILOSi REAL, @WKILOSi1 REAL, @wGRASAi REAL, @wGRASAi1 REAL, @PROTi REAL, @PROTi1 REAL, @RCSi1 REAL,@wPR REAL,@wCBA As CHAR(2)
DECLARE @WKILOSGrasa REAL, @WKILOSProt REAL
DECLARE @wTOTALKILOS REAL, @wTOTALKILOS305 REAL, @wTOTALGRASA REAL, @wTOTALPROT REAL, @wTOTALGRASA305 REAL, @wTOTALPROT305 REAL, @wDIASLac INT
DECLARE @wDIAS INT, @wFECHAi SMALLDATETIME, @wFechaSecado SMALLDATETIME
DECLARE @wFECHAi1 SMALLDATETIME, @wAB CHAR(1), @wFinalizada CHAR(1)
DECLARE @WKILOS REAL, @wGrasa REAL, @wProt REAL, @wRCSt REAL,@wCALCULADO CHAR(1),@RES_INCRECAL CHAR(2)
DECLARE @RES_TIT CHAR(7),@RES_GEN CHAR(14),@RES_FP SMALLDATETIME,@RES_NP INT,@RES_NC INT
DECLARE @RES_AB CHAR(1),@RES_KL REAL,@RES_TGR REAL,@RES_TPR REAL,@RES_FBA SMALLDATETIME,@RES_CBA CHAR(2),@RES_FCTR SMALLDATETIME
DECLARE @RES_RCS REAL,@TOTALRCS FLOAT,@GRASAFALTANTE REAL,@PROTEINAFALTANTE REAL,@FALTA CHAR(1),@CONT FLOAT,@KILOSFALTANTE REAL



-- BORRO LACTACIONES------------


	DELETE LACTACIONESTEMP
	WHERE  RES=@strRES
	AND NP=@intNP
	and usuario=@usuario
	
		
		DECLARE C1 cursor for 
		SELECT RES_TIT,RES_GEN,RES_COL,RES_FP,RES_NP,RES_NC,RES_AB,(RES_KLAM+RES_KLPM),RES_TGR,RES_TPR,@RES_INCRECAL,RES_FBA,RES_CBA,RES_FCTR ,RES_TRC
		FROM CONTROL
		WHERE 
		RES_GEN=@strRES
		AND RES_NP=@intNP
		AND RES_FPREV<= @datFPrev
		UNION
		SELECT TOP 1  'TTT','ZZZ','V','01/01/2000',100,100,'V',0,0,0,'00','01/01/2000','00','01/01/2000',0 FROM CONTROL
		ORDER BY RES_NC
	
--(((((((((((((((((((( LEO PRIMER REGISTRO ))))))))))))))))))))))))))))))
OPEN  C1 
FETCH NEXT FROM C1 INTO  @RES_TIT,@RES_GEN,@RES_COL,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_INCRECAL,@RES_FBA,@RES_CBA,@RES_FCTR,@RES_RCS
--rcsDatos.MoveFirst
SET @WKILOSi = @RES_KL

--MIRO LOS FALTANTES DE ANALITICA Y PRODUCCION
IF @RES_TGR=0  OR @RES_TGR<1.5  OR @RES_TGR>9 OR @RES_TPR=0 OR @RES_TPR<2.0 OR @RES_TPR>6 OR @RES_INCRECAL='02'
	BEGIN
		EXECUTE  PR_CONTROL_FALTANTE_ANALITICA  @RES_GEN,@RES_NP,@RES_NC,@RES_TGR,@RES_TPR,@RES_INCRECAL -- INSERTO VALORES FALTANTES EN TABLA FALTANTES
		DECLARE CF CURSOR FOR 
		SELECT GRASAFALTANTE,PROTEINAFALTANTE FROM FALTANTES -- SELECT DE TABLA FALTANTES
		OPEN  CF 
		FETCH NEXT FROM CF INTO  @GRASAFALTANTE,@PROTEINAFALTANTE
		CLOSE CF
		DEALLOCATE CF
		SET @RES_TGR=@GRASAFALTANTE;SET @RES_TPR=@PROTEINAFALTANTE
	END
--MIRO PRODUCCION
BEGIN
	EXECUTE  PR_CONTROL_FALTANTE_KILOS  @RES_GEN,@RES_NP,@RES_NC,@RES_KL -- INSERTO VALORES FALTANTES EN TABLA FALTANTES
	DECLARE CF CURSOR FOR 
	SELECT KILOSFALTANTE FROM FALTANTES -- SELECT DE TABLA FALTANTES
	OPEN  CF 
	FETCH NEXT FROM CF INTO  @KILOSFALTANTE
	CLOSE CF
	DEALLOCATE CF
	SET @RES_KL=@KILOSFALTANTE
END

SET @wGRASAi = @RES_TGR * (@RES_KL / 100)
SET @PROTi = @RES_TPR * (@RES_KL) / 100
SET @wFECHAi = @RES_FP
SET @WKILOSi1 = @RES_KL
SET @wGRASAi1 = @RES_TGR *@RES_KL/ 100
SET @PROTi1 = @RES_TPR * @RES_KL / 100


--SET @RCSi1=@RES_RCS
SET @wFECHAi1 = @RES_FCTR
SET @wRES_TIT=@RES_TIT
SET @wRES_COL=@RES_COL
SET @wID = @RES_GEN
SET @wNP = @RES_NP
SET @wFP = @RES_FP
SET @wCALCULADO = 'N'
SET @wTOTALKILOS =0;SET @wTOTALGRASA =0;SET @wTOTALPROT =0;SET @wTOTALKILOS305 =0;SET @wTOTALGRASA305 =0;SET @wTOTALPROT305 =0
SET @TOTALRCS=1;SET @FALTA='0';SET @CONT=0
WHILE (@@FETCH_STATUS=0 AND @RES_GEN<>'ZZZ') --.........................................................WHILE 1....................................................................................................................................................................................................................................
BEGIN
  WHILE (@RES_GEN = @wID And @RES_AB = 'A' And @RES_NP = @wNP) --...........................WHILE 2...........................................................................................................................................................................
	BEGIN--WHILE 2..................................................................................................................................................................................................................................................................................................................................................
	     --=======================KILOS NATURAL==========================================================================
	      SET @WKILOS = (@WKILOSi + @WKILOSi1) / 2
	      SET @wGrasa = (@wGRASAi + @wGRASAi1) / 2
	      SET @wProt = (@PROTi + @PROTi1) / 2
	      SET @wDIAS = DateDiff(DAY, @wFECHAi, @wFECHAi1)
	      SET @wTOTALKILOS = @wTOTALKILOS + (@WKILOS * @wDIAS)
	      SET @wTOTALGRASA = @wTOTALGRASA + (@wGrasa * @wDIAS)
	      SET @wTOTALPROT = @wTOTALPROT + (@wProt * @wDIAS)
	IF @RES_RCS=0  
	BEGIN
		SET @RES_RCS=1
	END
	      SET @TOTALRCS=@TOTALRCS * @RES_RCS
	      SET @CONT=@CONT+1
	      --====================KILOS 305================================================================================
	      If @wCALCULADO = 'N'
		BEGIN
	        	If DateDiff(DAY, @RES_FP, @wFECHAi1) > 305 
		  BEGIN
		         SET @wTOTALKILOS305 = @wTOTALKILOS305 + (@WKILOS) * (305 - DateDiff(DAY, @RES_FP, @wFECHAi))
		         SET @wTOTALGRASA305 = @wTOTALGRASA305 + @wGrasa * (305 - DateDiff(DAY, @RES_FP, @wFECHAi))
		         SET @wTOTALPROT305 = @wTOTALPROT305 + @wProt * (305 - DateDiff(DAY, @RES_FP, @wFECHAi))
		         SET @wCALCULADO = 'S'
		  END
	        	Else
		  BEGIN 
		          SET @wTOTALKILOS305 = @wTOTALKILOS
		          SET @wTOTALGRASA305 = @wTOTALGRASA
		          SET @wTOTALPROT305 = @wTOTALPROT
		  END
	         
      	     END 
      --====================================================================
	SET @wRES_TIT=@RES_TIT
	SET @wRES_COL=@RES_COL
      	SET @wID = @RES_GEN
	SET @wAB = @RES_AB
	SET @wNP = @RES_NP
	SET @wFP = @RES_FP
      	SET @WKILOSi = @RES_KL
      	SET @wGRASAi = @RES_TGR * @RES_KL / 100

      	SET @PROTi = @RES_TPR * @RES_KL / 100
	SET @wFECHAi = @RES_FCTR
      	/*IF @RES_FCTR IS NULL
	BEGIN
		SET @wFECHAi = Empty
	END
	ELSE
	BEGIN
		SET @wFECHAi = @RES_FCTR)
	END
	*/
      	FETCH NEXT FROM C1 INTO  @RES_TIT,@RES_GEN,@RES_COL,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_INCRECAL,@RES_FBA,@RES_CBA,@RES_FCTR,@RES_RCS
	-- PRUEBA POR LO DE LA CAUSA BAJA=10 O 09	........../////.................//////...........................////////
	IF @RES_CBA='10' OR @RES_CBA='09'
	BEGIN
		FETCH NEXT FROM C1 INTO  @RES_TIT,@RES_GEN,@RES_COL,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_INCRECAL,@RES_FBA,@RES_CBA,@RES_FCTR,@RES_RCS
	END
	-- ......///////.....//////....///////////////////............................//////...............

	
--MIRO LOS FALTANTES DE ANALITICA Y PRODUCCION
IF @RES_TGR=0  OR @RES_TGR<1.5  OR @RES_TGR>9 OR @RES_TPR=0 OR @RES_TPR<2.0 OR @RES_TPR>6 OR @RES_INCRECAL='02'
	BEGIN
		EXECUTE  PR_CONTROL_FALTANTE_ANALITICA  @RES_GEN,@RES_NP,@RES_NC,@RES_TGR,@RES_TPR,@RES_INCRECAL -- INSERTO VALORES FALTANTES EN TABLA FALTANTES
		DECLARE CF CURSOR FOR 
		SELECT GRASAFALTANTE,PROTEINAFALTANTE FROM FALTANTES -- SELECT DE TABLA FALTANTES
		OPEN  CF 
		FETCH NEXT FROM CF INTO  @GRASAFALTANTE,@PROTEINAFALTANTE
		CLOSE CF
		DEALLOCATE CF
		SET @RES_TGR=@GRASAFALTANTE;SET @RES_TPR=@PROTEINAFALTANTE
		SET @FALTA='1'
	END
--MIRO PRODUCCION
BEGIN
	EXECUTE  PR_CONTROL_FALTANTE_KILOS  @RES_GEN,@RES_NP,@RES_NC,@RES_KL -- INSERTO VALORES FALTANTES EN TABLA FALTANTES
	DECLARE CF CURSOR FOR 
	SELECT KILOSFALTANTE FROM FALTANTES -- SELECT DE TABLA FALTANTES
	OPEN  CF 
	FETCH NEXT FROM CF INTO  @KILOSFALTANTE
	CLOSE CF
	DEALLOCATE CF
	SET @RES_KL=@KILOSFALTANTE
END

      	SET @WKILOSi1 = @RES_KL
      	SET @wGRASAi1 = @RES_TGR * @RES_KL / 100
      	SET @PROTi1 = @RES_TPR * @RES_KL / 100
      	SET @wFECHAi1 = @RES_FCTR
    END --WHILE 2 .......................................................................................................................................................................................................................................................................................................................
      If @RES_GEN = @wID And @RES_AB = 'B' And   @RES_FBA IS NOT Null  --SI hay fsecado--->se hace con fsecado
	BEGIN--SI hay fsecado--->se hace con fsecado
	        SET @wFechaSecado = @RES_FBA
	        SET @WKILOS = @WKILOSi
	        SET @wGrasa = @wGRASAi
	        SET @wProt = @PROTi
	        SET @wDIAS = DateDiff(day, @wFECHAi, @RES_FBA)
	        SET @wDIASLac = DateDiff(day, @RES_FP, @RES_FBA)
	        SET @wTOTALKILOS = @wTOTALKILOS + (@WKILOS * @wDIAS)
	        SET @wTOTALGRASA = @wTOTALGRASA + (@wGrasa * @wDIAS)
	        SET @wTOTALPROT = @wTOTALPROT + (@wProt * @wDIAS)
	        SET @wFinalizada = 'S'

	        IF @wCALCULADO = 'N' 
		BEGIN
		          IF @wDIASLac > 305 
				BEGIN
			            SET @wTOTALKILOS305 = @wTOTALKILOS305 + (@WKILOS) * (@wDIASLac - 305)
			            SET @wTOTALGRASA305 = @wTOTALGRASA305 + (@wGrasa) * (@wDIASLac - 305)
			            SET @wTOTALPROT305 = @wTOTALPROT305 + (@wProt) * (@wDIASLac - 305)
			            SET @wCALCULADO = 'S'
				END
		         ELSE
				BEGIN
			            SET @wTOTALKILOS305 = @wTOTALKILOS
			            SET @wTOTALGRASA305 = @wTOTALGRASA
			            SET @wTOTALPROT305 = @wTOTALPROT
		          		END 
		END
		       -------GUARDAR LACTACION
			
			SET @TOTALRCS=POWER( @TOTALRCS ,(1/@CONT))
			INSERT INTO LACTACIONESTEMP(Establo,Res,COLLAR,NP,FPARTO,DIASLAC,KILOS,GRASA,PROTEINA,KILOS305,GRASA305,PROTEINA305,Fsecado,CausaSecado,Finalizada,FechaCalculada,RCS,usuario)
			VALUES (@RES_TIT,@wID,@wRES_COL,@wNP,@wFP,@wDIASLac, cast( @wTOTALKILOS as int) ,@wTOTALGRASA,@wTOTALPROT,cast(@wTOTALKILOS305 as int) ,@wTOTALGRASA305,@wTOTALPROT305,@wFechaSecado,@RES_CBA,'S',@wCALCULADO,@TOTALRCS,@usuario)
			PRINT 'DESPUES DE GUARDAR' + CAST(cast( @wTOTALKILOS as int) AS VARCHAR(50)) + '..' + CAST( cast(@wTOTALKILOS305 as int) AS VARCHAR(50))
			--cast( @wTOTALKILOS as int)        
		        FETCH NEXT FROM C1 INTO  @RES_TIT,@RES_GEN,@RES_COL,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_INCRECAL,@RES_FBA,@RES_CBA,@RES_FCTR,@RES_RCS
	
		        SET @wTOTALKILOS = 0;SET @wTOTALKILOS305 = 0;SET @wTOTALGRASA=0;SET @wTOTALGRASA305=0;SET @wTOTALPROT=0;SET @wTOTALPROT305=0;SET @TOTALRCS=1;SET @CONT=0
		        SET @WKILOSi = @RES_KL
		        SET @wGRASAi = @RES_TGR * ((@RES_KL) / 100)
		        SET @PROTi = @RES_TPR * (@RES_KL) / 100
		        SET @wFECHAi = @RES_FP
		        SET @WKILOSi1 = @RES_KL
		        SET @wGRASAi1 = @RES_TGR * ((@RES_KL) / 100)
		        SET @PROTi1 = @RES_TPR * (@RES_KL) / 100
		        SET @wFECHAi1 = @RES_FCTR
		        SET @wRES_TIT=@RES_TIT
		        SET @wID = @RES_GEN
		        SET @wNP = @RES_NP
		        SET @wFP = @RES_FP
		       SET @wCALCULADO='N'
		
	END--SI hay fsecado--->se hace con fsecado

     ELSE
	BEGIN--Si no hay fsecado----> fctr + 14.......vacaciones (+30) si dif>50..-> supongo que son vacaciones
          	If (@RES_GEN <> @wID Or @RES_NP <> @wNP) OR ( @RES_AB ='B')
		BEGIN--@RES_GEN <> @wID Or @RES_NP <> @wNP
	          SET @WKILOS = @WKILOSi
	          SET @wGrasa = @wGRASAi
	          SET @wProt = @PROTi
	          SET @wDIAS = 14
	          SET @wFechaSecado = DateAdd(day, 14, @wFECHAi)
	          SET @wTOTALKILOS = @wTOTALKILOS + (@WKILOS * @wDIAS)
	          SET @wTOTALGRASA = @wTOTALGRASA + (@wGrasa * @wDIAS)
	          SET @wTOTALPROT = @wTOTALPROT + (@wProt * @wDIAS)
	          SET @wDIASLac = DateDiff(day, @wFP, @wFECHAi) + 14
	          SET @wFinalizada = 'N'
	          	If @wCALCULADO = 'N' 
			BEGIN
		            If @wDIASLac > 305 
				BEGIN
		              		SET @wTOTALKILOS305 = @wTOTALKILOS305 + (@WKILOSi) * (305 - DateDiff(DAY, @wFP, @wFECHAi))
		              		SET @wTOTALGRASA305 = @wTOTALGRASA305 + (@wGRASAi) * (305 - DateDiff(DAY, @wFP, @wFECHAi))
		              		SET @wTOTALPROT305 = @wTOTALPROT305 + (@PROTi) * (305 - DateDiff(DAY, @wFP, @wFECHAi))
				END
		            Else
				BEGIN
			              SET @wTOTALKILOS305 = @wTOTALKILOS
			              SET @wTOTALGRASA305 = @wTOTALGRASA
			              SET @wTOTALPROT305 = @wTOTALPROT
		            	END 
	          	END
		
		SET @TOTALRCS=POWER( @TOTALRCS ,(1/@CONT))
            		INSERT INTO LACTACIONESTEMP(Establo,Res,COLLAR,NP,FPARTO,DIASLAC,KILOS,GRASA,PROTEINA,KILOS305,GRASA305,PROTEINA305,Fsecado,CausaSecado,Finalizada,FechaCalculada,RCS,usuario)
		VALUES (@wRES_TIT,@wID,@wRES_COL,@wNP,@wFP,@wDIASLac,cast(@wTOTALKILOS as int),@wTOTALGRASA,@wTOTALPROT,cast(@wTOTALKILOS305 as int),@wTOTALGRASA305,@wTOTALPROT305,@wFechaSecado,@RES_CBA,'N','S',@TOTALRCS,@usuario)
		
		
			IF (@RES_GEN =@wID)
				BEGIN
					FETCH NEXT FROM C1 INTO  @RES_TIT,@RES_GEN,@RES_COL,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_INCRECAL,@RES_FBA,@RES_CBA,@RES_FCTR,@RES_RCS
				END
		       
	 SET @wTOTALKILOS = 0; SET @wTOTALKILOS305 = 0;SET @wTOTALGRASA=0;SET @wTOTALGRASA305=0;SET @wTOTALPROT=0;SET @wTOTALPROT305=0;SET @TOTALRCS=1;SET @CONT=0
            	SET @WKILOSi = @RES_KL
            	SET @wGRASAi = @RES_TGR * @RES_KL / 100
            	SET @PROTi = @RES_TPR * @RES_KL / 100
            	SET @wFECHAi = @RES_FP
            	SET @WKILOSi1 = @RES_KL
            	SET @wGRASAi1 = @RES_TGR * @RES_KL / 100
            	SET @PROTi1 = @RES_TPR * @RES_KL / 100
            	SET @wFECHAi1 = @RES_FCTR
	SET @wRES_TIT=@RES_TIT
            	SET @wID = @RES_GEN
            	SET @wNP = @RES_NP
            	SET @wFP = @RES_FP
  	SET @wCALCULADO='N'
	         
	      END--@RES_GEN <> @wID Or @RES_NP <> @wNP
	END--Si no hay fsecado----> fctr + 14.......vacaciones (+30) si dif>50..-> supongo que son vacaciones

END --WHILE1

CLOSE C1
DEALLOCATE C1

update lactacionesTEMP --si la vaca ya no esta en el establo es que está finalizada
set finalizada='S'
from maestro
where finalizada='N'
and establo=maes_tit
and res=maes_gen
and maes_estado='0'
AND (MAES_FBA<=@datFPrev OR MAES_FBajaCenso<=@datFPrev)