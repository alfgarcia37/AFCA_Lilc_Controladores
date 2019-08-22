CREATE PROCEDURE [dbo].[sp_LecturaControl_delete]
(
@Original_Establo char(7) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 12:56:24. Copyright : Fernando Pérez Pla.
delete from [dbo].[LecturasControl]
WHERE [Establo]=@Original_Establo  

