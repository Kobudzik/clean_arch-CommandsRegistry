export default class Pager {
    //TODO dodać order i sorting
    private _index: number = 1;
    private _size: number = 10;
    private _totalRows: number = 0;
    private _sortBy: string = "";
    private _order: string = "ASC";

    public constructor(index: number = 1, size: number = 10, sortBy: string = "Id", _order: string = "ASC") {
        this._index = index;
        this._size = size;
        this._sortBy = sortBy;
        this._order = _order;
    }

    public get totalRows(): number {
        return this._totalRows;
    }

    public set totalRows(value: number) {
        this._totalRows = value;
    }

    public get index(): number {
        return this._index;
    }

    public set index(value: number) {
        this._index = value;
    }

    public get sortBy(): string {
        return this._sortBy;
    }

    public set sortBy(value: string) {
        this._sortBy = value;
    }

    public get order(): string {
        return this._order;
    }

    public set order(value: string) {
        this._order = value;
    }

    public get size(): number {
        return this._size;
    }

    public set size(value: number) {
        this._size = !isNaN(value) && value > 0 ? value : 10;
    }

    public get data(): PagerContract {
        return {
            index: this.index,
            size: this.size,
            sortBy: this.sortBy,
            order: this.order,
        };
    }
}

export interface PagerContract {
    index: number;
    size: number;
    sortBy: string;
    order: string;
}
