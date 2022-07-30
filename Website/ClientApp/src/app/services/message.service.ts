import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private httpclient: HttpClient) { }

  getMessages(): Observable<any[]> {
    return this.httpclient.get<[]>(`${environment.baseApi}/Sms/list`)
  }

  getMessage(id: string): Observable<any> {
    return this.httpclient.get(`${environment.baseApi}/Sms/${id}`)
  }
}
