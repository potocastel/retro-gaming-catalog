import { Outlet, NavLink } from "react-router-dom";

const Layout = () => {
    return (
        <>
            <nav className="navbar navbar-light bg-light p-2">
                <span className="navbar-brand mb-0 h1">Retro gaming catalog</span>
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
                            <NavLink className="nav-link text-white" to="/manufacturers"><i className="bi bi-wrench"></i> Manufacturers</NavLink>
                        </li>
                    </ul>
                </div>
                <div className="flex-grow-1 p-4">
                    <Outlet />
                </div>
            </div>
        </>
    )
};

export default Layout;