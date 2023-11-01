import { Injectable } from "@angular/core"
import { State, Action, StateContext } from "@ngxs/store"
import { DriverListTableDto } from "src/model/driver/driverListTableDto"
import { SearchableRequest } from "src/model/searchable/searchableRequest"
import { DriverService } from "src/service/driver/driver.service"

export namespace DriverActions {
    export class Get {
        static readonly type = '[Driver] Get'
        constructor (public request?: Partial<SearchableRequest>) { }
    }

    export class Set {
        static readonly type = '[Driver] Set'

        constructor (public drivers: DriverListTableDto[]){}
    }

    export class CalculatePayableTime {
        static readonly type = '[Driver] CalculatePayableTime'
    }
}

export interface DriverStateModel {
    drivers: DriverListTableDto[],
    searchableRequest: SearchableRequest
}

@State<DriverStateModel>({
    name: 'driver',
    defaults: {
        drivers: [],
        searchableRequest: new SearchableRequest()
    }
})
@Injectable()
export class DriverState {
    constructor (private driverService: DriverService) {}

    @Action(DriverActions.Get)
    getDrivers(ctx: StateContext<DriverStateModel>, action: DriverActions.Get) {
        let state = ctx.getState();
        ctx.setState({
            ...state,
            searchableRequest: Object.assign(state.searchableRequest, action.request)
        });
        state = ctx.getState();
        this.driverService.getDrivers(state.searchableRequest)
            .subscribe(response => {
                ctx.dispatch(new DriverActions.Set(response));
            })
        }
    
    @Action(DriverActions.Set)
    setDrivers(ctx: StateContext<DriverStateModel>, action: DriverActions.Set) {
        const state = ctx.getState();
        ctx.setState({
            ...state,
            drivers: action.drivers
        })
    }

    @Action(DriverActions.CalculatePayableTime)
    calculatePayableTime(ctx: StateContext<DriverStateModel>, action: DriverActions.CalculatePayableTime) {
        this.driverService.calculateDriversPayableTime()
            .subscribe(response => {
                ctx.dispatch(new DriverActions.Get());
            })
    }
}