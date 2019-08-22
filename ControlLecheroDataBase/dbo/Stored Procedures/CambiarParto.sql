







CREATE PROCEDURE dbo.CambiarParto
	(
		@strEstablo char(14),
		@strID char(14),
		@intOldNP int,
		@intNewNP int
	)
AS
	/* SET NOCOUNT ON */
	UPDATE CONTROL SET RES_NP=@intNewNP
	 WHERE RES_TIT= @strEstablo AND
		 RES_GEN= @strID AND
		  RES_NP=@intOldNP
	UPDATE MAESTRO SET MAES_NP=@intNewNP
	 WHERE MAES_TIT= @strEstablo AND
		 MAES_GEN= @strID AND
		  MAES_NP=@intOldNP
	UPDATE PARTOS SET NP=@intNewNP
		 WHERE ESTABLO= @strEstablo AND
		 ID= @strID AND
		  NP=@intOldNP








