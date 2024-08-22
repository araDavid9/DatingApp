import { Component, inject , OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgFor } from "@angular/common";
import {NavComponent} from "./nav/nav.component";
import {AccountService} from "./_services/account.service";
import {HomeComponent} from "./home/home.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,NgFor,NavComponent,HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  private accountService = inject(AccountService);

  ngOnInit()
  {
    this.setUser();
  }

  private setUser() // this method was created so if we will refresh it wont log out
  {
    const userString = localStorage.getItem("user"); //using the browser storagen
    if(userString)
    {
      const user = JSON.parse(userString);
      this.accountService.currentUser.set(user);
    }
  }



}


