import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { HttpClient} from '@angular/common/http';
@Component({
  selector: 'app-edit-photo',
  templateUrl: './edit-photo.component.html',
  styleUrls: ['./edit-photo.component.css'],
})
export class EditPhotoComponent {

 baseUrl = environment.apiUrl;
 user: User | undefined; 
 selectedFile: any;
  
 
  constructor( private http:HttpClient) {
  
    
  }


  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];    
    const reader = new FileReader();
    reader.readAsDataURL(this.selectedFile);
    reader.onload = () => {
       this.selectedFile = reader.result;      
        console.log(this.selectedFile);
    };
    }
    
    
  uploadFile(): void {
    this.http.post(this.baseUrl + "photos/AddPhoto",{BaseToString : this.selectedFile}).subscribe(
      (response:any) => {
        console.log('Upload successful', response);
      },
      (error:any) => {
        console.error('Upload error', error);
      }
    );
  }
 
 
}
