-- Batch submitted through debugger: SQLQuery1.sql|7|0|C:\Documents and Settings\alfongar\Configuración local\Temp\1\~vs1B.sql

-- Create date: 18/10/2013
-- para enviar lactaciiones a conafe
-- =============================================
CREATE PROCEDURE [dbo].[PR_GenerarLactaciones_Conafe]
	(@Vaca char(14),@Desde smalldatetime,@Hasta smalldatetime,@usuario as varchar(25) )

AS

DECLARE @RES_GEN CHAR(14),@RES_FPREV SMALLDATETIME,@RES_NP INT

DELETE FROM LactacionesTEMP -- pq solo borra las que le envio. Quiero borrar tb las de otros periodos. Por si las huebiere
--WHERE USUARIO=@USUARIO
IF @Vaca<>'*'
BEGIN
	DECLARE C33 cursor for 
	SELECT distinct RES_GEN,RES_NP,RES_FPREV
	FROM CONTROL WHERE RES_GEN=@Vaca
	AND RES_FPREV between @Desde and @Hasta
END
ELSE
BEGIN
	DECLARE C33 cursor for 
	SELECT distinct RES_GEN,RES_NP,RES_FPREV
	FROM CONTROL WHERE RES_FPREV between @Desde and @Hasta
	--
	--and res_tit='J399125'
	--hago esto temporal
END

/*
and res_gen in (
SELECT   res FROM Lactaciones  
Where FSecado between '01/01/2013' and '31/12/2013' AND FINALIZADA='S' 
and Dictamen in('0','1','2') and kilos<40000 and diaslac<850
and raza='11' 
and (pureza='B' or Pureza is null)
 and (numconafe + cast(np as char(1))) not in
(select (numconafeJ + cast(npj as char(1))) from certificablesJ))
*/
---

OPEN  C33 
FETCH NEXT FROM C33 INTO  @RES_GEN,@RES_NP,@RES_FPREV
--EXECUTE PR_GENERARLACTACIONES_LISTADOS @RES_GEN,@RES_NP,@RES_FPREV

WHILE (@@FETCH_STATUS=0 ) 
BEGIN
	EXECUTE PR_GENERARLACTACIONES_LISTADOS @RES_GEN,@RES_NP,@RES_FPREV,@usuario
	--EXECUTE PR_DictamenLactacionesTEMP '*',@RES_GEN,@RES_NP -- para conafe no me interesa
	FETCH NEXT FROM C33 INTO  @RES_GEN,@RES_NP,@RES_FPREV
END
CLOSE C33
DEALLOCATE C33

update [LactacionesTEMP]
set raza=maes_raza
from maestro
where res=maes_gen

update [LactacionesTEMP]
set pureza=a.pureza
from reggen..animales a
where res=codigoidentif

update [LactacionesTEMP]
set pureza=a.pureza
from reggen..animales a
where res=(pais+sexo+proani+numero)

DELETE FROM [LactacionesTEMP]
WHERE DICTAMEN='22'
AND FPARTO>'01/01/2009'
AND RES + CONVERT(varchar(10), FPARTO, 103)
IN (
SELECT AUD_GEN + CONVERT(varchar(10), CL_FP, 103)
  FROM [controllechero].[dbo].[ControlAud]
  WHERE    (AUD_KL>0 AND CL_KL>=0)
  AND (AUD_KL/CL_KL<0.6 OR AUD_KL/CL_KL>1.4)
  and ( (aud_INC='' OR aud_INC='00') and(  CL_INC='' OR CL_INC='00'  OR CL_INC='0')) 
  )

