CREATE PROCEDURE [dbo].[sp_Controlador_insert]
(
@Nucleo char(3) 
,@Id int 
,@Nombre char(75) 
,@Provincia char(15) 
,@Localidad char(50) 
,@Direccion char(50) 
,@CodigoPostal char(5) 
,@NIF char(12) 
,@Telefono1 char(10) 
,@Telefono2 char(10) 
,@FechaAlta smalldatetime 
,@FechaBaja smalldatetime 
,@Zona char(2) 
,@FechaNacimiento smalldatetime 
,@Mail char(50) 
,@Observaciones varchar(250) 
,@LoginID varchar(20) 
,@NumTerminal char(15) 
,@Cargo char(5) 
,@NumTerminalGral char(15) 
,@Empresa char(5) 
,@NumCuna char(15) 
,@Entidad char(4) 
,@Oficina char(4) 
,@DC char(2) 
,@Cuenta char(10) 
,@PrecioEstablo real 
,@SeguridadSocial real 
,@SueldoGlobal char(1) 
,@Medidores char(3) 
,@PuntoEntrega char(3) 
,@ParImpar char(1) 
,@Jornada char(10) 
,@KmPtoEntrega real 
)
AS
--Stored procedure created by FrameworkForge generator tool at 15/04/2019 13:30:25. Copyright : Fernando Pérez Pla.
insert into [dbo].[CONTROLADORES]
(
[NUCLEO]
,[ID]
,[DET]
,[PROVINCIA]
,[LOCALIDAD]
,[DIRECCION]
,[CP]
,[DNINIF]
,[TELEFONO1]
,[TELEFONO2]
,[FAlta]
,[FBaja]
,[ZONA]
,[FNACTO]
,[MAIL]
,[OBSERVACIONES]
,[LoginID]
,[NumTerminal]
,[Cargo]
,[NumTerminalGral]
,[Empresa]
,[Numcuna]
,[Entidad]
,[Oficina]
,[DC]
,[Cuenta]
,[PrecioEstablo]
,[SeguridadSocial]
,[Sueldoglobal]
,[Medidores]
,[PuntoEntrega]
,[ParImpar]
,[JORNADA]
,[KmPtoEntrega])
values(
@Nucleo
,@Id
,@Nombre
,@Provincia
,@Localidad
,@Direccion
,@CodigoPostal
,@NIF
,@Telefono1
,@Telefono2
,@FechaAlta
,@FechaBaja
,@Zona
,@FechaNacimiento
,@Mail
,@Observaciones
,@LoginID
,@NumTerminal
,@Cargo
,@NumTerminalGral
,@Empresa
,@NumCuna
,@Entidad
,@Oficina
,@DC
,@Cuenta
,@PrecioEstablo
,@SeguridadSocial
,@SueldoGlobal
,@Medidores
,@PuntoEntrega
,@ParImpar
,@Jornada
,@KmPtoEntrega)

SELECT 
 [NUCLEO] [Nucleo] 
,[ID] [Id] 
,[DET] [Nombre] 
,[PROVINCIA] [Provincia] 
,[LOCALIDAD] [Localidad] 
,[DIRECCION] [Direccion] 
,[CP] [CodigoPostal] 
,[DNINIF] [NIF] 
,[TELEFONO1] [Telefono1] 
,[TELEFONO2] [Telefono2] 
,[FAlta] [FechaAlta] 
,[FBaja] [FechaBaja] 
,[ZONA] [Zona] 
,[FNACTO] [FechaNacimiento] 
,[MAIL] [Mail] 
,[OBSERVACIONES] [Observaciones] 
,[LoginID] [LoginID] 
,[FechaID] [FechaID] 
,[NumTerminal] [NumTerminal] 
,[Cargo] [Cargo] 
,[NumTerminalGral] [NumTerminalGral] 
,[Empresa] [Empresa] 
,[Numcuna] [NumCuna] 
,[Entidad] [Entidad] 
,[Oficina] [Oficina] 
,[DC] [DC] 
,[Cuenta] [Cuenta] 
,[PrecioEstablo] [PrecioEstablo] 
,[SeguridadSocial] [SeguridadSocial] 
,[Sueldoglobal] [SueldoGlobal] 
,[Medidores] [Medidores] 
,[PuntoEntrega] [PuntoEntrega] 
,[ParImpar] [ParImpar] 
,[JORNADA] [Jornada] 
,[KmPtoEntrega] [KmPtoEntrega]  
 FROM CONTROLADORES 
 WHERE [ID]=@Id  
