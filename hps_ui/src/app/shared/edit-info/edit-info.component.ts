import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { ServiceService } from 'src/app/Services/service.service';

@Component({
  selector: 'app-edit-info',
  templateUrl: './edit-info.component.html',
  styleUrls: ['./edit-info.component.scss']
})
export class EditInfoComponent implements OnInit {

  data: any = null;

  form: FormGroup = this.fb.group({
    name: [null, [Validators.required]],
    country: [null, [Validators.required]],
    city: [null, [Validators.required]],
    languages: [null, [Validators.required]],
    resume: [null, [Validators.required]],
    dateofbirth: [null, [Validators.required]],
  });

  submitAction(){
    this.data.name = this.form?.value?.name;
    this.data.country = this.form?.value?.country;
    this.data.city = this.form?.value?.city;
    this.data.languages = this.form?.value?.languages;
    this.data.resume = this.form?.value?.resume;
    this.data.dateofbirth = this.form?.value?.dateofbirth;
    if (this.form.valid) {
      this.action.updateInfo(this.data)
        .subscribe(
          res => {
            Swal.fire('Success', 'Successfully Updated', 'success');
          },
          (err: any) => {
            Swal.fire('Error', err?.error?.message ?? 'Menu update Failed. Something went wrong. Please try again later.', 'error');
          }
        )
    } else {
      Swal.fire('Error', 'Plase Submit Valid Data', 'error');
    }
  }
  constructor(private fb: FormBuilder, private action: ServiceService) { }

  ngOnInit(): void {
  }

}
