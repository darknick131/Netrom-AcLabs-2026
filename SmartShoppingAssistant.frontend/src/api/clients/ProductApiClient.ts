import { http } from "../base/http"
import type { ProductInput, ProductModel } from "../models/ProductModel"
import { toProduct, type Product } from "../../components/shared/types/Product"

export const ProductsApi = {
    getAll: async (categoryId?: number): Promise<Product[]> => {
        const path = categoryId != null ? `/products?categoryId=${categoryId}` : '/products'
        const data = await http.get<ProductModel[]>(path)
        return data.map(toProduct)
    },
    create: async (data: ProductInput): Promise<Product> => {
        return toProduct(await http.post<ProductModel>('/products', data))
    },
    update: async (id: number, data: ProductInput): Promise<Product> => {
        return toProduct(await http.put<ProductModel>(`/products/${id}`, data))
    },
    remove: (id: number) => http.remove<void>(`/products/${id}`),
}
