import { api } from "../interceptors/authInterceptor";

export const ApiService = {
  async register(email: string, password: string) {
    const response = await api.post("/register", { email, password});
    if (response.status !== 200) throw new Error(`Registration failed: ${response.status}`);
    const data = await response.data;
    return data;
  },

  async login(email: string, password: string) {
    const response = await api.post("/login", { email, password });
    if (response.status !== 200) throw new Error(`Login failed: ${response.status}`);
    const data = await response.data;
    return data;
  },

  async me(){
    const response = await api.get("/api/users/me");
    if (response.status !== 200) throw new Error(`Error: ${response.status} ${response.statusText}`)
    const data = await response.data;
    return data;
  }
};