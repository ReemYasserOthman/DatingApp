import { map, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';


@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  members: Member[] = [];
  constructor(private http: HttpClient) { 

  }

  getMembers(){
   //return this.http.get<Member[]>(this.baseUrl + "users", this.getHttpOptions());

   if(this.members.length > 0) return of( this.members);

   return this.http.get<Member[]>(this.baseUrl + "users").pipe(
    map(resp =>{
        this.members = resp;
        return resp;
    })
   )    
  }

  getMember(username: string){

   const member = this.members.find(m => m.userName == username)
   if(member)
   return of(member);

   return this.http.get<Member>(this.baseUrl + "users/" + username); 
  }


  updateMember(member: Member){
    
    return this.http.put(this.baseUrl + "users" , member).pipe(
      map(()=> {
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index], ...member}
      })
    )
    }




  ////Anothr soiluation for pass token in request headers == jwt interceptor /////
   
  // getHttpOptions(){
  //   const userString= localStorage.getItem("user");
  //   if(!userString)
  //   return;
    
  //   const user = JSON.parse(userString);
  //   return{
  //     headers: new HttpHeaders({Authorization: "Bearer " + user.token })
  //   }
  // }

}
