// Categories (componenta principala)
// |-- PageHeader (Titlu + buton "Add Category")
// |-- Table (lista de categorii)
// |-- CategoryFormDialog (dialog pentru add/edit)
// |-- ConfirmDialog (dialof pentru delete)

import { Box, Button, Typography } from "@mui/material";

interface PageHeaderProps {
    title: string;
    actionLabel: string;
    onAction: () => void;
}

function PageHeader({ title, actionLabel, onAction }: PageHeaderProps) {
    return (
        <Box>
            <Typography>{title}</Typography>
            <Button onClick={onAction}>{actionLabel}</Button>
        </Box>
    );
}

export default PageHeader;