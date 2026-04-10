
-- ============================================================================
--             OBTENER DIAS DE LA SEMANA
-- ============================================================================

DROP PROCEDURE IF EXISTS ObtenerListaDias;
DELIMITER $$

CREATE PROCEDURE ObtenerListaDias()
BEGIN
    SELECT 
        IdDia,
        Nombre as Dia
    FROM Dias
    ORDER BY IdDia;
END $$

DELIMITER ;


-- ============================================================================
--             OBTENER HORARIOs RESTAURANTE
-- ============================================================================
DROP PROCEDURE IF EXISTS ObtenerHorariosRestaurante;
DELIMITER $$

CREATE PROCEDURE ObtenerHorariosRestaurante()
BEGIN
    SELECT 
        h.IdDia,
        d.Nombre AS Dia,
        h.HoraApertura,
        h.HoraCierre
    FROM HorarioRestaurante h
    INNER JOIN Dias d ON h.IdDia = d.IdDia
    ORDER BY h.IdDia;
END$$

DELIMITER ;

-- ============================================================================
--             REGISTRAR HORARIO RESTAURANTE
-- ============================================================================
DROP PROCEDURE IF EXISTS RegistrarHorario;
DELIMITER $$

CREATE PROCEDURE RegistrarHorario (
    IN p_IdDia TINYINT UNSIGNED,
    IN p_HoraApertura TIME,
    IN p_HoraCierre TIME,
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
bloque_principal: BEGIN
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

    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    START TRANSACTION;

    -- Validación: la hora de apertura debe ser menor que la de cierre
    IF p_HoraApertura >= p_HoraCierre THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'La hora de apertura debe ser menor que la hora de cierre.';
        ROLLBACK;
        LEAVE bloque_principal;
    END IF;

    -- Eliminar el horario anterior si existe
    DELETE FROM HorarioRestaurante WHERE IdDia = p_IdDia;

    -- Obtener tipo de entidad
    SELECT IdTipoEntidad INTO v_IdTipoEntidad 
    FROM TipoEntidades 
    WHERE Descripcion = 'HorarioRestaurante' 
    LIMIT 1;

    -- Insertar el nuevo horario
    INSERT INTO HorarioRestaurante (IdDia, HoraApertura, HoraCierre, IdTipoEntidad)
    VALUES (p_IdDia, p_HoraApertura, p_HoraCierre, v_IdTipoEntidad);

    COMMIT;

    SET p_CodigoError = 0;
    SET p_Mensaje = 'Horario registrado correctamente.';
END$$

DELIMITER ;
