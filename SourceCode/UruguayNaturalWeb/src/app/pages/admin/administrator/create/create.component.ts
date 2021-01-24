import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Admin } from 'src/app/models/admin';
import { AdministratorService } from 'src/app/services/administrator/administrator.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  showErrors = false;
  showSuccess = false;

  adminForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
    name: ['', [Validators.required]],
  })

  constructor(private formBuilder: FormBuilder, 
    private administratorService: AdministratorService) { 
  }

  ngOnInit(): void {
  }

  onSubmit(adminForm){
    console.log(this.adminForm)
    if(this.adminForm.invalid){
      this.showErrors = true;
      this.showSuccess = false;
      return;
    }

    this.showErrors = false;

    let params: Admin = {
      name: this.adminForm.controls.name.value,
      password: this.adminForm.controls.password.value,
      email: this.adminForm.controls.email.value
    }

    this.administratorService.create(params).subscribe(val => this.showSuccess = true,
      err => {
        for(var key in err){
          var styledKey = key[0].toLowerCase() + key.slice(1);
          if(styledKey in this.adminForm.controls){
            this.adminForm.controls[styledKey].setErrors({apiErrors: err[key]})
          }
        }
        this.showErrors = true;
    })
  }
}
