import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { BulkImportService } from 'src/app/services/bulk-import/bulk-import.service';

@Component({
  selector: 'app-bulk-imports',
  templateUrl: './bulk-imports.component.html',
  styleUrls: ['./bulk-imports.component.css']
})
export class BulkImportsComponent implements OnInit {
  selectedFile: File = null
  showErrors = false;
  showSuccess = false;
  formatNames$: Observable<string[]>;
  importForm = this.formBuilder.group({
    format: ['', [Validators.required]],
    file: ['', [Validators.required]]
  });
  selectedRegion = null;
  extraErrors = [];

  constructor(private formBuilder: FormBuilder, private bulkImportService: BulkImportService) {
    this.formatNames$ = bulkImportService.getFormatNames();
   }

  handleFileInput(files){
    this.selectedFile = files.item(0);
    document.getElementById('inputFile-label').innerHTML = files.item(0).name;
  }
  ngOnInit(): void {
  }

  onSubmit() {
    console.log(this.importForm)
    if(this.importForm.invalid){
      this.showErrors = true;
      this.showSuccess = false;
      return;
    }

    let params = new FormData();
    params.append('format', this.importForm.controls.format.value)
    params.append('file', this.selectedFile)
    this.extraErrors = []

    this.bulkImportService.parse(params).subscribe(val =>{
      this.showErrors = false;
      this.showSuccess = true;
    }, err => {
      for(var key in err){
        this.extraErrors = this.extraErrors.concat(err[key])
      }
      this.showErrors = true;
      this.showSuccess = false;
    });
  }
}
