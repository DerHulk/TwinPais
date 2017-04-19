import { Component } from '@angular/core';
import { GameService } from "./game.service";
import { twinPairs } from './entities';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent  {

  title = 'app works!';

  constructor() {;
  }
}
