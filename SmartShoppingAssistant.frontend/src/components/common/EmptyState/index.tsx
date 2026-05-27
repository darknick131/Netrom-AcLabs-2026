import { Box, Typography } from '@mui/material'
import InboxIcon from '@mui/icons-material/Inbox'

interface EmptyStateProps {
    message: string
}

function EmptyState({ message }: EmptyStateProps) {
    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                py: 6,
                gap: 1.5,
            }}
        >
            <InboxIcon sx={{ fontSize: 48, color: '#D8D8C4' }} />
            <Typography variant='body2' color='text.secondary'>
                {message}
            </Typography>
        </Box>
    )
}

export default EmptyState
