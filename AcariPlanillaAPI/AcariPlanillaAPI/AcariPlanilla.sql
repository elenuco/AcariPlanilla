CREATE DATABASE AcariPlanilla;
USE AcariPlanilla;
CREATE TABLE Usuarios
(
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    -- Agrega más campos según sea necesario
);
CREATE TABLE Boletas
(
    BoletaId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Usuarios(UserId),
    CodigoEmpleado NVARCHAR(20) NOT NULL,
    Corte DATE NOT NULL,
    DescuentoISSS DECIMAL(18, 2) NOT NULL,
    DescuentoAFP DECIMAL(18, 2) NOT NULL,
    DescuentoRenta DECIMAL(18, 2) NOT NULL,
    SueldoNeto DECIMAL(18, 2) NOT NULL

);

CREATE TABLE HistorialActualizacionPlanillas
(
    HistorialId INT PRIMARY KEY,
    BoletaId INT FOREIGN KEY REFERENCES Boletas(BoletaId),
    RutaDocumento NVARCHAR(MAX) NOT NULL,
    FechaActualizacion DATETIME NOT NULL DEFAULT GETDATE()
);


-- Insertar datos de ejemplo en la tabla "Usuarios"
INSERT INTO Usuarios (UserId, UserName, PasswordHash)
VALUES
    (1, ),
    (2, );
	-- Insertar datos de ejemplo en la tabla "Boletas"
INSERT INTO Boletas (BoletaId, UserId, CodigoEmpleado, Corte, DescuentoISSS, DescuentoAFP, DescuentoRenta, SueldoNeto)
VALUES
    (1, 1, 'CE001', '2023-11-01', 10.5, 20.3, 5.8, 1000.0),
    (2, 1, 'CE001', '2023-11-15', 11.2, 21.0, 6.2, 1050.0),
    (3, 2, 'CE002', '2023-11-01', 9.8, 19.5, 4.5, 950.0),
    (4, 2, 'CE002', '2023-11-15', 10.2, 20.0, 5.0, 980.0);

CREATE PROCEDURE SP_AgregarUsuario
@UserName varchar(50),
@PasswordHash varchar(50),
@Patron varchar(50)
AS
BEGIN

INSERT INTO Usuarios(UserName, PasswordHash) VALUES (@UserName, ENCRYPTBYPASSPHRASE(@Patron, @PasswordHash))

END

CREATE PROCEDURE SP_CrearBoleta
    @UserId INT,
    @CodigoEmpleado NVARCHAR(20),
    @Corte DATE,
    @SalarioBase DECIMAL(18, 2),  -- Agregamos un nuevo parámetro para el salario base
    @DescuentoISSS DECIMAL(18, 2) OUTPUT,  -- Cambiamos a un parámetro de salida para el descuento ISSS
    @DescuentoAFP DECIMAL(18, 2) OUTPUT,
    @DescuentoRenta DECIMAL(18, 2) OUTPUT,
    @SueldoNeto DECIMAL(18, 2) OUTPUT
AS
BEGIN
    DECLARE @AFP_Porcentaje DECIMAL(5, 2) = 0.0725;  -- Porcentaje de descuento AFP
    DECLARE @ISSS_Porcentaje DECIMAL(5, 2) = 0.03;   -- Porcentaje de descuento ISSS
    DECLARE @Renta_Porcentaje DECIMAL(5, 2) = 0.1;   -- Porcentaje de descuento Renta
    DECLARE @ExcesoRenta DECIMAL(18, 2) = @SalarioBase - 472.00;  -- Límite de salario para Renta

    -- Cálculo de descuentos
    SET @DescuentoISSS = CASE WHEN @SalarioBase * @ISSS_Porcentaje > 30.00 THEN 30.00 ELSE @SalarioBase * @ISSS_Porcentaje END;
    SET @DescuentoAFP = @SalarioBase * @AFP_Porcentaje;
    SET @DescuentoRenta = CASE WHEN @SalarioBase <= 472.00 THEN 0.00 ELSE @ExcesoRenta * @Renta_Porcentaje END;

    -- Cálculo de Sueldo Neto
    SET @SueldoNeto = @SalarioBase - @DescuentoISSS - @DescuentoAFP - @DescuentoRenta;

    -- Insertar la boleta en la tabla Boletas
    INSERT INTO Boletas (UserId, CodigoEmpleado, Corte, DescuentoISSS, DescuentoAFP, DescuentoRenta, SueldoNeto)
    VALUES (@UserId, @CodigoEmpleado, @Corte, @DescuentoISSS, @DescuentoAFP, @DescuentoRenta, @SueldoNeto);
END;



CREATE PROCEDURE SP_ValidarUsuario
@UserName varchar(50),
@PasswordHash varchar(50),
@Patron varchar(50)
AS 
BEGIN
	SELECT * FROM Usuarios WHERE UserName = @UserName and CONVERT(varchar(50), DECRYPTBYPASSPHRASE(@Patron, PasswordHash)) = @PasswordHash
END

SP_AgregarUsuario 'NombreUsuario2', 'HashContraseña2', 'Kotov'
SELECT * FROM Usuarios

DECLARE @UserId INT = 1; -- Reemplaza con el ID del usuario
DECLARE @CodigoEmpleado NVARCHAR(20) = 'E001'; -- Reemplaza con el código del empleado
DECLARE @Corte DATE = '2023-12-01'; -- Reemplaza con la fecha de corte
DECLARE @SalarioBase DECIMAL(18, 2) = 360.00; 
DECLARE @DescuentoISSS DECIMAL(18, 2) = 0; -- Reemplaza con el valor del descuento ISSS si es necesario
DECLARE @DescuentoAFP DECIMAL(18, 2) = 0; -- Reemplaza con el valor del descuento AFP si es necesario
DECLARE @DescuentoRenta DECIMAL(18, 2) = 0; -- Reemplaza con el valor del descuento de renta si es necesario
DECLARE @SueldoNeto DECIMAL(18, 2) = 0; -- Reemplaza con el sueldo neto

-- Ejecutar el procedimiento almacenado
EXEC SP_CrearBoleta
    @UserId = @UserId,
    @CodigoEmpleado = @CodigoEmpleado,
    @Corte = @Corte,
	@SalarioBase = @SalarioBase,
    @DescuentoISSS = @DescuentoISSS,
    @DescuentoAFP = @DescuentoAFP,
    @DescuentoRenta = @DescuentoRenta,
    @SueldoNeto = @SueldoNeto;

	CREATE PROCEDURE SP_CrearBoletaConHistorial
    @UserId INT,
    @CodigoEmpleado NVARCHAR(20),
    @Corte DATE,
    @DescuentoISSS DECIMAL(18, 2) = 0,
    @DescuentoAFP DECIMAL(18, 2) = 0,
    @DescuentoRenta DECIMAL(18, 2) = 0,
    @SueldoNeto DECIMAL(18, 2) = 0,
    @RutaDocumento NVARCHAR(MAX)
AS
BEGIN
    DECLARE @BoletaId INT;

    -- Crear la boleta
    INSERT INTO Boletas (UserId, CodigoEmpleado, Corte, DescuentoISSS, DescuentoAFP, DescuentoRenta, SueldoNeto)
    VALUES (@UserId, @CodigoEmpleado, @Corte, @DescuentoISSS, @DescuentoAFP, @DescuentoRenta, @SueldoNeto);

    -- Obtener el ID de la boleta recién insertada
    SET @BoletaId = SCOPE_IDENTITY();

    -- Registrar en el historial de actualización
    INSERT INTO HistorialActualizacionPlanillas (BoletaId, RutaDocumento, FechaActualizacion)
    VALUES (@BoletaId, @RutaDocumento, GETDATE());
END;
CREATE PROCEDURE SP_ObtenerDatosBoletas
AS
BEGIN
    SELECT
        U.UserName,
        B.CodigoEmpleado,
        B.Corte,
        B.DescuentoAFP,
        B.DescuentoISSS,
        B.DescuentoRenta,
        B.SueldoNeto
    FROM
        Boletas B
        INNER JOIN Usuarios U ON B.UserId = U.UserId;
END;
Select 