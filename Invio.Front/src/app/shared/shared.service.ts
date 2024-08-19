import { Injectable } from '@angular/core';
import { BsModalService, ModalOptions, BsModalRef } from 'ngx-bootstrap/modal';
import { NotificationComponent } from './components/models/notification/notification.component';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  BsModalRef: BsModalRef | undefined;

  constructor(private modalService: BsModalService) { }

  showNotification(isSuccess: boolean, title: string, message: string){
    const initialState: ModalOptions = {
      initialState: {
        isSuccess,
        title,
        message
      }
    };

    this.BsModalRef = this.modalService.show(NotificationComponent, initialState);
  }
}
