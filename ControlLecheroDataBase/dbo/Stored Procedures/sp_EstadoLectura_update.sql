CREATE PROCEDURE [dbo].[sp_EstadoLectura_update]
(
@EstadoID char(2) 
,@Nombre char(15) 
,@Observaciones varchar(200) 
,@Original_EstadoID char(2) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
update [dbo].[ESTADOS]
SET
[DET]=@Nombre
,[Observaciones]=@Observaciones
WHERE [ID]=@Original_EstadoID  

SELECT 
 [ID] [EstadoID] 
,[DET] [Nombre] 
,[Observaciones] [Observaciones]  
 FROM ESTADOS 
 WHERE [ID]=@Original_EstadoID  
