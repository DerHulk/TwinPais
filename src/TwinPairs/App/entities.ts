export module twinPairs {

    export class GamePosition {
        public Row: number;
        public Column: number;
    }

    export class CardMotiv {
        public Name: string;
        public Id: string;
    }

    export class Card {

        public Position: GamePosition;
        public Motiv: CardMotiv;
        public test: string;
        public test2: string;
        public State: string = "masked";

    }

    export class Game {
        public Id: string;
        public Player: Array<string>;
    }

    export class CreateGameCommandModel {
        public Cards:number;
        public IsPublic: boolean;
}
}

