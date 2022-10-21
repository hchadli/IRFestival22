import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PicturesApiService {
  private baseUrl = environment.apiBaseUrl + 'pictures';

  constructor(private httpClient: HttpClient) { }

  getAllUrls(): Observable<string[]> {
    return this.httpClient.get<string[]>(`${this.baseUrl}`);
  }

  upload(file: File): Observable<never> {
    const data = new FormData();
    data.set('file', file);

    return this.httpClient.post<never>(`${this.baseUrl}/upload`, data);
  }
}
