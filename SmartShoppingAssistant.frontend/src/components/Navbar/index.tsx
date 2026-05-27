import { AppBar, Toolbar, Box } from '@mui/material'
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
            <Toolbar sx={{ gap: 3, px: 3 }}>
                <Link to="/">
                    <Box
                        component="img"
                        src={logo}
                        alt="Smart Shopping Assistant"
                        sx={{ height: 48, display: 'block' }}
                    />
                </Link>
                <Box sx={{ display: 'flex', gap: 0.5 }}>
                    {navLinks.map(({ label, to }) => (
                        <Box
                            key={to}
                            component={NavLink}
                            to={to}
                            end={to === '/'}
                            sx={{
                                px: 1.5,
                                py: 0.75,
                                borderRadius: '9999px',
                                fontSize: '1rem',
                                fontWeight: 420,
                                color: 'rgba(249,248,242,0.65)',
                                textDecoration: 'none',
                                transition: 'color 0.15s, background-color 0.15s',
                                '&:hover': {
                                    color: '#F9F8F2',
                                    backgroundColor: 'rgba(249,248,242,0.06)',
                                },
                                '&.active': {
                                    color: '#C8C000',
                                    backgroundColor: 'rgba(200,192,0,0.10)',
                                },
                            }}
                        >
                            {label}
                        </Box>
                    ))}
                </Box>
            </Toolbar>
        </AppBar>
    )
}

export default Navbar
