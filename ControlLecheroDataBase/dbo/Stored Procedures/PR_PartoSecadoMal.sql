




CREATE PROCEDURE [dbo].[PR_PartoSecadoMal]
	(
		@datFecha smalldatetime
	)
AS
	
SELECT RES_ORD,RES_GEN as ID,RES_NOM,Res_FP,Res_NP,Res_Nc,Res_FN,Res_Tit,Res_FPrev,Res_Fctr,
Res_AB,Res_FBA,Res_CBA,Res_KLAM,Res_KLPM,RES_TGR,RES_TPR,RES_TLA,RES_TRC,Res_SW0,res_xab,Res_VelOrd,Res_FacP,Controlador,Res_Temperamento
FROM Control 
Where RES_FPrev>=@datFecha
AND RES_FPrev IS NOT NULL
 and res_ab='B'
AND RES_NC>1
 AND 
     ( 
	(DATEDIFF(DAY,RES_FP,RES_FBA)<0 OR DATEDIFF(DAY,RES_FP,RES_FBA)>500)
 	OR 
		(DATEDIFF(day,RES_FBA,RES_FPREV)<-3 OR DATEDIFF(day,RES_FBA,RES_FPREV)>70 ) 

	OR

	NOT(DATEDIFF(day,res_fP,res_fBA)>(RES_NC-2)*30  	AND  DATEDIFF(day,res_fP,res_fBA)<(RES_NC+2)*30 )
     )
