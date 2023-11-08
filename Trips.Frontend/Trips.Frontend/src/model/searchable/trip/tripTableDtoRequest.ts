import { Guid } from "guid-typescript";
import { SearchableRequest } from "../searchableRequest";

export class TripTableDtoRequest extends SearchableRequest {
    public DriverId? : number | null;

    constructor(init?: Partial<TripTableDtoRequest>) {
        super(init);
        this.DriverId = init?.DriverId;
    }
}