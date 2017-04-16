import { Component, Input, trigger, state, style, transition, animate } from '@angular/core';
import { GameService } from '../game.service';
import { twinPairs } from '../entities';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent  {

  GameId: number;
    Cards: Array<Array<twinPairs.Card>>;

    constructor(private route: ActivatedRoute, private gameService: GameService) {
      
        route.params.subscribe(params => {

            this.GameId = params["id"];
            gameService.loadCards(this.GameId).subscribe(x => {

                var rows = Math.max.apply(Math, x.map(r => r.Position.Row));
                var columns = Math.max.apply(Math, x.map(r => r.Position.Column));

                x.forEach(x => x.State = "masked");

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
     
        this.gameService.expose(this.GameId, card).subscribe(x => {
            card.Motive.Name = x.Name;
            card.State = "exposed";
        }, (error)=> alert(error));
    }

    public getCard(unsortedCard: Array<twinPairs.Card>, row: number, column: number): twinPairs.Card {

        return unsortedCard.find(x => x.Position.Column == column && x.Position.Row == row);

    }
  

}
