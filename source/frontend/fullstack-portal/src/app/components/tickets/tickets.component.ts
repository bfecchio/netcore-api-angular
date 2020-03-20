import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Ticket } from 'src/app/models/ticket';
import { Airline } from 'src/app/models/airline';
import { Airport } from 'src/app/models/airport';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AirportService } from 'src/app/services/airport.service';
import { AirlineService } from 'src/app/services/airline.service';
import { TicketsService } from 'src/app/services/tickets.service';
import * as moment from 'moment';
import { MatDialog } from '@angular/material/dialog';
import { AddDialogComponent } from './add-dialog/add-dialog.component';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css']
})
export class TicketsComponent implements OnInit {
  public searchForm: FormGroup;
  public dataSource = new MatTableDataSource<Ticket>();
  public displayedColumns: string[] = ['ticketId', 'passenger', 'airline', 'origin', 'destination', 'scheduled', 'actions'];

  public length: number = null;
  public airlines: Airline[] = [];
  public airports: Airport[] = [];


  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  constructor(
    private formBuilder: FormBuilder,
    private ticketService: TicketsService,
    private airportService: AirportService,
    private airlineService: AirlineService,
    public dialog: MatDialog,
  ) { }

  ngOnInit(): void {
    this.dataSource.paginator = this.paginator;

    this.searchForm = this.formBuilder.group({
      airline: [''],
      origin: [''],
      destination: [''],
      scheduled: [new Date(), '']
    });


    this.airportService.listAll()
      .subscribe(res => this.airports = [...res.data]);

    this.airlineService.listAll()
      .subscribe(res => this.airlines = [...res.data]);

    this.onSearch();
  }

  public addTicket() {
    const dialogRef = this.dialog.open(AddDialogComponent, {
      data: { ticket: new Ticket() }
    });
  }

  public editTicket(ticket: Ticket) {
    
  }

   
  public deleteTicket(ticket: Ticket) {
    
  } 


  public get form() { return this.searchForm.controls; }

  public onSearch() {
    if (this.form.invalid) {
      return;
    }

    const params = {
      pageSize: this.paginator.pageSize,
      pageIndex: this.paginator.pageIndex,
      airlineId: this.form.airline.value,
      originId: this.form.origin.value,
      destinationId: this.form.destination.value,
      scheduled: (this.form.scheduled.value ? moment(this.form.scheduled.value).format('YYYY-MM-DD') : undefined)
    };

    this.ticketService.findAll(params)
      .subscribe(
        res => {
          this.dataSource = new MatTableDataSource<Ticket>(res.data);
          this.length = res.length;
        }
      )
  }

  public onChangePaginator(event: PageEvent) {
    this.onSearch();
  }
}