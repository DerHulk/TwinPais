import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, BaseRequestOptions, RequestOptions } from '@angular/http';

import { AppComponent } from './app.component';
import { GameService } from "app/game.service";
import { GameComponent } from './game/game.component';
import { Routes, RouterModule } from "@angular/router";


export class ExtendeRequestOptions extends BaseRequestOptions {
  constructor () {
    super();
    this.headers.append('TwinPairs.Api','V1.0');
  }
} 

const appRoutes: Routes = [
    { path: 'game/:id', component: GameComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    GameComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule, RouterModule.forRoot(appRoutes)
  ],
  providers: [GameService, {provide: RequestOptions, useClass: ExtendeRequestOptions}],
  bootstrap: [AppComponent]
})
export class AppModule { }
