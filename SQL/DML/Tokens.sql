DROP PROCEDURE IF EXISTS RegistrarRefreshToken;
DELIMITER $$

CREATE PROCEDURE RegistrarRefreshToken (
    IN p_IdUsuario BIGINT UNSIGNED,
    IN p_RefreshToken VARCHAR(255),
    IN p_FechaInicio DATETIME,
    IN p_FechaCaducidad DATETIME,
    
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
bloque_principal: BEGIN
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;
    DECLARE v_error_mensaje TEXT;
    DECLARE v_token_existente INT DEFAULT 0;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;

    -- Handler para errores NOT FOUND
    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = 'Error: No se encontraron los datos requeridos en la configuración.';
    END;

    -- Inicializar valores de salida
    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    -- Validar si ya existe el RefreshToken
    SELECT COUNT(*) INTO v_token_existente
    FROM RefreshTokens
    WHERE RefreshToken = p_RefreshToken;

    IF v_token_existente > 0 THEN
        SET p_CodigoError = 1;
        LEAVE bloque_principal;
    END IF;

    START TRANSACTION;

        SELECT IdTipoEntidad INTO v_IdTipoEntidad
        FROM TipoEntidades
        WHERE Descripcion = 'RefreshTokens'
        LIMIT 1;

        INSERT INTO RefreshTokens (IdUsuario, RefreshToken, IdTipoEntidad, FechaInicio, FechaCaducidad)
        VALUES (p_IdUsuario, p_RefreshToken, v_IdTipoEntidad, p_FechaInicio, p_FechaCaducidad);

    COMMIT;

    SET p_CodigoError = 0;
    SET p_Mensaje = 'RefreshToken registrado correctamente.';

END$$

DELIMITER ;


-- ============================================================================
--             UTILIZAR REFRESH TOKEN 
-- ============================================================================

DROP PROCEDURE IF EXISTS UtilizarRefreshToken;
DELIMITER $$

CREATE PROCEDURE UtilizarRefreshToken (
    IN p_RefreshToken VARCHAR(255)
)
bloque_principal: BEGIN   
    DECLARE v_IdUsuario BIGINT UNSIGNED DEFAULT NULL;
    DECLARE v_FechaCaducidad DATETIME;
    DECLARE v_Utilizado INT UNSIGNED DEFAULT 0;
    DECLARE v_Revocado BIT DEFAULT 0;
    DECLARE v_DescripcionRol VARCHAR(50);
    DECLARE v_EsSospechoso BOOLEAN DEFAULT FALSE;
    
    -- Handler para errores
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    START TRANSACTION;

    -- Una sola consulta para obtener todo
    SELECT 
        rt.IdUsuario, 
        rt.FechaCaducidad, 
        rt.Utilizado, 
        rt.Revocado,
        r.Descripcion,
        (rt.Utilizado >= 1 OR rt.Revocado = 1) AS EsSospechoso
    INTO v_IdUsuario, v_FechaCaducidad, v_Utilizado, v_Revocado, v_DescripcionRol, v_EsSospechoso
    FROM RefreshTokens rt
    INNER JOIN Usuarios u ON rt.IdUsuario = u.IdUsuario
    INNER JOIN Roles r ON u.IdRol = r.IdRol
    WHERE rt.RefreshToken = p_RefreshToken
    FOR UPDATE;

    -- Validación temprana
    IF v_IdUsuario IS NULL THEN
        ROLLBACK;
        LEAVE bloque_principal;
    END IF;

    -- Update optimizado condicional
    IF v_EsSospechoso THEN
        -- Actividad sospechosa: revocar todos los tokens del usuario
        UPDATE RefreshTokens 
        SET Utilizado = CASE WHEN RefreshToken = p_RefreshToken THEN Utilizado + 1 ELSE Utilizado END,
            Revocado = 1
        WHERE IdUsuario = v_IdUsuario;
    ELSE
        -- Uso normal: solo incrementar contador
        UPDATE RefreshTokens
        SET Utilizado = Utilizado + 1
        WHERE RefreshToken = p_RefreshToken;
    END IF;

    COMMIT;

    -- Resultado
    SELECT 
        v_IdUsuario AS IdUsuario, 
        v_FechaCaducidad AS FechaCaducidad, 
        v_Utilizado + 1 AS Utilizado,
        v_Revocado OR v_EsSospechoso AS Revocado,
        v_DescripcionRol AS Rol;

END$$
DELIMITER ;




-- ============================================================================
--             REGISTRAR TOKEN DE RECUPERACION DE CONTRASENNA
-- ============================================================================
DROP PROCEDURE IF EXISTS RegistrarTokenRecuperacion;
DELIMITER $$

CREATE PROCEDURE RegistrarTokenRecuperacion (
    IN p_Correo VARCHAR(100),
    IN p_Token VARCHAR(255),
    IN p_FechaExpiracion DATETIME,    
    
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;
    DECLARE v_error_mensaje TEXT;    

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;

    -- Handler para errores NOT FOUND
    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = 'Error: No se encontraron los datos requeridos en la configuración.';
    END;

    -- Inicializar valores de salida
    SET p_CodigoError = 0;
    SET p_Mensaje = '';


    START TRANSACTION;

        SELECT IdTipoEntidad INTO v_IdTipoEntidad FROM TipoEntidades WHERE Descripcion = 'TokenRecuperacionContrasenna' LIMIT 1;

        INSERT INTO TokenRecuperacionContrasenna (Correo, Token, FechaExpiracion, IdTipoEntidad, Usado)
        VALUES (p_Correo, p_Token, p_FechaExpiracion, v_IdTipoEntidad, 0);

    COMMIT;

    SET p_CodigoError = 0;
    SET p_Mensaje = 'Token de recuperación de contraseña registrado correctamente.';

END$$

DELIMITER ;

-- ============================================================================
--             USAR TOKEN DE RECUPERACION DE CONTRASENNA
-- ============================================================================
DROP PROCEDURE IF EXISTS UtilizarTokenRecuperacion;
DELIMITER $$

CREATE PROCEDURE UtilizarTokenRecuperacion (
    IN p_Token VARCHAR(255)
)
BEGIN  

    UPDATE TokenRecuperacionContrasenna
    SET Usado = 1
    WHERE Token = p_Token
    AND Usado = 0;

    IF ROW_COUNT() = 0 THEN
        SELECT NULL AS Token;
    ELSE
        SELECT Token, Correo, FechaExpiracion
            FROM TokenRecuperacionContrasenna
            WHERE Token = p_Token;
    END IF;
END$$

DELIMITER ;


-- ============================================================================
--             REVOCAR REFRESH TOKEN
-- ============================================================================
DROP PROCEDURE IF EXISTS RevocarRefreshToken;
DELIMITER $$

CREATE PROCEDURE RevocarRefreshToken (    
    IN p_RefreshToken VARCHAR(255),
    
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
bloque_principal: BEGIN
    DECLARE v_error_mensaje TEXT;

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
        UPDATE RefreshTokens
        SET Revocado = 1
        WHERE RefreshToken = p_RefreshToken;
    COMMIT;

    SET p_CodigoError = 0;
    SET p_Mensaje = 'RefreshToken revocado correctamente.';

END$$

DELIMITER ;