import { Injectable } from "@angular/core"
import { State, Action, StateContext } from "@ngxs/store"
import { Guid } from "guid-typescript"
import { TripTableDtoRequest } from "src/model/searchable/trip/tripTableDtoRequest"
import { TripTableDto } from "src/model/trip/tripTableDto"
import { TripService } from "src/service/trip/trip.service"

export namespace TripActions {
    export class GetByDriver {
        static readonly type = '[Trip] GetByDriver'

        constructor(public request: Partial<TripTableDtoRequest>) {}
    } 

    export class Set {
        static readonly type = '[Trip] Set'

        constructor (public trips: TripTableDto[]) {}
    }
}

export interface TripStateModel {
    trips: TripTableDto[],
    searchableRequest: TripTableDtoRequest
}

@State<TripStateModel>({
    name: 'trip',
    defaults: {
        trips: [],
        searchableRequest: new TripTableDtoRequest()
    }
})
@Injectable()
export class TripState {
    constructor (private tripService: TripService) {}

    @Action(TripActions.GetByDriver)
    getTripsByDriver(ctx: StateContext<TripStateModel>, action: TripActions.GetByDriver) {
        let state = ctx.getState();
        ctx.setState({
            ...state,
            searchableRequest: Object.assign(state.searchableRequest, action.request)
        });
        state = ctx.getState();

        this.tripService.getTripsByDriver(state.searchableRequest)
            .subscribe(response => {
                ctx.dispatch(new TripActions.Set(response));
            })
    }

    @Action(TripActions.Set)
    setTrips(ctx: StateContext<TripStateModel>, action: TripActions.Set) {
        const state = ctx.getState();
        ctx.setState({
            ...state,
            trips: action.trips
        })
    }
}