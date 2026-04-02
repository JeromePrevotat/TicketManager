import LoginForm from "./components/loginForm";
import PrimarySearchAppBar from "./components/navbar";
import SideNav from "./components/sideNav";

export default function Home() {
  return (
    <div className = 'home'>
      <PrimarySearchAppBar />
      <div className = 'main-container'>
        <SideNav />
        <main>
          <LoginForm />
        </main>
      </div>
    </div>
  );
}
