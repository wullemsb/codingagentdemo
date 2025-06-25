import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { EventsService } from '../../services/events';
import { Event } from '../../models/models';

@Component({
  selector: 'app-event-detail',
  imports: [CommonModule, RouterModule],
  templateUrl: './event-detail.html',
  styleUrl: './event-detail.css'
})
export class EventDetailComponent implements OnInit {
  event: Event | null = null;
  isLoading = false;
  errorMessage = '';

  constructor(
    private eventsService: EventsService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadEvent(id);
    }
  }

  loadEvent(id: string): void {
    this.isLoading = true;
    this.errorMessage = '';
    
    this.eventsService.getEvent(id).subscribe({
      next: (event) => {
        this.event = event;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Failed to load event details. Please try again.';
        this.isLoading = false;
        console.error('Error loading event:', error);
      }
    });
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString();
  }

  formatTime(timeString: string): string {
    return timeString.substring(0, 5); // Display as HH:mm
  }
}
