/* eslint-disable */
import merge from "lodash/merge";

class FormHelper<T extends Record<string, any>> {
    private initial = {};

    public loading: boolean;

    [prop: string]: any;

    /**
     * Create a new Form instance.
     */
    public constructor(data: T) {
        this.loading = false;
        this.withData(data);
    }

    public setInitialValues(values: any): void {
        this.initial = {};

        merge(this.initial, values);
    }

    public withData(data: T): FormHelper<T> {
        this.setInitialValues(data);

        for (const field in data) {
            this[field as string] = data[field];
        }

        return this;
    }

    /**
     * Fetch all relevant data for the form.
     */
    public get data(): T {
        const data = {} as any;

        for (const property in this.initial) {
            data[property] = this[property] as any;
        }

        return data as T;
    }

    /**
     * Reset the form fields to initial values
     */
    public reset(): void {
        merge(this, this.initial);
    }

    public wait(): void {
        this.loading = true;
    }

    public finishWaiting(): void {
        this.loading = false;
    }

    public async ready(requests: Promise<boolean>[]): Promise<void> {
        this.wait();
        const result = await Promise.all(requests);
        this.finishWaiting();
    }

    public static create<T>(data: T): FormHelper<T> {
        return new FormHelper<T>(data);
    }
}

type Form<T> = FormHelper<T> & T;

interface FormFactory {
    new <T>(data: T): Form<T>;
    create<T>(data: T): Form<T>;
}

const Form: FormFactory = FormHelper as any;

export default Form;
