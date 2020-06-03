import { Effect, Reducer } from 'umi';
import { queryMyChemicals, postReturnChemical } from './service';
import { IChemical } from '@/models/entity';
import { message } from 'antd';

export interface MyChemicalModelState {
  data: IChemical[];
}

export interface MyChemicalModelType {
  namespace: string;
  state: MyChemicalModelState;
  effects: {
    fetch: Effect;
    returnChemical: Effect;
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
    *returnChemical({payload}, { call, put }) {
      const response = yield call(postReturnChemical,payload);
      message.success('退还成功')
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
