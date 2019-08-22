CREATE VIEW [AFCADOM\pc05].[VistaPartos_OLD]
AS
selecT  max(res_gen) as gen,max(res_np) as NP ,max(res_fp) as FP,MAX(RES_NOM) as DET,max(res_tit) as establo
from control where res_fp is not null
group by res_gen,res_np,res_fp
