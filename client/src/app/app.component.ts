import { Component, inject , OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from "@angular/common/http";
import { NgFor } from "@angular/common";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http: HttpClient = inject(HttpClient);
  title = 'client';
  users : any;

  ngOnInit() {
    this.http.get('https://localhost:5001/api/Users').subscribe({
      next: response=>{this.users = response
      response != null ? console.log(this.users) : console.log("empty")},
      error : err => {console.log(err)},
      complete : ()=>{console.log("Request has completed!")} // when the request has comeplted we also unsubrsibed
    });
  }
}
