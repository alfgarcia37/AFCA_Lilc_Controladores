CREATE PROCEDURE [dbo].[sp_EstadoLectura_deleteByID]
(
@EstadoID char(2) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
delete from [dbo].[ESTADOS]
WHERE [ID]=@EstadoID  
