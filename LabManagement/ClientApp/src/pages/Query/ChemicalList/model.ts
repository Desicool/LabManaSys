import { Effect, Reducer } from 'umi';
import { queryChemicals, destroyChemicals } from './service';
import { IChemical } from '@/models/entity';

export interface ChemicalListModelState {
  list: IChemical[];
}

export interface ChemicalListModelType {
  namespace: string;
  state: ChemicalListModelState;
  effects: {
    fetch: Effect;
    destroy: Effect;
  };
  reducers: {
    queryList: Reducer<ChemicalListModelState>;
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
    *destroy({payload}, {call, put}){
      const response = yield call(destroyChemicals, payload);
      yield put({
        type: 'fetch'
      });
    }
  },

  reducers: {
    queryList(state, action) {
      return {
        ...state,
        list: action.payload,
      };
    },
  },
};

export default Model;
