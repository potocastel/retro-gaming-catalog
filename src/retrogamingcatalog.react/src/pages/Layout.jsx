import { Outlet, NavLink } from "react-router-dom";

const Layout = () => {
    return (
        <>
            <nav class="navbar navbar-light bg-light p-2">
                <span class="navbar-brand mb-0 h1">Retro gaming catalog</span>
            </nav>            <div className="d-flex flex-row mb-auto">
                <div className="d-flex flex-column flex-shrink-0 p-3 text-white bg-dark sidebar">
                    <ul className="nav nav-pills flex-column mb-auto">
                        <li className="nav-item">
                            <NavLink className="nav-link text-white" to="/"><i className="bi bi-house"></i> Home</NavLink>
                        </li>
                        <li className="nav-item ">
                            <NavLink className="nav-link text-white" to="/games"><i className="bi bi-controller"></i> Games</NavLink>
                        </li>
                    </ul>
                </div>
                <div className="flex-grow-1">
                    <div className="d-flex flex-column">
                        <div className="d-flex flex-row-reverse bg-secondary">

                        </div>
                        <div className="p-4">
                            <Outlet />
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
};

export default Layout;