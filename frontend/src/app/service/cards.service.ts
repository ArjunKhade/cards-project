import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Card } from '../models/card.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class CardsService {
  constructor(private http: HttpClient) {}

  private BASE_URL = 'https://localhost:7153/api/cards';
  //
  getAllCards(): Observable<Card[]> {
    return this.http.get<Card[]>(this.BASE_URL);
  }

  deleteCard(id: string): Observable<Card> {
    return this.http.delete<Card>(this.BASE_URL + '/' + id);
  }

  updateCard(card: Card): Observable<Card> {
    return this.http.put<Card>(this.BASE_URL + '/' + card.id, card);
  }

  addCard(card: Card): Observable<Card> {
    card.id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<Card>(this.BASE_URL, card);
  }
}
