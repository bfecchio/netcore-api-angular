import { Airline } from './airline';
import { Airport } from './airport';

export class Ticket {
    public ticketId: number;
    public passenger: string;
    public airline: Airline;
    public origin: Airport;
    public destination: Airport;
    public scheduled: Date;
}
