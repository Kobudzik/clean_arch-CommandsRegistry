enum UserActions {
    // setAuthenticated = "user/setAuthenticated",
    setCurrentUser = "user/setCurrentUser",
    logout = "user/logout",
}

enum UserGetters {
    GET_AUTH_STATE = "user/GET_AUTH_STATE",
    GET_CURRENT_USER = "user/GET_CURRENT_USER",
    GET_CURRENT_USER_NAME = "user/GET_CURRENT_USER_NAME",
    GET_CURRENT_USER_ROLES = "user/GET_CURRENT_USER_ROLES",
}

export { UserActions, UserGetters };
