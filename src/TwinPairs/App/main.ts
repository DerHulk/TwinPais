import { bootstrap }    from '../node_modules/@angular/platform-browser-dynamic';
import { AppComponent } from './app.component';
import { HTTP_PROVIDERS } from '@angular/http';

bootstrap(AppComponent, [
    HTTP_PROVIDERS
]);