import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import { UserForLogin } from '../Model/user';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  baseurl = environment.baseUrl;
  constructor(private http:HttpClient) { }

  authUser(user:any){
    return this.http.post(`${this.baseurl} + '/Account/login`, user);
  //   let userArray= [];
  //   if(localStorage.getItem('Users')){
  //     userArray = JSON.parse(localStorage.getItem("Users") as string);
  //   }
  //   return userArray.find((p: { userName: any; Password: any; }) => p.userName === user.userName && p.Password === user.userPassword);
  }
}
