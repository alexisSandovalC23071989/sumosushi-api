
export class NuevoPedidoModel {
    IdTipoPedido: number;
    IdMetodoPago: number;
    PuntosCanjeados: number | undefined;
    NotaCliente: string;
    IdDireccion: number;
    CostoEnvio: number;
    constructor (
        idTipoPedido: number = 0,
        idMetodoPago: number = 0,
        puntosCanjeados: number | undefined = undefined,
        notaCliente: string = '',
        idDireccion: number = 0,
        costoEnvio: number = 0
    ) {
        this.IdTipoPedido = idTipoPedido;
        this.IdMetodoPago = idMetodoPago;
        this.PuntosCanjeados = puntosCanjeados;
        this.NotaCliente = notaCliente;
        this.IdDireccion = idDireccion;
        this.CostoEnvio = costoEnvio;
    }
    
    validar(): string[] {
        const errores: string[] = [];
        const soloEspacios = (valor: string) : boolean=> valor.trim() === "";
        
        if(this.IdTipoPedido <= 0) {
            errores.push("El tipo de pedido invalido.");
        }
        if(this.IdMetodoPago <= 0) {
            errores.push("El método de pago invalido.");
        }
        if(this.PuntosCanjeados !== undefined){
            if(this.PuntosCanjeados < 0) {
                errores.push("Los puntos canjeados deben ser mayores o iguales a 0.");
            }
        }
        return errores;
    }   

}
