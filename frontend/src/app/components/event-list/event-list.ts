import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EventsService } from '../../services/events';
import { Event, EventsFilter } from '../../models/models';

@Component({
  selector: 'app-event-list',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './event-list.html',
  styleUrl: './event-list.css'
})
export class EventListComponent implements OnInit {
  events: Event[] = [];
  filteredEvents: Event[] = [];
  filter: EventsFilter = {};
  isLoading = false;
  errorMessage = '';

  constructor(private eventsService: EventsService) {}

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents(): void {
    this.isLoading = true;
    this.errorMessage = '';
    
    this.eventsService.getEvents(this.filter).subscribe({
      next: (events) => {
        this.events = events;
        this.filteredEvents = events;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Failed to load events. Please try again.';
        this.isLoading = false;
        console.error('Error loading events:', error);
      }
    });
  }

  applyFilter(): void {
    this.loadEvents();
  }

  clearFilter(): void {
    this.filter = {};
    this.loadEvents();
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString();
  }

  formatTime(timeString: string): string {
    return timeString.substring(0, 5); // Display as HH:mm
  }
}
