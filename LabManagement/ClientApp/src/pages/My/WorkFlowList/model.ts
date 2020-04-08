import { Effect, Reducer } from 'umi';
import { queryFakeList } from './service';

import { IWorkFlow } from '@/models/entity';

export interface WorkFlowListStateType {
  list: IWorkFlow[];
}

export interface WorkFlowListModelType {
  namespace: string;
  state: WorkFlowListStateType;
  effects: {
    fetch: Effect;
    appendFetch: Effect;
  };
  reducers: {
    queryList: Reducer<WorkFlowListStateType>;
    appendList: Reducer<WorkFlowListStateType>;
  };
}

const Model: WorkFlowListModelType = {
  namespace: 'workFlowList',

  state: {
    list: [],
  },

  effects: {
    *fetch({ payload }, { call, put }) {
      const response = yield call(queryFakeList, payload);
      yield put({
        type: 'queryList',
        payload: Array.isArray(response) ? response : [],
      });
    },
    *appendFetch({ payload }, { call, put }) {
      const response = yield call(queryFakeList, payload);
      yield put({
        type: 'appendList',
        payload: Array.isArray(response) ? response : [],
      });
    },
  },

  reducers: {
    queryList(state, action) {
      return {
        ...state,
        list: action.payload,
      };
    },
    appendList(state = { list: [] }, action) {
      return {
        ...state,
        list: state.list.concat(action.payload),
      };
    },
  },
};

export default Model;
