export interface PagedResult<T> {
    items: T[]
    totalCount: number
    page: number
    pageSize: number
}

export interface QueryParams {
    page?: number
    pageSize?: number
    search?: string
    sortBy?: string
    sortDir?: 'asc' | 'desc'
    categoryId?: number
}
