import { Photo } from "./photo"

export interface Member {
    id: number
    userName: string
    photoUrl: string
    knownAs: string
    gender: string
    lookingFor: string
    introduction: string
    interests: string
    city: string
    country: string
    age: number
    created: string
    lastActive: string
    photos: Photo[]
  }