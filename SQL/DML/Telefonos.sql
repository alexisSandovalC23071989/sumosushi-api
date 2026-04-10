
-- ============================================================================
-- 1) Obtener telefonos de un usuario
-- ============================================================================

DELIMITER //

CREATE PROCEDURE ObtenerTelefonosUsuario(
    IN p_IdUsuario BIGINT UNSIGNED
)
BEGIN 
    -- Obtener los teléfonos del usuario
    SELECT Telefono 
    FROM TelefonosUsuarios
    WHERE IdUsuario = p_IdUsuario;
    
END //

DELIMITER ;