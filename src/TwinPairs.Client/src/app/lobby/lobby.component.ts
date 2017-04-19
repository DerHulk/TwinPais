import { Component } from '@angular/core';
import { GameService } from "../game.service";
import { twinPairs } from '../entities';

@Component({
  selector: 'app-lobby',
  providers: [GameService],
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent {

  public Games: twinPairs.Game[];
  public Player: twinPairs.Player;
  title = 'app works!';

  constructor(private gameService: GameService) {

    this.Games = new Array<twinPairs.Game>();
    this.gameService.whoIAm().subscribe(p =>
      this.Player = p, this.HandleKnowErros);
    this.gameService.loadGames().subscribe(x => {
      this.Games = x;
    });
  }

  public create(): void {
    this.gameService.createGame().subscribe(x => {
      this.gameService.loadGames().subscribe(x => {
        this.Games = x
      });
    });
  }

  public join(game: twinPairs.Game): void {
    this.gameService.join(game).subscribe(() => {
      alert('You have joined the game');
    });
    this.gameService.loadGames().subscribe(x => this.Games = x);
  }

  public canJoin(game: twinPairs.Game): Boolean {
    return game.State == twinPairs.GameStatus.WaitingForPlayers && !game.Players.some(p => p == this.Player.Name);
  }


  private HandleKnowErros(error: any) {

    if (error.status == 0)
      alert('Backend Server not availabe');
    else if (error.status == 403)
      alert('Not Authorized');
  }
}
