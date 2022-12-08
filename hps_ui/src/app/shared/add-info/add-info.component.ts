import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ServiceService } from 'src/app/Services/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-info',
  templateUrl: './add-info.component.html',
  styleUrls: ['./add-info.component.scss']
})
export class AddInfoComponent {

  constructor(private fb: FormBuilder, private action: ServiceService) { }

  form: FormGroup = this.fb.group({
    name: [null, [Validators.required]],
    country: [null, [Validators.required]],
    city: [null, [Validators.required]],
    languages: [null, [Validators.required]],
    resume: [null, [Validators.required]],
    dateofbirth: [null, [Validators.required]],
  });

  submitAction(){
    if (this.form.valid) {
      this.action.insertInfo(this.form.value).subscribe(
        res => {
          console.log(res);
          Swal.fire('Success', 'Successfully Saved', 'success');
          this.form.reset();
        },
        (err: any) => {
          Swal.fire('Error', err?.error?.message ?? 'Menu creation Failed. Something went wrong. Please try again later.', 'error');
        }
      )
    } else {
      Swal.fire('Error', 'Please Input Valid Data', 'error');
    }
  }

}
