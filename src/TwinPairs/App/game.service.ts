import { Injectable } from '@angular/core';
import { URLSearchParams, Http, HttpModule, JsonpModule, RequestOptions, Headers } from '@angular/http';
import { twinPairs } from './entities';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import 'rxjs/Rx';

export var Cards: twinPairs.Card[] = [
    { Position: { Column: 1, Row: 1 }, Motiv: { Id: "1", Name: "" }, test: "", test2: "", State: "masked" },
    { Position: { Column: 2, Row: 1 }, Motiv: { Id: "2", Name: "" }, test: "", test2: "", State: "masked" },
    { Position: { Column: 3, Row: 1 }, Motiv: { Id: "3", Name: "" }, test: "", test2: "", State: "masked" },
    { Position: { Column: 1, Row: 2 }, Motiv: { Id: "3", Name: "" }, test: "", test2: "", State: "masked" },
    { Position: { Column: 2, Row: 2 }, Motiv: { Id: "2", Name: "" }, test: "", test2: "", State: "masked" },
    { Position: { Column: 3, Row: 2 }, Motiv: { Id: "12", Name: "" }, test: "12", test2: "", State: "masked" },

    { Position: { Column: 1, Row: 3 }, Motiv: { Id: "3", Name: "" }, test: "", test2: "", State: "masked" },
    { Position: { Column: 2, Row: 3 }, Motiv: { Id: "2", Name: "" }, test: "", test2: "", State: "masked" },
    { Position: { Column: 3, Row: 3 }, Motiv: { Id: "12", Name: "" }, test: "12", test2: "", State: "masked" },

    { Position: { Column: 1, Row: 4 }, Motiv: { Id: "3", Name: "" }, test: "", test2: "", State: "masked" },
    { Position: { Column: 2, Row: 4 }, Motiv: { Id: "2", Name: "" }, test: "", test2: "", State: "masked" },
    { Position: { Column: 3, Row: 4 }, Motiv: { Id: "12", Name: "" }, test: "12", test2: "", State: "masked" },

];

@Injectable()
export class GameService {

    constructor(private http: Http) {
    }

    public loadCards(gameId: number): Observable<twinPairs.Card[]> {
        return this.http.get("./game/read/" + gameId).map(x=> <Array<twinPairs.Card>> x.json());
    }

    public expose(gameId: number, card: twinPairs.Card): Observable<twinPairs.CardMotiv> {
        return this.http.get("./game/expose/" + gameId + "/?row=" + card.Position.Row + "&column=" + card.Position.Column)
            .map(x => <twinPairs.CardMotiv>x.json());
    }

    public loadGames()
        : Observable<twinPairs.Game[]> {
        return this.http.get("./lobby/index/")
            .map(x => <Array<twinPairs.Game>>x.json());
    }

    public createGame() {
        var command = new twinPairs.CreateGameCommandModel();
        command.Cards = 4;
        command.IsPublic = true;

        this.http.post("./lobby/create", command ).subscribe();
    }

    public join(id: string): void {
        var test = this.http.post("./lobby/join?id=" + id, null).subscribe();
    }
}