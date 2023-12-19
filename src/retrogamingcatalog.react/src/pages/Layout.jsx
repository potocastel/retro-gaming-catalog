import React, { useEffect, useState } from 'react';
import { Outlet, NavLink } from "react-router-dom";
import Login from "../components/login/Login.jsx";

const Layout = () => {

    const [isAuthenticated, setIsAuthenticated] = useState(localStorage.getItem('token') !== null);

    const handleLogout = () => {
      // Supprimez le token du stockage local pour déconnecter l'utilisateur
      localStorage.removeItem('token');
      setIsAuthenticated(false);
    };
  
    useEffect(()=>{

    },[isAuthenticated]);

    return (
        <>
        {
            (isAuthenticated  ? (
                <>
            <nav className="navbar navbar-light bg-light p-2">
                <span className="navbar-brand mb-0 h1">Retro gaming catalog</span>

                <button className="btn btn-danger" onClick={handleLogout}><i className="bi bi-box-arrow-right"></i></button>

            </nav>            <div className="d-flex flex-row mb-auto">
                <div className="d-flex flex-column flex-shrink-0 p-3 text-white bg-dark sidebar">
                    <ul className="nav nav-pills flex-column mb-auto">
                        <li className="nav-item">
                            <NavLink className="nav-link text-white" to="/"><i className="bi bi-house"></i> Home</NavLink>
                        </li>
                        <li className="nav-item ">
                            <NavLink className="nav-link text-white" to="/games"><i className="bi bi-controller"></i> Games</NavLink>
                        </li>
                        <li className="nav-item ">
                            <NavLink className="nav-link text-white" to="/consoles"><i className="bi bi-pc-horizontal"></i> Consoles</NavLink>
                        </li>
                        <li className="nav-item ">
                            <NavLink className="nav-link text-white" to="/manufacturers"><i className="bi bi-wrench"></i> Manufacturers</NavLink>
                        </li>
                    </ul>
                </div>
                <div className="flex-grow-1 p-4">
                    <Outlet />
                </div>
            </div>
              </>) : (<Login onLogin={() => setIsAuthenticated(true)} />))
        }
        </>
    )
};

export default Layout;