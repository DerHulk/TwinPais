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

    public loadCards(): Array<twinPairs.Card> {
        return Cards; //ignore
    }

    public expose(card: twinPairs.Card): number {
        var value = this.http.get("./game/expose?row=" + card.Position.Row + "&column=" + card.Position.Column)
            .toPromise().then((value) => value.json()[1]);

        return Number(value);
    }

    public loadGames(onLoad: (load: twinPairs.Game[]) => void) {
        var games = this.http.get("./lobby/index/")
            .map(x => <Array<twinPairs.Game>>x.json())
            .subscribe(onLoad);
    }

    public createGame() {
        this.http.post("./lobby/create", null).subscribe();
    }

    public join(id: string): void {
        var test = this.http.post("./lobby/join?id=" + id, null).subscribe();
    }
}