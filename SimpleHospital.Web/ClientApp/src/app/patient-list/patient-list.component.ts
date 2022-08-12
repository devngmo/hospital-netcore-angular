import { Component, Inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';


@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html'
})
export class PatientListComponent {
  public patients: PatientViewModel[] = [];
  public paginatorData: PaginatorData = new PaginatorData();
  baseUrl: string = '';
  http: HttpClient;
  pageSize: number;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.pageSize = 20;
    if (localStorage.getItem('pageSize') != null) {
      this.pageSize = parseInt('' + localStorage.getItem('pageSize'));
    }
    this.loadPageByIndex(0);
  }

  handlePageEvent(evt: any) {
    console.log(evt);
    this.pageSize = evt.pageSize;
    localStorage.setItem('pageSize', '' + this.pageSize);
    this.loadPageByIndex(evt.pageIndex);
  }

  loadPageByIndex(index: number) {
    this.http.get<PatientViewModel[]>(this.baseUrl + `patients?page=${index + 1}&itemsPerPage=${this.pageSize}`, { observe: 'response' }).subscribe(response => {
      this.paginatorData = JSON.parse('' + response.headers.get('x-pagination'));
      var items = response.body;
      console.log(items);
      if (items != null) {
        this.patients = items;
      }
    }, error => console.error(error));
  }

  generateMassiveData() {
    this.http.post(this.baseUrl + `patients?cmd=generateMassiveData`, { observe: 'response' }).subscribe(response => {
      this.loadPageByIndex(0);
    }, error => console.error(error));
  }
}

interface PatientViewModel {
  orderNo: number;
  registrationTime: string;
  patientAge : number;
  patientName: string;
  reason: string;
}


class PaginatorData {
  currentPage: number = 1;
  itemsPerPage: number = 20;
  totalCount: number= 0;
  totalPages: number = 0;
}
