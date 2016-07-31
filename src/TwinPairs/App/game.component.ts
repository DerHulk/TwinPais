import { Component, Input, trigger, state, style, transition, animate  } from '@angular/core';
import { GameService } from './game.service';
import { twinPairs } from './entities';

@Component({
    providers: [GameService],
    selector: 'game',
    template: `
        <h1>{{title}}</h1>
        <ul>
            <li *ngFor="let c of Cards" (click)="expose(c)">
                <div class="card-container">
                  <div class="card" @cardState="c.State">
                    <figure class="front">{{c.Position.Row}}</figure>
                    <figure class="back">test</figure>
                  </div>
                </div>
            </li>
        </ul>`,
    animations: [
        trigger('cardState', [
            state('masked', style({
               
            })),
            state('exposed', style({
                transform: 'rotateY(180deg)',
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