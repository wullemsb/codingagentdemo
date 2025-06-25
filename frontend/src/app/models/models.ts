export interface Event {
  id: string;
  name: string;
  location: string;
  date: string; // ISO date string
  startTime: string; // Time format HH:mm
  createdAt: string; // ISO date string
  updatedAt: string; // ISO date string
}

export interface EventRegistration {
  id: string;
  eventId: string;
  name: string;
  email: string;
  pronouns?: string;
  optInForCommunication: boolean;
  createdAt: string; // ISO date string
}

export interface EventsFilter {
  dateFrom?: string; // ISO date string
  dateTo?: string; // ISO date string
  location?: string;
}