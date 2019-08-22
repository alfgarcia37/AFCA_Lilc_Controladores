CREATE PROCEDURE [dbo].[sp_LecturaControl_insert]
(
@NumControler int 
,@Establo char(7) 
,@Dia int 
,@Estado char(1) 
,@Orden char(1) 
,@VacasControl int 
,@FechaPrevista smalldatetime 
,@FechaControlador smalldatetime 
,@FechaAlta smalldatetime 
,@FechaBaja smalldatetime 
,@LoginID char(20) 
,@FechaID datetime 
,@Observaciones char(200) 
,@TipoControl char(2) 
,@Informe char(1) 
,@Usuario char(15) 
,@FechaUsuario smalldatetime 
,@TamanoEstablo char(1) 
,@SuministraDatos char(1) 
,@QuitarNombreVaca char(1) 
,@NombreVaca char(200) 
,@NumLote int 
,@SMS char(1) 
,@Movil char(9) 
,@Mail char(50) 
,@ParImpar char(1) 
,@Quincena char(1) 
,@EnviarMail char(1) 
,@MedidoresDigitales varchar(10) 
,@ListadoMedidoresDigitales char(1) 
,@Asignado int 
,@Kilometros real 
,@ListadoSoloMail char(1) 
,@ObervacionesMedidor varchar(100) 
,@Reproductivo char(1) 
,@Suelo char(2) 
,@Amarre char(1) 
,@Cetosis char(1) 
,@Frison char(1) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 12:56:24. Copyright : Fernando Pérez Pla.
insert into [dbo].[LecturasControl]
(
[NumControler]
,[Establo]
,[Dia]
,[Estados]
,[Orden]
,[VacasControl]
,[FPrevista]
,[FControlador]
,[Falta]
,[Fbaja]
,[LoginID]
,[FechaID]
,[Observaciones]
,[TipoControl]
,[informe]
,[usuario]
,[fusuario]
,[tamanoestablo]
,[SUMINISTRADATOS]
,[QuitarNombreVaca]
,[NombreVaca]
,[NumLote]
,[SMS]
,[Movil]
,[Mail]
,[PARIMPAR]
,[QUINCENA]
,[EnviarMail]
,[MedidoresDigitales]
,[ListadoMedidoresDigitales]
,[Asignado]
,[km]
,[ListadoSoloMail]
,[ObsMedidor]
,[Reproductivo]
,[Suelo]
,[Amarre]
,[Cetosis]
,[Frison])
values(
@NumControler
,@Establo
,@Dia
,@Estado
,@Orden
,@VacasControl
,@FechaPrevista
,@FechaControlador
,@FechaAlta
,@FechaBaja
,@LoginID
,@FechaID
,@Observaciones
,@TipoControl
,@Informe
,@Usuario
,@FechaUsuario
,@TamanoEstablo
,@SuministraDatos
,@QuitarNombreVaca
,@NombreVaca
,@NumLote
,@SMS
,@Movil
,@Mail
,@ParImpar
,@Quincena
,@EnviarMail
,@MedidoresDigitales
,@ListadoMedidoresDigitales
,@Asignado
,@Kilometros
,@ListadoSoloMail
,@ObervacionesMedidor
,@Reproductivo
,@Suelo
,@Amarre
,@Cetosis
,@Frison)

SELECT 
 [NumControler] [NumControler] 
,[Establo] [Establo] 
,[Dia] [Dia] 
,[Estados] [Estado] 
,[Orden] [Orden] 
,[VacasControl] [VacasControl] 
,[FPrevista] [FechaPrevista] 
,[FControlador] [FechaControlador] 
,[Falta] [FechaAlta] 
,[Fbaja] [FechaBaja] 
,[LoginID] [LoginID] 
,[FechaID] [FechaID] 
,[Observaciones] [Observaciones] 
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
 FROM LecturasControl 
 WHERE [Establo]=@Establo  
