import { Injectable } from '@angular/core';
import { twinPairs } from './entities';

export var Cards: twinPairs.Card[] = [
    { Postition: { Column: 1, Row: 1 }, Motiv: { Id: "1", Name: "" } , test:"", test2:""  },
{ Postition: { Column: 2, Row: 1 }, Motiv: { Id: "2", Name: "" },  test: "", test2:"" },
{ Postition: { Column: 3, Row: 1 }, Motiv: { Id: "3", Name: "" },  test:"", test2:""  },
{ Postition: { Column: 1, Row: 2 }, Motiv: { Id: "3", Name: "" },  test: "", test2:""  },
{ Postition: { Column: 1, Row: 2 }, Motiv: { Id: "2", Name: "" },  test: "", test2:""  },
{ Postition: { Column: 1, Row: 2 }, Motiv: { Id: "7", Name: "" },  test: "12345", test2:"" },
];

@Injectable()
export class GameService {

    public loadCards(): Array<twinPairs.Card> {
        return Cards;
    }

}