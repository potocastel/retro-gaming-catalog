
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from './pages/Layout.jsx'
import GamesManagement from './components/games/GamesManagement.jsx'
import ManufacturerManagement from './components/manufacturers/ManufacturerManagement.jsx'
import ConsoleManagement from "./components/consoles/ConsoleManagement.jsx";
import './App.css';

function App() {



    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route path="games" element={<GamesManagement />} />
                    <Route path="consoles" element={<ConsoleManagement />} />
                    <Route path="manufacturers" element={<ManufacturerManagement />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
    
}

export default App;