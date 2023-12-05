import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { HttpService } from './http/http.service';
import { Api } from '../_helpers/apiPath';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

 api = 'admin/';
  constructor(private httpService: HttpService) { }

  getUsersWithRoles() {
    return this.httpService.httpGet<User[]>(Api.admin + 'users-with-roles');
  }

  updateUserRoles(username: string, roles: string[]) {
    return this.httpService.httpPost<string[]>(Api.admin +  'edit-roles/'+ username  + '?roles=' + roles, {});
  }
}
