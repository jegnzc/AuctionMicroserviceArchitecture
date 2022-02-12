import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
// import { AngularFontAwesomeModule } from 'angular-font-awesome';
// import { StorageServiceModule } from 'angular-webstorage-service';
// import { MsalService } from './services/msal.service';
// import { SecurityGaurdService } from '../app/services/securitygaurd.service';
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    // AngularFontAwesomeModule,
    // StorageServiceModule,
  ],
  // providers: [MsalService, SecurityGaurdService],
  bootstrap: [AppComponent]
})
export class AppModule { }