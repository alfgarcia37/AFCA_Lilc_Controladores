USE [controllechero]
GO

/****** Object:  StoredProcedure [dbo].[sp_TorosToXMLByEstablo]    Script Date: 28/08/2019 20:24:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sp_TorosToXMLByEstablo]
(
@Establo char(7) ,
@controlador int
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
DELETE FROM [dbo].XMLTOROS WHERE TOR_GAN=@Establo

--TOROS  de la Tabla de Toros........
INSERT into [dbo].XMLTOROS (TOR_GEN,TOR_NOM,TOR_GAN,TOR_ORDEN)
						SELECT ID ,DET,@Establo,'1' FROM TOROS WHERE DBF='1'

--2. Aqui debo meter los ultimos toros inseminados QUE NO ESTEN EN TOROS =============================================================  
INSERT into [dbo].XMLTOROS (TOR_GEN,TOR_NOM,TOR_GAN,TOR_ORDEN)

SELECT  MAX (semental),MAX(NOMBRETORO),@Establo,'1' FROM Inseminaciones
 where Establo=@Establo
 and datediff(month,FregistroIns,getdate())<5  
AND LEN(SEMENTAL)=14 AND LEN(NOMBRETORO)>2
AND semental NOT IN (SELECT TOR_GEN FROM XMLTOROS where tor_gan=@Establo)
GROUP BY semental
 
 --ACTUALIZO LOS NOMBRES DE LOS TOROS......
UPDATE XMLTOROS SET TOR_NOM=NOMBRE FROM REGGEN..ANIMALES 
Where TOR_NOM = '' AND TOR_GEN=(pais+sexo+proani+numero) 

UPDATE XMLTOROS SET TOR_NOM=NOMBRE FROM REGGEN..PADRES
Where TOR_NOM = '' AND TOR_GEN=(pais+sexo+proani+numero)

--'3.AHORA meto los Padres de las terneras registradas DE LOS 4 ULTIMOS MESES..........................
INSERT into XMLToros (Tor_Gen,Tor_Nom,Tor_Gan,Tor_Mar,Tor_orden)

SELECT  DISTINCT NumPadre as id,REPLACE(NomPadre,'''','') ,@Establo,'','0'
 FROM reggen..Vacas 
 where CodigoGanaderia=@Establo
 AND DATEDIFF(MONTH,FECHAMOVIMIENTO,GETDATE())<13 
 AND (TIPOMOVIMIENTO='01' OR TIPOMOVIMIENTO='02')
 AND NumPadre not in (select Tor_gen from controllechero..xmlToros 
 WHERE TOR_GAN=@Establo)

 --== REGISTRADAS ...........................................................
--'4. Para meter los registrados de los ultimos 4 meses
INSERT INTO XMLRegistrados (Codigo,Establo,Registrados,controlador)

SELECT  codigoidentif ,@Establo,'1',@controlador 
FROM reggen..animales 
where ganpro=@Establo
and codigoidentif in (select codigoidentif 
from reggen..Vacas 
WHERE CodigoGanaderia=@Establo AND (TIPOMOVIMIENTO='01' OR TIPOMOVIMIENTO='02') 
AND DATEDIFF(MONTH,FECHAMOVIMIENTO,GETDATE())<5 )

--5. Para meter los ultimos nacimientos todavia no registrados ..............................................
 -- MADRE,VACA 'TERNERA',FNAC_HIJO 'FPARTO',@strEstabloConafe 'establo' ....,'0','1',Controlador)
 INSERT INTO XMLRegistrados(Codigo,Madre,Establo,FN) -- Después meto estos 3 valores porque no me lo permite ese procedimiento : ,'0','1',Controlador)
 EXEC [GetAnimalesNoRegistradosRIIA] @Establo

 Update XMLRegistrados
 set Registrados='0',
 MadreRegistrada='1'
 where Registrados<>'1'
 --'5.0 Meto Inseminaciones de los No Registrados

 -- '5.1 Paso los nombres de toros establos a nombre corto si lo tuviese
		--esto lo puedo hacer al final de generar porque es por controlador, no depende de ganaderia
 -- '6.VACAS MUERTAS O VENDIDAS QUE ESTAN EN MAESTRO ACTIVAS TODO


-- MADRE,VACA 'TERNERA',@strEstabloConafe 'establo',FNAC_HIJO 'FPARTO'

--ACTUALIZO NOMBRES.............................................................................................
--declare @NombreDB varchar(50)
--DECLARE @Nombre varchar(50)
--SET @Nombre=''
--SELECT @NombreDB=RTRIM(LTRIM(nombrevaca)) from lecturascontrol where Establo=@Establo
----PRINT 'NOMBB... ' + @NombreDB

--select *
--Into   #Temp
--From   XMLCONTROL WHERE Res_TIT=@Establo

--Declare @Id CHAR(14)
----PRINT '..A VER  ' + dbo.fn_ReemplazarNombresVacas('BENERA PINTA','BENERA')
--While (Select Count(*) From #Temp) > 0
--Begin
--    Select Top 1 @Id = RES_GEN ,@Nombre=RTRIM(LTRIM(RES_NOM)) From #Temp
--	--PRINT 'NOMBRE: ' + @Nombre + ':' +'NOMDB . ' + @NombreDB + 'RESULTADO...' + dbo.fn_ReemplazarNombresVacas(@Nombre,'BENERA')

--	UPDATE XMLCONTROL SET RES_NOM=dbo.fn_ReemplazarNombresVacas(@Nombre,@NombreDB) WHERE RES_GEN=@Id
--    --PRINT '...elininado: ' + @Nombre + '...' + @Id
--    Delete #Temp Where RES_GEN = @Id
--End
--GO


GO

