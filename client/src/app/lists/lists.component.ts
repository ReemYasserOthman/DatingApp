import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { Pagination } from '../_models/Pagination ';
import { MembersService } from '../_services/members.service';
import { BaseService } from '../_services/baseService/base-service.service';
import { HttpService } from '../_services/http/http.service';
import { Api } from '../_helpers/apiPath';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit{

  members: Member[] | undefined;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination | undefined;

  
  //BaseService
  baseService: BaseService<Member>;

constructor(private memberService: MembersService,
  httpService: HttpService) {
  this.baseService = new BaseService<Member>(httpService, Api.users);
}
  ngOnInit(): void {
   this.loadLikes();
   this.baseService.GetAll();
  }

  loadLikes() {
    this.memberService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe({
      next: response => {
        this.members = response.result;
        this.pagination = response.pagination;
      }
    })
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadLikes();
    }
  }

}
