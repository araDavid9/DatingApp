import {Component, EventEmitter, inject, Input, Output} from '@angular/core';
import { FormsModule} from "@angular/forms";
import { AccountService } from "../_services/account.service";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  @Input() usersFromHomeComponent : any; // other option is input.required<any>
  @Output() cancelRegister = new EventEmitter(); //other option = output<boolean>();
  model:any ={};
  private accountService = inject(AccountService);

  register()
  {
    console.log(this.model)
    this.accountService.Register(this.model).subscribe({
      next: res =>{
        console.log(res);
        this.cancelRegisterMode();
      },
      error: err =>{ console.log(err) },
      complete : ( ) => { console.log("Request has completed!")}
      }
    );
  }

  cancelRegisterMode()
  {
    this.cancelRegister.emit(false);
  }
}
