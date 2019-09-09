SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function dbo.fn_getControladorLectura_byEstado
(
	@numController int,
	@estado char(1)
)
returns int

as

begin
	declare @cantidad int

	select @cantidad =count(*)
	from dbo.LecturasControl 
	where NumControler=@numController and Estados=@estado

	return @cantidad	
end

go
--execute dbo.sp_ControladoresEnProgreso_select
CREATE PROCEDURE  dbo.sp_ControladoresEnProgreso_select
AS
--version 2 2019.08.15
with ec(cid) AS
(
	select  NumControler
	from dbo.LecturasControl
	where ESTADOS in(2,5,6)
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
	    case when enterminal<=1 and encurso=0 then 1
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
	  case when warning=1 then 'Aviso warning 1'
		   when warning=2 then 'Avis Warning 2'
		   else '' end Nota
from ew
	inner join dbo.CONTROLADORES c
		on ew.cid = c.ID

GO