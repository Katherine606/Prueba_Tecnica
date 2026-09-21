import api from "./api";

const loginUsuario = async (email: string, password: string) => {
  const response = await api.post("/auth/login", { email, password });
  return response.data.token;
};

export default loginUsuario;
