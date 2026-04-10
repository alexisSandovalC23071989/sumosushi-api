
export class AccessTokenClaimsModel {
    sub: string = "";
    exp: string = "";
    IdUsuario: string = "";
    Rol: string = "";
    Jwt: string = "";
  
    constructor(token?: string) {
      if (token) this.actualizarConJwt(token);
    }
  
    actualizarConJwt(token: string): void {
      const claims = this.extraerClaims(token);
      if (claims) {
        this.sub = claims.sub || "";
        this.exp = claims.exp?.toString() || "";
        this.IdUsuario = claims.IdUsuario || "";
        this.Rol = claims.Rol || "";
        this.Jwt = token;
      } else {
        this.limpiar();
      }
    }

    limpiar(): void {
      this.sub = "";
      this.exp = "";
      this.IdUsuario = "";
      this.Rol = "";
      this.Jwt = "";
    }

    private extraerClaims(token: string): Record<string, any> | null {
      if (!token) return null;
      try {
        const payloadBase64 = token.split('.')[1];
        const payloadJson = atob(payloadBase64);
        return JSON.parse(payloadJson);
      } catch (e) {
        console.error("Error al decodificar el JWT:", e);
        return null;
      }
    }
    // Para acciones basicas de navegacion no es necesario tener un token 
    // que no haya expirado, solo necesitamos que el token exista y verificar
    // el rol del usuario. 
    get estaAutenticado(): boolean {
      return !!this.Jwt;
    }

    // Para llamadas a la API es necesario tener un token 
    // que no haya expirado, por lo que debemos verificar
    // que el token exista y que no haya expirado.
    get esTokenValido(): boolean {
      if (!this.estaAutenticado) return false;
    
      const expEpoch = parseInt(this.exp, 10);
      if (isNaN(expEpoch)) return false;
    
      const ahora = Math.floor(Date.now() / 1000);
      const margenSegundos = 30;
    
      return expEpoch > (ahora + margenSegundos);
    }

    // SOLO PARA TESTING 
    mostrarInfoSesion() : void {
      console.log("===== Información de la sesión =====");
      console.log("sub:", this.sub);
      console.log("exp:", this.exp);
      console.log("IdUsuario:", this.IdUsuario);
      console.log("Rol:", this.Rol);
      console.log("Jwt:", this.Jwt);
      console.log("====================================");
    }
  }
  