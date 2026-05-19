import { AppBar, Toolbar, Button, Box } from "@mui/material";
import { NavLink, Link } from "react-router-dom";
import logo from '../../assets/logo.png';
// import './Navbar.css'

function Navbar(){
    return (
        <AppBar position = "static" color="default">
            <Toolbar>
                {/* daca ai o galerie cu poze, in loc de button pui Link cu img, ca sa nu pierzi contextul paginii */}
                <Link to = "/">  
                    <Box 
                        component = {"img"}                    
                        src = {logo}
                        alt = "Smart Shopping Assistant logo"
                        sx = {{ height:56, mr: 2}}
                    />
                </Link>
                <Button component={NavLink} to={"/"} variant = "contained" sx={{mr: 2, ml: 2}}> Home </Button>
                <Button component={NavLink} to={"/categories"} variant = "contained" sx={{mr: 2, ml: 2}}> Categories </Button>
                <Button component={NavLink} to={"/products"} variant = "contained" sx={{mr: 2, ml: 2}}> Products </Button>
            </Toolbar>
        </AppBar>
    )
}

export default Navbar;
