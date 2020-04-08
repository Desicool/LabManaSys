import { Reducer } from 'umi';

export interface IUser {
  userName?: string;
  realName?: string;
  userId?: number;
  userPassword?: string;
  labId?: number;
  labName?: string;
}
export interface IRole {
  roleId?: number;
  lid?: number;
  roleName?: string;
}
export interface CurrentUser {
  avatar?: string;
  name?: string;
  title?: string;
  group?: string;
  signature?: string;
  tags?: {
    key: string;
    label: string;
  }[];
  userid?: string;
  unreadCount?: number;
}

export interface UserModelState {
  currentUser?: IUser;
  roles?: IRole[];
  messageCount?: number;
}

export interface UserModelType {
  namespace: 'user';
  state: UserModelState;
  effects: {};
  reducers: {
    setCurrentUser: Reducer<UserModelState>;
    changeNotifyCount: Reducer<UserModelState>;
  };
}

const UserModel: UserModelType = {
  namespace: 'user',

  state: {
    currentUser: {},
  },

  effects: {},

  reducers: {
    setCurrentUser(state, action) {
      return {
        ...state,
        currentUser: action.payload.user,
        roles: action.payload.roles,
      };
    },
    changeNotifyCount(
      state = {
        currentUser: {},
      },
      action,
    ) {
      return {
        ...state,
        currentUser: {
          ...state.currentUser,
          notifyCount: action.payload.totalCount,
          unreadCount: action.payload.unreadCount,
        },
      };
    },
  },
};

export default UserModel;
