
CREATE PROCEDURE [dbo].[PR_REMESASANIMALESK]
( @REMESA CHAR(10),@ESTABLO char(7),@DESDE SMALLDATETIME,@Usuario varchar(50) )
AS
--SET @REMESA ='R1'
--SET @ESTABLO=''
--SET @DESDE='30/06/2012'

	
	
DECLARE @WESTABLO AS CHAR(7)

IF @ESTABLO='*'
BEGIN
	DECLARE C1 CURSOR FOR 
	SELECT CODGAN FROM REGGEN..GANADERIAS_MAESTRO 
	WHERE LEFT(CODGAN,3)='K94'
	AND CODENV<3
END
ELSE
BEGIN
	DECLARE C1 CURSOR FOR 
	SELECT CODGAN FROM REGGEN..GANADERIAS_MAESTRO 
	WHERE LEFT(CODGAN,3)='K94'
	AND CODENV<3
	AND CODGAN=@ESTABLO
END
OPEN  C1

FETCH NEXT FROM C1 INTO @WESTABLO
WHILE (@@FETCH_STATUS=0)
BEGIN
	
	INSERT INTO REMESASD (REMESA,ESTABLO,VACA,NOMBRE,FN)
	SELECT @REMESA,@WESTABLO,vaca,nombre,FN FROM TMP_CensoAFCA --' --No voy a TMP_PruebaAFCA pq no puedo saber el histórico de K's
	WHERE ESTABLO=@WESTABLO
	and LEFT(establo,3)='K94'
	AND usuario =@Usuario
		
FETCH NEXT FROM C1 INTO @WESTABLO
END--DEL WHILE

CLOSE C1
DEALLOCATE C1
--borro las que tienen<24 meses
DELETE FROM REMESASD
WHERE REMESA=@REMESA
AND DATEDIFF(DAY,FN,@DESDE)<730


-- si el sanitario es null -> voy a buscar el sanitario por el nombre a Vacas
update remesasD
set vaca=codigoidentif,
FN=FechaNacimiento
from reggen..vacas
where nomcria=nombre
AND establo=codigoganaderia
and vaca='XXXXXXXXXXXXXX'

--ACTUALIZO DATOS BANCARIOS
UPDATE REMESASD
SET BANCO=G.BANCOCUOTA,
SUCURSAL=G.SUCURSALCUOTA,
DC=G.DCCUOTA,
CUENTA=G.CUENTACUOTA,
formapago=G.formapago
FROM REGGEN..GANADERIAS_MAESTRO G
WHERE ESTABLO=CODGAN	
AND remesa=@remesa

--ACTUALIZO ESTADO--pongo todos los de FP=0:banco como pasados al cobro
update remesasD
set estado='1'
where remesa=@remesa
--ACTUALIZO ESTADO--pongo todos los de FP=1:caja como devueltos(de momento pdtes.)
update remesasD
set estado='0'
where remesa=@remesa
and formapago='1'--caja

-- ACTUALIZO BANCOS NULL A 0000
UPDATE REMESASD
SET BANCO='0000',
SUCURSAL='0000',
DC='00',
CUENTA='0000000000'
WHERE  remesa=@remesa
AND BANCO IS NULL
	
	
	