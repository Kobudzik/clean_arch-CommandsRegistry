export interface CreateUserModel {
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    password: string;
    isActive?: boolean;
    roles?: string[];
}

export interface UpdateUserModel {
    userId: string;
    firstName: string;
    lastName: string;
    themePrimaryColor: string;
}

export interface UserFormModel {
    id: string;
    userName: string;
    email: string;
    firstName: string;
    lastName: string;
    isActive: boolean;
    roles: string[];
    themePrimaryColor: string;
}

export interface UserListItemModel {
    publicId: string;
    userName: string;
    email: string;
    firstName: string;
    lastName: string;
    isActive: boolean;
    roles: string[];
}

export interface UserFilterModel {
    username: string;
    firstName: string;
    lastName: string;
}

export interface SettingsVM {
    themePrimaryColor: string;
}
