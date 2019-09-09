USE [controllechero]
GO
/****** Object:  StoredProcedure [dbo].[sp_GanaderiasToXMLByControlador]    Script Date: 31/08/2019 16:33:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].sp_CheckMaestroControlesToXML 
(
@Establo char(7) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
DECLARE @controladorString varchar(3)
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