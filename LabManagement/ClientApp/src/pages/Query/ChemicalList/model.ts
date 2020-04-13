import { Effect, Reducer } from 'umi';
import { queryChemicals } from './service';
import { IChemical } from '@/models/entity';

export interface ChemicalListModelState {
  list: IChemical[];
}

export interface ChemicalListModelType {
  namespace: string;
  state: ChemicalListModelState;
  effects: {
    fetch: Effect;
    appendFetch: Effect;
  };
  reducers: {
    queryList: Reducer<ChemicalListModelState>;
    appendList: Reducer<ChemicalListModelState>;
  };
}

const Model: ChemicalListModelType = {
  namespace: 'chemicalList',

  state: {
    list: [],
  },

  effects: {
    *fetch(_,{ call, put }) {
      const response = yield call(queryChemicals);
      yield put({
        type: 'queryList',
        payload: Array.isArray(response) ? response : [],
      });
    },
    *appendFetch({ payload }, { call, put }) {
      const response = yield call(queryChemicals, payload);
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
