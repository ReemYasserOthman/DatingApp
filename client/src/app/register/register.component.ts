import { AccountService } from './../_sevices/account.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
 /**@Input() usersFromHomeCommpnent: any;**/
 @Output() cancelRegister = new EventEmitter();

 model: any ={};

constructor(private accountService:AccountService) {}

 ngOnInit(): void {
   
  }

 register(){
  this.accountService.register(this.model).subscribe({
    next :() =>{
        this.cancel();
    },
    error :error=> console.error(error)    
  })
 }
 cancel(){
  this.cancelRegister.emit(false);
 }
}
