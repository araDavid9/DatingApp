import {Component, inject, model} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {AccountService} from "../_services/account.service";
import {BsDropdownModule} from "ngx-bootstrap/dropdown"


@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'

})
export class NavComponent {
  model : any ={};
  service = inject(AccountService);


  public login()
  {
    this.service.Login(this.model).subscribe({
      next : res =>
        {
          console.log(res);
        },
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
  }

  protected readonly localStorage = localStorage;
}
