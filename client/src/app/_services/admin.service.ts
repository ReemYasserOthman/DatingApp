import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { HttpService } from './http/http.service';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

 
  constructor(private httpService: HttpService) { }

  getUsersWithRoles() {
    return this.httpService.httpGet<User[]>('admin/users-with-roles');
  }

  updateUserRoles(username: string, roles: string[]) {
    return this.httpService.httpPost<string[]>( 'admin/edit-roles/'+ username  + '?roles=' + roles, {});
  }
}
