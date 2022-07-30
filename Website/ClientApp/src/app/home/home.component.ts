import { Component, OnInit } from '@angular/core';
import { MessageService } from 'src/app/services/message.service';
import { Message } from './message';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent implements OnInit {
  public messages: Message[] = [];
  public selectedMessage: any;
  constructor(private messageService: MessageService) {
  }
  
  ngOnInit() {
    this.messageService.getMessages().subscribe((response) => {
      this.messages = response;
    });
  }
}
