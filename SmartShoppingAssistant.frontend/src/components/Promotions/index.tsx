import { Box, Typography } from '@mui/material'

function Promotions() {
    return (
        <Box sx={{ p: 4 }}>
            <Typography variant="h4" gutterBottom>
                Promotions
            </Typography>
            <Typography variant="body1" color="text.secondary">
                Active promotions will appear here.
            </Typography>
        </Box>
    )
}

export default Promotions
