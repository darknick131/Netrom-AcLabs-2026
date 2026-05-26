// datele cum vin de la server
export interface CategoryModel {
    id: number
    name: string
    description?: string
}

// ce trimitem noi catre server
export interface CategoryInput {
    name: string
    description?: string
}

// un type in ts
// export type Category = {
//     id: number
// }