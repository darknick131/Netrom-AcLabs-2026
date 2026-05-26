import type { CategoryModel } from "../../../api/models/CategoryModel";

export interface Category {
    id: number;
    name: string;
    // ca sa nu ne dea undefined
    description: string;
}

export function toCategory(dto: CategoryModel): Category {
    return {
        id: dto.id,
        name: dto.name,
        description: dto.description ?? ""
    };
}
