import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: 'login.html',
  styleUrl: 'login.css'
})
export class LoginComponent {

  username: string = '';
  password: string = '';

  constructor(
    private loginService: LoginService,
    private router: Router
  ) {
  }

  login() {

    this.loginService
      .login(this.username, this.password)
      .subscribe({
        next: (response) => {

          localStorage.setItem('token', response.token);
          localStorage.setItem('userId', response.userId.toString());
          localStorage.setItem('userName', response.userName);
          localStorage.setItem('role', response.role);

          console.log('Login Success:', response);

          this.router.navigate(['/dashboard']);
        },

        error: (error) => {
          console.log('Login Failed:', error);
        }
      });
  }
}