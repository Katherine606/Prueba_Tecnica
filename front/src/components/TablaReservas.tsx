import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api";

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
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>("");
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    cargarMisReservas();
  }, [navigate]);

  const cargarMisReservas = async () => {
    try {
      setLoading(true);

      const response = await api.get("/reservations");
      setReservas(response.data);
    } catch (err: any) {
      if (err.response && err.response.data && err.response.data.mensaje) {
        setError(err.response.data.mensaje);
      } else {
        setError("Error al cargar tus reservas.");
      }
    } finally {
      setLoading(false);
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

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h2>Mis Reservas</h2>
        </div>

        <div>
          <button
            className="btn btn-outline-primary btn-sm"
            onClick={() => {
              navigate("/salas");
            }}
          >
            Volver
          </button>
        </div>
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
                <th scope="col">Inicio</th>
                <th scope="col">Fin</th>
                <th scope="col">Estado</th>
              </tr>
            </thead>
            <tbody>
              {reservas.length === 0 ? (
                <tr>
                  <td colSpan={5} className="text-center py-4 text-muted">
                    No tienes reservas registradas.
                  </td>
                </tr>
              ) : (
                reservas.map((reserva, index) => (
                  <tr key={reserva.id}>
                    <th scope="row">{index + 1}</th>
                    <td>
                      <strong>{reserva.salaNombre}</strong>
                    </td>
                    <td>{new Date(reserva.startTime).toLocaleString()}</td>
                    <td>{new Date(reserva.endTime).toLocaleString()}</td>
                    <td>{reserva.status}</td>
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
