﻿export module twinPairs {

    export class GamePosition {
        public Row: number;
        public Column: number;
    }

    export class CardMotiv {
        public Name: string;
        public Id: string;
    }

    export class Card {

        public Postition: GamePosition;
        public Motiv: CardMotiv;
        public test: string;
        public test2: string;
    }

}
