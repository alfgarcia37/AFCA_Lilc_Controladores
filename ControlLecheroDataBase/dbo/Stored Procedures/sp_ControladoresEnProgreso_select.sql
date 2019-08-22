--execute dbo.sp_ControladoresEnProgreso_select @finalizadas=1
CREATE PROCEDURE  [dbo].[sp_ControladoresEnProgreso_select]
(
  @finalizadas tinyint=0
)
AS
--version 3 2019.08.22

declare @hoy datetime
set @hoy=getdate()

;with ec(cid) AS
(
	select  NumControler
	from dbo.LecturasControl lc
			inner join dbo.CONTROLADORES c
				on lc.NumControler = c.ID 
	where c.FBaja is null and lc.Fbaja is null and 
			(
			 	   (@finalizadas=0 and ESTADOS in(2,5,6))
				   or
				   (@finalizadas=1 and ESTADOS not in(2,5) and FPrevista>eomonth(@hoy))		
			)
	group by NumControler
)
,et(cid,enbuzon,enterminal,encurso) AS
(
   select cid
		 ,dbo.fn_getControladorLectura_byEstado(cid,'6')
		 ,dbo.fn_getControladorLectura_byEstado(cid,'5')
		 ,dbo.fn_getControladorLectura_byEstado(cid,'2')
   from ec
)
,ew(cid,enbuzon,enterminal,encurso,warning) AS
(
	select cid,enbuzon,enterminal,encurso,
	    case when enterminal<=1 and encurso=0 and @finalizadas=0 then 1
			--when then 
			 else 0 end Warning
	from et
)
select cid as CodigoControlador,
	  c.DET	Nombre,
	  enbuzon EnBuzon,
	  enterminal EnTerminal,
	  encurso EnCurso,
	  warning Warning,
	  case when warning=1 then 'Planificar ganaderias'
		   when warning=2 then 'Aviso Warning 2'
		   else '' end Nota
from ew
	inner join dbo.CONTROLADORES c
		on ew.cid = c.ID

