import { action } from 'typesafe-actions';

// Types
export interface IAuth {
    username: string;
}

export enum AuthActionTypes {
    LOGIN = 'AUTH/LOGIN',
    LOGIN_SUCCESS = 'AUTH/LOGINSUCCESS',
    LOGOUT = 'AUTH/LOGOUT',
}

export interface IAuthState {
    readonly data: IAuth | undefined;
}

// Actions
export const signIn = (currentUserAlias: string) => action(AuthActionTypes.SIGN_IN, currentUserAlias);
export const signOut = () => action(AuthActionTypes.SIGN_OUT);

// Reducer
const initialState: IAuthState = {
    data: { MSAlias: 't-chyao@Microsoft.com' },
};

export const reducer = (state = initialState, actionParameter?: any) => {
    switch (actionParameter.type) {
        case AuthActionTypes.SIGN_IN: {
            return { ...state, data: actionParameter.payload };
        }
        case AuthActionTypes.SIGN_OUT: {
            return { ...state, data: null };
        }
        default: {
            return state;
        }
    }
};