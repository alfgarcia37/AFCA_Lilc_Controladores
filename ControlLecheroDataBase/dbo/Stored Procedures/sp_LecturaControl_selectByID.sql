CREATE PROCEDURE [dbo].[sp_LecturaControl_selectByID]
(
@Establo char(7) = null,
@CodigoControlador int =null
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 12:56:24. Copyright : Fernando Pérez Pla.
select 
[NumControler] [NumControler] 
,[Establo] [Establo] 
,gm.RAZONSOCIAL [EstabloNombre]
,[Dia] [Dia] 
,[Estados] [Estado] 
,e.DET [EstadoNombre]
,[Orden] [Orden] 
,[VacasControl] [VacasControl] 
,[FPrevista] [FechaPrevista] 
,[FControlador] [FechaControlador] 
,[Falta] [FechaAlta] 
,[Fbaja] [FechaBaja] 
,[LoginID] [LoginID] 
,[FechaID] [FechaID] 
,lc.[Observaciones] [Observaciones] 
,[TipoControl] [TipoControl] 
,[informe] [Informe] 
,[usuario] [Usuario] 
,[fusuario] [FechaUsuario] 
,[tamanoestablo] [TamanoEstablo] 
,[SUMINISTRADATOS] [SuministraDatos] 
,[QuitarNombreVaca] [QuitarNombreVaca] 
,[NombreVaca] [NombreVaca] 
,[NumLote] [NumLote] 
,[SMS] [SMS] 
,[Movil] [Movil] 
,[Mail] [Mail] 
,[PARIMPAR] [ParImpar] 
,[QUINCENA] [Quincena] 
,[EnviarMail] [EnviarMail] 
,[MedidoresDigitales] [MedidoresDigitales] 
,[ListadoMedidoresDigitales] [ListadoMedidoresDigitales] 
,[Asignado] [Asignado] 
,[km] [Kilometros] 
,[ListadoSoloMail] [ListadoSoloMail] 
,[ObsMedidor] [ObervacionesMedidor] 
,[Reproductivo] [Reproductivo] 
,[Suelo] [Suelo] 
,[Amarre] [Amarre] 
,[Cetosis] [Cetosis] 
,[Frison] [Frison] 
from dbo.[LecturasControl]  lc
		inner join [reggen].[dbo].[GANADERIAS_MAESTRO] gm
			on lc.establo = gm.CODGAN
		inner join dbo.ESTADOS e
			on lc.Estados = e.ID
WHERE Fbaja is null and (@Establo is null or( @Establo is not null and [Establo]=@Establo ))
		and (@CodigoControlador is null or ( @CodigoControlador is not null and NumControler=@CodigoControlador))

