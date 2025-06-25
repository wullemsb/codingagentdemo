import { Routes } from '@angular/router';
import { EventListComponent } from './components/event-list/event-list';
import { EventDetailComponent } from './components/event-detail/event-detail';
import { EventRegistrationComponent } from './components/event-registration/event-registration';

export const routes: Routes = [
  { path: '', redirectTo: '/events', pathMatch: 'full' },
  { path: 'events', component: EventListComponent },
  { path: 'event/:id', component: EventDetailComponent },
  { path: 'event/:id/register', component: EventRegistrationComponent },
  { path: '**', redirectTo: '/events' }
];
