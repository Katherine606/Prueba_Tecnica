import api from "./api";

export const obtenerSalas = async () => {
  const response = await api.get("/rooms");
  return response.data;
};

export const crearSalaService = async (
  name: string,
  capacity: number,
  location: string,
) => {
  const response = await api.post("/rooms", {
    name,
    capacity,
    location,
  });
  return response.data;
};
