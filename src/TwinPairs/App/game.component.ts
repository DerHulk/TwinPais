import { Component } from '@angular/core';
import { GameService } from './game.service';
import { twinPairs } from './entities';

@Component({
    providers: [GameService],
    selector: 'game',
    template: `
        <h1>{{title}}</h1>
        <ul>
            <li *ngFor="let c of Cards">
                {{ c.Position.Row}}
            </li>
        </ul>`
})
export class GameComponent {
    title = 'Hulk';
    Cards: Array<twinPairs.Card>;

    constructor(private gameService: GameService) {

        this.Cards = gameService.loadCards();

    }
}