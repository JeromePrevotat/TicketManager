import LoginForm from "./components/loginForm";
import PrimarySearchAppBar from "./components/navbar";
import RegisterForm from "./components/registerForm";
import SideNav from "./components/sideNav";

export default function Home() {
  return (
    <div className = 'home'>
      <PrimarySearchAppBar />
      <div className = 'main-container'>
        <SideNav />
        <main>
          {/* <LoginForm /> */}
          <RegisterForm />
        </main>
      </div>
    </div>
  );
}
