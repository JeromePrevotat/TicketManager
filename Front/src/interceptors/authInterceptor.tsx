import axios from "axios";
import { useAuthStore } from "../authStore";

const baseUrl = "http://localhost:5205";

export const api = axios.create({
  baseURL : baseUrl,
  withCredentials: true
});

api.interceptors.request.use((config) => {
  const token = useAuthStore.getState().user?.token;
  if (token){
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
})