
--execute [dbo].[sp_LecturaControl_update_state] @Establo='J390126',@Estado='1'
CREATE PROCEDURE [dbo].[sp_LecturaControl_update_state]
(
	@Establo char(7) 
   ,@Estado char(1) 
)
AS
--version 1 2019.08.22 14:30

update [dbo].[LecturasControl]
SET [Estados]=@Estado
WHERE [Establo]=@Establo  

