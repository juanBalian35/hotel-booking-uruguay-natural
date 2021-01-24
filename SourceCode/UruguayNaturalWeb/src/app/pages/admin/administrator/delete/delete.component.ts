import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { AdministratorBasicInfo } from 'src/app/models/administrator-basic-info';
import { AdministratorService } from 'src/app/services/administrator/administrator.service';
import { SessionService } from 'src/app/services/session/session.service';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent implements OnInit {
  showErrors = false;
  showSuccess = false;
  selectedAdministrator: AdministratorBasicInfo = null;
  administrators$: Observable<AdministratorBasicInfo[]>;

  adminForm = this.formBuilder.group({
    email: ['']
  })

  constructor(private formBuilder: FormBuilder, 
    private sessionService: SessionService,
    private administratorService: AdministratorService) { 
    this.administrators$ = administratorService.getAll();
  }

  ngOnInit(): void {
  }

  onSubmit(value){
    if(value.email.email == this.sessionService.getEmail()){
      this.showErrors = true;
      this.showSuccess = false;
      this.adminForm.controls.email.setErrors({frontErrors: ['Un usuario no puede eliminarse a si mismo.']})
      return;
    }

    this.administratorService.delete(this.selectedAdministrator.id).subscribe(val => {
      this.showSuccess = true;
      this.selectedAdministrator = null;
      this.showErrors = false;
      this.administrators$ = this.administratorService.getAll();
    })
  }
}
