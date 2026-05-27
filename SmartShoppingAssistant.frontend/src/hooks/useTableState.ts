import { useEffect, useRef, useState } from 'react'
import type { PagedResult, QueryParams } from '../api/models/PagedResult'

interface UseTableStateOptions<T> {
    fetchFn: (params: QueryParams) => Promise<PagedResult<T>>
    initialSortBy?: string
    initialPageSize?: number
}

export function useTableState<T>({
    fetchFn,
    initialSortBy = 'id',
    initialPageSize = 10,
}: UseTableStateOptions<T>) {
    const [items, setItems] = useState<T[]>([])
    const [totalCount, setTotalCount] = useState(0)
    const [page, setPageState] = useState(1)
    const [pageSize, setPageSizeState] = useState(initialPageSize)
    const [search, setSearchState] = useState('')
    const [debouncedSearch, setDebouncedSearch] = useState('')
    const [sortBy, setSortBy] = useState(initialSortBy)
    const [sortDir, setSortDir] = useState<'asc' | 'desc'>('asc')
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState('')
    const [reloadTick, setReloadTick] = useState(0)

    // Keep fetchFn current without making it a fetch effect dependency.
    // The function identity changes every render but its behavior doesn't —
    // we only want to re-fetch when the actual query params change.
    const fetchFnRef = useRef(fetchFn)
    useEffect(() => { fetchFnRef.current = fetchFn })

    useEffect(() => {
        const timer = setTimeout(() => {
            setDebouncedSearch(search)
            setPageState(1)
        }, 300)
        return () => clearTimeout(timer)
    }, [search])

    useEffect(() => {
        setLoading(true)
        setError('')
        fetchFnRef.current({ page, pageSize, search: debouncedSearch, sortBy, sortDir })
            .then((result) => {
                setItems(result.items)
                setTotalCount(result.totalCount)
                setError('')
            })
            .catch((err) => setError((err as Error).message))
            .finally(() => setLoading(false))
    }, [page, pageSize, debouncedSearch, sortBy, sortDir, reloadTick])

    function setPage(p: number) {
        setPageState(p)
    }

    function setPageSize(s: number) {
        setPageSizeState(s)
        setPageState(1)
    }

    function setSearch(s: string) {
        setSearchState(s)
    }

    function toggleSort(col: string) {
        if (col === sortBy) {
            setSortDir(d => d === 'asc' ? 'desc' : 'asc')
        } else {
            setSortBy(col)
            setSortDir('asc')
        }
        setPageState(1)
    }

    function reload() {
        setReloadTick(t => t + 1)
    }

    return { items, totalCount, page, pageSize, search, sortBy, sortDir, loading, error, setPage, setPageSize, setSearch, toggleSort, reload }
}
