import { Guid } from "guid-typescript";

export class TripTableDto {
    public Id! : Guid;
    public TripStartTimestamp! : Date;
    public TripEndTimestamp! : Date;
    public StartPlaceName? : string;
    public DestinationPlaceName? : string;

    public constructor(init?:Partial<TripTableDto>) {
        Object.assign(this, init);
    }
}