CREATE function [dbo].[fn_getControladorLectura_byEstado]
(
	@numController int,
	@estado char(1)
)
returns int

as
--version 2 2019.08.22
begin
	declare @cantidad int

	select @cantidad =count(*)
	from dbo.LecturasControl 
	where Fbaja is null and NumControler=@numController and Estados=@estado

	return @cantidad	
end

