







CREATE PROCEDURE dbo.PR_DuplicadosBD_DBF
	
AS
	/* SET NOCOUNT ON */
INSERT INTO CONTROLDBFDUPLICADOS
(RES_GEN,RES_NP, RES_NC, RES_TIT, RES_FPREV,controlador)

select DBF.RES_GEN,DBF.RES_NP, DBF.RES_NC, DBF.RES_TIT, DBF.RES_FPREV,dbf.controlador 
from  controlDBF DBF,control ct 
where 
DBF.res_gen=ct.res_gen and
DBF.res_np=ct.res_np and
DBF.res_nc=ct.res_nc






