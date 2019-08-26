







CREATE PROCEDURE PR_CAMBIARFECHAPARTO
	(
		@strEstablo char(14),
		@strID char(14),
		@intNP int,
		@datFP smalldatetime
	)
AS
	/* SET NOCOUNT ON */
	UPDATE CONTROL SET RES_FP=@datFP
	 WHERE RES_TIT= @strEstablo AND
		 RES_GEN= @strID AND
		  RES_NP=@intNP
	UPDATE MAESTRO SET MAES_FP=@datFP
	 WHERE MAES_TIT= @strEstablo AND
		 MAES_GEN= @strID AND
		  MAES_NP=@intNP
	UPDATE PARTOS SET FParto=@datFP
		 WHERE ESTABLO= @strEstablo AND
		 ID= @strID AND
		  NP=@intNP








