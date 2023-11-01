import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { DriversListComponent } from './drivers-list/drivers-list.component';
import { TripsListComponent } from './trips-list/trips-list.component';
import { GridModule } from '@progress/kendo-angular-grid';
import { NgxsModule } from '@ngxs/store';
import { HttpClientModule } from '@angular/common/http';
import { DriverState } from 'src/store/driver/driver.state';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { TripState } from 'src/store/trip/trip.state';
import { InputsModule } from '@progress/kendo-angular-inputs';



@NgModule({
  declarations: [
    AppComponent,
    DriversListComponent,
    TripsListComponent
  ],
  imports: [
    BrowserModule,
    GridModule,
    HttpClientModule,
    InputsModule,
    NgxsModule.forRoot([
      DriverState,
      TripState
    ]),
    NgxsReduxDevtoolsPluginModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
