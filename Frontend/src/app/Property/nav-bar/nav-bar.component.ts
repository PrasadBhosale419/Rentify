import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  loggedinUser!:string;
  loggedIn(){
    this.loggedinUser = localStorage.getItem('userName')!;
    return this.loggedinUser;
  }

  onLogOut(){
    localStorage.removeItem('userName');
    localStorage.removeItem('token');
  }
}
