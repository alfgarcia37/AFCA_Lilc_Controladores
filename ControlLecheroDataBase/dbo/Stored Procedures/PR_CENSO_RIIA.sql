-- Create date: 23/05/2013
-- Cotejar las terneras que tenemos en AFCA en RIIA con las que tenemos en CL
CREATE PROCEDURE [dbo].[PR_CENSO_RIIA]
	@Establo char(7),@DESDE SMALLDATETIME,@DIAS INT
AS
DECLARE @EstabloCONAFE CHAR(7),@ID CHAR(14),@EstabloCEA CHAR(14),@EstabloRecria CHAR(14), @FSalida as smalldatetime,@vaca char(14)
--SET @@EstabloCONAFE='J390029'
--SET @DESDE='30/06/2013'
--SET @USUARIO='ALFONGAR'
--set @DIAS=180
IF @ESTABLO='*'
	BEGIN
		TRUNCATE TABLE TMP_CensoRIIA
		DECLARE C1 CURSOR FOR 
		--SELECT ESTABLO FROM HISTORICOGANADERIAS WHERE FBAJA IS NULL
		SELECT codgan FROM  REGGEN..GANADERIAS_MAESTRO
		where codenv<>'3' and left(codgan,1) in ('J','K')
	END
ELSE
	BEGIN
		DELETE FROM TMP_CensoRIIA
		WHERE ESTABLO=@Establo
		DECLARE C1 CURSOR FOR 
		--SELECT ESTABLO FROM HISTORICOGANADERIAS WHERE FBAJA IS NULL
		SELECT codgan FROM  REGGEN..GANADERIAS_MAESTRO
		where codenv<>'3' and left(codgan,1) in ('J','K')
		AND codgan=@Establo
	END
OPEN  C1

FETCH NEXT FROM C1 INTO @EstabloCONAFE
WHILE (@@FETCH_STATUS=0)
BEGIN
	INSERT INTO TMP_CENSORIIA  (ESTABLO,vaca,FAltaRIIA,FBajaRIIA,FN,FCalculo,MesesCalculo)
	select distinct @EstabloCONAFE,convert(char(14),E.VACA),Fentrada,Fsalida,FN,@desde,@DIAS
	from RIIAREMO..RIIA_EXPLOTACION E,RIIAREMO..RIIA_ANIMALES A
	where E.ESTABLOCONAFE=@EstabloCONAFE
	and (@DESDE BETWEEN Fentrada and Fsalida OR(@DESDE>Fentrada and Fsalida is null))
	and E.VACA=A.VACA
	
	FETCH NEXT FROM C1 INTO @EstabloCONAFE
END
CLOSE C1 
DEALLOCATE C1 

-- inserto las de las nieves
IF @ESTABLO='*'
begin
	delete from riiaremo..lasnieves
	DECLARE CR CURSOR LOCAL SCROLL OPTIMISTIC FOR
	SELECT  ESTABLOCONAFE,ESTABLORECRIA,CEACONAFE	FROM [RIIAREMO].[dbo].[CentrosRecria]
	OPEN CR 
	FETCH NEXT FROM CR INTO @EstabloCONAFE,@EstabloRecria,@EstabloCEA
end
else
begin
	delete from riiaremo..lasnieves where establoconafe=@EstabloCONAFE
	DECLARE CR CURSOR LOCAL SCROLL OPTIMISTIC FOR
	SELECT  ESTABLOCONAFE,ESTABLORECRIA,CEACONAFE	FROM [RIIAREMO].[dbo].[CentrosRecria]
	where establoconafe=@EstabloCONAFE
	OPEN CR 
	FETCH NEXT FROM CR INTO @EstabloCONAFE,@EstabloRecria,@EstabloCEA
end

WHILE (@@FETCH_STATUS=0)
BEGIN--LAS NIEVES
	insert into riiaremo..LASNIEVES(ESTABLOCONAFE,VACA,FENTRADA,CEACONAFE,CEARECRIA)
	SELECT distinct @EstabloCONAFE,id_animal,mo_fc_lle,mo_id_eori,mo_id_edes
	 FROM RIIAREMO..RIIA_explotacion,[V_SITRANC].[remo].[dbo].[T_MOV_CANTABRIA] TMOV ,[V_SITRANC].[remo].[dbo].[T_MOVIND_CANTABRIA]
	 where mo_id_eori=@EstabloCEA AND mo_id_edes=@EstabloRecria
	 AND VACA COLLATE Modern_Spanish_CI_AS=id_animal
	 AND FSALIDA=CONVERT(varchar(10), mo_fc_lle, 103)
	 AND mo_fc_lle>'01/01/2011'
	 and movimiento=TMOV.identificador

	--hago esto en vez de update opq tarda menos
	insert into riiaremo..LASNIEVES(ESTABLOCONAFE,VACA,Fsalida,CEACONAFE,CEARECRIA)
	SELECT distinct @EstabloCONAFE,id_animal,mo_fc_sal,mo_id_eori,@EstabloRecria
	 FROM RIIAREMO..RIIA_explotacion,[V_SITRANC].[remo].[dbo].[T_MOV_CANTABRIA] TMOV ,[V_SITRANC].[remo].[dbo].[T_MOVIND_CANTABRIA]
	 where mo_id_eori=@EstabloRecria AND mo_id_edes=@EstabloCEA
	 AND VACA COLLATE Modern_Spanish_CI_AS=id_animal
	 AND Fentrada=CONVERT(varchar(10), mo_fc_sal, 103)
	 AND mo_fc_lle>'01/01/2011'
	 --04/08/2017 lo quito por problema con vacas del J391506
		--AND mo_fc_lle>DATEADD(MONTH,-32,GETDATE())
	 --
	 and movimiento=TMOV.identificador
	
	 DECLARE C1 CURSOR LOCAL SCROLL OPTIMISTIC FOR
		SELECT vaca,FSalida	FROM riiaremo..lasnieves where fentrada is null
		OPEN C1 
		FETCH NEXT FROM C1 INTO @vaca,@FSalida
		WHILE (@@FETCH_STATUS=0)
		begin
		update riiaremo..lasnieves
		set fsalida=@FSalida
		where vaca=@vaca and fentrada is not null
		FETCH NEXT FROM C1 INTO @vaca,@FSalida
		end
	close c1
	deallocate c1
--ahora tengo que eliminar las que salieron<@desde hacia otra explotación
	delete  riiaremo..lasnieves
	FROM RIIAREMO..lasnieves,[V_SITRANC].[remo].[dbo].[T_MOV_CANTABRIA] TMOV ,[V_SITRANC].[remo].[dbo].[T_MOVIND_CANTABRIA]
	--Ñapa 0097 19/01/2018... Puede salir de un "centro de recria" a otro "centro de recria=
		--where mo_id_eori=@EstabloRecria AND mo_id_edes<>@EstabloCEA
	
	where mo_id_eori=@EstabloRecria AND mo_id_edes<>(Select EstabloRecria COLLATE Modern_Spanish_CI_AS from RIIAREMO..CentrosRecria where CEACONAFE=@EstabloCEA)
	AND VACA COLLATE Modern_Spanish_CI_AS=id_animal
	AND mo_fc_lle>DATEADD(MONTH,-32,GETDATE())
	and movimiento=TMOV.identificador
 
	 
 --
 -- AHORA TENGO QUE METER EN TMP_CENSORIIA las que estában en las nieves en esa fecha o las que han ido y todavia no han vuelto
	INSERT INTO CONTROLLECHERO..TMP_CENSORIIA(ESTABLO,vaca,FCalculo,MesesCalculo)
	select  l.ESTABLOCONAFE,l.VACA,@DESDE,6
	from RIIAREMO..LASNIEVES l,riiaremo..riia_animales a
	where l.ESTABLOCONAFE=@EstabloCONAFE
	and (@DESDE BETWEEN Fentrada and Fsalida OR(@DESDE>Fentrada and Fsalida is null))
	and l.vaca=a.vaca
	and DATEDIFF(month, FN, @DESDE)<30-- si han pasado 30 meses se entiende que salio de las nieves a otro sitio
FETCH NEXT FROM CR INTO @EstabloCONAFE,@EstabloRecria,@EstabloCEA
END --DEL BEGIN DE LAS NIEVES
close CR
deallocate CR

 delete from riiaremo..lasnieves where Fentrada is null
 delete from riiaremo..lasnieves where Fsalida  is null and DATEDIFF(month, Fentrada, @DESDE)>30 -- no es normal que desde que lleguen no paran en 30. Se las mata
------------------------------------------------------------------------------
--actualizo FN
update TMP
set tmp.FN=a.fn
from TMP_CensoRIIA tmp,riiaremo..riia_animales a
where tmp.vaca=a.vaca
-- Elimino las que tengan <x meses. Lo hago aquí pq si filtro las de SITRANC, me trae todas las filas y tarda mucho
delete from TMP_CensoRIIA
where DATEDIFF(day, FN, @DESDE)<@DIAS
-----------
update TMP_CensoRIIA
set nombre=reggen..animales.nombre
from reggen..animales 
where Vaca=codigoidentif

update TMP_CensoRIIA
set nombre='XXX'
where nombre is null
--ACTUALIZO POR EL NOMBRE PQ HAY VACAS VIEJAS CON ESPH
update TMP_CensoRIIA
set FN=cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)),
VACA=(PAIS+SEXO+PROANI+NUMERO)
from REGGEN..ANIMALES A 
where TMP_CensoRIIA.NOMBRE=A.NOMBRE
AND (ESTABLO=A.GANCRI OR ESTABLO=A.GANPRO)
AND VACA IS NULL
--actualizo el sanitario que sea NULL a XXXXXXXXXXXXXX
update TMP_CensoRIIA
set vaca='XXXXXXXXXXXXXX',
FN='00:00:00'
where vaca is null
-- si el sanitario es null -> voy a buscar el sanitario por el nombre a Vacas
update TMP_CensoRIIA
set vaca=codigoidentif,
FN=FechaNacimiento
from reggen..vacas
where nomcria=nombre
AND establo=codigoganaderia
and vaca='XXXXXXXXXXXXXX'

update tmp_CENSOriia
SET CONTROLADOR=REPLICATE('0', (3 - LEN(convert(varchar(3) ,ASIGNADO)))) + convert(varchar(3),ASIGNADO)
FROM LECTURASCONTROL L
WHERE tmp_CENSOriia.ESTABLO=L.ESTABLO
AND FBAJA IS NULL
--POR SI HUBIESE BRITANICAS (Consejeria trabaja con UK. Nosotros con GB)
UPDATE [controllechero].[dbo].[TMP_CensoRIIA]
SET VACA='GB' + SUBSTRING(VACA,3,12)
WHERE LEFT(VACA,2)='GB'

--AHORA TENGO QUE BORRAR LAS DE RIIA QUE ESTAN EN LA TABLA DE VACASNORIIA
DELETE  TMP_CENSORIIA 
FROM VACASNORIIA V,TMP_CENSORIIA  T
WHERE T.ESTABLO=V.ESTABLO
AND T.VACA=V.VACA
--AHORA TENGO QUE BORRAR LAS  QUE ESTAN DE BAJA EN EL LIBRO
DELETE  TMP_CENSORIIA 
FROM [reggen].[dbo].[BAJAS DEL LIBRO] B,TMP_CENSORIIA  T
WHERE T.ESTABLO=B.GANPRO
AND T.VACA=B.CODIGOIDENTIF

--delete from TMP_Censoriia where establo='J390769'