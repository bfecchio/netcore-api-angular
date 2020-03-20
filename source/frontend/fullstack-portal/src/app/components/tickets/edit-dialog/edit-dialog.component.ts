import { CommonModule } from '@angular/common';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { AirlineService } from 'src/app/services/airline.service';
import { AirportService } from 'src/app/services/airport.service';
import { Airline } from 'src/app/models/airline';
import { Airport } from 'src/app/models/airport';
import { Ticket } from 'src/app/models/ticket';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css']
})
export class EditDialogComponent implements OnInit {
  form: FormGroup;
  today: Date = new Date();
  airlines: Airline[] = [];
  airports: Airport[] = [];

  constructor(
    private dialogRef: MatDialogRef<EditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Ticket,
    private airlineService: AirlineService,
    private airportService: AirportService
  ) { }


  ngOnInit() {
    this.buildForm();
    this.fillControls();
  }

  buildForm() {
    this.form = new FormGroup({
      ticketId: new FormControl(this.data.ticketId),
      passenger: new FormControl(this.data.passenger, Validators.required),
      airline: new FormGroup({
        airlineId: new FormControl(this.data.airline.airlineId, Validators.required),
      }),
      origin: new FormGroup({
        airportId: new FormControl(this.data.origin.airportId, Validators.required)
      }),
      destination: new FormGroup({
        airportId: new FormControl(this.data.destination.airportId, Validators.required)
      }),
      scheduled: new FormControl(this.data.scheduled, Validators.required),      
      flight: new FormControl(this.data.flight, Validators.required),      
      gate: new FormControl(this.data.gate, Validators.required),      
    });

  }

  fillControls() {
    this.airportService.listAll()
      .subscribe(res => this.airports = [...res.data]);

    this.airlineService.listAll()
      .subscribe(res => this.airlines = [...res.data]);
  }

  close() {
    this.dialogRef.close(null);
  }

  save() {
    if (this.form.invalid) {
      return
    }

    this.dialogRef.close(this.form.value);
  }
}
