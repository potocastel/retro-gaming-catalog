
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from './pages/Layout.jsx'
import Home from './components/Home.jsx'
import  GamesList  from './components/GamesList.jsx'
import './App.css';

function App() {



    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route path="games" element={<GamesList />} />
                    <Route path="hello" element={<Home />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
    
}

export default App;