import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, catchError, of, map } from 'rxjs';
import { Event, EventRegistration, EventsFilter } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class EventsService {
  private baseUrl = 'http://localhost:7071/api';

  constructor(private http: HttpClient) { }

  getEvents(filter?: EventsFilter): Observable<Event[]> {
    let params = new HttpParams();
    
    if (filter?.dateFrom) {
      params = params.set('dateFrom', filter.dateFrom);
    }
    if (filter?.dateTo) {
      params = params.set('dateTo', filter.dateTo);
    }
    if (filter?.location) {
      params = params.set('location', filter.location);
    }

    return this.http.get<Event[]>(`${this.baseUrl}/events`, { params })
      .pipe(
        catchError(this.handleError<Event[]>('getEvents', []))
      );
  }

  getEvent(id: string): Observable<Event | null> {
    return this.http.get<Event>(`${this.baseUrl}/events/${id}`)
      .pipe(
        catchError(this.handleError<Event | null>('getEvent', null))
      );
  }

  createEvent(event: Omit<Event, 'id' | 'createdAt' | 'updatedAt'>): Observable<Event | null> {
    return this.http.post<Event>(`${this.baseUrl}/events`, event)
      .pipe(
        catchError(this.handleError<Event | null>('createEvent', null))
      );
  }

  updateEvent(id: string, event: Omit<Event, 'id' | 'createdAt' | 'updatedAt'>): Observable<Event | null> {
    return this.http.put<Event>(`${this.baseUrl}/events/${id}`, event)
      .pipe(
        catchError(this.handleError<Event | null>('updateEvent', null))
      );
  }

  deleteEvent(id: string): Observable<boolean> {
    return this.http.delete<void>(`${this.baseUrl}/events/${id}`)
      .pipe(
        map(() => true),
        catchError(this.handleError<boolean>('deleteEvent', false))
      );
  }

  registerForEvent(eventId: string, registration: Omit<EventRegistration, 'id' | 'eventId' | 'createdAt'>): Observable<EventRegistration | null> {
    return this.http.post<EventRegistration>(`${this.baseUrl}/events/${eventId}/register`, registration)
      .pipe(
        catchError(this.handleError<EventRegistration | null>('registerForEvent', null))
      );
  }

  getEventRegistrations(eventId: string): Observable<EventRegistration[]> {
    return this.http.get<EventRegistration[]>(`${this.baseUrl}/events/${eventId}/registrations`)
      .pipe(
        catchError(this.handleError<EventRegistration[]>('getEventRegistrations', []))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`${operation} failed:`, error);
      return of(result as T);
    };
  }
}
