import { prepareEventListenerParameters } from '@angular/compiler/src/render3/view/template';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SessionService } from 'src/app/services/session/session.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  logInForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', []],
  })
  extraErrors = [];
  showErrors = false;

  constructor(private formBuilder: FormBuilder, private sessionService: SessionService, private router: Router) { 

  }

  onSubmit(){
    this.extraErrors = []
    if(!this.logInForm.valid){
      this.showErrors = true;
      return;
    }

    this.showErrors = false;
    this.sessionService.create({'email': this.logInForm.controls.email.value, 'password': this.logInForm.controls.password.value})
      .subscribe(coso => {
        this.router.navigateByUrl('/admin')
      },
      err => {
        for(var key in err){
          if(key in this.logInForm.controls){
            this.logInForm.controls[key].setErrors(err[key])
          }
          else{
            this.extraErrors = this.extraErrors.concat(err[key].map(x => key[0].toUpperCase() + key.slice(1) + " " + x.toLowerCase()))
          }
        }
      });
  }
}
