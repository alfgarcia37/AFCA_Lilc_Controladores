
-- Create date: 23/05/2013
-- Cotejar las terneras que tenemos en AFCA en RIIA con las que tenemos en CL
CREATE PROCEDURE [dbo].[PR_CENSO_AFCA]
	@Establo VARchar(7),@DESDE varchar(10),@DIAS INT,@NombreTabla varchar(500),@usuario varchar(50)
AS
DECLARE @query VARCHAR (1200)
/*
DECLARE @establo CHAR(14),@DESDE smalldatetime,@usuario varchar(50)
set @query=''
--SET @ESTABLO='J390029'
SET @DESDE='01/01/2015'
set @DIAS=180

--de vez en cuando ejecutar PR_Historico_Ganaderias
*/
--set @NombreTabla='TMP_pruebaAfca'
--set @NombreTabla='TMP_CensoAFCA'
IF @ESTABLO='*'
	BEGIN
		set @query ='DELETE FROM ' + @NombreTabla
		set @query =@query + ' WHERE usuario=''' + @usuario +''''
		exec(@query)
			IF @NombreTabla='TMP_pruebaAfca' -- PARA RECIBOS O CENSOS DE INFORME SANTOS
			BEGIN
				DECLARE C1 CURSOR FOR 
				SELECT ESTABLO FROM [controllechero].[dbo].[HistoricoGanaderias]
				where ((falta < @DESDE and fbaja is null) or (falta < @DESDE and fbaja >@DESDE))
			END
			ELSE
			BEGIN
				DECLARE C1 CURSOR FOR 
				SELECT codgan FROM  REGGEN..GANADERIAS_MAESTRO -- PARA CENSO AFCA-RIIA
				where codenv<>'3' and left(codgan,1) in ('J','K')
			END
	END
ELSE
	BEGIN
		set @query ='DELETE FROM ' + @NombreTabla + ' WHERE ESTABLO=''' + @ESTABLO +''''
		set @query =@query + ' AND usuario=''' + @usuario +''''
		exec(@query)
		print 'query: ' + @query
		
		IF @NombreTabla='TMP_pruebaAfca' -- PARA RECIBOS O CENSOS DE INFORME SANTOS
			BEGIN
				DECLARE C1 CURSOR FOR 
				SELECT ESTABLO FROM [controllechero].[dbo].[HistoricoGanaderias]
				where ((falta < @DESDE and fbaja is null) or (falta < @DESDE and fbaja >@DESDE))
				AND establo=@ESTABLO
			END
			ELSE
			BEGIN
				DECLARE C1 CURSOR FOR 
				SELECT codgan FROM  REGGEN..GANADERIAS_MAESTRO -- PARA CENSO AFCA-RIIA
				--where codenv<>'3' and left(codgan,1) in ('J','K')
				where codgan=@ESTABLO
		END
	END
OPEN  C1

FETCH NEXT FROM C1 INTO @ESTABLO
WHILE (@@FETCH_STATUS=0)
BEGIN
-- OVI MACHOS 
-- SEXO=''M''' = M

---
-- 1. SOLO LOS QUE ENTRARON EN VACAS CON ANTERIORIDAD A FECHAMOVIMIENTO Y TENIAN>=24 MESES 
set @query ='INSERT INTO ' + @NombreTabla + ' (ESTABLO,VACA,NOMBRE,USUARIO)'
--set @query ='INSERT INTO ' + @NombreTabla + ' (ESTABLO,VACA,NOMBRE,FCALCULO,MESESCALCULO)'
set @query =@query + ' SELECT ''' + @ESTABLO +''',CODIGOIDENTIF,NOMBRE,''' + @usuario +''' FROM REGGEN..ANIMALES '
set @query =@query + ' WHERE ganpro=''' + @Establo +''''
set @query =@query + ' AND  SEXO=''H'''
set @query =@query + ' AND DATEDIFF(DAY,CAST(cast((CAST(DIANA1 AS VARCHAR(2)) + ''/'' +CAST(MESNA1 AS VARCHAR(2)) + ''/'' + CAST(AÑONA1 AS VARCHAR(4))) as varchar(12)) AS smalldatetime),cast('''  + @DESDE + ''' as smalldatetime))>=' + cast(@DIAS as varchar(4)) 
  
exec(@query)
print 'altas dadas...' + @query

--2 ALTAS'01' O '07':COMPRADAS A OTRO ESTABLO CON FMOV<FX(RESTO AL CENSO)
--EXISTE EN ANIMALES PERO NO TENDRIA QUE ESTAR
set @query =		'DELETE FROM ' + @NombreTabla 
set @query =@query +' WHERE ESTABLO=''' + @ESTABLO +''''
set @query =@query +' AND VACA IN ('
set @query =@query +' SELECT  CODIGOIDENTIF ''altas'' FROM REGGEN..VACAS'
set @query =@query +' WHERE FECHAMOVIMIENTO> cast(''' + @DESDE +''' as smalldatetime)'
set @query =@query +' AND (((CODIGOGANADERIA=''' + @Establo +''' AND  TIPOMOVIMIENTO IN(''01'',''02'',''07''))'
set @query =@query +' 	  OR (TIPOMOVIMIENTO=''03'' AND CODIGOCOMPRADOR=''' + @Establo +''' )'
set @query =@query +'		   AND DATEDIFF(DAY, FECHANACIMIENTO,cast(''' + @DESDE + ''' as smalldatetime))>=' + cast(@DIAS as varchar(4)) +')'

print 'fon... ' +  @query
--                        ..... tiene que haber 2 ')' al final. uno para cerrar la linea sgte. y otro para la lin de arriba
set @query =@query +' AND CODIGOIDENTIF   IN  (SELECT CODIGOIDENTIF FROM REGGEN..ANIMALES'
set @query =@query +' WHERE ganpro=''' + @Establo +''' AND  SEXO=''H'''
set @query =@query + ' AND DATEDIFF(DAY,CAST(cast((CAST(DIANA1 AS VARCHAR(2)) + ''/'' +CAST(MESNA1 AS VARCHAR(2)) + ''/'' + CAST(AÑONA1 AS VARCHAR(4))) as varchar(12)) AS smalldatetime),cast('''  + @DESDE + ''' as smalldatetime))>='  + cast(@DIAS as varchar(4)) +')))' 
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)
print '2 bajas dadas...' + @query

--3. VENDIDAS O MUERTAS EN ESE PERIODO  <24 MESES--- (SUMO AL CENSO)
	-- NO ESTA EN ANIMALES PERO TENDRIA QUE ESTAR
set @query ='INSERT INTO ' + @NombreTabla + ' (ESTABLO,VACA,NOMBRE,usuario)'
set @query =@query +' SELECT  distinct ''' + @ESTABLO + ''',CODIGOIDENTIF,NOMCRIA,''' + @usuario +''' FROM REGGEN..VACAS'
set @query =@query +' WHERE FECHAMOVIMIENTO<''' + @DESDE + ''''

set @query =@query +'	AND ((CODIGOGANADERIA=''' + @Establo +'''  AND  TIPOMOVIMIENTO IN(''01'',''02'',''07'')) '
set @query =@query +'	OR (TIPOMOVIMIENTO=''03'' AND CODIGOCOMPRADOR=''' + @Establo +''' )'
set @query =@query +'		   AND DATEDIFF(DAY, FECHANACIMIENTO,''' + @DESDE + ''' )>='  + cast(@DIAS as varchar(4)) +')'
set @query =@query +'	AND CODIGOIDENTIF  IN (SELECT CODIGOIDENTIF FROM REGGEN..VACAS'
set @query =@query +'		WHERE  CODIGOGANADERIA=''' + @Establo +'''  AND TIPOMOVIMIENTO=''03'''	

set @query =@query +'		AND FECHAMOVIMIENTO>''' + @DESDE + ''' AND DATEDIFF(day, FECHANACIMIENTO,''' + @DESDE + ''' )>='  + cast(@DIAS as varchar(4)) +')'
--SOMASPRILLA 02/02/2018
set @query =@query +'	AND SUBSTRING(NumCria,4,1)=''H'''
--SOMASPRILLA 02/02/2018
set @query =@query +'	AND CODIGOIDENTIF   NOT IN  (SELECT CODIGOIDENTIF FROM REGGEN..ANIMALES'
set @query =@query +'		WHERE ganpro=''' + @Establo +'''  AND  SEXO=''H'' and CODIGOIDENTIF is not null'
--set @query =@query +')' --agc
set @query =@query + ' AND DATEDIFF(DAY,CAST(cast((CAST(DIANA1 AS VARCHAR(2)) + ''/'' +CAST(MESNA1 AS VARCHAR(2)) + ''/'' + CAST(AÑONA1 AS VARCHAR(4))) as varchar(12)) AS smalldatetime),cast('''  + @DESDE + ''' as smalldatetime))>='  + cast(@DIAS as varchar(4)) +')' 
--set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)
print '3. sumo: vendidas o muertas...' + @query
-- actualizo la FALTA AFCA
set @query ='UPDATE ' + @NombreTabla 
set @query =@query + ' SET FaltaAFCA=CONVERT(VARCHAR(10), fechamovimiento, 103) '
set @query =@query + ' from reggen..vacas,' + @NombreTabla
set @query =@query + ' where establo=''' + @Establo +''''
set @query =@query + ' and vaca=codigoidentif'
set @query =@query + ' and ((codigoganaderia=establo  and TIPOMOVIMIENTO IN(''01'',''02'',''07''))'
set @query =@query + ' 	OR (TIPOMOVIMIENTO=''03'' AND CODIGOCOMPRADOR=''' + @Establo +'''))'
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)
-- actualizo la FBaja AFCA
set @query ='UPDATE ' + @NombreTabla  -- En la vista tenemos que TIPOMOVIMIENTO='03' 
set @query =@query + ' 	SET FBajaAFCA= CONVERT(VARCHAR(10), fechamovimiento, 103)'
set @query =@query + ' 	from reggen..vista_vacas_03 v,' + @NombreTabla + ' t'
set @query =@query + ' 	where v.vaca=t.vaca'
set @query =@query + ' 	and codigocomprador<>''' + @Establo +'''' --solo quiero las que me compran otros, no las que yo compro
set @query =@query + ' 	and fechamovimiento>FaltaAFCA'
set @query =@query + ' 	and fechamovimiento in (select max(fechamovimiento) from reggen..vista_vacas_03 where v.vaca=t.vaca )'
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)

	--ACTUALIZO EL MOTIVO DE BAJA
	/*
	UPDATE @NombreTabla
	SET MOTIVO=CODIGOCOMPRADOR
	from reggen..vacas,@NombreTabla
	WHERE vaca=codigoidentif
	and CODIGOGANADERIA=@Establo AND TIPOMOVIMIENTO='03'
	*/
FETCH NEXT FROM C1 INTO @ESTABLO
print @ESTABLO
END--DEL WHILE

CLOSE C1
DEALLOCATE C1

PRINT 'SALE DEL BUCLE...'
set @query ='UPDATE ' + @NombreTabla + ' SET FCALCULO=''' + @DESDE + ''' ,MESESCALCULO=' + CAST(@DIAS AS VARCHAR(10))
set @query =@query + ' where usuario=''' + @usuario +''''
exec(@query)
PRINT 'nombre y meses tabla...' + @query

-- Elimino las que tengan <24 meses. Lo hago aquí pq si filtro las de SITRANC, me trae todas las filas y tarda mucho
set @query ='delete from ' + @NombreTabla
--WHERE FECHAMOVIMIENTO> cast(' + @DESDE +' as smalldatetime)'
set @query =@query + ' 	where DATEDIFF(day, FN,  cast(''' + @DESDE +''' as smalldatetime))<' + CAST(@DIAS AS VARCHAR(10))
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)
print('<24.... ' + @query)

--actualizo FN y nombre
set @query ='update NT' 
set @query =@query + ' set FN=cast(DIANA1 as char(2)) + ''/'' + cast(MESNA1 as char(2)) + ''/''+ cast(AÑONA1 as char(4)),'
set @query =@query + ' NT.nombre=reggen..animales.nombre'
set @query =@query + ' from reggen..animales,' + @NombreTabla + ' NT'
set @query =@query + ' where Vaca=codigoidentif'
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)
print('FN y nombre')
--ACTUALIZO POR EL NOMBRE PQ HAY VACAS VIEJAS CON ESPH
set @query ='update NT'
set @query =@query + ' set FN=cast(DIANA1 as char(2)) + ''/'' + cast(MESNA1 as char(2)) + ''/''+ cast(AÑONA1 as char(4)),'
set @query =@query + ' VACA=(PAIS+SEXO+PROANI+NUMERO)'
set @query =@query + ' from REGGEN..ANIMALES A, ' + @NombreTabla + ' NT'
set @query =@query + ' where NT.NOMBRE=A.NOMBRE'
set @query =@query + ' AND (ESTABLO=A.GANCRI OR ESTABLO=A.GANPRO)'
set @query =@query + ' AND VACA IS NULL'
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)
print('POR EL NOMBRE PQ HAY VACAS VIEJAS CON ESPH')

--actualizo el sanitario que sea NULL a XXXXXXXXXXXXXX
set @query ='update '  + @NombreTabla
set @query =@query + ' set vaca=''XXXXXXXXXXXXXX'','
set @query =@query + ' FN=''00:00:00'''
set @query =@query + ' where vaca is null'
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)
print('actualizo el sanitario que sea NULL')

-- si el sanitario es null -> voy a buscar el sanitario por el nombre a Vacas
set @query ='update '  + @NombreTabla
set @query =@query + ' set vaca=codigoidentif,'
set @query =@query + ' FN=FechaNacimiento'
set @query =@query + ' from reggen..vacas'
set @query =@query + ' where nomcria=nombre'
set @query =@query + ' AND establo=codigoganaderia'
set @query =@query + ' and vaca=''XXXXXXXXXXXXXX'''
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)
print('actualizo el sanitario que sea NULL')
--............................................................................
set @query ='update NT' 
set @query =@query + ' set FBajaMuerteRIIA=CONVERT(varchar(10), Fmuerte, 103)'
set @query =@query + ' from RIIAREMO..RIIA_ANIMALES R, ' + @NombreTabla + ' NT'
set @query =@query + ' where ESTABLO=ESTABLOconafe'
set @query =@query + ' and NT.VACA = R.VACA'
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)
print('actualizo el sanitario que sea NULL')
--3.empiezo ACTUALIZO FbajaRIIA'
/*update @NombreTabla 
SET FbajaRIIA=  CONVERT(varchar(10), Fsalida, 103)
from [RIIAREMO].[dbo].[RIIA_EXPLOTACION] e
where  ESTABLO=ESTABLOCONAFE
AND @NombreTabla.VACA =e.VACA
and ((@desde between fentrada and Fsalida) or (FENTRADA<=@DESDE and fsalida is  null))
*/
-- CEA'S..........................................................................................................
set @query ='update NT' 
set @query =@query + ' set NT.CEA=NUMSANITARIO'
set @query =@query + ' from rEGGEN..GANADERIAS_MAESTRO  ,'  + @NombreTabla + ' NT'
set @query =@query + ' where NT.ESTABLO = CODGAN'
set @query =@query + ' AND NT.usuario=''' + @usuario +''''
exec(@query)
print('actualizo el CEA '  + @query)
--FECHA BAJA RIIA..................................................................................................
--set @query ='update NT ' 
--set @query =@query + ' SET FbajaRIIA= CONVERT(varchar(10), (Fsalida), 103)'
--set @query =@query + ' from RIIAREMO..View_MaxFsalidaExplotacion V,' + @NombreTabla + ' NT'
--set @query =@query + ' where  ESTABLO=ESTABLOCONAFE'
--set @query =@query + ' and ''' + @DESDE + ''' >=Fsalida '
--set @query =@query + ' AND NT.VACA =V.VACA'
--set @query =@query + ' AND NT.VACA NOT IN (SELECT VACA FROM RIIAREMO..LASNIEVES)'
----set @query =@query + ' and Fsalida>Fentrada '--para quitar el NULL
--set @query =@query + ' AND usuario=''' + @usuario +''''
--exec(@query)



-- averrrrrrrrrrrrrrrrrrrrrrr====================================
set @query ='update NT ' 
set @query =@query + ' SET FbajaRIIA= CONVERT(varchar(10), (Fsalida), 103)'
set @query =@query + ' from RIIAREMO..View_MaxFsalidaExplotacionSanitario V,' + @NombreTabla + ' NT'
set @query =@query + ' where  CEA=ESTABLOSANITARIO'
set @query =@query + ' and ''' + @DESDE + ''' >=Fsalida '
set @query =@query + ' AND NT.VACA =V.VACA'
set @query =@query + ' AND NT.VACA NOT IN (SELECT VACA FROM RIIAREMO..LASNIEVES where ''' + @DESDE + ''' between FEntrada and Fsalida)'
--set @query =@query + ' and Fsalida>Fentrada '--para quitar el NULL
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)
--aver--------------------------------------------========================
-- ñAPA PARA ARREGLAR J390097 CON J390774. 01/12/2017
set @query ='update NT ' 
set @query =@query + 'SET FbajaRIIA=NULL'
set @query =@query + ' from tmp_censoafca NT ,RIIAREMO..RIIA_EXPLOTACION E'
set @query =@query + ' where ESTABLO=ESTABLOCONAFE '
set @query =@query + ' AND NT.VACA =E.VACA '
set @query =@query + ' AND (''' + @DESDE + '''>FENTRADA AND FSALIDA IS NULL)'
set @query =@query + ' AND FBajaRIIA IS NOT NULL'
set @query =@query + ' AND NT.VACA NOT IN (SELECT VACA FROM RIIAREMO..LASNIEVES where ''' + @DESDE + ''' between FEntrada and Fsalida)'
--set @query =@query + ' and Fsalida>Fentrada '--para quitar el NULL
set @query =@query + ' AND usuario=''' + @usuario +''''
exec(@query)


print('actualizo FBaja RIIA 04/008/2017... ' + @query)

--actualizo el controlador......................................................................
set @query ='update NT' 
set @query =@query + ' SET CONTROLADOR=REPLICATE(''0'', (3 - LEN(convert(varchar(3) ,ASIGNADO)))) + convert(varchar(3),ASIGNADO)'
set @query =@query + ' FROM CONTROLLECHERO..LECTURASCONTROL L,' + @NombreTabla + ' NT'
set @query =@query + ' WHERE NT.ESTABLO=L.ESTABLO'
set @query =@query + ' AND FBAJA IS NULL'
set @query =@query + ' AND NT.usuario=''' + @usuario +''''
exec(@query)
print('actualizo el controlador')
--actualizo la raza................................................................................................
set @query ='update NT' 
set @query =@query + ' set NT.raza=A.raza'
set @query =@query + ' from rEGGEN..ANIMALES  A,'  + @NombreTabla + ' NT'
set @query =@query + ' where NT.VACA = CODIGOIDENTIF'
set @query =@query + ' AND NT.usuario=''' + @usuario +''''
exec(@query)
print('actualizo el sanitario que sea NULL')

if @NombreTabla='TMP_CensoAFCA'
begin
	set @query ='delete from ' + @NombreTabla + ' where establo=''J390769'''
	set @query =@query + ' AND usuario=''' + @usuario +''''
	exec(@query)
	print 'query. ' + @query
end
