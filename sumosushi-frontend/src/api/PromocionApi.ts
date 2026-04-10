import { API_BASE } from '../Config';

import type { ActualizarPromocionModel } from '../models/promocion/ActualizarPromocionModel';
import type { PromocionModel } from '../models/promocion/PromocionModel';
import type { NuevaPromocionModel } from '../models/promocion/NuevaPromocionModel';

export async function obtenerPromocionesApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Promocion/ObtenerPromociones`, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` }
    });
};
export async function registrarPromocionApi(token: string, model: NuevaPromocionModel): Promise<Response> {
    const formData = new FormData();
    formData.append("Nombre", model.Nombre);
    formData.append("Descripcion", model.Descripcion);
    formData.append("FechaInicio", model.FechaInicio);
    formData.append("FechaFinal", model.FechaFinal);
    formData.append("Precio", model.Precio.toString());
    formData.append("CanjeablePuntos", model.CanjeablePuntos.toString());
    if (model.ImagenFile) formData.append("ImagenFile", model.ImagenFile);
    
    return await fetch(`${API_BASE}/Promocion/CrearPromocion`, {
        method: 'POST',
        headers: { 
            'Authorization': `Bearer ${token}` },
        body: formData
    });
};
export async function actualizarPromocionApi(token: string, model: ActualizarPromocionModel): Promise<Response> {
    const formData = new FormData();

    formData.append("IdPromocion", model.IdPromocion.toString());
    formData.append("Descripcion", model.Descripcion);
    formData.append("FechaInicio", model.FechaInicio);
    formData.append("FechaFinal", model.FechaFinal);
    formData.append("ImagenUrl", model.ImagenUrl); 
    if (model.ImagenFile) formData.append("ImagenFile", model.ImagenFile);
    formData.append("CanjeablePuntos", model.CanjeablePuntos.toString());
    formData.append("Estado", model.Estado);
    formData.append("Precio", model.Precio.toString());

    return await fetch(`${API_BASE}/Promocion/ActualizarPromocion`, {
        method: 'PUT',
        headers: {
            'Authorization': `Bearer ${token}`},
        body: formData
    });
};
export async function actualizarEstadoPromocionApi(token: string, model: PromocionModel): Promise<Response> {
    return await fetch(`${API_BASE}/Promocion/ActualizarEstadoPromocion`, {
        method: 'PUT',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({ idPromocion: model.IdPromocion })
    });
};

export default {
    obtenerPromocionesApi,
    registrarPromocionApi,
    actualizarPromocionApi,
    actualizarEstadoPromocionApi
};