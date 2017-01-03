import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { GameComponent, LobbyComponent, TwinPairComponent } from './game.component';
import { HttpModule, JsonpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';


const appRoutes: Routes = [
    { path: 'game/:id', component: GameComponent },
    { path: 'games', component: GameComponent },
    { path: '**', component: LobbyComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes), BrowserModule, HttpModule, JsonpModule],
    declarations: [GameComponent, TwinPairComponent, LobbyComponent],
    bootstrap: [TwinPairComponent]
})
export class AppModule { }