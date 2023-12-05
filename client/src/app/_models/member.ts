import { Photo } from "./photo"

export class Member {
    id = 0 
    userName =''
    photoUrl =''
    knownAs =''
    gender =''
    lookingFor =''
    introduction =''
    interests =''
    city =''
    country=''
    age = 0
    created =''
    lastActive =''
    photos: Photo[] = []
  }