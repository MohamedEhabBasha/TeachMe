import { Category } from "./category";

export interface Profile {
    id: string;
    introduction: string;
    description: string;
    interestedUsers: number
    categories: Category[];
}