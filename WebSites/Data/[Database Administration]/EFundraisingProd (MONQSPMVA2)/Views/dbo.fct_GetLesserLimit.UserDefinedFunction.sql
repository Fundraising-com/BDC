USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fct_GetLesserLimit]    Script Date: 02/14/2014 13:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Cette fonction retourne la borne inférieure d'une intervalle dans laquelle se trouve une valeur
-- Entrée:
	-- @Range: l'intervalle
	-- @Value: la valeur recherchée
	-- @LastValue: la dernière borne qui part de @LastValue jusqu'à l'infini
	-- @StartValue: le nombre auquel on part (habituellement 0 ou 1)
-- Sortie: La borne inférieure d'une intervalle
-- Nicolas Désy 27 juin 2003

/*Exemples: 

	On veut des intervalles de 20 qui débutent à 1 ([1,20], [21,40]...) et on veut savoir dans quelle 
	intervalle se trouve 45, le système va retourner 41 car ce chiffre se trouve dans l'intervalle [41,60] 
	On doit passer en paramètres: (45,20,(N'importe quoi),1)

	Des intervalles de 10, ça débute à 0 ([0
*/

CREATE  FUNCTION [dbo].[fct_GetLesserLimit] (@Value int, @Range int, @LastValue int, @StartValue int = 0) RETURNS Int
BEGIN
	DECLARE @Result int
	
	If @Value >= @LastValue
		Set @Result = @LastValue + @StartValue
	Else
		If @Value < @StartValue
			Set @Result = 0
		Else
			Set @Result = @Value - (@Value % @Range) + @StartValue
	RETURN @Result
END
GO
