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
        public Motive: CardMotiv;
        public test: string;
        public test2: string;
        public State: string = "masked";

    }

    export class Player {
      public Name:string;
    }

    export class Game {
        public Id: string;
        public Players: Array<string>;
        public State: GameStatus;
    }

    export class CreateGameCommandModel {
        public Cards:number;
        public IsPublic: boolean;
    }

    export class NotOnTurnError extends Error {
        constructor(m: string) {
            super(m);
        }
    }

    export class AuthentificationError extends Error {
        constructor(m: string) {
            super(m);
        }
    }

    export enum GameStatus {
        Initalized = 0,
        WaitingForPlayers = 1,
        ReadyToStart = 2,
        Running = 4,
        Finished = 8,
    }
}

