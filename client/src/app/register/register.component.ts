import { ToastrService } from 'ngx-toastr';
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

constructor(private accountService:AccountService, private toastr: ToastrService) {}

 ngOnInit(): void {
   
  }

 register(){
  this.accountService.register(this.model).subscribe({
    next :() =>{
        this.cancel();
    },
    error: error => {
      this.toastr.error(error.error);
      console.log(error);
    }    
  })
 }
 cancel(){
  this.cancelRegister.emit(false);
 }
}
