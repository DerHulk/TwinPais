import { Component, Input, trigger, state, style, transition, animate  } from '@angular/core';
import { GameService } from './game.service';
import { twinPairs } from './entities';

@Component({
    providers: [GameService],
    selector: 'game',
    template: `
        <h1>{{title}}</h1>
        <ul>
            <li *ngFor="let c of Cards" (click)="expose(c)" @cardState="c.State"  >
                {{c.Position.Row}}
            </li>
        </ul>`,
    animations: [
        trigger('cardState', [
            state('masked', style({
                backgroundColor: '#eee',
                transform: 'scale(1)'
            })),
            state('exposed', style({
                backgroundColor: '#cfd8dc',
                transform: 'scale(1.1)'
            })),
            transition('masked => exposed', animate('100ms ease-in')),
            transition('exposed => masked', animate('100ms ease-out'))
        ])
    ]
})
export class GameComponent {
    title = 'Hulk';
    Cards: Array<twinPairs.Card>;

    constructor(private gameService: GameService) {

        this.Cards = gameService.loadCards();
    }

    public expose(card: twinPairs.Card) {
        card.State = "exposed";
    }

}