import { User } from './../_models/User';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_sevices/account.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{

  model :any= {}
  
  constructor(public accounService: AccountService) {   
    
  }

  ngOnInit(): void {
   
  }
    
   

   login(){
    this.accounService.login(this.model).subscribe
    ({
      next :response=>{
         console.log(response);
         
      },
      error :error => console.log(error)
     
    })
    
   }

   logout(){

    this.accounService.logout();
    
  }
}
