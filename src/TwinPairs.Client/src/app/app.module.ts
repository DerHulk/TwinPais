import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, BaseRequestOptions, RequestOptions } from '@angular/http';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';

import { AppComponent } from './app.component';
import { GameService } from "app/game.service";
import { GameComponent } from './game/game.component';
import { Routes, RouterModule } from "@angular/router";
import { LobbyComponent } from './lobby/lobby.component';


export class ExtendeRequestOptions extends BaseRequestOptions {
  constructor () {
    super();
    this.headers.append('TwinPairs.Api','V1.0');
  }
} 

const appRoutes: Routes = [
    { path: '', redirectTo: 'lobby', pathMatch: 'full' },
    { path: 'lobby', component: LobbyComponent },
    { path: 'game/:id', component: GameComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    GameComponent,
    LobbyComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    NoopAnimationsModule,
    HttpModule, RouterModule.forRoot(appRoutes)
  ],
  providers: [GameService, {provide: RequestOptions, useClass: ExtendeRequestOptions}, {provide: LocationStrategy, useClass: HashLocationStrategy}],
  bootstrap: [AppComponent]
})
export class AppModule { }
