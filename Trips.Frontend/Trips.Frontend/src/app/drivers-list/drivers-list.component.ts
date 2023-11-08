import { Component } from '@angular/core';
import { Store } from '@ngxs/store';
import { SelectableSettings, SelectionEvent, SortSettings } from '@progress/kendo-angular-grid';
import { SortDescriptor } from '@progress/kendo-data-query';
import { Observable } from 'rxjs';
import { DriverListTableDto } from 'src/model/driver/driverListTableDto';
import { DriverActions } from 'src/store/driver/driver.state';
import { TripActions } from 'src/store/trip/trip.state';

@Component({
  selector: 'app-drivers-list',
  templateUrl: './drivers-list.component.html',
  styleUrls: ['./drivers-list.component.scss']
})
export class DriversListComponent {
  drivers$ : Observable<DriverListTableDto[]>;
  sort: SortDescriptor[] = [];

  selectableSettings: SelectableSettings = {
    enabled: true,
    mode: 'single',
  }

  sortSettings: SortSettings = {
    mode:'single',
    allowUnsort: true
  }

  constructor(private store: Store) {
    this.drivers$ = this.store.select(state => state.driver.drivers);
  }

  ngOnInit() {
    this.store.dispatch(new DriverActions.Get());
  }

  calculatePayableTimeClick() {
    this.store.dispatch(new DriverActions.CalculatePayableTime());
  }

  selectedRowChange(selectionEvent: SelectionEvent) {
    if (selectionEvent.selectedRows?.length) {
      var driver = selectionEvent.selectedRows[0].dataItem;
      this.store.dispatch(new TripActions.GetByDriver({ DriverId: driver.id }));
    }
  }

  onSearchValueChanged(newValue: string) {
    this.store.dispatch(new DriverActions.Get({SearchBy : newValue}));
    this.store.dispatch(new TripActions.GetByDriver({DriverId: null}));
  }
}
