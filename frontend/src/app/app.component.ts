import { Component } from '@angular/core';
import { CardsService } from './service/cards.service';
import { OnInit } from '@angular/core';
import { Card } from './models/card.model';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'cards';
  cards: Card[] = [];
  card: Card = {
    id: '',
    cardHolderName: '',
    cardNumber: '',
    expiryMonth: '',
    expiryYear: '',
    cvc: '',
  };
  constructor(private cardService: CardsService) {}

  ngOnInit(): void {
    this.getAllCards();
  }

  onSubmit() {
    if (this.card.id === '') {
      console.log(this.card);
      this.cardService.addCard(this.card).subscribe((res) => {
        console.log(res);
        this.getAllCards();
        this.card = {
          id: '',
          cardHolderName: '',
          cardNumber: '',
          expiryMonth: '',
          expiryYear: '',
          cvc: '',
        };
      });
    } else {
      this.updateCard(this.card);
    }
  }

  updateCard(card: Card) {
    this.cardService.updateCard(card).subscribe((res) => {
      this.getAllCards();
      console.log(res);
    });
  }

  getAllCards() {
    this.cardService.getAllCards().subscribe((response) => {
      // console.log(response);
      this.cards = response;
    });
  }

  deleteCard(id: string) {
    this.cardService.deleteCard(id).subscribe((res) => {
      console.log(res);
      this.getAllCards();
    });
  }

  populateForm(card: Card) {
    this.card = card;
  }
}
