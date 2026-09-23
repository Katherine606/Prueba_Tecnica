import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api";
import ModalCrearSala from "./ModalCrearSala"; // Importas tu modal independiente

interface Room {
  id: number;
  name: string;
  capacity: number;
  location: string;
}

export function TablaSalas() {
  const [rooms, setRooms] = useState<Room[]>([]);
  const [esAdmin, setEsAdmin] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>("");
  const [showModal, setShowModal] = useState<boolean>(false);
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

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

      setEsAdmin(rolUser.toLowerCase() === "admin");
    } catch (e) {
      console.error("No se pudo leer el rol del token", e);
    }

    cargarSalas();
  }, [navigate]);

  const cargarSalas = async () => {
    try {
      setLoading(true);
      const response = await api.get("/rooms");
      setRooms(response.data);
    } catch (err: any) {
      if (err.response && err.response.data && err.response.data.mensaje) {
        setError(err.response.data.mensaje);
      } else {
        setError("Error al cargar la lista de salas.");
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
          <h2>Gestión de Salas</h2>
        </div>

        <div>
          {!esAdmin && (
            <button
              className="btn btn-outline-primary btn-sm me-2"
              onClick={() => {
                navigate("/TablaReservas");
              }}
            >
              Ver mis reservas
            </button>
          )}
          <button
            className="btn btn-outline-danger btn-sm"
            onClick={() => {
              localStorage.removeItem("token");
              navigate("/login");
            }}
          >
            Cerrar Sesión
          </button>
        </div>
      </div>

      <div className="mb-3 d-flex gap-2">
        {esAdmin && (
          <button
            className="btn btn-primary btn-sm"
            onClick={() => setShowModal(true)} // Abre el modal de creación
          >
            + Crear sala
          </button>
        )}
        {esAdmin && (
          <button
            className="btn btn-outline-primary btn-sm"
            onClick={() => {
              navigate("/TablaReservas");
            }}
          >
            Gestionar reservas
          </button>
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
                <th scope="col">Nombre</th>
                <th scope="col">Capacidad</th>
                <th scope="col">Ubicación</th>
              </tr>
            </thead>
            <tbody>
              {rooms.length === 0 ? (
                <tr>
                  <td colSpan={4} className="text-center py-4 text-muted">
                    No hay salas registradas en el sistema.
                  </td>
                </tr>
              ) : (
                rooms.map((room, index) => (
                  <tr key={room.id}>
                    <th scope="row">{index + 1}</th>
                    <td>
                      <strong>{room.name}</strong>
                    </td>
                    <td>{room.capacity} personas</td>
                    <td>{room.location}</td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
      <ModalCrearSala
        show={showModal}
        onClose={() => setShowModal(false)}
        onSalaCreada={cargarSalas}
      />
    </div>
  );
}

export default TablaSalas;
