import { Outlet } from 'react-router-dom'
import PrimarySearchAppBar from './components/navbar'
import SideNav from './components/sideNav'
import './style/App.css'

function App() {
  return (
    <div className="home">
      <PrimarySearchAppBar />
        <main>
          <SideNav />
          <div className="main-content">
            <Outlet />
          </div>
        </main>
    </div>
  )
}

export default App
