
--======================================================
--    OBTENER TODAS LAS PRESENTACIONES
--======================================================

DROP PROCEDURE IF EXISTS ObtenerPresentaciones;
DELIMITER $$
CREATE PROCEDURE ObtenerPresentaciones()
BEGIN
    SELECT 
        IdPresentacion, 
        Descripcion AS Presentacion
    FROM Presentaciones;
END$$