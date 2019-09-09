USE [controllechero]
GO

/****** Object:  StoredProcedure [dbo].[sp_Controlador_delete]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Controlador_delete]
(
@Original_Id int 
)
AS
--Stored procedure created by FrameworkForge generator tool at 15/04/2019 13:30:25. Copyright : Fernando Pérez Pla.
delete from [dbo].[CONTROLADORES]
WHERE [ID]=@Original_Id  


GO

/****** Object:  StoredProcedure [dbo].[sp_Controlador_deleteByID]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Controlador_deleteByID]
(
@Id int 
)
AS
--Stored procedure created by FrameworkForge generator tool at 15/04/2019 13:30:25. Copyright : Fernando Pérez Pla.
delete from [dbo].[CONTROLADORES]
WHERE [ID]=@Id  

GO

/****** Object:  StoredProcedure [dbo].[sp_Controlador_insert]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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

GO

/****** Object:  StoredProcedure [dbo].[sp_Controlador_select]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Controlador_select]
AS
--Stored procedure created by FrameworkForge generator tool at 15/04/2019 13:30:25. Copyright : Fernando Pérez Pla.
select 
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
from dbo.[CONTROLADORES]

GO

/****** Object:  StoredProcedure [dbo].[sp_Controlador_selectByID]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Controlador_selectByID]
(
@Id int 
)
AS
--Stored procedure created by FrameworkForge generator tool at 15/04/2019 13:30:25. Copyright : Fernando Pérez Pla.
select 
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
from dbo.[CONTROLADORES]
WHERE [ID]=@Id  


GO

/****** Object:  StoredProcedure [dbo].[sp_Controlador_update]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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

GO

/****** Object:  StoredProcedure [dbo].[sp_EstadoLectura_delete]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EstadoLectura_delete]
(
@Original_EstadoID char(2) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
delete from [dbo].[ESTADOS]
WHERE [ID]=@Original_EstadoID  


GO

/****** Object:  StoredProcedure [dbo].[sp_EstadoLectura_deleteByID]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EstadoLectura_deleteByID]
(
@EstadoID char(2) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
delete from [dbo].[ESTADOS]
WHERE [ID]=@EstadoID  

GO

/****** Object:  StoredProcedure [dbo].[sp_EstadoLectura_insert]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EstadoLectura_insert]
(
@EstadoID char(2) 
,@Nombre char(15) 
,@Observaciones varchar(200) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
insert into [dbo].[ESTADOS]
(
[ID]
,[DET]
,[Observaciones])
values(
@EstadoID
,@Nombre
,@Observaciones)

SELECT 
 [ID] [EstadoID] 
,[DET] [Nombre] 
,[Observaciones] [Observaciones]  
 FROM ESTADOS 
 WHERE [ID]=@EstadoID  

GO

/****** Object:  StoredProcedure [dbo].[sp_EstadoLectura_select]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EstadoLectura_select]
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
select 
[ID] [EstadoID] 
,[DET] [Nombre] 
,[Observaciones] [Observaciones] 
from dbo.[ESTADOS]

GO

/****** Object:  StoredProcedure [dbo].[sp_EstadoLectura_selectByID]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EstadoLectura_selectByID]
(
@EstadoID char(2) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
select 
[ID] [EstadoID] 
,[DET] [Nombre] 
,[Observaciones] [Observaciones] 
from dbo.[ESTADOS]
WHERE [ID]=@EstadoID  


GO

/****** Object:  StoredProcedure [dbo].[sp_EstadoLectura_update]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EstadoLectura_update]
(
@EstadoID char(2) 
,@Nombre char(15) 
,@Observaciones varchar(200) 
,@Original_EstadoID char(2) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
update [dbo].[ESTADOS]
SET
[DET]=@Nombre
,[Observaciones]=@Observaciones
WHERE [ID]=@Original_EstadoID  

SELECT 
 [ID] [EstadoID] 
,[DET] [Nombre] 
,[Observaciones] [Observaciones]  
 FROM ESTADOS 
 WHERE [ID]=@Original_EstadoID  

GO

/****** Object:  StoredProcedure [dbo].[sp_LecturaControl_delete]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_LecturaControl_delete]
(
@Original_Establo char(7) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 12:56:24. Copyright : Fernando Pérez Pla.
delete from [dbo].[LecturasControl]
WHERE [Establo]=@Original_Establo  


GO

/****** Object:  StoredProcedure [dbo].[sp_LecturaControl_deleteByID]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_LecturaControl_deleteByID]
(
@Establo char(7) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 12:56:24. Copyright : Fernando Pérez Pla.
delete from [dbo].[LecturasControl]
WHERE [Establo]=@Establo  

GO

/****** Object:  StoredProcedure [dbo].[sp_LecturaControl_insert]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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

GO

/****** Object:  StoredProcedure [dbo].[sp_LecturaControl_select]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_LecturaControl_select]
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
from dbo.[LecturasControl] lc
		inner join [reggen].[dbo].[GANADERIAS_MAESTRO] gm
			on lc.establo = gm.CODGAN
		inner join dbo.ESTADOS e
			on lc.Estados = e.ID
where Fbaja is null
order by NumControler

GO

/****** Object:  StoredProcedure [dbo].[sp_LecturaControl_selectByID]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_LecturaControl_selectByID]
(
@Establo char(7) 
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
WHERE [Establo]=@Establo  


GO

/****** Object:  StoredProcedure [dbo].[sp_LecturaControl_update]    Script Date: 08/08/2019 13:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_LecturaControl_update]
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
,@Original_Establo char(7) 
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 12:56:24. Copyright : Fernando Pérez Pla.
update [dbo].[LecturasControl]
SET
[NumControler]=@NumControler
,[Dia]=@Dia
,[Estados]=@Estado
,[Orden]=@Orden
,[VacasControl]=@VacasControl
,[FPrevista]=@FechaPrevista
,[FControlador]=@FechaControlador
,[Falta]=@FechaAlta
,[Fbaja]=@FechaBaja
,[LoginID]=@LoginID
,[FechaID]=@FechaID
,[Observaciones]=@Observaciones
,[TipoControl]=@TipoControl
,[informe]=@Informe
,[usuario]=@Usuario
,[fusuario]=@FechaUsuario
,[tamanoestablo]=@TamanoEstablo
,[SUMINISTRADATOS]=@SuministraDatos
,[QuitarNombreVaca]=@QuitarNombreVaca
,[NombreVaca]=@NombreVaca
,[NumLote]=@NumLote
,[SMS]=@SMS
,[Movil]=@Movil
,[Mail]=@Mail
,[PARIMPAR]=@ParImpar
,[QUINCENA]=@Quincena
,[EnviarMail]=@EnviarMail
,[MedidoresDigitales]=@MedidoresDigitales
,[ListadoMedidoresDigitales]=@ListadoMedidoresDigitales
,[Asignado]=@Asignado
,[km]=@Kilometros
,[ListadoSoloMail]=@ListadoSoloMail
,[ObsMedidor]=@ObervacionesMedidor
,[Reproductivo]=@Reproductivo
,[Suelo]=@Suelo
,[Amarre]=@Amarre
,[Cetosis]=@Cetosis
,[Frison]=@Frison
WHERE [Establo]=@Original_Establo  

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
 WHERE [Establo]=@Original_Establo  

GO


