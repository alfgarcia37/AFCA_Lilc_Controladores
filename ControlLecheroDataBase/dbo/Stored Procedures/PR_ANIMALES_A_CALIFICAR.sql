CREATE PROCEDURE [dbo].[PR_ANIMALES_A_CALIFICAR](@wEstablo CHAR(7),@wFP as smalldatetime,@wUSUARIO VARCHAR(50))
--USARE LA TABLA MAESTROTEMP PARA OBTENER TODOS LOS ANIMALES
	
AS
	
DECLARE @MAES_TIT CHAR(7),@MAES_GEN CHAR(14),@CONTROLADOR INT,@DIB CHAR(14)
DECLARE @TIT CHAR(7),@GEN CHAR(14),@NP INT,@NC INT,@FP SMALLDATETIME

DELETE FROM MAESTROTEMP WHERE USUARIO=@wUSUARIO

--AHORA ACTUALIZO LOS VALORES DE TODAS LAS TABLAS
IF @wEstablo<>''
BEGIN
	Insert into MaestroTemp(Maes_GEN ,Maes_NOM ,Maes_FP,Maes_NP,Maes_Nc,Maes_Tit,Maes_Col,Maes_Estado,Maes_FBA,Maes_FN,Maes_FAE,Maes_Padre,Maes_Madre,Maes_Raza,Maes_Usuario,Maes_fUsuario,Maes_Sanitario,USUARIO) 
	Select Maes_GEN,Maes_NOM,Maes_FP,Maes_NP,Maes_Nc,Maes_Tit,Maes_Col,Maes_Estado,Maes_FBA,Maes_FN,Maes_FAE,Maes_Padre,Maes_Madre,Maes_Raza,Maes_Usuario,Maes_fUsuario,NULL,@wUSUARIO
	 from Maestro,Reggen..Ganaderias_maestro 
	WHERE MAES_FP<=@wFP  AND MAES_FP>='01/01/2009'
	AND  MAES_RAZA='11' AND  maes_estado<>'0' ---se incluyen Secas por si paren antes de que califique
	and Maes_tit=codgan
	AND MAES_TIT=@WESTABLO
	--AHORA METO LAS TERNERAS>20 MESES SIN PARIR
	insert into MaestroTemp(Maes_GEN ,Maes_NOM ,Maes_FP,Maes_NP,Maes_Nc,Maes_Tit,Maes_Col,Maes_Estado,Maes_FBA,Maes_FN,Maes_FAE,Maes_Padre,Maes_Madre,Maes_Raza,Maes_Usuario,Maes_fUsuario,Maes_Sanitario,USUARIO) 
	SELECT  CODIGOIdentif,NOMBRE,NULL,0,0,GANPRO,0,0,NULL,cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)),NULL,NUPAD1,NUMAD1,'11',@wUSUARIO,NULL,CODIGOIDENTIF,@wUSUARIO
	FROM RegGen..ANIMALES
	Where GANPRO = @wEstablo
	and NUMLAC=0 and AÑOBAJ=0
	and Sexo='H'
	and DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), getdate())>=20
	and DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), getdate())<60
	and CODIGOIdentif  not in(select Maes_Gen from maestro where Maes_tit=@wEstablo)
END
ELSE
BEGIN
	Insert into MaestroTemp(Maes_GEN ,Maes_NOM ,Maes_FP,Maes_NP,Maes_Nc,Maes_Tit,Maes_Col,Maes_Estado,Maes_FBA,Maes_FN,Maes_FAE,Maes_Padre,Maes_Madre,Maes_Raza,Maes_Usuario,Maes_fUsuario,Maes_Sanitario,USUARIO) 
	Select Maes_GEN,Maes_NOM,Maes_FP,Maes_NP,Maes_Nc,Maes_Tit,Maes_Col,Maes_Estado,Maes_FBA,Maes_FN,Maes_FAE,Maes_Padre,Maes_Madre,Maes_Raza,Maes_Usuario,Maes_fUsuario,NULL,@wUSUARIO
	 from Maestro,Reggen..Ganaderias_maestro 
	WHERE MAES_FP<=@wFP  AND MAES_FP>='01/01/2009'
	AND  MAES_RAZA='11' AND  maes_estado<>'0'
	and Maes_tit=codgan
	--AND MAES_TIT='J390452'


END	
	


Update MAESTROTEMP 
SET MAES_RAZA=NULL
where usuario= @wUSUARIO

Update MAESTROTEMP 
Set Maes_Sanitario = codigoidentif ,
maes_esph=  (PAIS+SEXO+PROANI+NUMERO) ,
MAES_RAZA=PUREZA -- USO LA RAZA PARA METER LA PUREZA
from reggen..animales 
where usuario= @wUSUARIO and maes_tit=ganpro 
and (maes_gen = (PAIS+SEXO+PROANI+NUMERO) OR maes_gen =CODIGOIDENTIF) 
--PONGO LOS VALORES Q ESTEN A NULL DE MAES_SANITARIO CON ID
UPDATE MAESTROTEMP 
SET MAES_SANITARIO =MAES_GEN
WHERE USUARIO= @wUSUARIO
AND MAES_SANITARIO IS NULL

UPDATE MAESTROTEMP 
SET MAES_COL =0
WHERE USUARIO= @wUSUARIO
AND MAES_COL IS NULL

UPDATE MAESTROTEMP 
SET MAES_NC =0
WHERE USUARIO= @wUSUARIO
AND MAES_NC IS NULL

--AHORA DEBO BORRAR LAS QUE YA SE HAYAN CALIFICADO, PORQUE NO ME INTERESAN

DELETE   MAESTROTEMP
FROM  REGGEN..CALIFICACIONES
WHERE  MAES_ESPH = (PAIS+SEXO+PROANI+NUMERO)  


/*
DECLARE C1 CURSOR  FOR
	SELECT MAES_GEN
	FROM MAESTROTEMP
	WHERE MAES_USUARIO=@wUSUARIO
	
	OPEN C1
	FETCH NEXT FROM C1 INTO @MAES_GEN
	
	WHILE ( @@FETCH_STATUS=0)
	BEGIN
		--ACTUALIZO MAESTRO
			UPDATE MAESTRO
			SET MAES_GEN=@MAES_GEN
			WHERE MAES_GEN=@DIB
		
		
		FETCH NEXT FROM C1 INTO @MAES_TIT
	END


CLOSE C1
DEALLOCATE C1
*/