import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { not } from 'rxjs/internal/util/not';
//import { UserService } from '../../Services/user.service';
import { UserForRegister } from '../../Model/user';
import alertify from 'alertifyjs'
import { AuthService } from '../../Services/auth.service';
import { AlertifyService } from '../../Services/alertify.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrl: './user-register.component.css'
})
export class UserRegisterComponent {
  
  registrationForm! : FormGroup;
  user! :UserForRegister;
  userSubmitted! : boolean;

  constructor(private fb:FormBuilder , private authService:AuthService, private alertify:AlertifyService){}

  ngOnInit(){
    // this.registrationForm = new FormGroup({
    //   userName : new FormControl(null, Validators.required),
    //   userEmail : new FormControl(null,[Validators.required, Validators.email]),
    //   userPassword : new FormControl(null,[Validators.required, Validators.minLength(8)]),
    //   confirmPassword : new FormControl(null, Validators.required),
    //   userMobile : new FormControl(null, [Validators.required, Validators.maxLength(10)])
    // },this.matchingPassword);
    this.createRegistrationForm();
  }

  createRegistrationForm(){
    this.registrationForm = this.fb.group({
      userName : [null, Validators.required],
      userEmail : [null,[Validators.required, Validators.email]],
      userPassword : [null,[Validators.required, Validators.minLength(8)]],
      confirmPassword : [null, Validators.required],
      userMobile : [null, [Validators.required, Validators.maxLength(10)]]
    },{validators:this.matchingPassword});
  }

  matchingPassword(fg: AbstractControl): ValidationErrors | null{
    return fg.get("userPassword")?.value === fg.get("confirmPassword")?.value?null:{notMatched:true};
  }

  get userName(){
    return this.registrationForm.get('userName') as FormControl;
  }

  get userEmail(){
    return this.registrationForm.get('userEmail') as FormControl;
  }

  get userPassword(){
    return this.registrationForm.get('userPassword') as FormControl;
  }

  get confirmPassword(){
    return this.registrationForm.get('confirmPassword') as FormControl;
  }

  get mobile(){
    return this.registrationForm.get('userMobile') as FormControl;
  }

  onSubmit() {
    console.log(this.registrationForm.value);
    this.userSubmitted = true;

    if (this.registrationForm.valid) {
        // this.user = Object.assign(this.user, this.registerationForm.value);
        this.authService.registerUser(this.userData()).subscribe(() =>
        {
            this.onReset();
            this.alertify.success('Congrats, you are successfully registered');
        },(error)=>{console.log(error)
            this.alertify.error('Registration failed')
        });
    }
  }

  onReset() {
    this.userSubmitted = false;
    this.registrationForm.reset();
}

  userData(): UserForRegister{
    return this.user={
      userName:this.userName.value,
      userEmail:this.userEmail.value,
      password:this.userPassword.value,
      userMobile:this.mobile.value
    };
  }
}
