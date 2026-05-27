import { http } from '../base/http'
import type { PromotionInput, PromotionModel } from '../models/PromotionModel'
import type { PagedResult, QueryParams } from '../models/PagedResult'
import { toPromotion, type Promotion } from '../../components/shared/types/Promotion'

export const PromotionsApi = {
    getAll: async (params?: QueryParams): Promise<PagedResult<Promotion>> => {
        const data = await http.get<PagedResult<PromotionModel>>('/promotions', params as Record<string, unknown>)
        return { ...data, items: data.items.map(toPromotion) }
    },
    create: async (data: PromotionInput): Promise<Promotion> => {
        return toPromotion(await http.post<PromotionModel>('/promotions', data))
    },
    update: async (id: number, data: PromotionInput): Promise<Promotion> => {
        return toPromotion(await http.put<PromotionModel>(`/promotions/${id}`, data))
    },
    remove: (id: number) => http.remove<void>(`/promotions/${id}`),
}
