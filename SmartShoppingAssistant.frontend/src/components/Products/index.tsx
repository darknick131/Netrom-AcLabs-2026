// Products
// |-- PageHeader (Titlu + buton "Add Product")
// |-- Table (lista de produse)
// |-- ProductFormDialog (dialog pentru add/edit)
// |-- ConfirmDialog (dialog pentru delete)

import {
    Alert, Box, Chip, CircularProgress, Container, IconButton,
    Paper, Table, TableBody, TableCell, TableContainer, TableHead,
    TableRow, Tooltip,
} from "@mui/material"
import { useEffect, useState } from "react"
import type { Product } from "../shared/types/Product"
import type { Category } from "../shared/types/Category"
import { ProductsApi } from "../../api/clients/ProductApiClient"
import { CategoriesApi } from "../../api/clients/CategoryApiClient"
import EditIcon from "@mui/icons-material/Edit"
import DeleteIcon from "@mui/icons-material/Delete"
import PageHeader from '../common/PageHeader'
import ProductFormDialog from './ProductFormDialog'
import ConfirmDialog from '../common/ConfirmDialog'
import EmptyState from '../common/EmptyState'

function Products() {
    const [products, setProducts] = useState<Product[]>([])
    const [categories, setCategories] = useState<Category[]>([])
    const [categoriesError, setCategoriesError] = useState(false)
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState("")

    const [formOpen, setFormOpen] = useState(false)
    const [editing, setEditing] = useState<Product | null>(null)

    const [deleting, setDeleting] = useState<Product | null>(null)
    const [confirmOpen, setConfirmOpen] = useState(false)

    function loadProducts() {
        setLoading(true)
        setError("")
        ProductsApi.getAll()
            .then((data) => {
                setProducts(data)
                setError("")
            })
            .catch((err) => {
                setError((err as Error).message)
            })
            .finally(() => {
                setLoading(false)
            })
    }

    function handleAdd() {
        setEditing(null)
        setFormOpen(true)
    }

    function handleEdit(product: Product) {
        setEditing(product)
        setFormOpen(true)
    }

    function handleDeleteClick(product: Product) {
        setDeleting(product)
        setConfirmOpen(true)
    }

    async function handleDelete() {
        if (deleting === null) return
        setConfirmOpen(false)
        try {
            await ProductsApi.remove(deleting.id)
            loadProducts()
        } catch (err) {
            setError((err as Error).message)
        }
    }

    useEffect(() => {
        loadProducts()
        CategoriesApi.getAll().then(setCategories).catch(() => setCategoriesError(true))
    }, [])

    return (
        <Container maxWidth="xl" sx={{ py: 4 }}>
            <PageHeader
                title="Products"
                actionLabel="Add Product"
                onAction={handleAdd}
            />

            {error !== "" && (
                <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>
            )}
            {categoriesError && (
                <Alert severity="warning" sx={{ mb: 2 }}>
                    Could not load categories — the category selector in the form will be empty.
                </Alert>
            )}

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
                                <TableCell>Price (RON)</TableCell>
                                <TableCell>Categories</TableCell>
                                <TableCell align="right">Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {products.map((product) => (
                                <TableRow key={product.id} hover>
                                    <TableCell>{product.name}</TableCell>
                                    <TableCell>{product.description}</TableCell>
                                    <TableCell>{product.price.toFixed(2)}</TableCell>
                                    <TableCell>
                                        <Box sx={{ display: "flex", gap: 0.5, flexWrap: "wrap" }}>
                                            {product.categories.map((cat) => (
                                                <Chip key={cat.id} label={cat.name} size="small" />
                                            ))}
                                        </Box>
                                    </TableCell>
                                    <TableCell align="right">
                                        <Tooltip title="Edit">
                                            <IconButton
                                                color="primary"
                                                onClick={() => handleEdit(product)}
                                            >
                                                <EditIcon />
                                            </IconButton>
                                        </Tooltip>
                                        <Tooltip title="Delete">
                                            <IconButton
                                                color="error"
                                                onClick={() => handleDeleteClick(product)}
                                            >
                                                <DeleteIcon />
                                            </IconButton>
                                        </Tooltip>
                                    </TableCell>
                                </TableRow>
                            ))}
                            {products.length === 0 && (
                                <TableRow>
                                    <TableCell colSpan={5} sx={{ border: 0 }}>
                                        <EmptyState message='No products yet.' />
                                    </TableCell>
                                </TableRow>
                            )}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}

            {formOpen && (
                <ProductFormDialog
                    product={editing}
                    categories={categories}
                    onClose={() => setFormOpen(false)}
                    onSaved={() => {
                        setFormOpen(false)
                        loadProducts()
                    }}
                />
            )}

            <ConfirmDialog
                open={confirmOpen}
                title="Delete product"
                description={`Are you sure you want to delete "${deleting?.name}"?`}
                confirmLabel="Delete"
                onConfirm={handleDelete}
                onCancel={() => setConfirmOpen(false)}
            />
        </Container>
    )
}

export default Products
