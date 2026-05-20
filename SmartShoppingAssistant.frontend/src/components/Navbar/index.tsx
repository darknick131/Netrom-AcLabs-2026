import { AppBar, Toolbar, Button, Box } from '@mui/material'
import { NavLink, Link } from 'react-router-dom'
import logo from '../../assets/logo.png'

const navLinks = [
    { label: 'Home', to: '/' },
    { label: 'Categories', to: '/categories' },
    { label: 'Products', to: '/products' },
    { label: 'Promotions', to: '/promotions' },
]

function Navbar() {
    return (
        <AppBar position="static">
            <Toolbar>
                <Link to="/">
                    <Box
                        component="img"
                        src={logo}
                        alt="Smart Shopping Assistant logo"
                        sx={{ height: 56, mr: 3 }}
                    />
                </Link>
                <Box sx={{ display: 'flex', gap: 0.5 }}>
                    {navLinks.map(({ label, to }) => (
                        <Button
                            key={to}
                            component={NavLink}
                            to={to}
                            end={to === '/'}
                            sx={{
                                color: 'inherit',
                                opacity: 0.8,
                                '&.active': {
                                    opacity: 1,
                                    color: 'primary.main',
                                    backgroundColor: 'rgba(200,192,0,0.12)',
                                },
                            }}
                        >
                            {label}
                        </Button>
                    ))}
                </Box>
            </Toolbar>
        </AppBar>
    )
}

export default Navbar
