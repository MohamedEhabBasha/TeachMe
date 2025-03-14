import { Photo } from "./photo";

export interface User {
    id: string;
    name: string;
    username: string;
    token: string;
    role: string;
    photo: Photo;
}