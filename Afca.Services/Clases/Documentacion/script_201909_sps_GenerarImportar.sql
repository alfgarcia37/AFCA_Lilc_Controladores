USE [controllechero]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_getControladorLectura_byEstado]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[fn_getControladorLectura_byEstado]
(
	@numController int,
	@estado char(1)
)
returns int

as

begin
	declare @cantidad int

	select @cantidad =count(*)
	from dbo.LecturasControl 
	where NumControler=@numController and Estados=@estado

	return @cantidad	
end

GO
/****** Object:  UserDefinedFunction [dbo].[fn_ReemplazarNombresVacas]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE function [dbo].[fn_ReemplazarNombresVacas]
(
	@Nombre varchar(50),
	@NombreComaVaca varchar(50)
)
-- @NombreComaVaca=xxx,yyy	@wnombre= xxx paloma yyy
returns varchar(50)

as
BEGIN
 --Declare @NombreComaVaca varchar(50) ='casablanca,culi' 
declare @wnombre varchar(50)=''--='casablanca zorra  culi '
 
  While CHARINDEX(',',@NombreComaVaca ) > 0
	BEGIN
        SET  @wnombre = RTrim(LTRIM(SUBSTRING(UPPER(@NombreComaVaca), 1, CHARINDEX( ',',UPPER(@NombreComaVaca)) - 1)))
		SET @Nombre=LTRIM(RTRIM(Replace (UPPER(@Nombre) ,UPPER(@wnombre),'')))
        SET  @NombreComaVaca = LTRIM(RTrim(SUBSTRING(UPPER(@NombreComaVaca), CHARINDEX(',',@NombreComaVaca )+1, 50 )))
    END
	return LTRIM(RTRIM(Replace (UPPER(@Nombre) ,UPPER(@NombreComaVaca),'')))
END

--DROP FUNCTION dbo.[fn_ReemplazarNombresVacas]
--GO
GO
/****** Object:  UserDefinedFunction [dbo].[func_ConversionUEa14Digitos]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_ConversionUEa14Digitos]
(	@codigocrotal char(14)
)
RETURNS char(14)
AS

BEGIN
	Declare @crotal char(14)

	Set @crotal=
	(CASE
		WHEN Left(@codigocrotal, 2) = 'BG' THEN Left(@codigocrotal, 2) + '0000' + Right(@codigocrotal, 8)
		WHEN Left(@codigocrotal, 2) = 'SI' THEN Left(@codigocrotal, 2) + '0000' + Right(@codigocrotal, 8)
		WHEN Left(@codigocrotal, 2) = 'LT' THEN Left(@codigocrotal, 2) + '0000' + Right(@codigocrotal, 8)
		WHEN Left(@codigocrotal, 2) = 'LU' THEN Left(@codigocrotal, 2) + '0000' + Right(@codigocrotal, 8)

		WHEN Left(@codigocrotal, 2) = 'AT' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
		WHEN Left(@codigocrotal, 2) = 'CZ' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
		WHEN Left(@codigocrotal, 2) = 'CY' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
		WHEN Left(@codigocrotal, 2) = 'SK' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
		WHEN Left(@codigocrotal, 2) = 'FI' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
		WHEN Left(@codigocrotal, 2) = 'NL' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
		WHEN Left(@codigocrotal, 2) = 'PT' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
		WHEN Left(@codigocrotal, 2) = 'BE' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)

		WHEN Left(@codigocrotal, 2) = 'DE' THEN   Left(@codigocrotal, 2) + '00' + Right(@codigocrotal, 10)
		WHEN Left(@codigocrotal, 2) = 'EE' THEN   Left(@codigocrotal, 2) + '00' + Right(@codigocrotal, 10)
		WHEN Left(@codigocrotal, 2) = 'FR' THEN   Left(@codigocrotal, 2) + '00' + Right(@codigocrotal, 10)
		WHEN Left(@codigocrotal, 2) = 'HU' THEN   Left(@codigocrotal, 2) + '00' + Right(@codigocrotal, 10)
		WHEN Left(@codigocrotal, 2) = 'AD' THEN   Left(@codigocrotal, 2) + '00' + Right(@codigocrotal, 10)

		WHEN Left(@codigocrotal, 2) = 'DK' THEN   Left(@codigocrotal, 2) + '0' + Right(@codigocrotal, 11)
		WHEN Left(@codigocrotal, 2) = 'SE' THEN   Left(@codigocrotal, 2) + '0' + Right(@codigocrotal, 11)

		WHEN Left(@codigocrotal, 2) = 'ES' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
		WHEN Left(@codigocrotal, 2) = 'EL' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
		WHEN Left(@codigocrotal, 2) = 'IE' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
		WHEN Left(@codigocrotal, 2) = 'IT' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
		WHEN Left(@codigocrotal, 2) = 'LV' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
		WHEN Left(@codigocrotal, 2) = 'PL' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
		WHEN Left(@codigocrotal, 2) = 'UK' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
		WHEN Left(@codigocrotal, 2) = 'RO' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
		WHEN Left(@codigocrotal, 2) = 'AD' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
		WHEN Left(@codigocrotal, 2) = 'CH' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)

	
	
	END
	)

  return @crotal
END
GO
/****** Object:  UserDefinedFunction [dbo].[func_ExisteVelocidad]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_ExisteVelocidad]
(	@codigocrotal char(14)
)
RETURNS int
AS
BEGIN
	DECLARE @velocidad int
  SELECT @velocidad=count(*)  from  control Where res_gen=@codigocrotal and Res_NP=1 and Res_NC BETWEEN 2 AND 6 and Res_VELORD BETWEEN '0' AND '3'
  return @velocidad
END
GO
/****** Object:  UserDefinedFunction [dbo].[func_GetPartoMultiple]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[func_GetPartoMultiple]
(	@codigocrotal char(14),
	@fp smalldatetime
)
RETURNS int
AS
BEGIN
	DECLARE @CountPartos int
  select  @CountPartos=count(*) from RIIAREMO..RIIA_PARTOS where MADRE=@codigocrotal and fnac_hijo=@fp 
  return @CountPartos
END
GO
/****** Object:  UserDefinedFunction [dbo].[func_GetUltimoControl]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create function [dbo].[func_GetUltimoControl]
(	@codigocrotal char(14),
	@np int
)
RETURNS int
AS
BEGIN
	DECLARE @RCS REAL
 DECLARE @NC int=0
  select top 1 @NC=res_nc  from control where RES_GEN=@codigocrotal and RES_NP=@np and RES_AB='B' 
  return dbo.func_GetRcsSecado(@codigocrotal,@np, COALESCE(@NC-1,0))
END
GO
/****** Object:  UserDefinedFunction [dbo].[func_KmLecturasFinalizadas]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_KmLecturasFinalizadas]
(	@numcontroler int,
	@Establo char(7) 
	
)
RETURNS int
AS
BEGIN
	DECLARE @km int
	SELECT @km=km  from LecturasFinalizadas 
	where  numcontroler=@numcontroler and establo=@Establo
	and Propio='0' and km<>0
	return COALESCE(@km,0)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ActDuplicadosXML_Control]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO








Create PROCEDURE [dbo].[SP_ActDuplicadosXML_Control]
	
(
@Establo char(7) 
)
AS
	/* SET NOCOUNT ON */
INSERT INTO CONTROLDBFDUPLICADOS
(RES_GEN,RES_NP, RES_NC, RES_TIT, RES_FPREV,controlador)

select DBF.RES_GEN,DBF.RES_NP, DBF.RES_NC, DBF.RES_TIT, DBF.RES_FPREV,dbf.controlador 
from  controlDBF DBF,control ct 
where 
DBF.res_gen=ct.res_gen and
DBF.res_np=ct.res_np and
DBF.res_nc=ct.res_nc






GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizacionesGlobalesToXMLByControlador]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ActualizacionesGlobalesToXMLByControlador]
(
@Controlador int 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
DECLARE @controladorString varchar(3)
DECLARE @USUARIO VARCHAR(50)
SELECT @USUARIO= RTRIM(LTRIM(substring(SYSTEM_USER, charindex('\',SYSTEM_USER)+1,LEN(SYSTEM_USER))))
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))


-- '.......Grabo en INcidencias......................
INSERT INTO IncidenciasGenerales (DET,Tipo,FechaIncid,Fecha1,Establo,Usuario,controlador)
SELECT 'Exportar','10',GETDATE(),FPREV,CODIGO,@USUARIO,@controladorString
FROM XMLDATOS
WHERE CONTROLADOR=@controladorString

--............BorraBajasLibro().....................................................
Delete XMLRegistrados 
FROM [reggen].[dbo].[BAJAS DEL LIBRO] B,XMLRegistrados  X
Where X.Establo = B.GANPRO
AND X.CODIGO=B.CODIGOIDENTIF
        
--.....GuardarMadreNoRegistrada() 'Ahora pongo 0 si la madre no está registrada....
UPDATE XMLRegistrados 
SET MadreRegistrada='0' 
where madre  not in (select codigoidentif from Reggen..Animales where codigoidentif is not null )

-- 'Meto los nuevos partos desde RIIA de las vacas que van 'B'. Pongo el campo AltaEnServidor=1 para que el terminal tome los datos del nuevo parto
--  'solo cojo los que están en B, ya que me interesa que hayan metido antes el secado
Update XMLCONTROL
SET RES_FP=CONVERT(varchar(10), FNAC_HIJO, 103),
RES_SEXT=RIGHT(SEXO,1),
RES_AB='B',
RES_RIIA='1'
FROM RIIAREMO..RIIA_PARTOS
Where RES_GEN = Madre
AND RES_AB='B'				
AND DATEDIFF(MONTH,FNAC_HIJO,GETDATE())<=3

-- 'Ahora debo actualizar el valor de FechaUltIns...nO SE USA
--  ObjXML.GuardarActualizarFechaUltIns()

-- ObjCLM2.BorrarCLM2_UP(wControlador)
Delete FROM CLM2..CLM2_UP_CONTROL where Controlador = @Controlador
Delete FROM CLM2..CLM2_UP_GANADERIAS where Controlador=@Controlador
Delete FROM CLM2..CLM2_UP_TOROS where Controlador=@Controlador
Delete FROM CLM2..CLM2_UP_REGISTRADOS where Controlador=@Controlador
Delete FROM CLM2..CLM2_UP_INSEMINACIONES where Controlador=@Controlador
---
INSERT INTO CLM2..CLM2_UP_GANADERIAS (Codigo,Nombre,Telefono,FechaPrevistaDeControl,TipoControl,Cetosis,Mensaje,Moroso,Controlador)
SELECT codigo,razon,telef,FPREV,TCONTROL,Cetosis,Mensaje,Moroso,@Controlador FROM controllechero..xmldatos
where Controlador =@Controlador

INSERT INTO CLM2..CLM2_UP_CONTROL (CodigoCrotal,Nombre,collar,MovimientoEstado,GanaderiaCodigo,NP,NC,FechaParto,SexoTernero,
MovimientoBajaFecha,MovimientoBajaCausa,MovimientoEstadoOriginal,AltaEnservidor,KilosAnterior,VelocidadOrdeno,IncidenciaControl,Temperamento,Muerta,Controlador)
SELECT Res_GEn,Res_Nom,Res_Col,Res_AB,Res_tit,Res_NP,Res_NC,Res_FP,Res_SexT,
Res_FBA,Res_CBA,Res_XAB,Res_RIIA,Res_Kant,Res_Velord,Res_inc,Res_Temperamento,Res_Muerta,@Controlador
FROM controllechero..xmlCONTROL where RES_CONTROLADOR=@Controlador

INSERT INTO CLM2..CLM2_UP_INSEMINACIONES (CodigoCrotal,Nombre,Ganaderiacodigo,FechaInseminacion,NumInseminacion,TipoInseminacion,Toro,NombreToro,Raza,Tecnico,
Dosis,FechaGestacion,TipoGestacion,ResultadoGestacion,MetodoGestacion,Controlador)
 SELECT Ins_Vac,Ins_Nom,Ins_Gan,INs_Dat,Ins_Num,INs_Tip,Ins_Tor,Ins_Nto,Ins_Raz,Ins_Tec,Ins_Dos,Ges_Dat,Ges_Tip,Ges_Res,Ges_Met,@Controlador
FROM controllechero..xmlInsem 
where Ins_Controlador = @Controlador    

INSERT INTO CLM2..CLM2_UP_REGISTRADOS
(CodigoCrotal,Registrado,Madre,GanaderiaCodigo,FN,MadreRegistrada,Controlador)
SELECT Codigo,Registrados,Madre,Establo,FN,MadreRegistrada,@Controlador FROM controllechero..xmlRegistrados
where Controlador =@Controlador

INSERT INTO CLM2..CLM2_UP_TOROS (Codigo,Nombre,GanaderiaCodigo,Orden,Controlador)
SELECT Tor_Gen,Tor_Nom,Tor_gan,Tor_orden,@Controlador FROM controllechero..xmlToros

--PutEstadoEnCurso( NodLecturas.Controlador, "2")
UPDATE LECTURASCONTROL
 SET Estados='2' 
 FROM XMLDATOS
WHERE ESTABLO=CODIGO AND CONTROLADOR=@Controlador

		--MsgBox(" Proceso de Exportación realizado  ", 64)
		--refresco
GO
/****** Object:  StoredProcedure [dbo].[SP_ASIGNA_INSEMINACION_PARTO]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[SP_ASIGNA_INSEMINACION_PARTO]
(@ESTABLO char(7), @VACA char(14))
AS
-- DESPUES TENGO QUE VOLVER A RENUMERAR INSEMINACIONES
DECLARE @P_ID CHAR(14),@P_WID CHAR(14),@NINS INT,@P_WNP INT,@P_NP INT,@FINS SMALLDATETIME,@P_FP SMALLDATETIME,@WFINS  SMALLDATETIME,@W_FINS SMALLDATETIME,@WESTABLO char(7)

		DECLARE C1 cursor LOCAL SCROLL OPTIMISTIC FOR
		SELECT ID,NP,FPARTO FROM PARTOS
		 WHERE ESTABLO=@ESTABLO 
		OR (ESTABLO=@ESTABLO AND ID=@VACA)
		ORDER BY ID,FPARTO DESC -- PORQUE así me aseguro que ninguna fins>fparto
		OPEN C1 
		FETCH NEXT FROM C1 INTO @P_ID,@P_NP,@P_FP
		SET @P_WID=''; SET @P_WNP=@P_NP;SET @WFINS='01/01/2000'
		WHILE (@@FETCH_STATUS=0)
		   BEGIN
				UPDATE INSEMINACIONES
				SET NP=@P_NP
				WHERE ID=@P_ID
				AND FINSEMINACION< @P_FP --AND FINSEMINACION>@WFINS
				AND DATEDIFF( MONTH,FINSEMINACION,@P_FP) BETWEEN 6 AND 18

			--COJO LA UTLIMA FECHA DE INSEMINACION PARA NO VOLVER A ACTUALIZAR
			DECLARE C2  CURSOR LOCAL SCROLL OPTIMISTIC FOR
			SELECT MAX(FINSEMINACION) FROM INSEMINACIONES
			WHERE ID=@P_WID
			AND NP=@P_WNP
			OPEN C2 
			FETCH NEXT FROM C2 INTO @WFINS
			
			IF (@@FETCH_STATUS=0)
			BEGIN
			  SET @WFINS='01/01/2000'
			END
			CLOSE C2;DEALLOCATE C2
			FETCH NEXT FROM C1 INTO @P_ID,@P_NP,@P_FP
		END
		
		CLOSE C1
		DEALLOCATE C1
			
	
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckDuplicadosXMLControl]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO








CREATE PROCEDURE [dbo].[SP_CheckDuplicadosXMLControl]
	
(
@Controlador int 
)
AS
DECLARE @controladorString char(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))
	/* SET NOCOUNT ON */
--INSERT INTO CONTROLDBFDUPLICADOS
--(RES_GEN,RES_NP, RES_NC, RES_TIT, RES_FPREV,controlador)

select X.RES_GEN,X.RES_NP, X.RES_NC, X.RES_TIT
from  control C,XMLCONTROL X
where 
X.Res_Controlador=@Controlador
AND X.res_gen=C.res_gen 
AND X.res_np=C.res_np 
AND X.res_nc=C.res_nc






GO
/****** Object:  StoredProcedure [dbo].[SP_CheckDuplicadosXMLDatos]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[SP_CheckDuplicadosXMLDatos]
	
(
@Controlador int 

)
AS
--declare @Controlador int
--set @Controlador=6
DECLARE @controladorString char(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))
	/* SET NOCOUNT ON */
	--APROVECHO EL PRIMER CHECK PARA METER LAS DOWN A XML


	Delete FROM XMLControl where Res_controlador=@Controlador 
	Delete FROM XMLDatos where convert(int,controlador)=@Controlador
	Delete FROM XMLInsem  where ins_controlador=@Controlador
	Delete FROM XMLNacimientos  where controlador=@controladorString
	Delete FROM XMLRegistrados where controlador=@controladorString

	INSERT INTO ControlLechero..XMLDatos ( Codigo,Razon,Fprev,FCOMCONTROL,FechaCarga,FechaDescarga,TControl,Notas,Controlador,Cetosis,AntOrd_HH,AntOrd_mm,Tanque,Rutinas,firma)
    SELECT Codigo,Nombre,FechaPrevistaDeControl,FechaComienzoControl,FechaCarga,FechaDescarga,TipoControl,Notas,@controladorString, Cetosis, HoraAnteriorOrdeno,'00',TanqueLitros,RutinasControl, Firma 
    FROM [CLM2].[dbo].[CLM2_DOWN_GANADERIAS] 
    where controlador=@Controlador

	INSERT INTO ControlLechero..XMLControl(
    RES_ORD, RES_GEN, RES_NOM, RES_FN, RES_TIT,RES_COL, RES_FP, RES_PAD, RES_MAD, RES_FAE,
    RES_NP, RES_NC, RES_AB, RES_FBA, RES_CBA,RES_KLE, RES_KGR, RES_TGR, RES_TPR, RES_TLA, RES_TRC,
    RES_FIN, RES_NIN, RES_SEM, RES_TIN,RES_LAM, RES_LPM, RES_LTO, RES_KANT,RES_NEO, RES_XAB, RES_XFBA, RES_XCBA,
    RES_FACP, RES_VELORD, RES_TEMPERAMENTO, RES_INC,RES_SEXT, RES_TAMT, RES_COMT, RES_RETP,RES_FCTR, RES_FCTR1,Res_Controlador) 
    SELECT CodigoBarras,CodigoCrotal,Nombre,null,GanaderiaCodigo, Collar , FechaParto, null, null, null,
    NP, NC, MovimientoEstado, MovimientoBajaFecha, MovimientoBajaCausa, 0, 0, 0, 0, 0, 0,
    Null, 0, null, null,ControlMananaKilosLeche, ControlTardeKilosLeche, 0, 0,
    vACAnUEVA, MovimientoEstadoOriginal, null,'',
	FacilidadParto, VelocidadOrdeno, Temperamento, IncidenciaControl,
	SexoTernero, TamanoTernero, conformacionTernero, RetencionPlacenta,
	ControlMananaFecha, ControlTardeFecha,@Controlador 
	FROM CLM2..CLM2_DOWN_CONTROL
	where controlador=@Controlador

	INSERT INTO controllechero..xmlInsem(Ins_Vac,Ins_Nom,Ins_Gan,INs_Dat,Ins_Num,INs_Tip,Ins_Tor,Ins_Nto,Ins_Raz,Ins_Tec,
    Ins_Dos,Ges_Dat,Ges_Tip,Ges_Res,Ges_Met,Ins_Controlador)
    SELECT CodigoCrotal,Nombre,Ganaderiacodigo,FechaInseminacion,NumInseminacion,TipoInseminacion,Toro,NombreToro,Raza,Tecnico,
    Dosis,FechaGestacion,TipoGestacion,ResultadoGestacion,MetodoGestacion,@Controlador
    FROM  CLM2..CLM2_DOWN_INSEMINACIONES
    where controlador=@Controlador

	INSERT INTO ControlLechero..XMLNacimientos(ESTABLO,CODIGO,NOMBRE,MADRE,PADRE,Tipo,
    FECHA_NACIMIENTO,COLLAR,SEXO,PARTO_GEMELAR,SEXO_HERMANO,CAPA,ET,portadora,
    FECHA_INSEMINACION,FECHA_DECLARACION,SILUETAS,controlador) 
    SELECT  GanaderiaCodigo,CodigoCrotal,Nombre,MadreCodigo,PadreCodigo,CONVERT(INT, Tipo),
    CONVERT(smalldatetime,NacimientoFecha,103), Collar, Sexo,PartoGemelar,SexoGemelo, Capa,right(ET,1),CodigoPortadora,
    FechaInseminacion,DeclaracionFecha,ImagenCarta,@controladorString 'CONTROLADOR' 
    From CLM2..CLM2_DOWN_NACIMIENTOS 
    WHERE Controlador=@Controlador

	INSERT INTO ControlLechero..XMLRCS ( Establo,Barras,DET,FEntregaGanadero,FEntregaControlador,Controlador)
    SELECT GanaderiaCodigo,CodigoBarras,Nombre,FechaCreada,FechaDescarga,@controladorString 
    FROM CLM2..CLM2_DOWN_RCS 
    where controlador=@Controlador

SET @controladorString='006'
select CODIGO
from  XMLDATOS X,LecturasFinalizadas L
where 
x.CODIGO=L.Establo
AND X.FPREV=L.FPrevista
AND CONTROLADOR=@controladorString




GO
/****** Object:  StoredProcedure [dbo].[SP_CheckDuplicadosXMLPartos]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO








CREATE PROCEDURE [dbo].[SP_CheckDuplicadosXMLPartos]
	
(
@Controlador int 
)
AS
DECLARE @controladorString char(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))
	

SELECT X.RES_GEN
FROM XMLCONTROL X ,PARTOS P
WHERE X.Res_Controlador=@Controlador
AND X.RES_GEN=P.ID 
AND X.RES_NP=P.NP
AND ((X.RES_XAB='B' AND X.RES_AB='A') OR X.RES_NEO='*')








GO
/****** Object:  StoredProcedure [dbo].[sp_CheckMaestroControlesToXML]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[sp_CheckMaestroControlesToXML] 
(
@Establo char(7) 
)
AS
declare @valordevuelto varchar(200)=''
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
DECLARE @controladorString varchar(3)
exec [PR_Check_Maestro_Control] @Establo

--set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))



--DELETE FROM [dbo].XMLDATOS WHERE CONTROLADOR=@controladorString
--insert into [dbo].XMLDATOS (Codigo,Razon,Act,Fcarga,Fprev,Fctrl,AntOrd_HH,AntOrd_mm,TControl,Telef,Mensaje,Tanque,Controlador,Cetosis,Moroso)

-- select ESTABLO,RAZONSOCIAL,'0',GETDATE(),FprevISTA,null,null,null,TIPOCONTROL,Movil,NULL,NULL,@controladorString,Cetosis,'0'
-- from LecturasControl,Reggen..GANADERIAS_MAESTRO 
-- WHERE NumControler=@Controlador and estados='1'
-- and fbaja is null
-- AND ESTABLO=CODGAN
-- --'VOY A VER SI ES MOROSO......
--UPDATE XMLDATOS 
--SET MOROSO='1' FROM REGGEN..MOROSOS 
--Where MOROSG = 2 AND CODGAN=CODIGO 
GO
/****** Object:  StoredProcedure [dbo].[sp_ControlesFromXMLByEstablo]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--drop function [dbo].[func_ExisteVelocidad]
--go
--create function [dbo].func_ExisteVelocidad
--(	@codigocrotal char(14)
--)
--RETURNS int
--AS
--BEGIN
--	DECLARE @velocidad int
--  SELECT @velocidad=count(*)  from  control Where res_gen=@codigocrotal and Res_NP=1 and Res_NC BETWEEN 2 AND 6 and Res_VELORD BETWEEN '0' AND '3'
--  return @velocidad
--END
--GO
CREATE PROCEDURE  [dbo].[sp_ControlesFromXMLByEstablo] 
(
@Establo char(7),@Controlador int ,@FPrevista smalldatetime,@TipoControl char(2)
)
AS
--set @Establo='J391727'
--set @Controlador=98
--set  @FPrevista='24/8/2019'
--set @TipoControl='1T'
DECLARE @controladorString varchar(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))

DECLARE @ORD char(14), @GEN char(14), @NOM char(30), @FN smalldatetime , @TIT char(7),@COL char(4), @FP smalldatetime,@NP int, @NC int, @AB char(1), @FBA smalldatetime, @CBA char(2)
DECLARE @LAM float, @LPM float,@NEO char(1), @XAB char(1),@VELORD char(1), @TEMPERAMENTO char(1)
DECLARE @INC char(2),@FACP char(1), @SEXT char(1), @TAMT char(1), @COMT char(1), @RETP char(1),@FCTR smalldatetime,@FCTR1 smalldatetime,@FCTRM smalldatetime,@FCTRT smalldatetime
DECLARE @MAES_NP int, @MAES_NC int,@Maes_FP smalldatetime,@MAES_ESTADO char(1)
set @LAM=0
set @LPM=0
--TEMPORAL PARA PROBAR ESTO------------------------------------------------------
-----------------------------------------------------------------------------------
	Delete FROM XMLControl where res_Controlador=@Controlador
	
	INSERT INTO ControlLechero..XMLControl(RES_ORD, RES_GEN, RES_NOM, RES_FN, RES_TIT,RES_COL, RES_FP, 
    RES_NP, RES_NC, RES_AB, RES_FBA, RES_CBA,RES_LAM, RES_LPM,
	RES_NEO, RES_XAB,RES_VELORD, RES_TEMPERAMENTO, RES_INC,
    RES_FACP, RES_SEXT, RES_TAMT, RES_COMT, RES_RETP,
	RES_FCTR,RES_FCTR1, Res_Controlador) 

    SELECT CodigoBarras,CodigoCrotal,Nombre,null,GanaderiaCodigo, Collar , FechaParto,
    NP, NC, MovimientoEstado, MovimientoBajaFecha, MovimientoBajaCausa,ControlMananaKilosLeche, ControlTardeKilosLeche,
    vACAnUEVA, MovimientoEstadoOriginal,VelocidadOrdeno, Temperamento, IncidenciaControl,
	convert(char(1),convert(INT,FacilidadParto)), 
	convert(char(1),convert(INT,SexoTernero)), convert(char(1),convert(INT,TamanoTernero)),convert(char(1),convert(INT,conformacionTernero)) ,convert(char(1),convert(INT,RetencionPlacenta)) ,
	ControlMananaFecha, ControlTardeFecha,@Controlador 
	FROM CLM2..CLM2_DOWN_CONTROL 	where controlador=@Controlador
	
     UPDATE CONTROLLECHERO..XMLCONTROL SET RES_FN=CONVERT(VARCHAR(10),MAES_FN,103)-- No estoy seguro de que haga falta estoAGC. Esto lo hacia en xmltoSql
     FROM CONTROLLECHERO..MAESTRO
     WHERE RES_GEN=MAES_GEN
---------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------
declare C1 cursor LOCAL SCROLL OPTIMISTIC FOR
SELECT RES_ORD, RES_GEN, RES_NOM, RES_FN, RES_TIT,RES_COL, RES_FP,RES_NP, RES_NC, RES_AB, RES_FBA, RES_CBA,RES_LAM, RES_LPM,
RES_NEO, RES_XAB,RES_VELORD, RES_TEMPERAMENTO, RES_INC,RES_FACP, RES_SEXT, RES_TAMT, RES_COMT, RES_RETP,RES_FCTR,RES_FCTR1, Res_Controlador
FROM XMLControl 
WHERE Res_Tit=@Establo 
AND (NOT(RES_XAB='B' AND RES_AB='B') OR rtrim(ltrim(RES_CBA))>'1')  --No cojo las que fueron de baja y vienen de baja
order by res_np,res_nc desc
OPEN C1 
FETCH NEXT FROM C1 INTO  @ORD, @GEN, @NOM, @FN, @TIT,@COL, @FP,@NP, @NC, @AB, @FBA, @CBA,@LAM, @LPM,@NEO, @XAB,@VELORD, @TEMPERAMENTO, @INC,@FACP, @SEXT, @TAMT, @COMT, @RETP,@FCTRM,@FCTRT, @Controlador

WHILE (@@FETCH_STATUS=0)
BEGIN
	-- Preprocesado: recorrido de collection de RefrescarDatosControlDBF COALESCE(@km,0)
	SET @XAB=COALESCE(@XAB,'')
	SET @CBA=COALESCE(RIGHT('0' + CAST(@CBA AS VARCHAR), 2),'')
	SET @ORD=COALESCE(@ORD,'')
	SET @COL=COALESCE(@COL,'')
	SET @NEO=COALESCE(@NEO,'')

	SET @VELORD=COALESCE(@VELORD,'')
	SET @FACP=COALESCE(@FACP,'')
	SET @SEXT=COALESCE(@SEXT,'')
	SET @TAMT=COALESCE(@TAMT,'')
	SET @COMT=COALESCE(@COMT,'')
	SET @RETP=COALESCE(@RETP,'')
	SET @INC=RIGHT('0' + CAST(ltrim(rtrim(@INC)) AS VARCHAR), 2)--simpre viene con 0 por defecto
	SET @FP=COALESCE(NULL,Convert(varchar(10),@FP,103)) 
	SET @GEN=UPPER(@GEN)
	SET @NOM=UPPER(@NOM)
	SET  @LAM=COALESCE(@LAM,0)
	SET  @LPM=COALESCE(@LPM,0)
	
	IF (@XAB = 'B' And @AB = 'A') Or (@AB = 'A' And @CBA <> '') Or @NP = 0 -- '..de seca a Alta============== IF 1
		BEGIN
              SET @NP = @NP + 1
              SET @NC= 1
		END
        Else
		BEGIN
                SET @NC = IIf(@NC = 0, 1, @NC)
        END -- ============================================================================================== FIN IF 1
		 -- procesado GuardarControlSQL.................................
	IF Not (@AB = 'B' And @XAB = 'B' And @CBA <> '01' And Not (@CBA >= '11' And @CBA <= '17')) --=========== IF 2 para quitarme la morralla de las NP0 nc0
	BEGIN
		--'Esto lo hago porque ahora tengo que generar un nuevo registro para las vacaciones (cuando se seca y se da de alta)
		--...Se que ocurre esto cuando EstadoOriginal=A And Estado=A and FBAJA is not null
		-- .Primero inserto el registro de baja y luego el de alta para que actualice maestro
		IF @XAB = 'A' And @AB ='A' And  @FBA IS NOT NULL -- 'Genero nuevo registro
		begin
			-- 'recupero ficha maestro.
			--ObjMaestro.GetActual(nodDatos.Vaca)
			SELECT @Maes_FP=Maes_FP, @MAES_NP=Maes_NP,@MAES_NC=Maes_nc from Maestro where Maes_tit=@Establo and maes_gen=@GEN
			INSERT INTO Control (RES_GEN,RES_NOM,RES_FN,RES_TIT,RES_COL, RES_FP, RES_NP,RES_NC,RES_AB,RES_FBA,RES_CBA,RES_FPREV,CONTROLADOR)
			VALUES ( @GEN,@NOM,@FN,@Establo,@COL,@Maes_FP,@MAES_NP,@MAES_NC + 1,'B',@FBA,@CBA,@FPrevista,@Controlador)
		end
	END--================================================================================================ IF 2

	If @AB='A' 
	begin
        set  @FBA = null
        set  @CBA=''
	end
	Else
	begin
		set  @FBA = Convert(varchar(10),@FBA)
        --set @CBA=RIGHT('0' + CAST(@CBA AS VARCHAR), 2)
    End 
	if  @TipoControl= '1M' or  @TipoControl='1T'  
		begin
		print 'va'
		--print '...' + convert(varchar,@LAM)
			set  @LAM=@LAM + @LPM
			set  @LPM=@LAM
			print '...' + convert(varchar,@LAM)
		end 
	if  @TipoControl= 'RX'		
		begin
			set  @LAM=@LAM/2
			set  @LPM=@LAM
		end    
	if  @TipoControl= '31'  or  @TipoControl='32' OR @TipoControl='33'
		begin
			set  @LAM=@LAM*1.5
			set  @LPM=@LPM*1.5
		end
--.....................................................................................................
	SET @NEO =IIF (((@NP = 1 And @NC = 1 And @AB='A') Or @NEO = '1' ),'1','0')
	If (@NP = 1 And (@NC >= 1 And @NC <= 6)) AND [dbo].func_ExisteVelocidad(@GEN)=1 --    RES_VELORD, TENGO QUE COMPROBAR QUE NO HALLA COGIDO YA LA VELOCIDAD
	begin
		SET  @VELORD=''
	end
	SET @FCTR= Convert(varchar(10),@FCTRM,103)
	SET @FCTR= ISNULL(@FCTR, Convert(varchar(10),@FCTRT,103))

	If @INC> '00' -- 'Tengo que insertarlo en patologias si no existe
	begin
		INSERT INTO Patologias (res,FPatologia,FRegistroPatologia,TipoPatologia,establo,Controlador)
		values (@GEN, @FCTR ,CONVERT (VARCHAR(10),GETDATE(),103),@INC, @Establo ,@Controlador)
	end

	If @AB = 'B' Or (@XAB = 'A' And @AB = 'A' And  @FBA is not null )--Si existe el parto -> debo actualizar la FBaja y Causa de PARTOS
	begin
		Update partos set FBAJA=@FBA,CBAJA=@CBA where ID=@GEN and np=@NP
	end
	--'Cada vez que inserto un control debo actualizar el NP,Nc,FP,Fbaja,Estado de Maestro
	If @CBA > '01' And Not (@CBA >= '11' And @CBA <= '17') 
	begin
		set @MAES_ESTADO='0'
	end
	Else
	begin
		set @MAES_ESTADO=IIF((@CBA = '01' Or (@CBA >= '11' And @CBA <= '17')),'B','A') --And wEstado = "B" Then
	End 
	UPDATE Maestro 
	SET Maes_NP =@NP,
	Maes_Nc=@NC,
	Maes_FP=@FP,
	MAES_ESTADO=@MAES_ESTADO,
	Maes_FBA=@FBA
	WHERE maes_Tit=@Establo 
	and maes_GEN = @GEN
       
	Insert Into Control (RES_ORD, RES_GEN, RES_NOM, RES_FN, RES_TIT,RES_COL,
	RES_FP,RES_NP, RES_NC, RES_AB, RES_FBA, RES_CBA,RES_KLAM, RES_KLPM,RES_SW0, RES_XAB,
	RES_VELORD, RES_TEMPERAMENTO, RES_INC,RES_FACP, RES_sexo, RES_TAM, RES_CONFORMACION, RES_PLACENTA,
	RES_FCTR,RES_FCTRM,RES_FCTRT,RES_FPREV,CONTROLADOR)
	VALUES( @ORD, @GEN, @NOM, @FN, @TIT,@COL, 
	@FP,@NP, @NC, @AB, @FBA, @CBA,@LAM, @LPM,@NEO, @XAB,
	@VELORD, @TEMPERAMENTO, @INC,@FACP, @SEXT, @TAMT, @COMT, @RETP,
	@FCTR,@FCTRM,@FCTRT,@FPrevista, @Controlador)
 
	 FETCH NEXT FROM C1 INTO   @ORD, @GEN, @NOM, @FN, @TIT,@COL, @FP,@NP, @NC, @AB, @FBA, @CBA,@LAM, @LPM,@NEO, @XAB,@VELORD, @TEMPERAMENTO, @INC,@FACP, @SEXT, @TAMT, @COMT, @RETP,@FCTR,@FCTR1, @Controlador
END--WHILE
GO
/****** Object:  StoredProcedure [dbo].[sp_ControlesToXMLByEstablo]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ControlesToXMLByEstablo]
(
@Establo char(7) ,
@controlador int
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
DELETE FROM [dbo].XMLCONTROL WHERE RES_TIT=@Establo
--ORDEN COLLAR -> COLLAR O 4 ULTIMOS
DECLARE @Orden char(1)
select @Orden=orden from LecturasControl where establo=@Establo

insert into [dbo].XMLCONTROL 
		(RES_GEN, RES_NOM, RES_FN,RES_TIT,RES_COL, RES_FP,
         RES_NP,RES_NC,RES_AB,RES_FBA,
		 RES_KANT,RES_XAB,RES_VELORD,RES_TEMPERAMENTO,Res_Controlador)

 select Maes_Gen,Maes_Nom,Maes_FN,@Establo,CASE WHEN @Orden='1'  THEN Maes_col  ELSE Right(Maes_gen,4)  END,Maes_FP,
      Maes_NP, CASE WHEN Maes_Estado='A'  THEN Maes_NC   ELSE Maes_NC+1 END, Maes_Estado,Maes_FBA,
      0,Maes_Estado,'0','0',@controlador
 from Maestro
 WHERE Maes_Tit=@Establo and Maes_Estado<>'0'
 
 --'VELOCIDAD DE ORDEÑO......
UPDATE XMLCONTROL
SET RES_VELORD=velord FROM PARTOS 
where establo=@Establo and ID=RES_GEN AND NP=RES_NP
and velord IS not null
--TEMPERAMENTO ?????

--LAST KILOS
DECLARE @LastFprevista smalldatetime
SELECT @LastFprevista=max(fPrevista) FROM LecturasFinalizadas WHERE establo=@Establo 

UPDATE XMLControl SET RES_KANT = cast((RES_KLAM + RES_KLPM)/2  as decimal(5,2)) 
FROM CONTROL C,XMLCONTROL X 
WHERE X.RES_TIT = C.RES_TIT 
AND C.RES_TIT=@Establo 
AND C.RES_FPREV=@LastFprevista
AND X.RES_GEN=C.RES_GEN

UPDATE XMLControl SET RES_KANT =0
WHERE RES_KANT IS NULL

--VACAS JOVENES
DECLARE @MES1 INT,@MES2 INT
SELECT @MES1=PARAM1,@MES2=PARAM2  FROM ParametrosGenerales WHERE ID ='MES12'

insert into [dbo].XMLCONTROL 
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

--ACTUALIZO COLLARES COLLARES DE TERNERAS (COLLAR U ORDEN)...............................................
UPDATE XMLCONTROL
SET RES_COL=CASE WHEN @Orden='1'  THEN Right(numestablo,4)  ELSE Right(RES_GEN,4)  END
FROM REGGEN..VACAS
WHERE RES_TIT=CODIGOGANADERIA
AND RES_TIT=@Establo
--AND (RES_GEN=NUMCRIA OR 
AND RES_GEN=CODIGOIDENTIF
and numestablo is not null
AND RES_COL IS  NULL
--ACTUALIZO NOMBRES.............................................................................................
--ACTUALIZO NOMBRES.............................................................................................
declare @NombreDB varchar(50)
DECLARE @Nombre varchar(50)
SET @Nombre=''
SELECT @NombreDB=RTRIM(LTRIM(nombrevaca)) from lecturascontrol where Establo=@Establo
--PRINT 'NOMBB... ' + @NombreDB

select *
Into   #Temp
From   XMLCONTROL WHERE Res_TIT=@Establo

Declare @Id CHAR(14)
--PRINT '..A VER  ' + dbo.fn_ReemplazarNombresVacas('BENERA PINTA','BENERA')
While (Select Count(*) From #Temp) > 0
Begin
    Select Top 1 @Id = RES_GEN ,@Nombre=RTRIM(LTRIM(RES_NOM)) From #Temp
	--PRINT 'NOMBRE: ' + @Nombre + ':' +'NOMDB . ' + @NombreDB + 'RESULTADO...' + dbo.fn_ReemplazarNombresVacas(@Nombre,'BENERA')

	UPDATE XMLCONTROL SET RES_NOM=dbo.fn_ReemplazarNombresVacas(@Nombre,@NombreDB) WHERE RES_GEN=@Id
    --PRINT '...elininado: ' + @Nombre + '...' + @Id
    Delete #Temp Where RES_GEN = @Id
End
GO
/****** Object:  StoredProcedure [dbo].[sp_GanaderiasFromXMLByControlador]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[sp_GanaderiasFromXMLByControlador] 
(
@Controlador int 
)
AS

--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
DECLARE @controladorString varchar(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))

DECLARE @Establo char(7),@LitrosTanque INT,@FPrevista SMALLDATETIME,@TipoControl CHAR(2),@FComControl SMALLDATETIME,@FCarga SMALLDATETIME,@FDescarga SMALLDATETIME
declare @Asignado int,@km int,@wControlador int ,@wEstablo char(7),@HoraAnteriorOrdeno char(2)

--TEMPORAL PARA PROBAR ESTO------------------------------------------------------
-----------------------------------------------------------------------------------
Delete FROM XMLDatos where convert(int,controlador)=@Controlador
	
	INSERT INTO ControlLechero..XMLDatos ( Codigo,Razon,Fprev,FCOMCONTROL,FCarga,FechaDescarga,TControl,Notas,Controlador,Cetosis,AntOrd_HH,Tanque,Rutinas,firma)
    SELECT Codigo,Nombre,FechaPrevistaDeControl,FechaComienzoControl,FechaCarga,FechaDescarga,TipoControl,Notas,@controladorString, Cetosis, HoraAnteriorOrdeno,TanqueLitros,RutinasControl, Firma 
    FROM [CLM2].[dbo].[CLM2_DOWN_GANADERIAS] 
    where controlador=@Controlador
----------------------------------------------------
--------------------------------------------------------
declare C1 cursor LOCAL SCROLL OPTIMISTIC FOR
select  codigo ,fprev ,TControl,FComControl,FCarga,FechaDescarga FROM XMLDatos where Controlador=@controladorString
OPEN C1 
FETCH NEXT FROM C1 INTO @Establo, @FPrevista,@TipoControl,@FComControl,@FDescarga,@HoraAnteriorOrdeno

WHILE (@@FETCH_STATUS=0)
BEGIN

	select @wEstablo=establo,@km=km,@Asignado=asignado from lecturascontrol where establo=@Establo
	print 	@wEstablo +'...' + cast(@km as varchar(10)) + '..' +  cast(@Asignado as varchar(10))

	Insert Into LecturasFinalizadas(Establo,NumControler,TipoControl,Fprevista,FReal,FDescarga,GeneraTXT,Informe)
	Values ( @Establo,@Controlador,@TipoControl,@FPrevista,convert(varchar(10),@FComControl,103),@FDescarga, '1','1') 
	 If @Asignado = @Controlador 
	 BEGIN
		UPDATE LecturasFinalizadas
		SET PROPIO='1',
		ControladorNoPropio=0,
		KM=@km*2,
		KMASIGNADO=@km*2
		WHERE ESTABLO=@Establo AND FPREVISTA=@FPrevista
	 END
	 ELSE
	 BEGIN
		UPDATE LecturasFinalizadas
		SET PROPIO='0',
		ControladorNoPropio=@Asignado,
		KM=dbo.func_KmLecturasFinalizadas(@Controlador,@Establo),
		KMASIGNADO=@km*2,
		ActualOrdenoM= (case when @TipoControl='1M' then @FComControl else Null end),
		ActualOrdenoT= (case when @TipoControl='1T' then @FComControl else Null end),
		AnteriorOrdeno=@HoraAnteriorOrdeno + ':00',
		FReal=convert(varchar(10),@FComControl,103)
		WHERE ESTABLO=@Establo AND FPREVISTA=@FPrevista
	 END
	 --'.................Ahora actualizo LECTURAS....................................
        UPDATE LECTURASCONTROL 
		SET Estados='0',
		FPrevista=dateadd(month,1,FPrevista),
        TipoControl  = (case when @TipoControl='1M' then '1T' else '1M' end)
        WHERE ESTABLO=@Establo
 
	 FETCH NEXT FROM C1 INTO  @Establo, @FPrevista,@TipoControl,@FComControl,@FDescarga,@HoraAnteriorOrdeno
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GanaderiasToXMLByControlador]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[sp_GanaderiasToXMLByControlador] 
(
@Controlador int 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
DECLARE @controladorString varchar(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))



DELETE FROM [dbo].XMLDATOS WHERE CONTROLADOR=@controladorString
insert into [dbo].XMLDATOS (Codigo,Razon,Act,Fcarga,Fprev,Fctrl,AntOrd_HH,AntOrd_mm,TControl,Telef,Mensaje,Tanque,Controlador,Cetosis,Moroso)

 select ESTABLO,RAZONSOCIAL,'0',GETDATE(),FprevISTA,null,null,null,TIPOCONTROL,Movil,NULL,NULL,@controladorString,Cetosis,'0'
 from LecturasControl,Reggen..GANADERIAS_MAESTRO 
 WHERE NumControler=@Controlador and estados='1'
 and fbaja is null
 AND ESTABLO=CODGAN
 --'VOY A VER SI ES MOROSO......
UPDATE XMLDATOS 
SET MOROSO='1' FROM REGGEN..MOROSOS 
Where MOROSG = 2 AND CODGAN=CODIGO 
GO
/****** Object:  StoredProcedure [dbo].[sp_InseminacionesFromXMLByEstablo]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[sp_InseminacionesFromXMLByEstablo] 
(
@Establo char(7),@Controlador int ,@FPrevista smalldatetime,@TipoControl char(2)
)
AS
--set @Establo='J391727'
--set @Controlador=98
--set  @FPrevista='24/8/2019'
--set @TipoControl='1T'
DECLARE @controladorString varchar(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))


DECLARE @VAC char(14),@DAT smalldatetime,@TIP char(1),@MAD char(14),@NUM int, @TOR char(14),@NTO varchar(40),@GAN char(7),@TEC char(2),@RAZ char(2),@DOS char(1)
DECLARE @GES_DAT smalldatetime,@GES_TIP char(1),@GES_RES char(1),@Ges_Met char(1),@GES_CONF smalldatetime,@CONF_RES char(1)

--Falta meter lo del diagnostico de Confirmación
--TEMPORAL PARA PROBAR ESTO------------------------------------------------------
-----------------------------------------------------------------------------------
	Delete FROM XMLInsem where ins_Controlador=@Controlador
	
INSERT INTO controllechero..xmlInsem(Ins_Vac,Ins_Nom,Ins_Gan,INs_Dat,Ins_Num,INs_Tip,Ins_Tor,Ins_Nto,Ins_Raz,Ins_Tec,
       Ins_Dos,Ges_Dat,Ges_Tip,Ges_Res,Ges_Met,Ins_Controlador)
       SELECT CodigoCrotal,Nombre,Ganaderiacodigo,FechaInseminacion,NumInseminacion,TipoInseminacion,Toro,NombreToro,Raza,Tecnico,
       Dosis,FechaGestacion,TipoGestacion,ResultadoGestacion,MetodoGestacion,Controlador
       FROM  CLM2..CLM2_DOWN_INSEMINACIONES
       where controlador=@Controlador
	
---------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------
-- INS_DAT FInseminacion
-- GES_DAT FGestacion
-- GES_RES Resultado ( + - )
-- GES_CONF FConfirmación
-- CONF_RES Resultado ( + - )

declare C1 cursor LOCAL SCROLL OPTIMISTIC FOR
SELECT INS_VAC,INS_DAT,INS_TIP,INS_MAD,INS_NUM, INS_TOR,INS_NTO,INS_GAN,INS_TEC,INS_RAZ,INS_DOS,GES_DAT,GES_TIP,GES_RES,Ges_Met
FROM XMLINSEM  where INS_controlador=@Controlador
OPEN C1 
FETCH NEXT FROM C1 INTO @VAC,@DAT,@TIP,@MAD,@NUM, @TOR,@NTO,@GAN,@TEC,@RAZ,@DOS,@GES_DAT,@GES_TIP,@GES_RES ,@Ges_Met

WHILE (@@FETCH_STATUS=0)
BEGIN
	-- Preprocesado: recorrido de collection de RefrescarDatosControlDBF COALESCE(@km,0)
	 
	 SET @DAT=Convert(varchar(10),@DAT,103)
	 SET @GES_DAT=Convert(varchar(10),@GES_DAT,103) 
	 SET @GES_TIP=COALESCE(@GES_TIP,'')
	 SET @Ges_Met=COALESCE(@Ges_Met,'')
	 SET @GES_RES=COALESCE(@GES_RES,'')
	 set @TEC= IIf(@TEC='00','V','G')



	--tengo que pasar del numero de ins. Para ello hay que modificar cosas en el ContLech
    INSERT INTO Inseminaciones (ID,NP,NIns,FInseminacion,TipoIns,MadreEmbrion,
        Semental,NombreToro,Establo,Tecnico,EstabloIns,RazaToro,Dosis,NumControlador,FRegistroIns)
        VALUES (@VAC,0,0,convert(varchar(10),@DAT,103),@TIP,@MAD,
        @TOR ,@NTO,@GAN,@TEC,@GAN,@RAZ,@DOS,@Controlador,convert(varchar(10),getdate(),103))
	

 
	 FETCH NEXT FROM C1 INTO   @VAC,@DAT,@TIP,@MAD,@NUM, @TOR,@NTO,@GAN,@TEC,@RAZ,@DOS,@GES_DAT,@GES_TIP,@GES_RES ,@Ges_Met,@GES_CONF
END--WHILE
--actualizo y reordeno inseminaciones
exec [SP_ASIGNA_INSEMINACION_PARTO] @Establo,''
GO
/****** Object:  StoredProcedure [dbo].[sp_InseminacionesToXMLByEstablo]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_InseminacionesToXMLByEstablo]
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
-- cambio NINS por FechaIns...
DELETE FROM XMLINSEM WHERE  usuario=@USUARIO
Insert into XMLINSEM(INS_VAC,INS_GAN,INS_DAT,usuario) 
select vaca,Establo,FInseminacion,@USUARIO
--Insert into View_MaxInseminaciones(id,NINS,establo,det,usuario) 
--select id as ID,max(Nins) as NINS,Establo,Maes_nom,@USUARIO
from ViewInseminacionesEstabloNPIdFinseminacion 
where establo=@Establo
AND fInseminacion>@FechaDesde --'solo meto las de x meses atrás (p.ej). No tiene sentido más
AND NP=0 --SOLO QUIERO LOS QUE ESTEN SIN ASOCIAR AL PARTO ANTERIOR (ya vinculado con sus respectivas inseminaciones)

--ESTO LO PUEDO HACER cuando termine con todos los establos
UPDATE XMLInsem
SET Ins_Tip=TipoIns,
Ins_tor=SEMENTAL,
Ins_Nto=NombreToro,
Ges_Dat= FDiagnostico,
Ges_Res= TipoDiagnostico,
Ges_Tip=MetDiagnostico,
Conf_DAT= FConfirmacion,
CONF_RES=TipoDiagnosticoConf,
Ins_controlador=6
from Inseminaciones
where INS_VAC=ID and INS_GAN=Establo

-- '______---- Borro inseminaciones que no estén en XML_Control (las que han mueto no tiene sentido que vayan)
--            ObjXML.BorrarInsNoCenso(NodLecturas.Establo)




--INSERT INTO XMLInsem
-- (Ins_Vac,Ins_Nom,Ins_Dat,Ins_Tip,Ges_Dat,Ges_Tip,
-- Ins_Mad,Ins_Num,Ins_Tor,Ins_Nto,Ins_Gan,
-- Ins_Tec,Ins_Raz,Ins_Dos,Ins_Controlador)

--SELECT V.ID,V.DET,FInseminacion,TipoIns,FDIAGNOSTICO,V.TipoDiagnostico
--madreEmbrion,V.NIns,SEMENTAL,nombretoro,v.Establo
--Tecnico,RazaToro,Dosis,numcontrolador 

--From Inseminaciones as I,View_MaxInseminaciones as V 
--Where usuario=@USUARIO
--AND v.Establo =@Establo
--AND V.ID=I.ID 
--AND V.NINS=I.NINS --09/03/2018 para que no casque por duplicadas y porque no tiene sentido
--AND I.finseminacion>@FechaDesde
--order by I.id,I.nins 

----================================================================
--SELECT TOP (1000) [ID]
--      ,[DET]
--      ,[ANONA]
--      ,[MESNA]
--      ,[DIANA]
--      ,[COLLAR]
--      ,[USUARIO]
--  FROM [controllechero].[dbo].[VacasJovenes]


----VACAS JOVENES
--DECLARE @MES1 INT,@MES2 INT
--SELECT @MES1=PARAM1,@MES2=PARAM2  FROM ParametrosGenerales WHERE ID ='MES12'
----Meto las terneras
--INSERT INTO VACASJOVENES (ID,DET)

--SELECT CODIGOIdentif ,NOMBRE
--FROM RegGen..ANIMALES
--Where GANPRO = @Establo
--and NUMLAC=0 and AÑOBAJ=0 and Sexo='H'
--and DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), getdate())>=@MES1
--And DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), getdate())<=@MES2
--and  CODIGOIdentif  not in(select Maes_Gen from maestro where Maes_tit=@Establo)

 --Para Hallar las que todavia no se han confirmado
 --       '1º Hallo las últimas inseminaciones
--Insert into View_MaxInseminaciones(id,NINS,establo,det,usuario) select I.id as ID,max(Nins) as NINS,Establo,VJ.DET,@USUARIO 
--from inseminaciones I,VacasJovenes VJ 
--WHERE I.establo=@Establo
--AND I.NP=0 
--AND I.ID=VJ.ID 
--AND I.fInseminacion>@FechaDesde
--group by i.id,establo,VJ.DET
        

--UPDATE XMLINSEM
--SET TIPODIAGNOSTICO=I.TIPODIAGNOSTICO 
--FROM INSEMINACIONES I,View_MaxInseminaciones V
--WHERE  V.ID=I.ID 
--AND I.NP=0 
--AND V.NINS=I.NINS 
       
--SELECT V.ID,v.DET,NP,I.NIns,FInseminacion,SEMENTAL,nombretoro,numcontrolador,
--TipoIns,madreEmbrion,v.Establo,Tecnico,FDIAGNOSTICO,FConfirmacion,V.TipoDiagnostico,TipoDiagnosticoConf,MetDiagnostico,RazaToro,Dosis,Fconafe,SementalErroneo 
--From Inseminaciones as I,View_MaxInseminaciones as V 
--Where usuario=@USUARIO
--AND V.Establo=I.Establo  
--AND V.ID=I.ID 
--AND I.NP=0 
--AND V.NINS=I.NINS 
-- PROCESO: iNSEMINAR -> DIAGNOSTICAR (0,1,2,3)  -> CONFIRMAR(0,1,2,3)
--Me sivo de FINs,Fdiag,Fconfir.
--METO SinDiagnosticar Y SinConfirmar porque el tercer paso sería Confirmación
        --Select Case wDiagnostico
        --    Case "Confirmadas"
        --        AND (V.TipoDiagnostico='1') " '0=no se sabe, 1=+, 2=-,3=dudoso
        --        order by datediff(day,Finseminacion,FDiagnostico) desc "
															--    Case "SinConfirmar"
															--        sSQL = sSQL & "AND (V.TipoDiagnostico='0')"
															--        sSQL = sSQL & "order by datediff(day,FDiagnostico,getdate()) desc 
        --    Case "SinDiagnosticar"
        --        AND (V.TipoDiagnostico='' or V.TipoDiagnostico is null) 
        --        order by datediff(day,Finseminacion,getdate()) desc 
        --End Select















GO
/****** Object:  StoredProcedure [dbo].[sp_NacimientosFromXMLByEstablo]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--drop function [dbo].[func_ConversionUEa14Digitos]
--go
--create function [dbo].func_ConversionUEa14Digitos
--(	@codigocrotal char(14)
--)
--RETURNS char(14)
--AS

--BEGIN
--	Declare @crotal char(14)

--	Set @crotal=
--	(CASE
--		WHEN Left(@codigocrotal, 2) = 'BG' THEN Left(@codigocrotal, 2) + '0000' + Right(@codigocrotal, 8)
--		WHEN Left(@codigocrotal, 2) = 'SI' THEN Left(@codigocrotal, 2) + '0000' + Right(@codigocrotal, 8)
--		WHEN Left(@codigocrotal, 2) = 'LT' THEN Left(@codigocrotal, 2) + '0000' + Right(@codigocrotal, 8)
--		WHEN Left(@codigocrotal, 2) = 'LU' THEN Left(@codigocrotal, 2) + '0000' + Right(@codigocrotal, 8)

--		WHEN Left(@codigocrotal, 2) = 'AT' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
--		WHEN Left(@codigocrotal, 2) = 'CZ' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
--		WHEN Left(@codigocrotal, 2) = 'CY' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
--		WHEN Left(@codigocrotal, 2) = 'SK' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
--		WHEN Left(@codigocrotal, 2) = 'FI' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
--		WHEN Left(@codigocrotal, 2) = 'NL' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
--		WHEN Left(@codigocrotal, 2) = 'PT' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)
--		WHEN Left(@codigocrotal, 2) = 'BE' THEN Left(@codigocrotal, 2) + '000' + Right(@codigocrotal, 9)

--		WHEN Left(@codigocrotal, 2) = 'DE' THEN   Left(@codigocrotal, 2) + '00' + Right(@codigocrotal, 10)
--		WHEN Left(@codigocrotal, 2) = 'EE' THEN   Left(@codigocrotal, 2) + '00' + Right(@codigocrotal, 10)
--		WHEN Left(@codigocrotal, 2) = 'FR' THEN   Left(@codigocrotal, 2) + '00' + Right(@codigocrotal, 10)
--		WHEN Left(@codigocrotal, 2) = 'HU' THEN   Left(@codigocrotal, 2) + '00' + Right(@codigocrotal, 10)
--		WHEN Left(@codigocrotal, 2) = 'AD' THEN   Left(@codigocrotal, 2) + '00' + Right(@codigocrotal, 10)

--		WHEN Left(@codigocrotal, 2) = 'DK' THEN   Left(@codigocrotal, 2) + '0' + Right(@codigocrotal, 11)
--		WHEN Left(@codigocrotal, 2) = 'SE' THEN   Left(@codigocrotal, 2) + '0' + Right(@codigocrotal, 11)

--		WHEN Left(@codigocrotal, 2) = 'ES' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
--		WHEN Left(@codigocrotal, 2) = 'EL' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
--		WHEN Left(@codigocrotal, 2) = 'IE' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
--		WHEN Left(@codigocrotal, 2) = 'IT' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
--		WHEN Left(@codigocrotal, 2) = 'LV' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
--		WHEN Left(@codigocrotal, 2) = 'PL' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
--		WHEN Left(@codigocrotal, 2) = 'UK' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
--		WHEN Left(@codigocrotal, 2) = 'RO' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
--		WHEN Left(@codigocrotal, 2) = 'AD' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)
--		WHEN Left(@codigocrotal, 2) = 'CH' THEN    Left(@codigocrotal, 2) + Right(@codigocrotal, 12)

	
	
--	END
--	)

--  return @crotal
--END
--GO

CREATE PROCEDURE  [dbo].[sp_NacimientosFromXMLByEstablo] 
(
@Establo char(7),@Controlador int ,@FPrevista smalldatetime,@TipoControl char(2)
)
AS

DECLARE @controladorString varchar(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))

DECLARE @VAC char(14),@DAT smalldatetime,@TIP char(1),@MAD char(14),@NUM int, @TOR char(14),@NTO varchar(40),@GAN char(7),@TEC char(2),@RAZ char(2),@DOS char(1)
DECLARE @GES_DAT smalldatetime,@GES_TIP char(1),@GES_RES char(1),@Ges_Met char(1),@GES_CONF smalldatetime,@CONF_RES char(1)

--TEMPORAL PARA PROBAR ESTO------------------------------------------------------
-----------------------------------------------------------------------------------
Delete FROM XMLNacimientos where Controlador=@controladorString
	
INSERT INTO ControlLechero..XMLNacimientos(ESTABLO,CODIGO,NOMBRE,MADRE,PADRE,Tipo,
FECHA_NACIMIENTO,COLLAR,SEXO,PARTO_GEMELAR,SEXO_HERMANO,CAPA,ET,portadora,
FECHA_INSEMINACION,FECHA_DECLARACION,SILUETAS,controlador)

SELECT  GanaderiaCodigo,CodigoCrotal,Nombre,MadreCodigo,PadreCodigo,CONVERT(INT, Tipo),
CONVERT(smalldatetime,NacimientoFecha,103), Collar, Sexo,PartoGemelar,SexoGemelo, Capa,right(ET,1),CodigoPortadora,
FechaInseminacion,DeclaracionFecha,ImagenCarta,@controladorString
From CLM2 . dbo . CLM2_DOWN_NACIMIENTOS 
 WHERE Controlador=@Controlador
	
---------------------------------------------------------------------------------------------------------------------------------------
--1.Cambio USA por 840
UPDATE XMLNACIMIENTOS SET PADRE='840' +  RIGHT(PADRE,11)
FROM REGGEN..PADRES
WHERE RIGHT(PADRE,8)=NUMERO AND PAIS='840'
;
--1.B. Actualizo las importadas GetConversionUEa14Digitos
with ec(eccodigo,ecmadre)
 AS
(
SELECT   CODIGO,MADRE
  FROM XMLNacimientos
 )
 --duda: se podría hacer lo de abajo, pero no se si es func recursiva. Preguntar a Fer (es más rápido que probarlo) ;-)
UPDATE XMLNACIMIENTOS
SET MADRE=dbo.func_ConversionUEa14Digitos(MADRE)
from ec
WHERE CODIGO=eccodigo


--======================================================
--1. inserto los datos en el buzon de Reggen..BUZONNACIMIENTOS
-- '1.A. Inserto los 01-Normal (tienen que tener padre y madre)
	INSERT INTO REGGEN..BUZONNACIMIENTOS(CODIGO,ESTABLO,CONTROLADOR,MADRE,PADRE,FECHA_NACIMIENTO,
	COLLAR,SEXO,NOMBRE,PARTO_GEMELAR,SEXO_HERMANO,CAPA,ET,TIPO,
	FECHA_INSEMINACION,FECHA_DECLARACION,TIPO_MOVIMIENTO,SALIDA, FECHA_GRABACION,
    PADRE_ORIGINAL, FN_ORIGINAL, FINS_ORIGINAL)   

	SELECT CODIGO,ESTABLO,@controladorString,UPPER(MADRE),UPPER(PADRE),FECHA_NACIMIENTO,
	Replicate('0', 5 - ltrim(rtrim(len(collar))))+ ltrim(rtrim(collar)), '2', UPPER(Nombre),PARTO_GEMELAR,SEXO_HERMANO,CAPA,ET,TIPO,
	FECHA_INSEMINACION,Convert(varchar(10), FECHA_DECLARACION, 103),'01','0',CONVERT(varchar(10), GETDATE(), 103),
    UPPER(PADRE),CONVERT(varchar(10), FECHA_NACIMIENTO, 103),FECHA_INSEMINACION 
	FROM XMLNACIMIENTOS 
	WHERE (PADRE Is Not NULL AND LTRIM(RTRIM(PADRE))<>'') And CONTROLADOR=@controladorString
           
--  '1.B. Inserto los 02-auxiliares ( NO tienen padre y madre)
    INSERT INTO REGGEN..BUZONNACIMIENTOS(CODIGO,ESTABLO,CONTROLADOR,MADRE,PADRE,
    FECHA_NACIMIENTO,COLLAR,SEXO,NOMBRE,
    PARTO_GEMELAR,SEXO_HERMANO,CAPA,ET,TIPO,FECHA_INSEMINACION,
    FECHA_DECLARACION,TIPO_MOVIMIENTO,SALIDA,FECHA_GRABACION,
    PADRE_ORIGINAL,FN_ORIGINAL,FINS_ORIGINAL)   

    SELECT CODIGO,ESTABLO,'" & String.Format("{0:000}", CInt(wControlador)) & "' 'CONTROLADOR','XXXH0000000000' ,'XXXM0000000000',
    CONVERT(varchar(10), FECHA_NACIMIENTO, 103),Replicate('0', 5 - ltrim(rtrim(len(collar))))+ ltrim(rtrim(collar)),'2',UPPER(NOMBRE),
    PARTO_GEMELAR,SEXO_HERMANO,CAPA,ET,TIPO,NULL,
    Convert(varchar(10), FECHA_DECLARACION, 103),'02','0',GETDATE(),
    UPPER(PADRE),CONVERT(varchar(10), FECHA_NACIMIENTO, 103),FECHA_INSEMINACION 
    From XMLNACIMIENTOS 
    Where (PADRE Is Null Or PADRE ='') AND CONTROLADOR=@controladorString
           
    --ahora pongo todos los collares 00000 a blancos
    UPDATE REGGEN..BUZONNACIMIENTOS 
    SET COLLAR='' WHERE COLLAR='00000'
 
    UPDATE REGGEN..BUZONNACIMIENTOS 
    SET NOMBRE=REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(NOMBRE, 'á', 'a'), 'é','e'), 'í', 'i'), 'ó', 'o'), 'ú','u')
    UPDATE REGGEN..BUZONNACIMIENTOS 
    SET NOMBRE=UPPER(nombre)
-- 2.1 -- si no hay directorios creados, los creo . miro el correspodiente al dia y mes
	DECLARE @ano CHAR(4),@mes char(2),@Pathfirmas varchar(100),@MakePathfirmas varchar(100)
	set @ano=CAST(year(getdate()) AS VARCHAR(4))
	set @mes=CAST(month(getdate()) AS VARCHAR(2))
	set @mes=REPLICATE('0', (2 - LEN(@mes)))+ @mes
	set @Pathfirmas='DIR "' + 'C:\AFCA_CTXSQL\CONTLECH\FIRMAS\' + @ano +'\' + @mes + '"'
	DECLARE @output INT
	--EXEC @output = XP_CMDSHELL 'DIR "C:\MyNewFolder" /B', NO_OUTPUT
	EXEC @output = XP_CMDSHELL @Pathfirmas, NO_OUTPUT -- quito /b, creo que sobra
	IF @output = 1
		begin	
		  --PRINT 'File Donot exists'
			set @MakePathfirmas='MD "' + 'C:\AFCA_CTXSQL\CONTLECH\FIRMAS\' + @ano +'\' + @mes + '"'
			EXEC master.dbo.xp_cmdshell @MakePathfirmas
	end
-- 2.2 -- INSERTO LAS IMAGENES
					 --Make sure the following statement is executed to enable file IO
					-- From http://msdn.microsoft.com/en-us/library/ms191188.aspx
					--------- --------- --------- --------- --------- --------- --------- 
					--sp_configure 'show advanced options', 1;
					--GO
					--RECONFIGURE;
					--GO
					--EXEC sp_configure 'xp_cmdshell', 1;  
					--GO  
					--RECONFIGURE;  
					--go
					--sp_configure 'Ole Automation Procedures', 1;
					--GO
					--RECONFIGURE;
					--GO
	DECLARE @ImageData varbinary(max)
	DECLARE @Path nvarchar(1024);
	DECLARE @Filename NVARCHAR(1024);
	set @Filename = 'imagen.jpg';
	SET @Path = 'C:\AFCA_CTXSQL\CONTLECH\FIRMAS\' + @ano +'\' + @mes ;
	DECLARE @FullPathToOutputFile NVARCHAR(2048);
	DECLARE @ObjectToken INT

	declare C1 cursor LOCAL SCROLL OPTIMISTIC FOR
	SELECT  CODIGO,FIRMA FROM XMLDATOS WHERE CONTROLADOR=@controladorString
	OPEN C1 
	FETCH NEXT FROM C1 INTO @Establo,@ImageData

	WHILE (@@FETCH_STATUS=0)
	BEGIN
		set @Filename= @Establo + '.jpg'
		set @FullPathToOutputFile=@Path  + '\' + @Filename;

		EXEC sp_OACreate 'ADODB.Stream', @ObjectToken OUTPUT;
		EXEC sp_OASetProperty @ObjectToken, 'Type', 1;
		EXEC sp_OAMethod @ObjectToken, 'Open';
		EXEC sp_OAMethod @ObjectToken, 'Write', NULL, @ImageData;
		EXEC sp_OAMethod @ObjectToken, 'SaveToFile', NULL, @FullPathToOutputFile, 2;
		EXEC sp_OAMethod @ObjectToken, 'Close';
		EXEC sp_OADestroy @ObjectToken;

		FETCH NEXT FROM C1 INTO  @Establo,@ImageData
	END
	close c1 
	deallocate c1
-- 3. ================================ SILUETAS ===================================
	SET @Path = 'C:\AFCA_CTXSQL\REGISTRO\BUZON\'
	declare C1 cursor LOCAL SCROLL OPTIMISTIC FOR
	SELECT CODIGO,SILUETAS FROM XMLNACIMIENTOS WHERE CONTROLADOR=@controladorString
	OPEN C1 
	FETCH NEXT FROM C1 INTO @VAC,@Establo,@ImageData

	WHILE (@@FETCH_STATUS=0)
	BEGIN
		--3.1 Silueta......
		set @Filename= @VAC + '.jpg'
		set @FullPathToOutputFile=@Path  + '\' + @Filename;

		EXEC sp_OACreate 'ADODB.Stream', @ObjectToken OUTPUT;
		EXEC sp_OASetProperty @ObjectToken, 'Type', 1;
		EXEC sp_OAMethod @ObjectToken, 'Open';
		EXEC sp_OAMethod @ObjectToken, 'Write', NULL, @ImageData;
		EXEC sp_OAMethod @ObjectToken, 'SaveToFile', NULL, @FullPathToOutputFile, 2;
		EXEC sp_OAMethod @ObjectToken, 'Close';
		EXEC sp_OADestroy @ObjectToken;

		--3.2 Firma del Ganadero
		set @Filename= 'F_' + @VAC + '.jpg'
		set @FullPathToOutputFile=@Path  + '\' + @Filename;

		Select @ImageData=firma  from XMLdatos  WHERE CODIGO=@Establo

		EXEC sp_OACreate 'ADODB.Stream', @ObjectToken OUTPUT;
		EXEC sp_OASetProperty @ObjectToken, 'Type', 1;
		EXEC sp_OAMethod @ObjectToken, 'Open';
		EXEC sp_OAMethod @ObjectToken, 'Write', NULL, @ImageData;
		EXEC sp_OAMethod @ObjectToken, 'SaveToFile', NULL, @FullPathToOutputFile, 2;
		EXEC sp_OAMethod @ObjectToken, 'Close';
		EXEC sp_OADestroy @ObjectToken;

		FETCH NEXT FROM C1 INTO  @VAC,@Establo,@ImageData
	END
	close c1 
	deallocate c1

--DECLARE @output INT
--EXEC @output = XP_CMDSHELL 'DIR "C:\MyNewFolder" /B', NO_OUTPUT
--EXEC @output = XP_CMDSHELL 'DIR "C:\MyNewFolder"', NO_OUTPUT -- quito /b, creo que sobra
--IF @output = 1
--      PRINT 'File Donot exists'
--		EXEC master.dbo.xp_cmdshell 'MD C:\MyNewFolder'
--ELSE
--      PRINT 'File exists'


GO
/****** Object:  StoredProcedure [dbo].[sp_NacimientosToXMLByEstablo]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_NacimientosToXMLByEstablo]
(
@Establo char(7) ,
@vaca char(14),
@controlador int
)
AS
DECLARE @USUARIO VARCHAR(50)
SELECT @USUARIO= RTRIM(LTRIM(substring(SYSTEM_USER, charindex('\',SYSTEM_USER)+1,LEN(SYSTEM_USER))))

DECLARE @FechaDesde smalldatetime
set @FechaDesde=DateAdd("m", -12, getdate())

 --== REGISTRADAS ...........................................................
--'4. Para meter los registrados de los ultimos 4 meses

DELETE FROM [dbo].XMLRegistrados WHERE ESTABLO=@Establo
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
 INSERT INTO XMLRegistrados(Madre,Codigo,FN,Establo)  -- Después meto estos 3 valores porque no me lo permite ese procedimiento : ,'0','1',Controlador)
 EXEC [GetAnimalesNoRegistradosRIIA] @Establo

 Update XMLRegistrados
 set Registrados='0',
 MadreRegistrada='1'
 where Registrados<>'1'
GO
/****** Object:  StoredProcedure [dbo].[SP_PasarDownToXML]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[SP_PasarDownToXML]
	
(
@Controlador int 

)
AS
--declare @Controlador int
--set @Controlador=6
DECLARE @controladorString char(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))
	/* SET NOCOUNT ON */
	--APROVECHO EL PRIMER CHECK PARA METER LAS DOWN A XML


	Delete FROM XMLControl where Res_controlador=@Controlador 
	Delete FROM XMLDatos where convert(int,controlador)=@Controlador
	Delete FROM XMLInsem  where ins_controlador=@Controlador
	Delete FROM XMLNacimientos  where controlador=@controladorString
	Delete FROM XMLRegistrados where controlador=@controladorString

	INSERT INTO ControlLechero..XMLDatos ( Codigo,Razon,Fprev,FCOMCONTROL,FechaDescarga,TControl,Notas,Controlador,Cetosis,AntOrd_HH,AntOrd_mm,Tanque,Rutinas,firma)
    SELECT Codigo,Nombre,FechaPrevistaDeControl,FechaComienzoControl,FechaDescarga,TipoControl,Notas,@controladorString, Cetosis, HoraAnteriorOrdeno,'00',TanqueLitros,RutinasControl, Firma 
    FROM [CLM2].[dbo].[CLM2_DOWN_GANADERIAS] 
    where controlador=@Controlador



	--.... Revisar para no tener que hacer esto....
	--UPDATE CLM2..CLM2_DOWN_CONTROL SET SexoTernero=CONVERT(INT,SexoTernero),
 --   TamanoTernero=CONVERT(INT,TamanoTernero),
 --   FacilidadParto=CONVERT(INT,FacilidadParto),
 --   conformacionTernero=CONVERT(INT,conformacionTernero),
 --   RetencionPlacenta=CONVERT(INT,RetencionPlacenta),
 --   VelocidadOrdeno=CONVERT(INT,VelocidadOrdeno)
	--........................

	INSERT INTO ControlLechero..XMLControl(RES_ORD, RES_GEN, RES_NOM, RES_FN, RES_TIT,RES_COL, RES_FP, RES_PAD, RES_MAD, RES_FAE,
    RES_NP, RES_NC, RES_AB, RES_FBA, RES_CBA,RES_KLE, RES_KGR, RES_TGR, RES_TPR, RES_TLA, RES_TRC,
    RES_FIN, RES_NIN, RES_SEM, RES_TIN,RES_LAM, RES_LPM, RES_LTO, RES_KANT,RES_NEO, RES_XAB, RES_XFBA, RES_XCBA,
    RES_FACP, RES_VELORD, RES_TEMPERAMENTO, RES_INC,RES_SEXT, RES_TAMT, RES_COMT, RES_RETP,RES_FCTR, RES_FCTR1,Res_Controlador) 
    SELECT CodigoBarras,CodigoCrotal,Nombre,null,GanaderiaCodigo, Collar , FechaParto, null, null, null,
    NP, NC, MovimientoEstado, MovimientoBajaFecha, MovimientoBajaCausa, 0, 0, 0, 0, 0, 0,
    Null, 0, null, null,ControlMananaKilosLeche, ControlTardeKilosLeche, 0, 0,
    vACAnUEVA, MovimientoEstadoOriginal, null,'',
	convert(char(1),convert(INT,FacilidadParto)), VelocidadOrdeno, Temperamento, IncidenciaControl,
	convert(char(1),convert(INT,SexoTernero)), convert(char(1),convert(INT,TamanoTernero)),convert(char(1),convert(INT,conformacionTernero)) ,convert(char(1),convert(INT,RetencionPlacenta)) ,
	ControlMananaFecha, ControlTardeFecha,@Controlador 
	FROM CLM2..CLM2_DOWN_CONTROL
	where controlador=@Controlador

	INSERT INTO controllechero..xmlInsem(Ins_Vac,Ins_Nom,Ins_Gan,INs_Dat,Ins_Num,INs_Tip,Ins_Tor,Ins_Nto,Ins_Raz,Ins_Tec,
    Ins_Dos,Ges_Dat,Ges_Tip,Ges_Res,Ges_Met,Ins_Controlador)
    SELECT CodigoCrotal,Nombre,Ganaderiacodigo,FechaInseminacion,NumInseminacion,TipoInseminacion,Toro,NombreToro,Raza,Tecnico,
    Dosis,FechaGestacion,TipoGestacion,ResultadoGestacion,MetodoGestacion,@Controlador
    FROM  CLM2..CLM2_DOWN_INSEMINACIONES
    where controlador=@Controlador

	INSERT INTO ControlLechero..XMLNacimientos(ESTABLO,CODIGO,NOMBRE,MADRE,PADRE,Tipo,
    FECHA_NACIMIENTO,COLLAR,SEXO,PARTO_GEMELAR,SEXO_HERMANO,CAPA,ET,portadora,
    FECHA_INSEMINACION,FECHA_DECLARACION,SILUETAS,controlador) 
    SELECT  GanaderiaCodigo,CodigoCrotal,Nombre,MadreCodigo,PadreCodigo,CONVERT(INT, Tipo),
    CONVERT(smalldatetime,NacimientoFecha,103), Collar, Sexo,PartoGemelar,SexoGemelo, Capa,right(ET,1),CodigoPortadora,
    FechaInseminacion,DeclaracionFecha,ImagenCarta,@controladorString 'CONTROLADOR' 
    From CLM2..CLM2_DOWN_NACIMIENTOS 
    WHERE Controlador=@Controlador

	INSERT INTO ControlLechero..XMLRCS ( Establo,Barras,DET,FEntregaGanadero,FEntregaControlador,Controlador)
    SELECT GanaderiaCodigo,CodigoBarras,Nombre,FechaCreada,FechaDescarga,@controladorString 
    FROM CLM2..CLM2_DOWN_RCS 
    where controlador=@Controlador

--SET @controladorString='006'
--select CODIGO
--from  XMLDATOS X,LecturasFinalizadas L
--where 
--x.CODIGO=L.Establo
--AND X.FPREV=L.FPrevista
--AND CONTROLADOR=@controladorString




GO
/****** Object:  StoredProcedure [dbo].[SP_PasarXMLToUP]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[SP_PasarXMLToUP]
	
(
@Controlador int 

)
AS
--declare @Controlador int
--set @Controlador=6
DECLARE @controladorString char(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))
	/* SET NOCOUNT ON */
	--APROVECHO EL PRIMER CHECK PARA METER LAS DOWN A XML


	Delete CLM2..CLM2_UP_CONTROL  FROM CLM2..CLM2_UP_CONTROL where Controlador =@Controlador
	Delete FROM CLM2..CLM2_UP_GANADERIAS where Controlador=@Controlador
	Delete FROM CLM2..CLM2_UP_TOROS where Controlador=@Controlador
	Delete FROM CLM2..CLM2_UP_REGISTRADOS where Controlador=@Controlador
	Delete FROM CLM2..CLM2_UP_INSEMINACIONES where Controlador=@Controlador

	INSERT INTO CLM2..CLM2_UP_GANADERIAS (Codigo,Nombre,Telefono,FechaPrevistaDeControl,TipoControl,Cetosis,Mensaje,Moroso,Controlador)
    SELECT codigo,razon,telef,FPREV,TCONTROL,Cetosis,Mensaje,Moroso,@Controlador
	FROM controllechero..xmldatos
    where Controlador =@Controlador

	INSERT INTO CLM2..CLM2_UP_CONTROL (CodigoCrotal,Nombre,collar,MovimientoEstado,GanaderiaCodigo,NP,NC,FechaParto,SexoTernero,
    MovimientoBajaFecha,MovimientoBajaCausa,MovimientoEstadoOriginal,AltaEnservidor,KilosAnterior,VelocidadOrdeno,IncidenciaControl,Temperamento,Muerta,Controlador)
    SELECT Res_GEn,Res_Nom,Res_Col,Res_AB,Res_tit,Res_NP,Res_NC,Res_FP,Res_SexT, Res_FBA,Res_CBA,Res_XAB,Res_RIIA,Res_Kant,Res_Velord,Res_inc,Res_Temperamento,Res_Muerta,@Controlador
    FROM controllechero..xmlCONTROL where RES_CONTROLADOR=@Controlador

	INSERT INTO CLM2..CLM2_UP_INSEMINACIONES  (CodigoCrotal,Nombre,Ganaderiacodigo,FechaInseminacion,NumInseminacion,TipoInseminacion,Toro,NombreToro,Raza,Tecnico,
    Dosis,FechaGestacion,TipoGestacion,ResultadoGestacion,MetodoGestacion,Controlador)
    SELECT Ins_Vac,Ins_Nom,Ins_Gan,INs_Dat,Ins_Num,INs_Tip,Ins_Tor,Ins_Nto,Ins_Raz,Ins_Tec,Ins_Dos,Ges_Dat,Ges_Tip,Ges_Res,Ges_Met,@Controlador
    FROM controllechero..xmlInsem  where Ins_Controlador =@Controlador

	INSERT INTO CLM2..CLM2_UP_REGISTRADOS (CodigoCrotal,Registrado,Madre,GanaderiaCodigo,FN,MadreRegistrada,Controlador)
    SELECT Codigo,Registrados,Madre,Establo,FN,MadreRegistrada,@Controlador
	FROM controllechero..xmlRegistrados  where Controlador =@Controlador

	INSERT INTO CLM2..CLM2_UP_TOROS (Codigo,Nombre,GanaderiaCodigo,Orden,Controlador)
    SELECT Tor_Gen,Tor_Nom,Tor_gan,Tor_orden,@Controlador FROM controllechero..xmlToros 
	-- Falta poner controlador where Controlador =@Controlador
	




GO
/****** Object:  StoredProcedure [dbo].[sp_RcsFromXMLByControlador]    Script Date: 09/09/2019 12:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[sp_RcsFromXMLByControlador] 
(
@Controlador int 
)
AS

--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
DECLARE @controladorString varchar(3)
set @controladorString=REPLICATE('0', (3 - LEN(CAST(@Controlador AS VARCHAR(2)))))  + CAST(@Controlador AS VARCHAR(2))

DECLARE @Establo char(7),@FDescarga SMALLDATETIME

--TEMPORAL PARA PROBAR ESTO------------------------------------------------------
-----------------------------------------------------------------------------------
	Delete FROM XMLRegistrados where controlador=@controladorString
	
	INSERT INTO ControlLechero..XMLRCS (Establo,Barras,DET,FEntregaGanadero,FEntregaControlador,Controlador)
    SELECT GanaderiaCodigo,CodigoBarras,Nombre,FechaCreada,FechaDescarga,Controlador 
    FROM CLM2..CLM2_DOWN_RCS where controlador=@controladorString
----------------------------------------------------
--------------------------------------------------------
	
	Select @FDescarga=FechaDescarga from XMLDATOS, XMLRCS where Codigo=Establo

	INSERT INTO RCS (Barras,det,Establo,FENTREGAGANADERO,FEntregaControlador,FDescargaControlador,
	RCS,FRegistroRCS,Controlador)
    select Barras,Establo,det ,Convert(varchar(10),FEntregaGanadero, 103),Convert(varchar(10),FEntregaControlador, 103),Convert(varchar(10),@FDescarga, 103),
	RCS,Convert(varchar(10),getdate(), 103),@Controlador
	from XMLRCS --cargo todos independiente del controlador
	 

GO
/****** Object:  StoredProcedure [dbo].[sp_TorosToXMLByEstablo]    Script Date: 09/09/2019 12:20:16 ******/
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
--DECLARE @Establo char(7) ,@controlador int
--SET @Establo='J390029'
--SET @controlador=6
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

--DELETE FROM [dbo].XMLRegistrados WHERE ESTABLO=@Establo
--INSERT INTO XMLRegistrados (Codigo,Establo,Registrados,controlador)

--SELECT  codigoidentif ,@Establo,'1',@controlador 
--FROM reggen..animales 
--where ganpro=@Establo
--and codigoidentif in (select codigoidentif 
--from reggen..Vacas 
--WHERE CodigoGanaderia=@Establo AND (TIPOMOVIMIENTO='01' OR TIPOMOVIMIENTO='02') 
--AND DATEDIFF(MONTH,FECHAMOVIMIENTO,GETDATE())<5 )

----5. Para meter los ultimos nacimientos todavia no registrados ..............................................
-- -- MADRE,VACA 'TERNERA',FNAC_HIJO 'FPARTO',@strEstabloConafe 'establo' ....,'0','1',Controlador)
-- INSERT INTO XMLRegistrados(Madre,Codigo,FN,Establo)  -- Después meto estos 3 valores porque no me lo permite ese procedimiento : ,'0','1',Controlador)
-- EXEC [GetAnimalesNoRegistradosRIIA] @Establo

-- Update XMLRegistrados
-- set Registrados='0',
-- MadreRegistrada='1'
-- where Registrados<>'1'
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
