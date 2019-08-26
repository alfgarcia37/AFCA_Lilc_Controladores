CREATE PROCEDURE [dbo].[sp_Controlador_delete]
(
@Original_Id int 
)
AS
--Stored procedure created by FrameworkForge generator tool at 15/04/2019 13:30:25. Copyright : Fernando Pérez Pla.
delete from [dbo].[CONTROLADORES]
WHERE [ID]=@Original_Id  

