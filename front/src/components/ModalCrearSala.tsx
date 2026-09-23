import { useState } from "react";
import { crearSalaService } from "../services/salas";

interface ModalCrearSalaProps {
  show: boolean;
  onClose: () => void;
  onSalaCreada: () => void;
}

export function ModalCrearSala({
  show,
  onClose,
  onSalaCreada,
}: ModalCrearSalaProps) {
  const [nombre, setNombre] = useState<string>("");
  const [capacidad, setCapacidad] = useState<number | "">("");
  const [ubicacion, setUbicacion] = useState<string>("");
  const [error, setError] = useState<string>("");

  if (!show) return null;

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    if (!nombre || capacidad === "" || !ubicacion) {
      setError("Todos los campos son obligatorios.");
      return;
    }

    try {
      await crearSalaService(nombre, Number(capacidad), ubicacion);
      setNombre("");
      setCapacidad("");
      setUbicacion("");
      onSalaCreada();
      onClose();
    } catch (err: any) {
      if (err.response && err.response.data) {
        const data = err.response.data;
        if (data.errors) {
          const primerCampo = Object.keys(data.errors)[0];
          setError(data.errors[primerCampo][0]);
        } else if (data.mensaje) {
          setError(data.mensaje);
        } else if (data.title) {
          setError(data.title);
        } else {
          setError("Error al registrar la sala.");
        }
      } else {
        setError("Error al conectar con el servidor.");
      }
    }
  };

  return (
    <div
      className="modal show d-block"
      tabIndex={-1}
      style={{ backgroundColor: "rgba(0, 0, 0, 0.4)" }}
    >
      <div className="modal-dialog modal-dialog-centered">
        <div className="modal-content border-0 shadow">
          <div className="modal-header border-0 pb-0">
            <h5 className="modal-title fw-bold">Nueva Sala</h5>
            <button
              type="button"
              className="btn-close"
              onClick={onClose}
            ></button>
          </div>

          <form onSubmit={handleSubmit}>
            <div className="modal-body pt-2">
              {error && (
                <div className="alert alert-danger py-2 mb-3" role="alert">
                  {error}
                </div>
              )}

              <div className="mb-3">
                <label className="form-label text-muted small fw-semibold">
                  Nombre
                </label>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Ej. Auditorio Principal"
                  value={nombre}
                  onChange={(e) => setNombre(e.target.value)}
                  required
                />
              </div>

              <div className="mb-3">
                <label className="form-label text-muted small fw-semibold">
                  Capacidad
                </label>
                <input
                  type="number"
                  className="form-control"
                  placeholder="Ej. 10"
                  value={capacidad}
                  onChange={(e) =>
                    setCapacidad(
                      e.target.value === "" ? "" : Number(e.target.value),
                    )
                  }
                  required
                />
              </div>

              <div className="mb-3">
                <label className="form-label text-muted small fw-semibold">
                  Ubicación
                </label>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Ej. Piso 2, Ala Norte"
                  value={ubicacion}
                  onChange={(e) => setUbicacion(e.target.value)}
                  required
                />
              </div>
            </div>

            <div className="modal-footer border-0 pt-0">
              <button
                type="button"
                className="btn btn-light btn-sm px-3"
                onClick={onClose}
              >
                Cancelar
              </button>
              <button type="submit" className="btn btn-primary btn-sm px-3">
                Guardar
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}

export default ModalCrearSala;
