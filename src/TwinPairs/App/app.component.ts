import { Component } from '@angular/core';
import { RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from '@angular/router-deprecated';
import { GameComponent } from './game.component';

@Component({
    selector: 'my-app',
    template: `
        <h1>{{title}}</h1>
        <a [routerLink]="['Game']">Game</a>
        <router-outlet></router-outlet>
      `,
    directives: [ROUTER_DIRECTIVES],
    providers: [
        ROUTER_PROVIDERS,
    ]
})
@RouteConfig([
    {
        path: '/Game',
        name: 'Game',
        component: GameComponent,
        useAsDefault: true
    }
])
export class AppComponent {
    title = 'Tour of Heroes 123';
}