

import { PromocionMenuModel } from "./PromocionMenuModel";
import { CategoriaMenuModel } from "./CategoriaMenuModel";


export class MenuCompletoModel {
    VersionMenu: number;
    Promociones: PromocionMenuModel[];
    Categorias: CategoriaMenuModel[];
    constructor(
        versionMenu: number,
        promociones: PromocionMenuModel[],
        categorias: CategoriaMenuModel[]
    ){
        this.VersionMenu = versionMenu;
        this.Promociones = promociones;
        this.Categorias = categorias;
    }
    static fromJson(json: any): MenuCompletoModel {
        return new MenuCompletoModel(
            json.versionMenu,
            json.promociones.map(PromocionMenuModel.fromJson),
            json.categorias.map(CategoriaMenuModel.fromJson)
        );
    }
}
