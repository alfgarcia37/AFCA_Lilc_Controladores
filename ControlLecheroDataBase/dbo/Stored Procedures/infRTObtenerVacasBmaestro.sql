









CREATE PROCEDURE dbo.infRTObtenerVacasBmaestro
	(
		@Establo char(7),
		@FControl smalldatetime
	)
AS

/* INFORME - REBAÑO TOTAL */
IF @Establo<>''
	BEGIN
		select res_fba,res_fp,maes_gen,maes_np,maes_nc from maestro,control 
		where 
		maes_tit=@Establo and 
		maes_gen=res_gen and 
		maes_estado='B' and
		maes_np=res_np and
		maes_nc=res_nc and
		datediff(month,res_fba,@FCONTROL)<5
		order by maes_gen, maes_np
	END
ELSE
	BEGIN
		select res_fba,res_fp,maes_gen,maes_np,maes_nc from maestro,control 
		where 
		maes_tit=res_tit and 
		maes_gen=res_gen and 
		maes_estado='B' and
		maes_np=res_np and
		maes_nc=res_nc and
		datediff(month,res_fba,@FCONTROL)<5
		order by maes_gen, maes_np
	END





