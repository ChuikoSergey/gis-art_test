import { Guid } from "guid-typescript";

export class TripTableDto {
    public Id! : Guid;
    public DriverId? : number;
    public Pickup? : string;
    public Dropoff? : string;

    public constructor(init?:Partial<TripTableDto>) {
        Object.assign(this, init);
    }
}