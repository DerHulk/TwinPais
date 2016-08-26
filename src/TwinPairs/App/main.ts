import { bootstrap }    from '../node_modules/@angular/platform-browser-dynamic';
import { GameComponent } from './game.component';
import { HTTP_PROVIDERS, JsonpModule  } from '@angular/http';

bootstrap(GameComponent, [
    HTTP_PROVIDERS, JsonpModule
]);