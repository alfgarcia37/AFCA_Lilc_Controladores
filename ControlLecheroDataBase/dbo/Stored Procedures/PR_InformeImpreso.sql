








CREATE PROCEDURE PR_InformeImpreso
	(
		@Establo char(7)
	)
AS

UPDATE LECTURASCONTROL  
SET INFORME='0'
WHERE Establo = @Establo







