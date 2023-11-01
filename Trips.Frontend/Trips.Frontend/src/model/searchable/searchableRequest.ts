export class SearchableRequest {
    public OrderBy? : string
    public AscendingOrder! : boolean
    public SearchBy? : string

    public constructor(init?:Partial<SearchableRequest>) {
        Object.assign(this, init);
    }
}