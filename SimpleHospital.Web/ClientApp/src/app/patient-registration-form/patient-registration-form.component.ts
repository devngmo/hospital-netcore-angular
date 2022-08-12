import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractControl, FormControl, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
//import { MatIcon } from '@angular/material/icon';
import { Location } from '@angular/common';

@Component({
  selector: 'app-patient-registration-form',
  templateUrl: './patient-registration-form.component.html',
  styleUrls: ['./patient-registration-form.component.css']
})

export class PatientRegistrationFormComponent {
  baseUrl: string = '';
  http: HttpClient;

  patientName = new FormControl('Nguyen Van X', [Validators.required]);
  patientAge = new FormControl('20', [ageValidator(0, 150)]);
  reason = new FormControl('hurt some where', [Validators.required, Validators.minLength(20)]);
  _location: Location;
  constructor(private location:Location, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this._location = location;    
  }

  save() {
    var model = new PatientRegistrationViewModel();
    model.patientAge = this.patientAge.value;
    model.patientName = this.patientName.value;
    model.reason = this.reason.value;
    this.http.post(this.baseUrl + 'PatientRegistration', model).subscribe(response => {
      console.log(response);
      this._location.back();
      
    }, error => console.error(error));
  }
}

class PatientRegistrationViewModel {
  registrationTime: string = '';
  patientAge : number = 0;
  patientName: string = '';
  reason: string = '';
}

export function ageValidator(ageMin: number, ageMax: number): ValidatorFn {
  return (control: AbstractControl): { [key: string]: boolean } | null => {
    var v = control.value;
    return (v <= ageMin || ageMax >= v) ? null : { 'outOfRange': true };
  };
}
