import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{

  model :any= {}
  user:User | null= null;
  
  
  constructor(public accountService: AccountService,private router: Router, 
    private toastr: ToastrService) {     
  }

  ngOnInit(): void {   
  }
      

   login(){
    this.accountService.login(this.model).subscribe
    ({
      next: _ =>  {

      //   this.accountService.currentUser$.subscribe({
      //     next: user => this.user = user
                     
      //  })
      
        this.router.navigateByUrl('/members')      
        this.model = {};
      }
    })
   }

   logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}

