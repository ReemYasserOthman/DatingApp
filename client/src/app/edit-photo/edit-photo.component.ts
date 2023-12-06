import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HttpClient,  HttpRequest, HttpResponse } from '@microsoft/signalr';
import { Member } from '../_models/member';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { take } from 'rxjs';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-edit-photo',
  templateUrl: './edit-photo.component.html',
  styleUrls: ['./edit-photo.component.css'],
})
export class EditPhotoComponent {
 @Input() member: Member | undefined;
 baseUrl = environment.apiUrl;
 user: User | undefined; 
 selectedFile: any;
  
  message: string | undefined;
  @Output() public onUploadFinished = new EventEmitter();
 

  constructor(private accountService: AccountService, private memberService: MembersService,
    private http: HttpClient) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user
      }
    })
  }


  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];    
    const reader = new FileReader();
    reader.readAsDataURL(this.selectedFile);
    reader.onload = () => {
       this.selectedFile = reader.result;
        console.log(reader.result);
    };
    }
    
    
  uploadFile(): void {
    this.http.post(this.baseUrl + "photos/AddPhoto", this.selectedFile).then(
      (response:any) => {
        console.log('Upload successful', response);
      },
      (error:any) => {
        console.error('Upload error', error);
      }
    );
  }
 
  // onFileSelected(event: any): void {
  //   const file: File = event.target.files[0];

  //   if (file) {
  //     const formData = new FormData();
  //     formData.append('file', file, file.name);

  //     //this.uploadFile(formData);
  //   }
  // }

 

 
}
