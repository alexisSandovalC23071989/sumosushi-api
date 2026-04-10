
export class EtiquetaModel {
    public IdEtiqueta: number;
    public Etiqueta: string;
    constructor(idEtiqueta: number, etiqueta: string) {
        this.IdEtiqueta = idEtiqueta;
        this.Etiqueta = etiqueta;
    }
    static fromJson(json: any): EtiquetaModel {
        return new EtiquetaModel(
            json.idEtiqueta,
            json.etiqueta
        );
    }
}