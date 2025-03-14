import { DecimalPipe, NgClass, NgFor, NgIf, NgStyle } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { AccountService } from '../../../_services/account.service';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-photo-edit',
  standalone: true,
  imports: [FileUploadModule, NgClass, NgStyle, NgIf, NgFor, DecimalPipe],
  templateUrl: './photo-edit.component.html',
  styleUrl: './photo-edit.component.css'
})
export class PhotoEditComponent implements OnInit{
  private accountService = inject(AccountService);
  uploader!: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;

  ngOnInit(): void {
    this.initializeUploader();
  }
  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }
  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'userProfile/updateProfilePhoto',
      authToken: 'Bearer ' + this.accountService.currentUser()?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 2 * 1024 * 1024,
      additionalParameter: { UserId: this.accountService.currentUser()?.id }
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false; // As we are not using cookies
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      const photo = JSON.parse(response);
      console.log(photo);
      const user = this.accountService.currentUser();

      if (user) {
        user.photo.id = photo.pubicId;
        user.photo.url = photo.url;
  
        this.accountService.setCurrentUser(user);
      }
    }
  }
}
