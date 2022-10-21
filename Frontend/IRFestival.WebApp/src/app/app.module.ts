import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutModule } from './layout/layout.module';
import { HttpClientModule } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { MsalModule, MsalRedirectComponent } from '@azure/msal-angular';
import { PublicClientApplication } from '@azure/msal-browser';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,

    HttpClientModule,

    LayoutModule,

        MsalModule.forRoot(new PublicClientApplication({
      auth: {
        clientId: '19c5ae92-945b-4e9c-8ccb-851463ab5dfc',
        authority: '',
        redirectUri: environment.redirectUrl
      },
      cache: {
        cacheLocation: 'localStorage'
      }

    }), null, null)

  ],

  providers: [],
  bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule { }
