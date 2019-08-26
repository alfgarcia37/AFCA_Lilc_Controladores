USE [controllechero]
GO

/****** Object:  StoredProcedure [dbo].[sp_GanaderiasToXMLByControlador]    Script Date: 25/08/2019 18:18:03 ******/
DROP PROCEDURE [dbo].[sp_GanaderiasToXMLByControlador]
GO

/****** Object:  StoredProcedure [dbo].[sp_GanaderiasToXMLByControlador]    Script Date: 25/08/2019 18:18:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GanaderiasToXMLByControlador]
(
@Controlador int 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.


insert into [dbo].XMLDATOS
(
CODIGO
,CONTROLADOR
)
 select top 10 establo,'6' from LecturasControl
GO

