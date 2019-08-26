CREATE PROCEDURE [dbo].[sp_EstadoLectura_insert]
(
@EstadoID char(2) 
,@Nombre char(15) 
,@Observaciones varchar(200) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
insert into [dbo].[ESTADOS]
(
[ID]
,[DET]
,[Observaciones])
values(
@EstadoID
,@Nombre
,@Observaciones)

SELECT 
 [ID] [EstadoID] 
,[DET] [Nombre] 
,[Observaciones] [Observaciones]  
 FROM ESTADOS 
 WHERE [ID]=@EstadoID  
