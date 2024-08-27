import { Component, inject } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { AccountService } from "../_services/account.service";
import { BsDropdownModule } from "ngx-bootstrap/dropdown"
import { Router, RouterLink, RouterLinkActive } from "@angular/router";


@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,BsDropdownModule,RouterLink,RouterLinkActive],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'

})
export class NavComponent {
  model : any ={};
  service = inject(AccountService);
  private router = inject(Router)


  public login()
  {
    this.service.Login(this.model).subscribe({
      next :() => this.router.navigateByUrl("/members"),
      error : err =>
        {
            alert(err);
            console.log("There was an error:"+err);
        }
    });
  }
  public logOut()
  {
    localStorage.removeItem("user");
    this.service.currentUser.set(null);
    this.router.navigateByUrl("/");
  }

}
