





CREATE PROCEDURE [dbo].[PR_CheckLactaciones] 
(@strESTABLO CHAR(7))
AS

DECLARE @wRES_TIT CHAR(7),@wID CHAR(14), @wNP INT, @wNC INT, @wFP SMALLDATETIME, @wFC SMALLDATETIME
DECLARE @WKILOSi REAL, @WKILOSi1 REAL, @wGRASAi REAL, @wGRASAi1 REAL, @PROTi REAL, @PROTi1 REAL, @wPR REAL,@wCBA As CHAR(2)
DECLARE @WKILOSGrasa REAL, @WKILOSProt REAL
DECLARE @wTOTALKILOS REAL, @wTOTALKILOS305 REAL, @wTOTALGRASA REAL, @wTOTALPROT REAL, @wTOTALGRASA305 REAL, @wTOTALPROT305 REAL, @wDIASLac INT
DECLARE @wDIAS INT, @wFECHAi SMALLDATETIME, @wFechaSecado SMALLDATETIME
DECLARE @wFECHAi1 SMALLDATETIME, @wAB CHAR(1), @wFinalizada CHAR(1)
DECLARE @WKILOS REAL, @wGrasa REAL, @wProt REAL, @wCALCULADO CHAR(1)
DECLARE @RES_TIT CHAR(7),@RES_GEN CHAR(14),@RES_FP SMALLDATETIME,@RES_NP INT,@RES_NC INT
DECLARE @RES_AB CHAR(1),@RES_KL REAL,@RES_TGR REAL,@RES_TPR REAL,@RES_FBA SMALLDATETIME,@RES_CBA CHAR(2),@RES_FCTR SMALLDATETIME

SET @strESTABLO='J390029'

-- BORRO LACTACIOINES------------
DELETE LACTACIONES
WHERE ESTABLO=@strESTABLO
AND FPARTO>'01/01/06'

DECLARE C1 cursor for 
SELECT RES_TIT,RES_GEN,RES_FP,RES_NP,RES_NC,RES_AB,(RES_KLAM+RES_KLPM),RES_TGR,RES_TPR,RES_FBA,RES_CBA,RES_FCTR 
FROM CONTROL
WHERE 
RES_TIT=@strESTABLO AND
--RES_GEN='ESPH0201795756' AND
RES_FP BETWEEN '01/01/2006' AND '31/12/2007'
--RES_FP<='01/02/05'
--and res_gen='ESPH3101211740'and (((RES_KLAM + RES_KLPM)<>0 and RES_AB='A') OR ((RES_KLAM + RES_KLPM)=0 and RES_AB='B'))
UNION
SELECT TOP 1  'TTT','ZZZ','01/01/2000',0,0,'V',0,0,0,'01/01/2000','00','01/01/2000' FROM CONTROL
ORDER BY RES_GEN,RES_NP,RES_NC

OPEN  C1 
FETCH NEXT FROM C1 INTO  @RES_TIT,@RES_GEN,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_FBA,@RES_CBA,@RES_FCTR
--rcsDatos.MoveFirst
SET @WKILOSi = @RES_KL
SET @wGRASAi = @RES_TGR * (@RES_KL / 100)
SET @PROTi = @RES_TPR * (@RES_KL) / 100
SET @wFECHAi = @RES_FP
SET @WKILOSi1 = @RES_KL
SET @wGRASAi1 = @RES_TGR *@RES_KL/ 100
SET @PROTi1 = @RES_TPR * @RES_KL / 100
SET @wFECHAi1 = @RES_FCTR
SET @wRES_TIT=@RES_TIT
SET @wID = @RES_GEN
SET @wNP = @RES_NP
SET @wFP = @RES_FP
SET @wCALCULADO = 'N'
SET @wTOTALKILOS =0;SET @wTOTALGRASA =0;SET @wTOTALPROT =0;SET @wTOTALKILOS305 =0;SET @wTOTALGRASA305 =0;SET @wTOTALPROT305 =0
WHILE (@@FETCH_STATUS=0 AND @RES_GEN<>'ZZZ') --WILE 1
BEGIN
  WHILE (@RES_GEN = @wID And @RES_AB = 'A' And @RES_NP = @wNP) --WHILE 2
	BEGIN--WHILE 2
	     --=======================KILOS NATURAL==========================================================================
	      SET @WKILOS = (@WKILOSi + @WKILOSi1) / 2
	      SET @wGrasa = (@wGRASAi + @wGRASAi1) / 2
	      SET @wProt = (@PROTi + @PROTi1) / 2
	      SET @wDIAS = DateDiff(DAY, @wFECHAi, @wFECHAi1)
	      SET @wTOTALKILOS = @wTOTALKILOS + (@WKILOS * @wDIAS)
	      SET @wTOTALGRASA = @wTOTALGRASA + (@wGrasa * @wDIAS)
	      SET @wTOTALPROT = @wTOTALPROT + (@wProt * @wDIAS)
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
      	FETCH NEXT FROM C1 INTO  @RES_TIT,@RES_GEN,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_FBA,@RES_CBA,@RES_FCTR

      	SET @WKILOSi1 = @RES_KL
      	SET @wGRASAi1 = @RES_TGR * @RES_KL / 100
      	SET @PROTi1 = @RES_TPR * @RES_KL / 100
      	SET @wFECHAi1 = @RES_FCTR
    END --WHILE 2
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
			INSERT INTO LACTACIONES(Establo,Res,NP,FPARTO,DIASLAC,KILOS,GRASA,PROTEINA,KILOS305,GRASA305,PROTEINA305,Fsecado,CausaSecado,Finalizada,FechaCalculada)
			VALUES (@RES_TIT,@wID,@wNP,@wFP,@wDIASLac,@wTOTALKILOS,@wTOTALGRASA,@wTOTALPROT,@wTOTALKILOS305,@wTOTALGRASA305,@wTOTALPROT305,@wFechaSecado,@RES_CBA,'S',@wCALCULADO)
		        
		        FETCH NEXT FROM C1 INTO  @RES_TIT,@RES_GEN,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_FBA,@RES_CBA,@RES_FCTR
	
		        SET @wTOTALKILOS = 0;SET @wTOTALKILOS305 = 0;SET @wTOTALGRASA=0;SET @wTOTALGRASA305=0;SET @wTOTALPROT=0;SET @wTOTALPROT305=0
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
            	INSERT INTO LACTACIONES(Establo,Res,NP,FPARTO,DIASLAC,KILOS,GRASA,PROTEINA,KILOS305,GRASA305,PROTEINA305,Fsecado,CausaSecado,Finalizada,FechaCalculada)
		VALUES (@wRES_TIT,@wID,@wNP,@wFP,@wDIASLac,@wTOTALKILOS,@wTOTALGRASA,@wTOTALPROT,@wTOTALKILOS305,@wTOTALGRASA305,@wTOTALPROT305,@wFechaSecado,@RES_CBA,'N','S')
	
			IF (@RES_GEN =@wID)
				BEGIN
					FETCH NEXT FROM C1 INTO  @RES_TIT,@RES_GEN,@RES_FP,@RES_NP,@RES_NC,@RES_AB,@RES_KL,@RES_TGR,@RES_TPR,@RES_FBA,@RES_CBA,@RES_FCTR
				END
		       
	 SET @wTOTALKILOS = 0; SET @wTOTALKILOS305 = 0;SET @wTOTALGRASA=0;SET @wTOTALGRASA305=0;SET @wTOTALPROT=0;SET @wTOTALPROT305=0
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




