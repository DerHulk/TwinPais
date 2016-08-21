import { bootstrap }    from '../node_modules/@angular/platform-browser-dynamic';
import { AppComponent } from './app.component';
import { HTTP_PROVIDERS, JsonpModule  } from '@angular/http';

bootstrap(AppComponent, [
    HTTP_PROVIDERS, JsonpModule
]);