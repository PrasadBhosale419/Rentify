import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LoginService } from '../../Services/login.service';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import { AlertifyService } from '../../Services/alertify.service';
import { UserForLogin } from '../../Model/user';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrl: './user-login.component.css'
})
export class UserLoginComponent {

  constructor(private auth:LoginService, private route:Router, private authService:AuthService, private alertify:AlertifyService){}
  onLogin(loginForm: NgForm) {
    console.log(loginForm.value);
    // const token = this.authService.authUser(loginForm.value);
    this.authService.authUser(loginForm.value).subscribe(
        (response: UserForLogin) => {
            console.log(response);
            const user = response;
            if (user) {
                localStorage.setItem('token', user.token);
                localStorage.setItem('userName', user.userName);
                this.alertify.success('Login Successful');
                this.route.navigate(['/']);
            }
        },(error)=>{
          console.log(error);
          this.alertify.error('Login Failed');
        }
    );
}
}
