








CREATE PROCEDURE dbo.infObtenerDatosControlador
	(
		@Establo char(7),
		@FPrevista smalldatetime
	)
AS

/* INFORME - REBAÑO TOTAL */
select convert(char(2),id) + '-' + det from controladores,lecturasfinalizadas
where establo=@establo and fprevista=@fprevista
and numcontroler=id








