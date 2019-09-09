USE [controllechero]
GO
/****** Object:  StoredProcedure [dbo].[sp_TorosToXMLByEstablo]    Script Date: 28/08/2019 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter PROCEDURE [dbo].[sp_InseminacionesToXMLByEstablo]
(
@Establo char(7) ,
@controlador int
)
AS
DECLARE @USUARIO VARCHAR(50)
SELECT @USUARIO= RTRIM(LTRIM(substring(SYSTEM_USER, charindex('\',SYSTEM_USER)+1,LEN(SYSTEM_USER))))

DECLARE @FechaDesde smalldatetime
set @FechaDesde=DateAdd("m", -12, getdate())

--==================PARA METER INSEMINACIONES EN CURSO...NP=0 y que no tengan diagnostico de gestación positivo======================
DELETE FROM View_MaxInseminaciones WHERE  usuario=@USUARIO

Insert into View_MaxInseminaciones(id,NINS,establo,det,usuario) 
select id as ID,max(Nins) as NINS,Establo,Maes_nom,@USUARIO
from inseminaciones,Maestro where establo=@Establo
AND Inseminaciones.Establo=Maestro.Maes_tit 
AND Inseminaciones.ID=Maestro.Maes_Gen 
AND Inseminaciones.fInseminacion>@FechaDesde --'solo meto las de x meses atrás (p.ej). No tiene sentido más
group by id,establo,maes_nom


INSERT INTO XMLInsem
 (Ins_Vac,Ins_Nom,Ins_Dat,Ins_Tip,Ges_Dat,Ges_Tip,
 Ins_Mad,Ins_Num,Ins_Tor,Ins_Nto,Ins_Gan,
 Ins_Tec,Ins_Raz,Ins_Dos,Ins_Controlador)

SELECT V.ID,V.DET,FInseminacion,TipoIns,FDIAGNOSTICO,V.TipoDiagnostico
madreEmbrion,V.NIns,SEMENTAL,nombretoro,v.Establo
Tecnico,RazaToro,Dosis,numcontrolador 

From Inseminaciones as I,View_MaxInseminaciones as V 
Where usuario=@USUARIO
AND v.Establo =@Establo
AND V.ID=I.ID 
AND V.NINS=I.NINS --09/03/2018 para que no casque por duplicadas y porque no tiene sentido
AND I.finseminacion>@FechaDesde
order by I.id,I.nins 

--================================================================






--VACAS JOVENES
DECLARE @MES1 INT,@MES2 INT
SELECT @MES1=PARAM1,@MES2=PARAM2  FROM ParametrosGenerales WHERE ID ='MES12'

insert into [dbo].VacasJovenes 
		(RES_GEN, RES_NOM, RES_FN,RES_TIT, 
		 RES_NP,RES_NC,RES_AB,RES_XAB,RES_KANT, Res_Controlador)

SELECT CODIGOIdentif ,NOMBRE,CAST(CAST(DIANA1 AS VARCHAR(2)) + '/' +CAST(MESNA1 AS VARCHAR(2)) + '/' + CAST(AÑONA1 AS VARCHAR(4)) AS smalldatetime),@Establo,
		0,0,'B','B',0,@controlador
FROM RegGen..ANIMALES
Where GANPRO = @Establo
and NUMLAC=0 and AÑOBAJ=0
and Sexo='H'
and DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), getdate())>=@MES1
And DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), getdate())<=@MES2
and  CODIGOIdentif  not in(select Maes_Gen from maestro where Maes_tit=@Establo)


















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


