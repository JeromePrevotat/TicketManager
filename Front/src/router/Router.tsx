import { Route, Routes } from "react-router-dom";
import App from "../App";
import LoginForm from "../components/loginForm";
import RegisterForm from "../components/registerForm";

export default function AppRouter() {
  return (
    <Routes>
      <Route element={<App />} />
      <Route path="/" element={<App />} >
        <Route path="login" element={<LoginForm />} />
        <Route path="register" element={<RegisterForm />} />
      </Route>
    </Routes>
  )
}