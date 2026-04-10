-- ============================================================================
--             OBTENER INFO RESTAURANTE
-- ============================================================================
DROP PROCEDURE IF EXISTS ObtenerInfoRestaurante;
DELIMITER $$

CREATE PROCEDURE ObtenerInfoRestaurante()
BEGIN
  SELECT 
    IdRestaurante,
    Nombre,
    Direccion,
    Telefono,
    Correo,
    Imagen
  FROM Restaurante
  LIMIT 1;
END $$

DELIMITER ;


-- ============================================================================
--             UPDATE INFORMACION RESTAURANTE
-- ============================================================================
DROP PROCEDURE IF EXISTS ActualizarInformacionRestaurante;
DELIMITER $$

CREATE PROCEDURE ActualizarInformacionRestaurante (
    IN p_Nombre VARCHAR(100),
    IN p_Direccion TEXT,
    IN p_Telefono VARCHAR(20),
    IN p_Correo VARCHAR(100),
    IN p_LogoUrl VARCHAR(2083),

    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
bloque_principal: BEGIN
    DECLARE v_error_mensaje TEXT;

    -- Manejo de errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;

    -- Inicializar variables de salida
    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    START TRANSACTION;

        -- Actualizar datos del restaurante
        UPDATE Restaurante
        SET Nombre = p_Nombre,
            Direccion = p_Direccion,
            Telefono = p_Telefono,
            Correo = p_Correo,
            LogoUrl = p_LogoUrl;

        -- Verificar si realmente se modificó algún dato
        IF ROW_COUNT() = 0 THEN
            ROLLBACK;
            SET p_CodigoError = 1;
            SET p_Mensaje = 'La información del restaurante ya estaba actualizada.';
            LEAVE bloque_principal;
        END IF;

    COMMIT;

    SET p_CodigoError = 0;
    SET p_Mensaje = 'Información del restaurante actualizada correctamente.';

END$$
DELIMITER ;
