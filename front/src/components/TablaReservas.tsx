import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  obtenerReservas,
  obtenerMisReservas,
  aprobarReservaService,
  rechazarReservaService,
} from "../services/reservas";

interface Reservation {
  id: number;
  startTime: string;
  endTime: string;
  status: string;
  usuarioNombre: string;
  salaNombre: string;
}

export function TablaReservas() {
  const [reservas, setReservas] = useState<Reservation[]>([]);
  const [esAdmin, setEsAdmin] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>("");
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    let adminCheck = false;
    try {
      const payloadBase64 = token.split(".")[1];
      const decodedPayload = JSON.parse(atob(payloadBase64));

      const rolUser =
        decodedPayload[
          "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        ] ||
        decodedPayload["role"] ||
        decodedPayload["Rol"] ||
        "";

      adminCheck = rolUser.toLowerCase() === "admin";
      setEsAdmin(adminCheck);
    } catch (e) {
      console.error("No se pudo leer el rol del token", e);
    }

    cargarDatos(adminCheck);
  }, [navigate]);

  const cargarDatos = async (isAdmin: boolean) => {
    try {
      setLoading(true);
      setError("");
      const data = isAdmin
        ? await obtenerReservas()
        : await obtenerMisReservas();
      setReservas(data);
    } catch (err: any) {
      if (err.response && err.response.data && err.response.data.mensaje) {
        setError(err.response.data.mensaje);
      } else {
        setError("Error al cargar las reservas.");
      }
    } finally {
      setLoading(false);
    }
  };

  const handleAprobar = async (id: number) => {
    try {
      await aprobarReservaService(id);
      cargarDatos(esAdmin);
    } catch (err: any) {
      alert("Error al aprobar la reserva");
    }
  };

  const handleRechazar = async (id: number) => {
    try {
      await rechazarReservaService(id);
      cargarDatos(esAdmin);
    } catch (err: any) {
      alert("Error al rechazar la reserva");
    }
  };

  if (loading) {
    return (
      <div className="text-center mt-5">
        <div className="spinner-border text-primary" role="status">
          <span className="visually-hidden">Cargando...</span>
        </div>
      </div>
    );
  }

  const colSpanTotal = esAdmin ? 6 : 5;

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h2>{esAdmin ? "Gestión General de Reservas" : "Mis Reservas"}</h2>
        </div>

        <div>
          <button
            className="btn btn-outline-primary btn-sm"
            onClick={() => {
              navigate("/salas");
            }}
          >
            Volver a Salas
          </button>
        </div>
      </div>

      <div className="mb-3 d-flex gap-2">
        {!esAdmin && (
          <button className="btn btn-primary btn-sm">+ Crear Reserva</button>
        )}
      </div>

      <div className="card shadow-sm p-3">
        {error && (
          <div className="alert alert-danger" role="alert">
            {error}
          </div>
        )}

        <div className="table-responsive">
          <table className="table align-middle">
            <thead className="table-light">
              <tr>
                <th scope="col">#</th>
                <th scope="col">Sala</th>
                {esAdmin && <th scope="col">Usuario</th>}
                <th scope="col">Inicio</th>
                <th scope="col">Fin</th>
                <th scope="col">Estado</th>
                {esAdmin && <th scope="col">Acciones</th>}
              </tr>
            </thead>
            <tbody>
              {reservas.length === 0 ? (
                <tr>
                  <td
                    colSpan={colSpanTotal}
                    className="text-center py-4 text-muted"
                  >
                    No hay reservas registradas.
                  </td>
                </tr>
              ) : (
                reservas.map((reserva, index) => (
                  <tr key={reserva.id}>
                    <th scope="row">{index + 1}</th>
                    <td>
                      <strong>{reserva.salaNombre}</strong>
                    </td>
                    {esAdmin && <td>{reserva.usuarioNombre || "N/D"}</td>}
                    <td>{new Date(reserva.startTime).toLocaleString()}</td>
                    <td>{new Date(reserva.endTime).toLocaleString()}</td>
                    <td>{reserva.status}</td>

                    {esAdmin && (
                      <td>
                        <div className="d-flex gap-2">
                          <button
                            className="btn btn-success btn-sm"
                            onClick={() => handleAprobar(reserva.id)}
                          >
                            Aprobar
                          </button>
                          <button
                            className="btn btn-danger btn-sm"
                            onClick={() => handleRechazar(reserva.id)}
                          >
                            Rechazar
                          </button>
                        </div>
                      </td>
                    )}
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default TablaReservas;
