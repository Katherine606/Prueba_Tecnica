import api from "./api";

export const obtenerReservas = async () => {
  const response = await api.get("/reservations/todas");
  return response.data;
};

export const obtenerMisReservas = async () => {
  const response = await api.get("/reservations");
  return response.data;
};

export const aprobarReservaService = async (id: number) => {
  const response = await api.patch(`/reservations/${id}/approve`);
  return response.data;
};

export const rechazarReservaService = async (id: number) => {
  const response = await api.patch(`/reservations/${id}/decline`);
  return response.data;
};
