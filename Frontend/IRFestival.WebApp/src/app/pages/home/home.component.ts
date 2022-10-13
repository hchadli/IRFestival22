import { Component, OnInit } from '@angular/core';
import { FestivalApiService } from 'src/app/api-services/festival-api.service';
import { Observable } from 'rxjs';
import { shareReplay, map, groupBy } from 'rxjs/operators';
import { Artist } from 'src/app/api/models/artist.model';
import { Schedule, ScheduleItem } from 'src/app/api/models/schedule.model';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  artists$: Observable<Artist[]>;

  AppInsights: ApplicationInsights

  private schedule$: Observable<ScheduleItem[]>;
  scheduleByDate$: Observable<{date: string, items: ScheduleItem[]}[]>;

  constructor(private festivalApiService: FestivalApiService) { 
    this.AppInsights = new ApplicationInsights({
      config: {
        connectionString: environment.connectionstringAppInsights,
        enableAutoRouteTracking: true  // option to log all route changes
      }
    });
  }

  ngOnInit(): void {

    this.AppInsights.loadAppInsights();
    this.AppInsights.trackPageView();

    this.artists$ = this.festivalApiService
      .getArtists()
      .pipe(shareReplay());

    this.schedule$ = this.festivalApiService
      .getSchedule()
      .pipe(
        map(s => s.items),
        shareReplay()
      );
    this.scheduleByDate$ = this.schedule$.pipe(
      map(s => this.groupByDate(s))
    )
  }

  private groupByDate(scheduleItems: ScheduleItem[]): {date: string, items: ScheduleItem[]}[] {
    const scheduleByDate: {date: string, items: ScheduleItem[]}[] = [];
    for(let item of scheduleItems) {
      let filteredItems = scheduleByDate.filter(x => this.toDateWithoutTime(x.date) === this.toDateWithoutTime(item.time));
      const existingItem = filteredItems.length === 0 ? null : filteredItems[0];
      if (!existingItem) {
        scheduleByDate.push({date: this.toDateWithoutTime(item.time), items: [item]});
      } else {
        existingItem.items.push(item);
      }
    }
    return scheduleByDate;
  }

  private toDateWithoutTime(date: string): string {
    const dateWithoutTime = new Date(date);
    dateWithoutTime.setHours(0, 0, 0, 0);
    return dateWithoutTime.toISOString();
  }
}
