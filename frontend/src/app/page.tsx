import LoginForm from "./components/loginForm";
import PrimarySearchAppBar from "./components/navbar";

export default function Home() {
  return (
    <div>
      <PrimarySearchAppBar />
      <main>
        <LoginForm />
      </main>
    </div>
  );
}
