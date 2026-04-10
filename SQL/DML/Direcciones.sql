
DELIMITER //

CREATE PROCEDURE ObtenerDireccionesUsuario(
    IN p_IdUsuario BIGINT UNSIGNED
)
BEGIN 
    SELECT         
        d.Detalle,
        d.IdDistrito,
        dist.Nombre AS Distrito,
        c.IdCanton,
        c.Nombre AS Canton
    FROM Direcciones d
    INNER JOIN Distritos dist ON d.IdDistrito = dist.IdDistrito
    INNER JOIN Cantones c ON dist.IdCanton = c.IdCanton
    WHERE d.IdUsuario = p_IdUsuario
    ORDER BY d.DireccionPrincipal DESC, d.FechaRegistro DESC;
    
END //

DELIMITER ;