const BaseUrl = "http://localhost:5205";

export const ApiService = {
  async register(email: string, password: string) {
    const response = await fetch(`${BaseUrl}/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, password }),
    });
    return response.json();
  }
};