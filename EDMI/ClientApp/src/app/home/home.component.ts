import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ElectricMeterModel } from '../model/electricMeter.model';
import { FormControl, FormGroup, Validators  } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public electricMeterFormGroup: FormGroup;
  public electricMeterModel: ElectricMeterModel;
  public showAlert: boolean;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.electricMeterModel = new ElectricMeterModel;
    this.showAlert = false;
    this.electricMeterFormGroup = new FormGroup({
      id: new FormControl(''),
      serialNumber: new FormControl('', Validators.required),
      firmwareVersion: new FormControl(''),
      state: new FormControl('', Validators.required)
    });
  }

  public handleClickSaveData(){
    this.showAlert = false;
    this.electricMeterModel.id = this.electricMeterFormGroup.controls['id'].value;
    this.electricMeterModel.serialNumber = this.electricMeterFormGroup.controls['serialNumber'].value;
    this.electricMeterModel.firmwareVersion = this.electricMeterFormGroup.controls['firmwareVersion'].value;
    this.electricMeterModel.state = this.electricMeterFormGroup.controls['state'].value;

    const data = [
      this.electricMeterModel.id,
      this.electricMeterModel.serialNumber,
      this.electricMeterModel.firmwareVersion,
      this.electricMeterModel.state
    ]

    this.http.post('https://localhost:44336/weatherforecast', data).subscribe(result => {
      this.electricMeterFormGroup.reset();
      this.showAlert = true;
    }, error => console.error(error));
  }
}
