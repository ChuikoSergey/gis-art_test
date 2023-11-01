import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { TripTableDtoRequest } from "src/model/searchable/trip/tripTableDtoRequest";
import { TripTableDto } from "src/model/trip/tripTableDto";

@Injectable({
    providedIn: 'root'
})
export class TripService {
    private baseUrl = 'http://localhost:5000/api';

    constructor(private http: HttpClient) {}

    getTripsByDriver(request?: TripTableDtoRequest): Observable<TripTableDto[]> {
        return this.http.get<TripTableDto[]>(`${this.baseUrl}/trip`, {
            params: new HttpParams()
                .set('driverId', request?.DriverId?.toString() || "")
                .set('searchBy', request?.SearchBy || "")
                .set('orderBy', request?.OrderBy || "")
                .set('ascendingOrder', request?.AscendingOrder || false)
        });
    }
}