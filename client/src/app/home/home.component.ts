import {Component, inject, OnInit} from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from "@angular/common/http";


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  registerMode = false;
  http: HttpClient = inject(HttpClient);
  users: any;

  ngOnInit() {
    this.getUsers();
  }
  registerModeOn() {
    this.registerMode = !this.registerMode; // if true than false and oposite
  }

  cancelRegisterMode(event: boolean)
  {
    console.log(this.registerMode);
    this.registerMode = event;
  }

  private getUsers()
  {
    this.http.get('https://localhost:5001/api/Users').subscribe({
      next: response=>{ this.users = response},
      error : err => {console.log(err)},
      complete : ()=>{console.log("Request has completed!")} // when the request has comeplted we also unsubrsibed
    });
  }


}
