import { useAuthStore } from "../authStore";

const BaseUrl = "http://localhost:5205";

export const ApiService = {
  async authFetch(url: string, options: RequestInit = {}) {
    const token = useAuthStore.getState().user?.token as string;
    const headers = {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`,
      ...options.headers,
    };
    return fetch(url, { ...options, headers });
  },

  async register(email: string, password: string) {
    const response = await fetch(`${BaseUrl}/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, password }),
    });
    if (!response.ok) throw new Error(`Registration failed: ${response.status}`);
    const text = await response.text();
    const data = text ? JSON.parse(text) : null;
    return data;
  },

  async login(email: string, password: string) {
    const response = await fetch(`${BaseUrl}/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, password })
    });
    if (!response.ok) throw new Error(`Login failed: ${response.status}`);
    const text = await response.text();
    const data = text ? JSON.parse(text) : null;
    return data;
  },

  async me(){
    const response = await this.authFetch(`${BaseUrl}/api/users/me`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      }
    });
    if (!response.ok) throw new Error(`Error: ${response.status} ${response.statusText}`)
    return response.json();
  }
};