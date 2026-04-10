import { ProductosPedidoAdminModel } from "./ProductosPedidoAdminModel";
import { PromocionesPedidoAdminModel } from "./PromocionesPedidoAdminModel";

export class PedidoAdminModel {
    IdPedido: number;
    FechaCreacion: string;
    CostoEnvio: number;
    PuntosCanjeados: number;
    TotalConPuntos: number;
    TotalPagado: number;
    MetodoPago: string;
    TipoPedido: string;
    Notacliente: string;
    IdEstadoPedido: number;
    EstadoPedido: string;

    Fecha: string;
    Hora: string;

    IdUsuario: number;
    Nombre: string;
    Apellidos: string;
    Telefonos: string;
    Direccion: string;

    productos: ProductosPedidoAdminModel[];
    promociones: PromocionesPedidoAdminModel[];

    constructor(
        idPedido: number,
        fechaCreacion: string,
        notacliente: string,
        tipoPedido: string,
        metodoPago: string,
        costoEnvio: number,
        totalPagado: number,
        puntosCanjeados: number,
        totalConPuntos: number,
        idEstado: number,
        estado: string,
        idUsuario: number,
        nombre: string,
        apellido: string,
        telefono: string,
        direccion: string,
        promociones: PromocionesPedidoAdminModel[],
        productos: ProductosPedidoAdminModel[]
    ) {
        this.IdPedido = idPedido;
        this.FechaCreacion = fechaCreacion;
        const dateObj = new Date(fechaCreacion);
        this.Fecha = dateObj.toLocaleDateString('es-CR');
        this.Hora = dateObj.toLocaleTimeString('es-CR', { hour: '2-digit', minute: '2-digit' });
        this.Notacliente = notacliente;
        this.TipoPedido = tipoPedido;
        this.MetodoPago = metodoPago;
        this.CostoEnvio = costoEnvio;
        this.TotalPagado = totalPagado;
        this.PuntosCanjeados = puntosCanjeados;
        this.TotalConPuntos = totalConPuntos;
        this.IdEstadoPedido = idEstado;
        this.EstadoPedido = estado;
        this.IdUsuario = idUsuario;
        this.Nombre = nombre;
        this.Apellidos = apellido;
        this.Telefonos = telefono;
        this.Direccion = direccion;
        this.promociones = promociones;
        this.productos = productos;
    }

    static fromJSON(json: any): PedidoAdminModel {
        console.log("JSON recibido en fromJSON:", json);
        return new PedidoAdminModel(
            json.idPedido,
            json.fechaCreacion,
            json.notaCliente,
            json.tipoPedido,
            json.metodoPago,
            json.costoEnvio,
            json.totalPagado,
            json.puntosCanjeados,
            json.totalConPuntos,
            json.idEstadoPedido,
            json.estadoPedido,
            json.idUsuario,
            json.nombre,
            json.apellidos,
            json.telefonos,
            json.direccion,
            (json.promociones ?? []).map(PromocionesPedidoAdminModel.fromJSON),
            (json.productos ?? []).map(ProductosPedidoAdminModel.fromJSON)
        );
    }
}
