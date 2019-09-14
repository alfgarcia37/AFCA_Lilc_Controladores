USE [controllechero]
GO

/****** Object:  View [dbo].[ViewInseminacionesEstabloNPIdFinseminacion]    Script Date: 10/09/2019 17:45:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewInseminacionesEstabloNPIdFinseminacion]
AS
SELECT        MAX(Establo) AS Establo, MAX(ID) AS Vaca, MAX(FInseminacion) AS FInseminacion
FROM            dbo.Inseminaciones
WHERE        (NP = 0)
GROUP BY Establo, ID
GO
