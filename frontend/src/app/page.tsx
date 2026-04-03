'use client';
import { useContext } from "react";
import { UserContext } from "./contexts/userContext";
import LoginForm from "./components/loginForm";
import PrimarySearchAppBar from "./components/navbar";
import RegisterForm from "./components/registerForm";
import SideNav from "./components/sideNav";

export default function Home() {
  const currentUser = useContext(UserContext);

  return (
    <div className = 'home'>
      <PrimarySearchAppBar />
      <div className = 'main-container'>
        <SideNav />
        <main>
          <RegisterForm />
          <LoginForm />
        </main>
      </div>
    </div>
  );
}
