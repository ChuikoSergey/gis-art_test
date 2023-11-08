export class DriverListTableDto {
    public PayableTime? : number | undefined;
    public Id! : number;

    public constructor(init?:Partial<DriverListTableDto>) {
        Object.assign(this, init);
    }
}