// Categories (componenta principala)
// |-- PageHeader (Titlu + buton "Add Category")
// |-- Table (lista de categorii)
// |-- CategoryFormDialog (dialog pentru add/edit)
// |-- ConfirmDialog (dialof pentru delete)

import { Box, Container, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Tooltip, IconButton, CircularProgress, Paper, Alert } from "@mui/material"
import { useEffect, useState } from "react";
import type { Category } from "../shared/types/Category";
import { CategoriesApi } from "../../api/clients/CategoryApiClient";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import PageHeader from "../common/PageHeader";

function Categories() {
    // state pentru lista de categorii
    const [categories, setCategories] = useState<Category[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    function loadCategories() {
        setLoading(true);
        setError("");
        CategoriesApi.getAll().then((data) => {
            setCategories(data);
            setError("")
        })
            .catch((err) => {
                setError((err as Error).message);
            })
            .finally(() => {
                setLoading(false);
            })
    }

    function handleAdd() {

    }

    function handleEdit(category: Category) {

    }

    function handleDeleteClick(category: Category) {

    }

    useEffect(() => {
        loadCategories();
    }, []);

    return (
        <Container maxWidth="xl" sx={{ py: 4 }}>
            {/*  {} asta e pentru cod de js -> conditional rendering in cazul nostru */}
            <PageHeader
                title="Categories"
                actionLabel="Add Category"
                onAction={handleAdd}
            />

            {
                error !== "" && (
                    <Alert severity="error" sx={{ mb: 2 }}>
                        {error}
                    </Alert>
                )
            }

            {loading ? (
                <Box sx={{ display: "flex", justifyContent: "center", mt: 4 }}>
                    <CircularProgress />
                </Box>
            ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Name</TableCell>
                                <TableCell>Description</TableCell>
                                <TableCell align="right">Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {categories.map((category) => (
                                <TableRow key={category.id} hover>
                                    <TableCell>{category.name}</TableCell>
                                    <TableCell>{category.description}</TableCell>
                                    <TableCell align="right">
                                        <Tooltip title="Edit">
                                            <IconButton
                                                color="primary"
                                                onClick={() => handleEdit(category)}
                                            >
                                                <EditIcon />
                                            </IconButton>
                                        </Tooltip>
                                        <Tooltip title="Delete">
                                            <IconButton
                                                color="error"
                                                onClick={() => handleDeleteClick(category)}
                                            >
                                                <DeleteIcon />
                                            </IconButton>
                                        </Tooltip>
                                    </TableCell>
                                </TableRow>
                            ))}
                            {categories.length === 0 && (
                                <TableRow>
                                    <TableCell colSpan={3} align="center">
                                        No categories yet.
                                    </TableCell>
                                </TableRow>
                            )}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
        </Container>
    )
}

export default Categories;