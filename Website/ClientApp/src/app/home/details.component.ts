import { Component, OnInit } from '@angular/core';
import { MessageService } from 'src/app/services/message.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { Message } from './message';

@Component({
  templateUrl: 'details.component.html'
})
export class DetailsComponent implements OnInit {
  id: any;
  constructor(private router: Router, private messageService: MessageService, private route: ActivatedRoute) { }
  message: any;
  ngOnInit() {
    this.route.params.subscribe(paramsId => {
      this.id = paramsId.id;
    });
    this.getMessage();
  }
  getMessage(): void {
    this.messageService.getMessage(this.id).subscribe(message => {
      this.message = message;
    });
  }
  go_back() {
    this.router.navigate(["/home"]);
  }
}
