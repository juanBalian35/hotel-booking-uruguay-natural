import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Admin } from 'src/app/models/admin';
import { AdministratorBasicInfo } from 'src/app/models/administrator-basic-info';
import { AdministratorService } from 'src/app/services/administrator/administrator.service';
import { SessionService } from 'src/app/services/session/session.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  showErrors = false;
  selectedAdministrator = null
  showSuccess = false;
  administrators$: Observable<AdministratorBasicInfo[]>;
  adminForm = this.formBuilder.group({
    emailSelect: [''],
    email: ['', [Validators.email]],
    password: ['', []],
    name: ['', []],
  })
  
  constructor(private formBuilder: FormBuilder, 
    private sessionService: SessionService,
    private administratorService: AdministratorService) { 
    this.administrators$ = administratorService.getAll();
  }

  ngOnInit(): void {
  }

  selectAdministrator(value){
    this.showSuccess = false;
    this.showErrors = false;
    if(value == null) return;
    this.selectedAdministrator = value;

    this.adminForm.controls.email.setValue(value.email)
    this.adminForm.controls.password.setValue(value.password)
    this.adminForm.controls.name.setValue(value.name)
  }

  onSubmit(adminForm){
    console.log(this.adminForm)
    if(this.adminForm.invalid){
      this.showErrors = true;
      this.showSuccess = false;
      return;
    }

    this.showErrors = false;

    let params = {}
    if (this.adminForm.controls.name.value != ''){
      params['name'] = this.adminForm.controls.name.value;
    }
    if (this.adminForm.controls.email.value != ''){
      params['email'] = this.adminForm.controls.email.value;
    }
    if (this.adminForm.controls.password.value != ''){
      params['password'] = this.adminForm.controls.password.value;
    }

    console.log(params)

    this.administratorService.modify(this.selectedAdministrator.id, params).subscribe(val => {
      this.showSuccess = true;
      this.administrators$ = this.administratorService.getAll();
    },
      err => {
        for(var key in err){
          var styledKey = key[0].toLowerCase() + key.slice(1);
          if(styledKey in this.adminForm.controls){
            this.adminForm.controls[styledKey].setErrors({apiErrors: err[key]})
          }
        }
        this.showErrors = true;
        console.log(this.adminForm.controls)
    })
  }
}