import { Link, NavLink } from "react-router-dom"

function Navbar() {
    return (
        <nav className="flex ">
            <NavLink className="text-lg m-5" to={'/'}>
                Cofrinho<br/>de casa
            </NavLink>

            <div className="m-5">
                <Link className="text-lg align-middle">Pessoas</Link>
            </div>
        </nav>
    )
}

export default Navbar