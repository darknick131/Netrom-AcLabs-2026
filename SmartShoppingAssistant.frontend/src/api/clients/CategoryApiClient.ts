import { http } from '../base/http'
import type { CategoryInput, CategoryModel } from '../models/CategoryModel'
import type { PagedResult, QueryParams } from '../models/PagedResult'
import { toCategory, type Category } from '../../components/shared/types/Category'

export const CategoriesApi = {
    getAll: async (params?: QueryParams): Promise<PagedResult<Category>> => {
        const data = await http.get<PagedResult<CategoryModel>>('/categories', params as Record<string, unknown>)
        return { ...data, items: data.items.map(toCategory) }
    },
    create: async (data: CategoryInput): Promise<Category> => {
        return toCategory(await http.post<CategoryModel>('/categories', data))
    },
    update: async (id: number, data: CategoryInput): Promise<Category> => {
        return toCategory(await http.put<CategoryModel>(`/categories/${id}`, data))
    },
    remove: (id: number) => http.remove<void>(`/categories/${id}`),
}
