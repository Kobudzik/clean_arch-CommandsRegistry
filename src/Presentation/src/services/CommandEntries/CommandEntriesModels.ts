export interface CommandFormModel {
    id: number;
    name: string;
    jsonSchema: string;
}

export interface CreateCommandModel {
    name: string;
    jsonSchema: string;
}

export interface CommandFilterModel {
    name: string;
}
