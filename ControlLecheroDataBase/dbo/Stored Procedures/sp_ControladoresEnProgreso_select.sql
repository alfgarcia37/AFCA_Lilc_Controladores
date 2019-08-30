USE [controllechero]
GO
/****** Object:  StoredProcedure [dbo].[sp_ControladoresEnProgreso_select]    Script Date: 30/08/2019 17:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--execute dbo.sp_ControladoresEnProgreso_select @finalizadas=0
ALTER PROCEDURE  [dbo].[sp_ControladoresEnProgreso_select]
(
  @finalizadas tinyint=0
)
AS
--version 3 2019.08.22

declare @hoy datetime
set @hoy=getdate()


;with el(cid,pendientes) as
(
	 select lec.ID,sum(pendiente)
	 from dbo.CONTROLADORES c	
		inner join 
	 (select NumControler ID, case when FPrevista>eomonth(@hoy) then 0 else 1 end	pendiente				
	 from dbo.LecturasControl lc
				inner join dbo.CONTROLADORES c
					on lc.NumControler = c.ID 
	 where c.FBaja is null and lc.Fbaja is null) lec
		on c.ID = lec.ID
	group by lec.ID
)
,ec(cid) AS
(
	select ID
	from dbo.CONTROLADORES c
				inner join el
						on c.ID = el.cid
		where (@finalizadas=0 and pendientes>0)
					   or
			  (@finalizadas=1 and pendientes=0)
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

