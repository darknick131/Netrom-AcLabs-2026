// Promotions
// |-- PageHeader (Titlu + buton "Add Promotion")
// |-- Table (lista de promotii)
// |-- PromotionFormDialog (dialog pentru add/edit)
// |-- ConfirmDialog (dialog pentru delete)

import {
    Alert, Box, Chip, CircularProgress, Container, IconButton,
    Paper, Table, TableBody, TableCell, TableContainer, TableHead,
    TableRow, Tooltip,
} from "@mui/material"
import { useEffect, useState } from "react"
import type { Promotion } from "../shared/types/Promotion"
import { PromotionReward, PromotionType } from "../shared/types/Promotion"
import { PromotionsApi } from "../../api/clients/PromotionApiClient"
import type { Category } from "../shared/types/Category"
import type { Product } from "../shared/types/Product"
import { CategoriesApi } from "../../api/clients/CategoryApiClient"
import { ProductsApi } from "../../api/clients/ProductApiClient"
import EditIcon from "@mui/icons-material/Edit"
import DeleteIcon from "@mui/icons-material/Delete"
import PageHeader from '../common/PageHeader'
import PromotionFormDialog from './PromotionFormDialog'
import ConfirmDialog from '../common/ConfirmDialog'
import EmptyState from '../common/EmptyState'

const promotionTypeLabel: Record<PromotionType, string> = {
    [PromotionType.Quantity]: "Quantity",
    [PromotionType.CartTotal]: "Cart Total",
}

const promotionRewardLabel: Record<PromotionReward, string> = {
    [PromotionReward.FreeItems]: "Free Items",
    [PromotionReward.PercentDiscount]: "% Discount",
}

function Promotions() {
    const [promotions, setPromotions] = useState<Promotion[]>([])
    const [categories, setCategories] = useState<Category[]>([])
    const [products, setProducts] = useState<Product[]>([])
    const [lookupError, setLookupError] = useState(false)
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState("")

    const [formOpen, setFormOpen] = useState(false)
    const [editing, setEditing] = useState<Promotion | null>(null)

    const [deleting, setDeleting] = useState<Promotion | null>(null)
    const [confirmOpen, setConfirmOpen] = useState(false)

    function loadPromotions() {
        setLoading(true)
        setError("")
        PromotionsApi.getAll()
            .then((data) => {
                setPromotions(data)
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

    function handleEdit(promotion: Promotion) {
        setEditing(promotion)
        setFormOpen(true)
    }

    function handleDeleteClick(promotion: Promotion) {
        setDeleting(promotion)
        setConfirmOpen(true)
    }

    async function handleDelete() {
        if (deleting === null) return
        setConfirmOpen(false)
        try {
            await PromotionsApi.remove(deleting.id)
            loadPromotions()
        } catch (err) {
            setError((err as Error).message)
        }
    }

    useEffect(() => {
        loadPromotions()
        CategoriesApi.getAll().then(setCategories).catch(() => setLookupError(true))
        ProductsApi.getAll().then(setProducts).catch(() => setLookupError(true))
    }, [])

    return (
        <Container maxWidth="xl" sx={{ py: 4 }}>
            <PageHeader
                title="Promotions"
                actionLabel="Add Promotion"
                onAction={handleAdd}
            />

            {error !== "" && (
                <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>
            )}
            {lookupError && (
                <Alert severity="warning" sx={{ mb: 2 }}>
                    Could not load products or categories — some selectors in the form will be empty.
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
                                <TableCell>Type</TableCell>
                                <TableCell>Threshold</TableCell>
                                <TableCell>Reward</TableCell>
                                <TableCell>Value</TableCell>
                                <TableCell>Status</TableCell>
                                <TableCell align="right">Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {promotions.map((promotion) => (
                                <TableRow key={promotion.id} hover>
                                    <TableCell>{promotion.name}</TableCell>
                                    <TableCell>{promotionTypeLabel[promotion.type]}</TableCell>
                                    <TableCell>{promotion.threshold}</TableCell>
                                    <TableCell>{promotionRewardLabel[promotion.reward]}</TableCell>
                                    <TableCell>
                                        {promotion.reward === PromotionReward.PercentDiscount
                                            ? `${promotion.rewardValue}%`
                                            : `${promotion.rewardValue} items`}
                                    </TableCell>
                                    <TableCell>
                                        <Chip
                                            label={promotion.isActive ? "Active" : "Inactive"}
                                            color={promotion.isActive ? "success" : "default"}
                                            size="small"
                                        />
                                    </TableCell>
                                    <TableCell align="right">
                                        <Tooltip title="Edit">
                                            <IconButton
                                                color="primary"
                                                onClick={() => handleEdit(promotion)}
                                            >
                                                <EditIcon />
                                            </IconButton>
                                        </Tooltip>
                                        <Tooltip title="Delete">
                                            <IconButton
                                                color="error"
                                                onClick={() => handleDeleteClick(promotion)}
                                            >
                                                <DeleteIcon />
                                            </IconButton>
                                        </Tooltip>
                                    </TableCell>
                                </TableRow>
                            ))}
                            {promotions.length === 0 && (
                                <TableRow>
                                    <TableCell colSpan={7} sx={{ border: 0 }}>
                                        <EmptyState message='No promotions yet.' />
                                    </TableCell>
                                </TableRow>
                            )}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}

            {formOpen && (
                <PromotionFormDialog
                    promotion={editing}
                    categories={categories}
                    products={products}
                    onClose={() => setFormOpen(false)}
                    onSaved={() => {
                        setFormOpen(false)
                        loadPromotions()
                    }}
                />
            )}

            <ConfirmDialog
                open={confirmOpen}
                title="Delete promotion"
                description={`Are you sure you want to delete "${deleting?.name}"?`}
                confirmLabel="Delete"
                onConfirm={handleDelete}
                onCancel={() => setConfirmOpen(false)}
            />
        </Container>
    )
}

export default Promotions
