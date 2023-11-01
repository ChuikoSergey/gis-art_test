export class DriverListTableDto {
    public PayableTime? : number | undefined;
    public Name! : string;

    public constructor(init?:Partial<DriverListTableDto>) {
        Object.assign(this, init);
    }
}