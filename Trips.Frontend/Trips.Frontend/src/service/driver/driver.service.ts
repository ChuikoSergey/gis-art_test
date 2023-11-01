import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DriverListTableDto } from 'src/model/driver/driverListTableDto';
import { SearchableRequest } from 'src/model/searchable/searchableRequest';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  private baseUrl = 'http://localhost:5000/api/driver';

  constructor(private http: HttpClient) {}

  getDrivers(searchableRequest?: SearchableRequest): Observable<DriverListTableDto[]> {
    return this.http.get<DriverListTableDto[]>(`${this.baseUrl}`, {
      params: new HttpParams()
        .set('searchBy', searchableRequest?.SearchBy || "")
        .set('orderBy', searchableRequest?.OrderBy || "")
        .set('ascendingOrder', searchableRequest?.AscendingOrder || false)
  });
  }

  calculateDriversPayableTime(): Observable<any> {
    return this.http.put(`${this.baseUrl}/payableTime/calculate`, null);
  }
}
