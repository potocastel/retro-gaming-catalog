
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from './pages/Layout.jsx'
import Home from './components/Home.jsx'
import GamesList from './components/GamesList.jsx'
import ManufacturerList from './components/ManufacturerList.jsx'
import './App.css';

function App() {



    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route path="games" element={<GamesList />} />
                    <Route path="manufacturers" element={<ManufacturerList />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
    
}

export default App;