import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@microsoft/signalr';
import { FileItem, FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  
 @Input() member: Member |undefined;
 hasBaseDropzoneOver = false;
 uploader: FileUploader | undefined;
 baseUrl = environment.apiUrl;
 user: User | undefined;


 constructor(private accountService: AccountService, private memberService: MembersService) {

  this.accountService.currentUser$.pipe(take(1)).subscribe({
    next: user => {
      if (user) this.user = user
    }
  })

}

ngOnInit(): void {
  this.initializeUploader();
  
}

//File
initializeUploader() {
  this.uploader = new FileUploader({
    url: this.baseUrl + 'photos/AddPhotoFile',
    authToken: 'Bearer ' + this.user?.token,
    isHTML5: true,
    allowedFileType: ['image'],
    removeAfterUpload: true,
    autoUpload: false,
    maxFileSize: 10 * 1024 * 1024,
    
  }); 


  this.uploader.onAfterAddingFile = (file) => {
    file.withCredentials = false;   
    console.log(file);
   
  }

  this.uploader.onSuccessItem = (item, response, status, headers) => {
    if (response) {
      const photo = JSON.parse(response); 
      console.log(photo);
           
      this.member?.photos.push(photo);
      if(photo.isMain && this.user && this.member){
        this.user.photoUrl = photo.url;
        this.member.photoUrl = photo.url;
        this.accountService.setCurrentUser(this.user); 
      }
    }
  }
}

initializeUploader2() {
  this.uploader = new FileUploader({
    url: this.baseUrl + 'photos/AddPhoto/',
    authToken: 'Bearer ' + this.user?.token,
    isHTML5: true,
    allowedFileType: ['image'],
    removeAfterUpload: true,
    autoUpload: false, 
    maxFileSize: 10 * 1024 * 1024
  });

  this.uploader.onBeforeUploadItem = (item: FileItem) => {
    
    this.convertFileToBase64(item._file, (base64String) => {
      
      //item.url = this.baseUrl + 'photos/AddPhoto/' + encodeURIComponent(base64String);
      const urlWithQueryParam = `${this.baseUrl}photos/AddPhoto?base64String=${encodeURIComponent(base64String)}`;
      item.url = urlWithQueryParam;
     
    });
  };

  
  this.uploader.onSuccessItem = (item, response, status, headers) => {
    if (response) {
      const photo = JSON.parse(response);
      console.log(photo);

      this.member?.photos.push(photo);
      if (photo.isMain && this.user && this.member) {
        this.user.photoUrl = photo.url;
        this.member.photoUrl = photo.url;
        this.accountService.setCurrentUser(this.user);
      }
    }
  };
   
}

convertFileToBase64(file: File, callback: (base64String: string) => void): void {
  const reader = new FileReader();
  reader.onloadend = () => {
    const base64String = reader.result as string;
    callback(base64String);
  };
  reader.readAsDataURL(file);
}


fileOverBase(e: any) {
  this.hasBaseDropzoneOver = e;
}

setMainPhoto(photo: Photo) {
  this.memberService.setMainPhoto(photo.id).subscribe({
    next: _ => {
      if (this.user && this.member) {
        this.user.photoUrl = photo.url;
        this.accountService.setCurrentUser(this.user);
        this.member.photoUrl = photo.url;
        this.member.photos.forEach(p => {
          if (p.isMain) p.isMain = false;
          if (p.id === photo.id) p.isMain = true;
        })
      }
    }
  })
}

deletePhoto(photoId: number) {
  this.memberService.deletePhoto(photoId).subscribe({
    next: _ => {
      if (this.member) {
        this.member.photos = this.member?.photos.filter(x => x.id !== photoId)
      }
    }
  })
}

}



  

