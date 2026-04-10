-- ============================================================================
--            OBTENER DATOS DE SESION CON CORREO
-- ============================================================================
-- No se usan codigos de error 
DROP PROCEDURE IF EXISTS ObtenerDatosInicioSesion;
DELIMITER $$

CREATE PROCEDURE ObtenerDatosInicioSesion (
    IN p_Correo VARCHAR(100)
)
BEGIN
    SELECT 
        u.IdUsuario,
        r.Descripcion AS Rol,
        u.Contrasenna
    FROM Usuarios u
    INNER JOIN Roles r ON u.IdRol = r.IdRol
    WHERE u.Correo = p_Correo
    AND u.IdEstado = (SELECT IdEstado FROM Estados WHERE Descripcion = 'Activo' LIMIT 1);
END$$

DELIMITER ;

-- ============================================================================
--             OBTENER ROL DE USUARIO
-- ============================================================================
DROP PROCEDURE IF EXISTS ObtenerRolUsuario;
DELIMITER $$

CREATE PROCEDURE ObtenerRolUsuario (
    IN p_IdUsuario BIGINT UNSIGNED
)
BEGIN
    SELECT 
        r.Descripcion AS Rol
    FROM Usuarios u
    INNER JOIN Roles r ON u.IdRol = r.IdRol
    WHERE u.IdUsuario = p_IdUsuario
    AND u.IdEstado = (SELECT IdEstado FROM Estados WHERE Descripcion = 'Activo' LIMIT 1);
END$$

DELIMITER ;

-- ============================================================================
--             CAMBIAR CONTRASENA DE USUARIO DE RECUPERACION 
-- ============================================================================
DROP PROCEDURE IF EXISTS CambiarContrasennaRecuperacion;
DELIMITER $$

CREATE PROCEDURE CambiarContrasennaRecuperacion (
    IN p_Correo VARCHAR(100),
    IN p_Contrasenna VARCHAR(255),

    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    DECLARE v_error_mensaje TEXT;
    DECLARE v_correo_existe VARCHAR(100);    

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;    

    -- Inicializar valores de salida
    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    START TRANSACTION;

    -- Verificar si el correo existe
    SELECT Correo INTO v_correo_existe FROM Usuarios WHERE Correo = p_Correo LIMIT 1;

    IF v_correo_existe IS NULL THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'Correo electrónico inválido.';
        ROLLBACK;
    ELSE
        -- Actualizar contraseña
        UPDATE Usuarios 
        SET Contrasenna = p_Contrasenna 
        WHERE Correo = p_Correo;

        COMMIT;
        SET p_CodigoError = 0;
        SET p_Mensaje = 'Contraseña actualizada correctamente.';
    END IF;
END$$

DELIMITER ;


-- ============================================================================
--             CAMBIAR CONTRASENA DE USUARIO LOGUEADO
-- ============================================================================
DROP PROCEDURE IF EXISTS CambiarContrasennaLogueado;
DELIMITER $$

CREATE PROCEDURE CambiarContrasennaLogueado (
    IN p_IdUsuario VARCHAR(100),
    IN p_Contrasenna VARCHAR(255),
    IN p_NuevaContrasenna VARCHAR(255),
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    DECLARE v_error_mensaje TEXT;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;

    main_block: BEGIN
        -- Inicializar valores de salida
        SET p_CodigoError = 0;
        SET p_Mensaje = '';

        START TRANSACTION;

        -- Verificar si el usuario y contraseña actual coinciden
        UPDATE Usuarios
        SET Contrasenna = p_NuevaContrasenna 
        WHERE IdUsuario = p_IdUsuario AND Contrasenna = p_Contrasenna;

        IF ROW_COUNT() = 0 THEN
            SET p_CodigoError = 1;
            SET p_Mensaje = 'Contraseña incorrecta.';
            ROLLBACK;
            LEAVE main_block;
        END IF;

        COMMIT;
        SET p_CodigoError = 0;
        SET p_Mensaje = 'Contraseña actualizada correctamente.';
    END main_block;
END$$
DELIMITER ;


-- ============================================================================
--            OBTENER DATOS DE SESION CON IdUsuario
-- ============================================================================

DROP PROCEDURE IF EXISTS DatosValidacionCambioContrasenna;
DELIMITER $$

CREATE PROCEDURE DatosValidacionCambioContrasenna (
    IN p_IdUsuario VARCHAR(100)
)
BEGIN
    SELECT IdUsuario, Correo, Contrasenna
    FROM Usuarios
    WHERE IdUsuario = p_IdUsuario
    AND IdEstado = (SELECT IdEstado FROM Estados WHERE Descripcion = 'Activo' LIMIT 1);
END$$

DELIMITER ;