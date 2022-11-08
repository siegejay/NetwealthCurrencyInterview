import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';


import { AppRoutingModule } from "./app-routing.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './angular-material.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { environment } from '../environments/environment';
import { ConnectionService, ConnectionServiceModule, ConnectionServiceOptions, ConnectionServiceOptionsToken } from 'angular-connection-service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: ConnectionServiceOptionsToken,
      useValue: <ConnectionServiceOptions>{
        heartbeatUrl: environment.baseUrl + 'api/heartbeat'
      }
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
