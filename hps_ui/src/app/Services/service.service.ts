import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  constructor(private http: HttpClient) { }

  getAllInfo(){
    return this.http.get('https://localhost:44388/api/Info');
  }
  insertInfo(requestBody: any){
    return this.http.post('https://localhost:44388/api/Info/CreateInfo', requestBody);
  }
  updateInfo(requestBody: any){
    return this.http.put('https://localhost:44388/api/Info/UpdateInfo', requestBody);
  }
  delete(requestBody: any){
    return this.http.delete('https://localhost:44388/api/Info/DeleteInfo', requestBody);
  }
}
