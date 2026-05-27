import { Box, Typography, Button } from '@mui/material'
import { useNavigate } from 'react-router-dom'

function NotFound() {
    const navigate = useNavigate()

    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center',
                minHeight: 'calc(100vh - 64px)',
                gap: 2,
                textAlign: 'center',
                px: 2,
            }}
        >
            <Typography variant="h1" sx={{ fontSize: '6rem', fontWeight: 300, color: 'primary.main' }}>
                404
            </Typography>
            <Typography variant="h5" color="text.secondary">
                Pagina nu a fost gasita.
            </Typography>
            <Button variant="contained" onClick={() => navigate('/')}>
                Inapoi la Home
            </Button>
        </Box>
    )
}

export default NotFound
