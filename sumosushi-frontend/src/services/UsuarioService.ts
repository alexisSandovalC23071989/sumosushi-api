//Routes
import { goToLogin } from "../routes/Routes";
//Utilidades
import { responseEstatusOk } from "./utils/ResponseStatus";
//Apis
import {
  actualizarPerfilApi,
  actualizarRolUsuarioApi,
  adminDeshabilitarCuenta,
  adminRehabilitarCuenta,
  obtenerColaboradoresApi,
  obtenerPerfilApi,
  obtenerUsuarioPorCorreoApi,
  obtenerUsuariosDeshabilitadosApi,
  registrarUsuarioApi,
  guardarTelefonoApi,
  eliminarTelefonoApi,
  guardarDireccionUsuarioApi,
  eliminarDireccionUsuarioApi
} from "../api/UsuarioApi";
//Modelos
import type { ActualizarRolModel } from "../models/usuario/ActualizarRolModel";
import { DireccionCompletaModel } from "../models/direccion/DireccionCompletaModel";
import type { RegistroUsuarioModel } from "../models/auth/RegistroUsuarioModel";
import type { RespuestaModel } from "../models/RespuestaModel";
import { PerfilCompletoUsuarioModel } from "../models/usuario/PerfilCompletoModel";
import { UsuarioFormModel } from "../models/usuario/UsuarioFormModel";
import type { NuevaDireccionModel } from '../models/direccion/NuevaDireccionModel';

export async function registroUsuarioService(usuario: RegistroUsuarioModel) {
    try {
        const errores = usuario.validar();
        if (errores.length > 0){
            alert(errores.join('\n'));
            return;
        }

        const response = await registrarUsuarioApi(usuario); 
        if (!responseEstatusOk(response)) return;    

        const respuesta: RespuestaModel = await response.json();
        alert(respuesta.mensaje);

        if (!respuesta.exito) return;
        goToLogin();   
             
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
    }
}
export async function obtenerPerfilService(token: string): Promise<PerfilCompletoUsuarioModel> {
    try {
      const response = await obtenerPerfilApi(token);
      if (!await responseEstatusOk(response)) {
        return new PerfilCompletoUsuarioModel();
      }
  
      const json = await response.json();
  
      if (!json.exito) {
        alert(json.mensaje ?? "Error al obtener el perfil.");
        return new PerfilCompletoUsuarioModel();
      }
  
      const datos = json.datos;
  
      if (!datos) {
        alert("No se encontró información del perfil.");
        return new PerfilCompletoUsuarioModel();
      }
  
      const direcciones = Array.isArray(datos.direcciones)
        ? datos.direcciones.map((d: any) => new DireccionCompletaModel(
          d.idDireccion, 
          d.canton, 
          d.distrito, 
          d.detalle, 
          d.idCanton, 
          d.idDistrito))
        : [];

      const telefonos = Array.isArray(datos.telefonos)
        ? datos.telefonos.map((t: any) => t)
        : [];

      return new PerfilCompletoUsuarioModel(
        datos.nombre,
        datos.apellidos,
        datos.correo,
        telefonos,
        direcciones
      );
  
    } catch (error: any) {
      alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
      return new PerfilCompletoUsuarioModel();
    }
}
export async function obtenerColaboradoresService(token: string): Promise<UsuarioFormModel[]>{
  try {
    const response = await obtenerColaboradoresApi(token);
    if (!await responseEstatusOk(response)) {
      return [];
    }

    const json = await response.json();
    if (!Array.isArray(json.datos)) {
      return [];
    }

    return json.datos.map((item: any) => new UsuarioFormModel(
      item.idUsuario,
      item.nombre,
      item.apellidos,
      item.correo,
      item.idRol,
      item.rol
    ));
  } catch (error: any) {
    alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
    return [];
  }
}
export async function actualizarRolUsuarioService(token: string, model: ActualizarRolModel): Promise<boolean>{
    try {
        const response: Response = await actualizarRolUsuarioApi(token, model);
        if (!await responseEstatusOk(response)) return false;

        const json: RespuestaModel = await response.json();
        if (!json.exito) {
            alert(json.mensaje);
            return false;
        }

        alert(json.mensaje);
        return true;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return false;
    }
}
export async function adminDeshabilitarCuentaService(token: string, model: UsuarioFormModel): Promise<boolean>{
    try {
        const response: Response = await adminDeshabilitarCuenta(token, model);
        if (!await responseEstatusOk(response)) return false;

        const json: RespuestaModel = await response.json();
        if (!json.exito) {
            alert(json.mensaje);
            return false;
        }
        
        alert(json.mensaje);
        return true;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return false;
    }
}
export async function adminRehabilitarCuentaService(token: string, model: UsuarioFormModel): Promise<boolean>{
    try {
        const response: Response = await adminRehabilitarCuenta(token, model);
        if (!await responseEstatusOk(response)) return false;

        const json: RespuestaModel = await response.json();
        if (!json.exito) {
            alert(json.mensaje);
            return false;
        }
        
        alert(json.mensaje);
        return true;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return false;
    }
}
export async function obtenerUsuarioPorCorreoService(token: string, correo: string): Promise<UsuarioFormModel[]> {
  try {
      const response = await obtenerUsuarioPorCorreoApi(token, correo);
      if (!await responseEstatusOk(response)) return [];

      const json = await response.json();
      console.log(json);

      if (!json.exito || !json.datos) {
          alert(json.mensaje ?? "No se encontró el usuario.");
          return [];
      }

      const usuario = new UsuarioFormModel(
          json.datos.idUsuario,
          json.datos.nombre,
          json.datos.apellidos,
          json.datos.correo,
          json.datos.idRol,
          json.datos.rol
      );

      return [usuario];

  } catch (error: any) {
      alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
      return [];
  }
}
export async function obtenerUsuariosDeshabilitadosService(token: string): Promise<UsuarioFormModel[]> {
  try {
    const response = await obtenerUsuariosDeshabilitadosApi(token);
    if (!await responseEstatusOk(response)) {
      return [];
    }

    const json = await response.json();
    if (!Array.isArray(json.datos)) {
      return [];
    }

    return json.datos.map((item: any) => new UsuarioFormModel(
      item.idUsuario,
      item.nombre,
      item.apellidos,
      item.correo,
      item.idRol,
      item.rol
    ));
  } catch (error: any) {
    alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
    return [];
  }
}
export async function actualizarPerfilService(token: string, model: PerfilCompletoUsuarioModel): Promise<boolean>{
    try {
        const response: Response = await actualizarPerfilApi(token, model);
        if (!await responseEstatusOk(response)) return false;
        const json: RespuestaModel = await response.json();
        if (!json.exito) {
            alert(json.mensaje);
            return false;
        }        
        alert(json.mensaje);
        return true;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return false;
    }
}
export async function guardarTelefonoService(token: string, telefono: string): Promise<boolean>{
  try {
      const response: Response = await guardarTelefonoApi(token, telefono);
      if (!await responseEstatusOk(response)) return false;
      const json: RespuestaModel = await response.json();
      if (!json.exito) {
          alert(json.mensaje);
          return false;
      }        
      alert(json.mensaje);
      return true;
  } catch (error: any) {
      alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
      return false;
  }
}
export async function eliminarTelefonoService(token: string, telefono: string): Promise<boolean>{
  try {
      const response: Response = await eliminarTelefonoApi(token, telefono);
      if (!await responseEstatusOk(response)) return false;
      const json: RespuestaModel = await response.json();
      if (!json.exito) {
          alert(json.mensaje);
          return false;
      }        
      alert(json.mensaje);
      return true;
  } catch (error: any) {
      alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
      return false;
  }
}
export async function guardarDireccionService(token: string, model: NuevaDireccionModel): Promise<boolean>{
  try {
      const response: Response = await guardarDireccionUsuarioApi(token, model);
      if (!await responseEstatusOk(response)) return false;
      const json: RespuestaModel = await response.json();
      if (!json.exito) {
          alert(json.mensaje);
          return false;
      }        
      alert(json.mensaje);
      return true;
  } catch (error: any) {
      alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
      return false;
  }
}
export async function eliminarDireccionService(token: string, idDireccion: number): Promise<boolean>{
  try {
      const response: Response = await eliminarDireccionUsuarioApi(token, idDireccion);
      if (!await responseEstatusOk(response)) return false;
      const json: RespuestaModel = await response.json();
      if (!json.exito) {
          alert(json.mensaje);
          return false;
      }        
      alert(json.mensaje);
      return true;
  } catch (error: any) {
      alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
      return false;
  }
}




