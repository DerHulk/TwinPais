import { Injectable } from '@angular/core';
import { twinPairs } from './entities';

export var Cards: twinPairs.Card[] = [
    { Position: { Column: 1, Row: 1 }, Motiv: { Id: "1", Name: "" }, test: "", test2: "", State: "masked"   },
    { Position: { Column: 2, Row: 1 }, Motiv: { Id: "2", Name: "" }, test: "", test2: "", State: "masked" },
    { Position: { Column: 3, Row: 1 }, Motiv: { Id: "3", Name: "" }, test: "", test2: "", State: "masked"  },
    { Position: { Column: 1, Row: 2 }, Motiv: { Id: "3", Name: "" }, test: "", test2: "", State: "masked"  },
    { Position: { Column: 1, Row: 2 }, Motiv: { Id: "2", Name: "" }, test: "", test2: "", State: "masked"  },
    { Position: { Column: 1, Row: 2 }, Motiv: { Id: "12", Name: "" }, test: "12", test2: "", State: "masked" },
];

@Injectable()
export class GameService { 

    public loadCards(): Array<twinPairs.Card> {
        return Cards; //ignore
    }

}