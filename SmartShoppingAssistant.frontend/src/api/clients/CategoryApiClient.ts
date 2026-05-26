import { http } from "../base/http"
import type { CategoryInput, CategoryModel } from "../models/CategoryModel"
import { toCategory, type Category } from "../../components/shared/types/Category"

// componenta -> categoriesApi -> http -> server 
export const CategoriesApi = {
    getAll: async (): Promise<Category[]> => {
        const data = await http.get<CategoryModel[]>('/categories')
        return data.map(toCategory)
    },
    // to do get by id
    create: async (data: CategoryInput): Promise<Category> => {
        return toCategory(
            await http.post<CategoryModel>('/categories', data)
        )
    },
    update: async (id: number, data: CategoryInput): Promise<Category> => {
        return toCategory(
            await http.put<CategoryModel>(`/categories/${id}`, data)
        )
    },
    remove: (id: number) => http.remove<void>(`/categories/${id}`)
}