
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from './pages/Layout.jsx'
import Home from './components/Home.jsx'
import GamesManagement from './components/games/GamesManagement.jsx'
import ManufacturerList from './components/manufacturers/ManufacturerList.jsx'
import './App.css';

function App() {



    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route path="games" element={<GamesManagement />} />
                    <Route path="manufacturers" element={<ManufacturerList />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
    
}

export default App;