
-- NUEVOS REGISTROS 

-- Codigo 0 todo bien
-- Codigo 1 error controlado
-- Codigo 2 error no controlado

DROP PROCEDURE IF EXISTS RegistrarCategoria;
DELIMITER $$
CREATE PROCEDURE RegistrarCategoria (
    -- Parametros 
    IN p_Descripcion VARCHAR(50),

    -- SIEMPRE HAY ESTOS DE SALIDA 
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    -- ESTE SE OCUPA PARA CUALQUIER REGISTRO
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;
    DECLARE v_error_mensaje TEXT;
    -- --------------------------------------
    -- VARIABLES PARA VALIDACIONES 
    DECLARE v_descripcion_existe INT DEFAULT 0;

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

    -- VERIFICACIONES DE ERRORES CONTROLADOS 
    SELECT COUNT(*) INTO v_descripcion_existe FROM CategoriaProductos WHERE Descripcion = p_Descripcion LIMIT 1;

    IF v_descripcion_existe > 0 THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'La descripcion ya existe.';
        ROLLBACK;
    ELSE        
        -- *** Este se ocupa para cualquier registro *** 
        SELECT IdTipoEntidad INTO v_IdTipoEntidad FROM TipoEntidades WHERE Descripcion = 'CategoriaProductos' LIMIT 1;

        -- Insertar descripcion en la tabla CategoriaProductos
        INSERT INTO CategoriaProductos (Descripcion, IdTipoEntidad)
            VALUES (p_Descripcion, v_IdTipoEntidad);

        COMMIT;
        SET p_CodigoError = 0;
        SET p_Mensaje = 'Descripcion registrada correctamente.';
    END IF;

END$$
DELIMITER ;


-- Deshabilitar
DROP PROCEDURE IF EXISTS DeshabilitarCategoria;
DELIMITER $$
CREATE PROCEDURE DeshabilitarCategoria (
    -- Parametros 
    IN p_IdCategoria VARCHAR(50)

    -- SIEMPRE HAY ESTOS DE SALIDA 
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    -- ESTE SE OCUPA PARA CUALQUIER REGISTRO
    DECLARE v_idEstado SMALLINT UNSIGNED;
    DECLARE v_error_mensaje TEXT;
    ----------------------------------------------
    -- VARIABLES PARA VALIDACIONES 

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
      
        -- *** Este se ocupa para cualquier registro *** 
        SELECT IdEstado INTO v_idEstado FROM Estados WHERE Descripcion = 'Inactivo' LIMIT 1;

        UPDATE CategoriaProductos
        SET IdEstado = v_idEstado;

        if ROW_COUNT() = 0 THEN
            SET p_CodigoError = 1;
            SET p_Mensaje = 'No se encontraron registros para actualizar.';
            ROLLBACK;
        ELSE
            COMMIT;
            SET p_CodigoError = 0;
            SET p_Mensaje = 'Estado actualizado correctamente.';
        END IF;

        COMMIT;

    END IF;

END$$
DELIMITER ;
