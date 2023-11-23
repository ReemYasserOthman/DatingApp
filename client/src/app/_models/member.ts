import { Photo } from "./photo"

export interface Member {
    id: number
    userName: string
    photoUrl: string
    knowAs: string
    gender: string
    lokingFor: string
    introudction: string
    interests: string
    city: string
    country: string
    age: number
    created: string
    lastActive: string
    photos: Photo[]
  }