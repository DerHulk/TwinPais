import { Component, Input, trigger, state, style, transition, animate  } from '@angular/core';
import { GameService } from './game.service';
import { twinPairs } from './entities';

@Component({
    providers: [GameService],
    selector: 'twinpairs',
    template: `
                <h1>Twinpairs!</h1>
                <router-outlet></router-outlet>
              `
})
export class TwinPairComponent {

    constructor(private gameService: GameService) {
        
    }
}
//test
@Component({
    providers: [GameService],
    selector: 'lobby',
    templateUrl: '/template/lobby',
})
export class LobbyComponent {

    public Games: twinPairs.Game[];

    constructor(private gameService: GameService) {
        this.gameService.loadGames(x => this.Games = x); //123
    }

    public clicked(): void {
        //this.gameService.join("1");
        this.gameService.createGame();
    }
}

@Component({
    providers: [GameService],
    selector: 'game',
    templateUrl: '/template/board',
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

    Cards: Array<Array<twinPairs.Card>>;

    constructor(private gameService: GameService) {

        var loadedCards = gameService.loadCards();
        var rows = Math.max.apply(Math, loadedCards.map(x => x.Position.Row));
        var columns = Math.max.apply(Math, loadedCards.map(x => x.Position.Column));

        this.Cards = new Array<Array<twinPairs.Card>>();

        for (var r = 1; r <= rows; r++) {
            var rowArray = new Array<twinPairs.Card>();
            this.Cards.push(rowArray);

            for (var c = 1; c <= columns; c++) {
                rowArray.push(this.getCard(loadedCards, r, c));
            }
        }

    }

    public expose(card: twinPairs.Card) {
        card.State = "exposed";
        var value = this.gameService.expose(card);
        card.Motiv.Name = value.toString();
    }

    public getCard(unsortedCard:Array<twinPairs.Card>, row: number, column: number): twinPairs.Card {

        return unsortedCard.find(x => x.Position.Column == column && x.Position.Row == row);

    }
}
