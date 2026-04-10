

-- ============================================================================
--             REGISTRO USUARIO   *** ASI SON LOS PROCEDIMIENTOS DE REGISTRAR DATOS
-- ============================================================================
-- Codigo 0 todo bien
-- Codigo 1 error controlado
-- Codigo 2 error no controlado
----------------------------------------------
DROP PROCEDURE IF EXISTS RegistrarUsuario;
DELIMITER $$
CREATE PROCEDURE RegistrarUsuario (
    IN p_Nombre VARCHAR(50),
    IN p_Apellidos VARCHAR(100),
    IN p_Correo VARCHAR(100),
    IN p_Telefono VARCHAR(20),
    IN p_Contrasenna VARCHAR(100),
    
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    -- ESTE SE OCUPA PARA CUALQUIER REGISTRO
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;

    DECLARE v_error_mensaje TEXT;
    DECLARE v_IdRol SMALLINT UNSIGNED;
    DECLARE v_IdEstado SMALLINT UNSIGNED;    

    DECLARE v_correo_existe INT DEFAULT 0;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;

    -- Handler para errores NOT FOUND (por SELECT INTO que no devuelve nada)
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

    -- Verificar si el correo ya existe
    SELECT COUNT(*) INTO v_correo_existe FROM Usuarios WHERE Correo = p_Correo;

    IF v_correo_existe > 0 THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El correo electrónico ya está registrado.';
        ROLLBACK;
    ELSE
        -- Obtener IDs necesarios
        SELECT IdRol INTO v_IdRol FROM Roles WHERE Descripcion = 'Usuario' LIMIT 1;
        SELECT IdEstado INTO v_IdEstado FROM Estados WHERE Descripcion = 'Activo' LIMIT 1;
        
        -- *** Este se ocupa para cualquier registro *** 
        SELECT IdTipoEntidad INTO v_IdTipoEntidad FROM TipoEntidades WHERE Descripcion = 'Usuario' LIMIT 1;

        -- Insertar usuario
        INSERT INTO Usuarios (Nombre, Apellidos, Correo, Contrasenna, IdRol, IdEstado, IdTipoEntidad)
            VALUES (p_Nombre, p_Apellidos, p_Correo, p_Contrasenna, v_IdRol, v_IdEstado, v_IdTipoEntidad);

        SELECT IdTipoEntidad INTO v_IdTipoEntidad FROM TipoEntidad WHERE Nombre = 'TelefonoUsuario' LIMIT 1;
        INSERT INTO TelefonosUsuarios (IdUsuario, Telefono, IdTipoEntidad)
            VALUES (LAST_INSERT_ID(), p_Telefono, v_IdTipoEntidad);

        COMMIT;
        SET p_CodigoError = 0;
        SET p_Mensaje = 'Usuario registrado correctamente.';
    END IF;

END$$
DELIMITER ;


-- ============================================================================
--             OBTENER PERFIL DE USUARIO
-- ============================================================================

DELIMITER

CREATE PROCEDURE ObtenerPerfilUsuario(
    IN p_IdUsuario BIGINT UNSIGNED
)
BEGIN 
    -- Obtener información básica del usuario
    SELECT 
        Nombre,
        Apellidos,
        Correo
    FROM Usuarios
    WHERE IdUsuario = p_IdUsuario;
    
END //

DELIMITER ;

-- ========================================
-- ActualizarPerfilUsuario
-- ========================================
DROP PROCEDURE IF EXISTS ActualizarPerfilUsuario;
DELIMITER $$
CREATE PROCEDURE ActualizarPerfilUsuario (
    IN p_IdUsuario    BIGINT UNSIGNED,
    IN p_Nombre       VARCHAR(50),
    IN p_Apellidos    VARCHAR(100),
    
    OUT p_CodigoError INT,
    OUT p_Mensaje     TEXT
)
BEGIN
    DECLARE v_error_msg TEXT;
    DECLARE v_IdEstado  INT;

    -- Manejo de errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_msg = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje    = CONCAT('Error en servidor: ', v_error_msg);
    END;

    START TRANSACTION;
    -- 1. Actualiza nombre y apellidos si vienen no nulos
    UPDATE Usuarios
    SET 
      Nombre    = COALESCE(NULLIF(p_Nombre, ''), Nombre),
      Apellidos = COALESCE(NULLIF(p_Apellidos, ''), Apellidos)
    WHERE IdUsuario = p_IdUsuario;

    COMMIT;
    SET p_CodigoError = 0;
    SET p_Mensaje    = 'Perfil actualizado correctamente.';
END$$
DELIMITER ;

-- ========================================
-- DesactivarCuentaUsuario
-- ========================================
DROP PROCEDURE IF EXISTS DesactivarCuentaUsuario;
DELIMITER $$
CREATE PROCEDURE DesactivarCuentaUsuario (
    IN p_IdUsuario    BIGINT UNSIGNED,
    OUT p_CodigoError INT,
    OUT p_Mensaje     TEXT
)
BEGIN
    DECLARE v_error_msg TEXT;
    DECLARE v_IdEstado  INT;
    -- Manejador de errores
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_msg = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje    = CONCAT('Error en servidor: ', v_error_msg);
    END;

    START TRANSACTION;
    -- Obtiene el IdEstado correspondiente a 'Inactivo'
    SELECT IdEstado
      INTO v_IdEstado
      FROM Estados
     WHERE Descripcion = 'Inactivo'
     LIMIT 1;

    -- Actualiza el estado del usuario
    UPDATE Usuarios
       SET IdEstado = v_IdEstado
     WHERE IdUsuario = p_IdUsuario;

    COMMIT;
    SET p_CodigoError = 0;
    SET p_Mensaje    = 'Cuenta desactivada correctamente.';
END$$
DELIMITER ;

-- ============================================================================
--             OBTENER PERFIL DE USUARIO CON CORREO
-- ============================================================================
DROP PROCEDURE IF EXISTS ObtenerUsuarioPorCorreo;
DELIMITER $$

CREATE PROCEDURE ObtenerUsuarioPorCorreo (
    IN p_Correo VARCHAR(100)
)
BEGIN
    SELECT 
        U.IdUsuario,
        U.Nombre,
        U.Apellidos,
        U.IdRol,
        R.Descripcion AS Rol
    FROM Usuarios U
    INNER JOIN Roles R ON U.IdRol = R.IdRol
    WHERE U.Correo = p_Correo
    LIMIT 1;
END $$

DELIMITER ;


-- ============================================================================
--             ACTUALIZAR ROL DE USUARIO
-- ============================================================================
DROP PROCEDURE IF EXISTS ActualizarRolUsuario;
DELIMITER $$

CREATE PROCEDURE ActualizarRolUsuario (
    IN p_IdUsuario INT UNSIGNED,
    IN p_IdRol SMALLINT UNSIGNED,
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

    -- Inicializar
    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    START TRANSACTION;

    -- Validar existencia del usuario
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = p_IdUsuario) THEN
        ROLLBACK;
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El usuario especificado no existe.';
    ELSE
        -- Actualizar el rol
        UPDATE Usuarios
        SET IdRol = p_IdRol
        WHERE IdUsuario = p_IdUsuario;

        COMMIT;
        SET p_CodigoError = 0;
        SET p_Mensaje = 'Rol actualizado correctamente.';
    END IF;
END $$

DELIMITER ;

-- ============================================================================
--             DESHABILITAR USUARIO POR CORREO
-- ============================================================================
DROP PROCEDURE IF EXISTS DeshabilitarUsuarioPorCorreo;
DELIMITER $$

CREATE PROCEDURE DeshabilitarUsuarioPorCorreo (
    IN p_Correo VARCHAR(100),
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    DECLARE v_error_mensaje TEXT;
    DECLARE v_IdEstado SMALLINT UNSIGNED;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;

    -- Handler para NOT FOUND (por SELECT INTO sin resultados)
    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SET p_CodigoError = 3;
        SET p_Mensaje = 'No se encontró el estado Inactivo';
    END;

    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    START TRANSACTION;

    -- Verificar existencia del usuario
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Correo = p_Correo) THEN
        ROLLBACK;
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El usuario especificado no existe.';
    ELSE
        -- Obtener IdEstado de Inactivo directamente
        SELECT IdEstado INTO v_IdEstado
        FROM Estados
        WHERE Descripcion = 'Inactivo'
        LIMIT 1;

        -- Actualizar estado
        UPDATE Usuarios
        SET IdEstado = v_IdEstado
        WHERE Correo = p_Correo;

        COMMIT;
        SET p_CodigoError = 0;
        SET p_Mensaje = 'Usuario deshabilitado correctamente.';
    END IF;

END $$

DELIMITER ;




-- ============================================================================
--             REHABILITAR USUARIO POR CORREO
-- ============================================================================
DROP PROCEDURE IF EXISTS RehabilitarUsuarioPorCorreo;
DELIMITER $$

CREATE PROCEDURE RehabilitarUsuarioPorCorreo (
    IN p_Correo VARCHAR(100),
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    DECLARE v_error_mensaje TEXT;
    DECLARE v_IdEstado SMALLINT UNSIGNED;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;

    -- Handler para NOT FOUND (por SELECT INTO sin resultados)
    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SET p_CodigoError = 3;
        SET p_Mensaje = 'No se encontró el estado Inactivo';
    END;

    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    START TRANSACTION;

    -- Verificar existencia del usuario
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Correo = p_Correo) THEN
        ROLLBACK;
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El usuario especificado no existe.';
    ELSE
        -- Obtener IdEstado de Inactivo directamente
        SELECT IdEstado INTO v_IdEstado
        FROM Estados
        WHERE Descripcion = 'Activo'
        LIMIT 1;

        -- Actualizar estado
        UPDATE Usuarios
        SET IdEstado = v_IdEstado
        WHERE Correo = p_Correo;

        COMMIT;
        SET p_CodigoError = 0;
        SET p_Mensaje = 'Usuario habilitado correctamente.';
    END IF;

END $$

DELIMITER ;

-- ============================================================================
--             OBTENER USUARIO POR CORREO
-- ============================================================================

DROP PROCEDURE IF EXISTS ObtenerUsuarioPorCorreo;
DELIMITER $$
CREATE PROCEDURE ObtenerUsuarioPorCorreo (
    IN p_Correo VARCHAR(100)
)   
BEGIN
    SELECT 
        U.IdUsuario,
        U.Nombre,
        U.Apellidos,
        U.Correo,
        U.IdRol,
        R.Descripcion AS Rol
    FROM Usuarios U
    INNER JOIN Roles R ON U.IdRol = R.IdRol
    WHERE U.Correo = p_Correo
    LIMIT 1;
END $$

DROP PROCEDURE IF EXISTS ObtenerUsuarioPorCorreo;
DELIMITER $$

-- ============================================================================
--             OBTENER COLABORADORES DE RESTAURANTE
-- ============================================================================

DROP PROCEDURE IF EXISTS ObtenerColaboradoresRestaurante;
DELIMITER $$

CREATE PROCEDURE ObtenerColaboradoresRestaurante()
BEGIN
    DECLARE v_error_mensaje TEXT;
    DECLARE v_IdRolAdmin SMALLINT UNSIGNED;
    DECLARE v_IdRolColaborador SMALLINT UNSIGNED;
    DECLARE v_IdEstadoActivo SMALLINT UNSIGNED;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = v_error_mensaje;
    END;

    -- Handler para casos donde no se encuentren resultados con SELECT INTO
    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'No se encontraron uno o más identificadores requeridos (roles o estado activo).';
    END;

    START TRANSACTION;

    -- Obtener IDs dinámicamente por descripción
    SELECT IdRol INTO v_IdRolAdmin FROM Roles WHERE Descripcion = 'Admin';
    SELECT IdRol INTO v_IdRolColaborador FROM Roles WHERE Descripcion = 'Colaborador';
    SELECT IdEstado INTO v_IdEstadoActivo FROM Estados WHERE Descripcion = 'Activo';

    -- Consulta de colaboradores y administradores activos
    SELECT 
        U.IdUsuario,
        U.Nombre,
        U.Apellidos,
        U.Correo,
        U.IdRol,
        R.Descripcion AS Rol
    FROM Usuarios U
    INNER JOIN Roles R ON U.IdRol = R.IdRol
    WHERE U.IdRol IN (v_IdRolAdmin, v_IdRolColaborador)
      AND U.IdEstado = v_IdEstadoActivo
    ORDER BY R.Descripcion, U.Nombre, U.Apellidos;

    COMMIT;
END $$

DELIMITER ;

-- ============================================================================
--             OBTENER USUARIOIS DESHABILITADOS 
-- ============================================================================
DROP PROCEDURE IF EXISTS ObtenerUsuariosDeshabilitados;
DELIMITER $$

CREATE PROCEDURE ObtenerUsuariosDeshabilitados (
)
BEGIN
    DECLARE v_DescripcionEstado VARCHAR(25) DEFAULT 'Inactivo';
    DECLARE v_IdEstado SMALLINT UNSIGNED;

    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'No se encontro el estado Inactivo';
    END;

    SELECT IdEstado INTO v_IdEstado FROM Estados WHERE Descripcion = v_DescripcionEstado;

    SELECT 
        U.IdUsuario,
        U.Nombre,
        U.Apellidos,
        U.Correo,
        U.IdRol,
        R.Descripcion AS Rol
    FROM Usuarios U
    INNER JOIN Roles R ON U.IdRol = R.IdRol
    WHERE IdEstado = v_IdEstado
    ORDER BY 
    LIMIT 1;
END $$

DELIMITER ;



-- ============================================================================
--             REGISTRAR TELEFONO DE USUARIO
-- ============================================================================

DROP PROCEDURE IF EXISTS AgregarTelefonoUsuario;
DELIMITER $$
CREATE PROCEDURE AgregarTelefonoUsuario (
    IN p_IdUsuario BIGINT UNSIGNED,
    IN p_Telefono VARCHAR(20),

    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
proc: BEGIN
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;
    DECLARE v_telefono_existente INT DEFAULT 0;
    DECLARE v_error_mensaje TEXT;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;

    -- Inicialización
    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    START TRANSACTION;

    -- Verificar si el usuario existe
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = p_IdUsuario) THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El usuario no existe.';
        ROLLBACK;
        LEAVE proc;
    END IF;

    -- Verificar si ya tiene registrado ese teléfono
    SELECT COUNT(*) INTO v_telefono_existente
    FROM TelefonosUsuarios
    WHERE IdUsuario = p_IdUsuario AND Telefono = p_Telefono;

    IF v_telefono_existente > 0 THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El teléfono ya está registrado para este usuario.';
        ROLLBACK;
    ELSE
        -- Obtener tipo de entidad para teléfono de usuario
        SELECT IdTipoEntidad INTO v_IdTipoEntidad
        FROM TipoEntidades
        WHERE Descripcion = 'TelefonoUsuario'
        LIMIT 1;

        -- Insertar teléfono
        INSERT INTO TelefonosUsuarios (IdUsuario, Telefono, IdTipoEntidad)
        VALUES (p_IdUsuario, p_Telefono, v_IdTipoEntidad);

        COMMIT;
        SET p_CodigoError = 0;
        SET p_Mensaje = 'Teléfono registrado correctamente.';
    END IF;

END$$
DELIMITER ;

-- ============================================================================
--             ELIMINAR TELEFONO DE USUARIO
-- ============================================================================


DROP PROCEDURE IF EXISTS EliminarTelefonoUsuario;
DELIMITER $$
CREATE PROCEDURE EliminarTelefonoUsuario (
    IN p_IdUsuario BIGINT UNSIGNED,
    IN p_Telefono VARCHAR(20),

    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    DECLARE v_telefono_existente INT DEFAULT 0;
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

    -- Verificar existencia del usuario
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = p_IdUsuario) THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El usuario no existe.';
        ROLLBACK;
        LEAVE proc;
    END IF;

    -- Verificar existencia del teléfono para el usuario
    SELECT COUNT(*) INTO v_telefono_existente
    FROM TelefonosUsuarios
    WHERE IdUsuario = p_IdUsuario AND Telefono = p_Telefono;

    IF v_telefono_existente = 0 THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El teléfono no está registrado para este usuario.';
        ROLLBACK;
    ELSE
        -- Eliminar el teléfono
        DELETE FROM TelefonosUsuarios
        WHERE IdUsuario = p_IdUsuario AND Telefono = p_Telefono;

        COMMIT;
        SET p_CodigoError = 0;
        SET p_Mensaje = 'Teléfono eliminado correctamente.';
    END IF;

END$$
DELIMITER ;
