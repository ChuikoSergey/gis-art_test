import { Component } from '@angular/core';
import { Store } from '@ngxs/store';
import { SortSettings } from '@progress/kendo-angular-grid';
import { SortDescriptor } from '@progress/kendo-data-query';
import { Observable } from 'rxjs';
import { TripTableDto } from 'src/model/trip/tripTableDto';
import { TripActions } from 'src/store/trip/trip.state';

@Component({
  selector: 'app-trips-list',
  templateUrl: './trips-list.component.html',
  styleUrls: ['./trips-list.component.scss']
})
export class TripsListComponent {
  trips$ : Observable<TripTableDto[]>;

  sortSettings: SortSettings = {
    mode:'single',
    allowUnsort: true
  }

  constructor(private store: Store) {
    this.trips$ = this.store.select(state => state.trip.trips);
  }

  onSearchValueChanged(newValue: string) {
    this.store.dispatch(new TripActions.GetByDriver({SearchBy : newValue}));
  }

  sortChange(sort: SortDescriptor[]) {
    const sortFields = sort[0];
    this.store.dispatch(new TripActions.GetByDriver({
      AscendingOrder: sortFields?.dir == 'asc',
      OrderBy: sortFields?.field
    }));
  }
}
