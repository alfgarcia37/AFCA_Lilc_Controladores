USE [CLM2]
GO

/****** Object:  StoredProcedure [dbo].[sp_UpdateEstadoGanaderia_FromTerminal]    Script Date: 13/12/2019 10:07:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdateEstadoGanaderia_FromTerminal]
(
	@Establo char(7),
	@Estado char(3)
)
AS
	UPDATE ControlLechero..LecturasControl SET ESTADOS=case when @Estado='BUZ' then '6'
															when @Estado='DEL' then '3'
															when @Estado='TER' then '5'
															else ESTADOS end
	WHERE Establo=@Establo


GO


