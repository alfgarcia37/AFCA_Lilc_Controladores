
CREATE PROCEDURE [dbo].[PR_DuplicadosPartos_DBF]
	
AS
	/* SET NOCOUNT ON */
SELECT RES_ORD,RES_GEN as ID,RES_NOM,Res_FP,Res_Col,Res_NP,Res_Nc,Res_AB,Res_xab,Res_FN,Res_Tit,Res_FPrev,Res_Fctr,
Res_FBA,Res_CBA,Res_KLAM,Res_KLPM,RES_INC,RES_TGR,RES_TPR,RES_TLA,RES_TRC,Res_acetona,Res_BHB,Res_SW0,res_vel,Controlador,res_facp,res_velord,Res_Temperamento,Res_urea
FROM CONTROLDBF C,PARTOS P
WHERE C.RES_GEN=P.ID AND
C.RES_XAB='B' AND C.RES_AB='A'
AND C.RES_NP=P.NP