import { goToLogin } from "../../routes/Routes";

export async function responseEstatusOk(response: Response): Promise<boolean> {
    if (response.ok) {
        return true;
    }

    try {
        const contentType = response.headers.get("Content-Type");
        const isJson = contentType && contentType.includes("application/json");
        const data = isJson ? await response.json() : await response.text();

        switch (response.status) {
            case 400:
                if (isJson && typeof data === 'object') {
                    if (data.errors) {
                        const errores = Object.values(data.errors)
                            .flat()
                            .join("\n");
                        alert(errores);
                    } else if (data.title) {
                        alert(data.title);
                    } else {
                        alert("Solicitud incorrecta (400)");
                    }
                } else {
                    alert("Solicitud incorrecta:\n" + data);
                }
                break;

            case 401:
                alert("No estás autenticado. Iniciá sesión primero. (Error 401)");
                goToLogin();
                break;

            case 403:
                alert("No tenés permiso para realizar esta acción. (Error 403)");
                break;

            default:
                if (isJson && typeof data === 'object') {
                    const titulo = data.title ?? data.mensaje ?? "Error del servidor";
                    const detalle =
                        data?.datos?.mensaje ??
                        data?.detail ??
                        (typeof data === 'object' ? JSON.stringify(data, null, 2) : "");

                    alert(`${titulo}\n${detalle}`);
                } else if (typeof data === 'string') {
                    alert(`Error ${response.status}:\n${data}`);
                } else {
                    alert(`Error desconocido (código ${response.status})`);
                }
                break;
        }

    } catch (error) {
        console.error("Error al manejar la respuesta:", error);
        alert("Ocurrió un error inesperado al procesar la respuesta del servidor.");
    }

    return false;
}
