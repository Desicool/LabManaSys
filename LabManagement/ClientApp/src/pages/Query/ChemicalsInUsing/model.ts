import { Effect, Reducer } from 'umi';
import { queryMyChemicals } from './service';
import { IChemical } from '@/models/entity';

export interface MyChemicalModelState {
  data: IChemical[];
}

export interface MyChemicalModelType {
  namespace: string;
  state: MyChemicalModelState;
  effects: {
    fetch: Effect;
  };
  reducers: {
    fetchSuccess: Reducer<MyChemicalModelState>
  };
}

const Model: MyChemicalModelType = {
  namespace: 'myChemical',

  state: {
    data: [],
  },

  effects: {
    *fetch(_, { call, put }) {
      const response = yield call(queryMyChemicals);
      yield put({
        type: 'fetchSuccess',
        payload: response,
      });
    },
  },

  reducers: {
    fetchSuccess(state, action) {
      return {
        ...state,
        data: action.payload,
      };
    },
  },
};

export default Model;
