import { Component, OnInit } from '@angular/core';
import { ServiceService } from 'src/app/Services/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-info-list',
  templateUrl: './info-list.component.html',
  styleUrls: ['./info-list.component.scss']
})
export class InfoListComponent implements OnInit {

  infoList: any;

  constructor(private action: ServiceService) { }

  ngOnInit(): void {
    this.getAll();
  }

  getAll(){
    this.action.getAllInfo().subscribe((res: any) => {
      console.log(res);
      this.infoList = res.payload;
    })
  }

  deleteAction(item: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.action.delete(item)
          .subscribe(
            (res: any) => {
              Swal.fire('Deleted!', 'Record has been deleted.', 'success');
              this.getAll();
            },
            (err: any) => {
              Swal.fire('Error', err?.error?.message ?? 'Info deletion Failed. Something went wrong. Please try again later.', 'error');
            }
          );
      }
    })
  }
}
