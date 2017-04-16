import { Injectable } from '@angular/core';
import { URLSearchParams, Http, HttpModule, JsonpModule, RequestOptions, Headers, Response } from '@angular/http';
import { twinPairs } from './entities';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import 'rxjs/Rx';

@Injectable()
export class GameService {

    constructor(private http: Http) {
    }

    public whoIAm():Observable<twinPairs.Player>{
       return this.http.get("./lobby/whoIam").map(x=> <twinPairs.Player>x.json());
    }

    public loadCards(gameId: number): Observable<twinPairs.Card[]> {
        return this.http.get("./game/read/" + gameId).map(x => <Array<twinPairs.Card>>x.json());
    }

    public expose(gameId: number, card: twinPairs.Card): Observable<twinPairs.CardMotiv> {
        return this.http.get("./game/expose/" + gameId + "/?row=" + card.Position.Row + "&column=" + card.Position.Column)
            .map(x => {

                if (x.status == 403)
                    throw new twinPairs.NotOnTurnError("You are not on turn.");

                return <twinPairs.CardMotiv>x.json();
            });
    }

    public loadGames()
        : Observable<twinPairs.Game[]> {
        return this.http.get("./lobby/index/")
            .map(x =>  
                <Array<twinPairs.Game>>x.json());
    }

    public createGame() : Observable<Response> {
        var command = new twinPairs.CreateGameCommandModel();
        command.Cards = 4;
        command.IsPublic = true;

        return this.http.post("./lobby/create", command);
    }

    public join(game: twinPairs.Game): Observable<void> {
        return this.http.post("./lobby/join?id=" + game.Id, null)
            .map(response => {
                if (response.status == 200) {
                    game.State = twinPairs.GameStatus.WaitingForPlayers;
                }
                if (response.status == 201) {
                    game.State = twinPairs.GameStatus.Running;
                }
            });
    }
}
