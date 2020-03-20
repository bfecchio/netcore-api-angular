import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { AirlineService } from 'src/app/services/airline.service';
import { AirportService } from 'src/app/services/airport.service';
import { Airline } from 'src/app/models/airline';
import { Airport } from 'src/app/models/airport';

@Component({
  selector: 'app-add-dialog',
  templateUrl: './add-dialog.component.html',
  styleUrls: ['./add-dialog.component.css']
})
export class AddDialogComponent implements OnInit {
  public form: FormGroup;
  public today: Date = new Date();
  public airlines: Airline[] = [];
  public airports: Airport[] = [];

  constructor(
    public formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<AddDialogComponent>,
    private airlineService: AirlineService,
    private airportService: AirportService
  ) { }

  ngOnInit() {
    
    this.airportService.listAll()
    .subscribe(res => this.airports = [...res.data]);
    
    this.airlineService.listAll()
    .subscribe(res => this.airlines = [...res.data]);
    
    this.form = this.formBuilder.group({
      passenger: ['', Validators.required],
      airline: ['', Validators.required],
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      scheduled: [new Date(), Validators.required]
    });

  }

  onCloseDialog() {
    this.dialogRef.close();
  }

  submit() {
    this.dialogRef.close(this.form.value);
  }

}
