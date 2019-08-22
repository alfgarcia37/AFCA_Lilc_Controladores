CREATE PROCEDURE [dbo].[sp_Controlador_update]
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
,@Original_Id int 
)
AS
--Stored procedure created by FrameworkForge generator tool at 15/04/2019 13:30:25. Copyright : Fernando Pérez Pla.
update [dbo].[CONTROLADORES]
SET
[NUCLEO]=@Nucleo
,[DET]=@Nombre
,[PROVINCIA]=@Provincia
,[LOCALIDAD]=@Localidad
,[DIRECCION]=@Direccion
,[CP]=@CodigoPostal
,[DNINIF]=@NIF
,[TELEFONO1]=@Telefono1
,[TELEFONO2]=@Telefono2
,[FAlta]=@FechaAlta
,[FBaja]=@FechaBaja
,[ZONA]=@Zona
,[FNACTO]=@FechaNacimiento
,[MAIL]=@Mail
,[OBSERVACIONES]=@Observaciones
,[LoginID]=@LoginID
,[NumTerminal]=@NumTerminal
,[Cargo]=@Cargo
,[NumTerminalGral]=@NumTerminalGral
,[Empresa]=@Empresa
,[Numcuna]=@NumCuna
,[Entidad]=@Entidad
,[Oficina]=@Oficina
,[DC]=@DC
,[Cuenta]=@Cuenta
,[PrecioEstablo]=@PrecioEstablo
,[SeguridadSocial]=@SeguridadSocial
,[Sueldoglobal]=@SueldoGlobal
,[Medidores]=@Medidores
,[PuntoEntrega]=@PuntoEntrega
,[ParImpar]=@ParImpar
,[JORNADA]=@Jornada
,[KmPtoEntrega]=@KmPtoEntrega
WHERE [ID]=@Original_Id  

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
 WHERE [ID]=@Original_Id  
