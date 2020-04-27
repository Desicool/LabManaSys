import { Effect, Reducer, IPostClaimFormParam } from 'umi';
import { queryClaimDetail } from './service';
import { IChemical } from '@/models/entity';

export interface ClaimFormChemicalModelState {
  data?: IPostClaimFormParam;
}

export interface ClaimFormChemicalModelType {
  namespace: string;
  state: ClaimFormChemicalModelState;
  effects: {
    fetch: Effect;
  };
  reducers: {
    fetchSuccess: Reducer<ClaimFormChemicalModelState>
  };
}

const Model: ClaimFormChemicalModelType = {
  namespace: 'claimFormChemical',

  state: {},

  effects: {
    *fetch({ payload }, { call, put }) {
      const response = yield call(queryClaimDetail, payload.formid);
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
