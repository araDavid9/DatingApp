import {inject, Injectable, signal} from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {User} from "../Models/User";
import {map} from "rxjs";

@Injectable({
  providedIn: 'root'
})

export class AccountService {
  private  http  = inject(HttpClient);
  private baseUrl :string = "https://localhost:5001/api/";
  currentUser = signal<User | null>(null);

  public Login(i_model:any)
  {
    console.log(i_model);
    return this.http.post<User>(this.baseUrl + "Account/Login", i_model).pipe(
      map(user =>{
        if(user)
        {
          localStorage.setItem("user",JSON.stringify(user));
          this.currentUser.set(user);
        }

      })
    )
  }
  public Register(i_model:any)
  {
    return this.http.post<User>(this.baseUrl + "Account/Register", i_model).pipe(
      map(user =>{
        if(user)
        {
          localStorage.setItem("user",JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      }));
  }

  /*public LoadAllUsers()
  {
    this.http.get('https://localhost:5001/api/Users').subscribe({
      next: response=>{this.users = response
        response != null ? console.log(this.users) : console.log("empty")},
      error : err => {console.log(err)},
      complete : ()=>{console.log("Request has completed!")} // when the request has comeplted we also unsubrsibed
    });
  }*/
}
