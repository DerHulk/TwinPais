import { Component, Input, trigger, state, style, transition, animate } from '@angular/core';
import { GameService } from '../game.service';
import { twinPairs } from '../entities';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css'],
    animations: [
        trigger('cardState', [
            state('masked', style({
            })),
            state('exposed', style({
                transform: 'rotateY(180deg)',
            })),
            state('paired', style({
                transform: 'rotateY(90deg)',
            })),
            transition('masked => exposed', animate('100ms ease-in')),
            transition('exposed => masked', animate('100ms ease-out')),
            transition('* => paired', animate('100ms ease-out')),
        ])
    ]
})
export class GameComponent {

    GameId: number;
    Cards: Array<Array<twinPairs.Card>>;
    Pairs: number = 0;
    LastCard: twinPairs.Card;

    constructor(private route: ActivatedRoute, private gameService: GameService) {

        route.params.subscribe(params => {

            this.GameId = params["id"];
            gameService.loadCards(this.GameId).subscribe(x => {

                var rows = Math.max.apply(Math, x.map(r => r.Position.Row));
                var columns = Math.max.apply(Math, x.map(r => r.Position.Column));

                x.forEach(x => {
                    x.State = "masked";
                    x.Motive = new twinPairs.CardMotiv();
                });

                this.Cards = new Array<Array<twinPairs.Card>>();

                for (var r = 0; r <= rows; r++) {
                    var rowArray = new Array<twinPairs.Card>();
                    this.Cards.push(rowArray);

                    for (var c = 0; c <= columns; c++) {
                        rowArray.push(this.getCard(x, r, c));
                    }
                }

            });
        });

    }

    public expose(card: twinPairs.Card) {


        if (card.State != "masked")
            return;

        this.gameService.expose(this.GameId, card).subscribe(x => {

            card.Motive.Name = x.Name;
            card.State = "exposed";

            if (this.LastCard != null && this.LastCard != card && this.LastCard.Motive.Name == card.Motive.Name) {
                this.Pairs++;
                this.LastCard.State = "paired";
                card.State = "paired";
            }

            this.LastCard = card;

        }, (error) => {

            this.LastCard = null;
            if (error.status == 412) {
                this.Cards.forEach(r => {
                    r.forEach(c => {
                        if (c.State == "exposed")
                            c.State = "masked"
                    }
                    );
                });
            }
            else
                alert(error)
        });
    }

    public getCard(unsortedCard: Array<twinPairs.Card>, row: number, column: number): twinPairs.Card {

        return unsortedCard.find(x => x.Position.Column == column && x.Position.Row == row);

    }


}
