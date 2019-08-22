CREATE PROCEDURE [dbo].[sp_EstadoLectura_select]
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
select 
[ID] [EstadoID] 
,[DET] [Nombre] 
,[Observaciones] [Observaciones] 
from dbo.[ESTADOS]
