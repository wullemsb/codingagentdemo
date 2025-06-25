import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { EventsService } from '../../services/events';
import { Event, EventRegistration } from '../../models/models';

@Component({
  selector: 'app-event-registration',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './event-registration.html',
  styleUrl: './event-registration.css'
})
export class EventRegistrationComponent implements OnInit {
  event: Event | null = null;
  registration: Omit<EventRegistration, 'id' | 'eventId' | 'createdAt'> = {
    name: '',
    email: '',
    pronouns: '',
    optInForCommunication: false
  };
  
  isLoading = false;
  isSubmitting = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private eventsService: EventsService,
    private route: ActivatedRoute,
    private router: Router
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

  onSubmit(form: NgForm): void {
    if (!form.valid || !this.event) {
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.eventsService.registerForEvent(this.event.id, this.registration).subscribe({
      next: (result) => {
        if (result) {
          this.successMessage = 'Registration successful! You have been registered for this event.';
          // Clear the form
          this.registration = {
            name: '',
            email: '',
            pronouns: '',
            optInForCommunication: false
          };
          form.resetForm();
        } else {
          this.errorMessage = 'Registration failed. Please try again.';
        }
        this.isSubmitting = false;
      },
      error: (error) => {
        this.errorMessage = 'Registration failed. You may already be registered for this event.';
        this.isSubmitting = false;
        console.error('Error registering for event:', error);
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
