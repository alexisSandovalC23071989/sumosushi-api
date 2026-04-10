
export class PresentacionModel {
    IdPresentacion: number;
    Presentacion: string;
    constructor(idPresentacion: number, presentacion: string){
        this.IdPresentacion = idPresentacion;
        this.Presentacion = presentacion;
    }
    static fromJson(json: any): PresentacionModel {
        return new PresentacionModel(
            json.idPresentacion,
            json.presentacion
        );
    }
}
