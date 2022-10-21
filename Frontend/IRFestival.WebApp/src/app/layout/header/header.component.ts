import { Component, OnInit } from '@angular/core';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { InteractionStatus } from '@azure/msal-browser';
import { filter, takeUntil } from 'rxjs';
import { AppSettingsApiService } from 'src/app/api-services/appsettings-api.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit {
  festivalName = environment.festivalName;
  loginDisplay: any


  constructor(private appSettingsApiService: AppSettingsApiService, private msalService : MsalService, private broadcastService : MsalBroadcastService) {}

  ngOnInit(): void {
    this.appSettingsApiService
      .getSettings()
      .subscribe(
        (appsettings) =>
          (this.festivalName =
            appsettings.festivalName ?? environment.festivalName)
    );

    this.broadcastService.inProgress$
      .pipe(
        filter((status: InteractionStatus) => status === InteractionStatus.None),
    ).subscribe(() => {
      this.setLoginDisplay();
    })
  }
  private _destroying$(_destroying$: any): import("rxjs").OperatorFunction<InteractionStatus, InteractionStatus> {
    throw new Error('Method not implemented.');
  }

  login() {
    this.msalService.loginRedirect();
  }

    logout() {
      this.msalService.logoutRedirect({
      postLogoutRedirectUri: environment.redirectUrl
    });
  }

  setLoginDisplay() {
    this.loginDisplay = this.msalService.instance.getAllAccounts().length > 0;
  }
}
