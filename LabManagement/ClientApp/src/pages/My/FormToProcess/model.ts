import { Effect, Reducer } from 'umi';
import { queryMessage } from './service';
import { IClaimForm, IDeclarationForm, IFinancialForm } from '@/models/entity';

export interface IMsgResult{
  cform: IClaimForm[];
  dform: IDeclarationForm[];
  fform: IFinancialForm[];
}
export interface FormToProcessState{
  msg?: IMsgResult;
}
export interface FormToProcessModelType {
  namespace: string;
  state: FormToProcessState;
  effects: {
    fetchMessage: Effect;
  };
  reducers: {
    fetchMessageSuccess: Reducer<FormToProcessState>;
  };
}

const Model: FormToProcessModelType = {
  namespace: 'myFormToProcess',

  state: {
    
  },

  effects: {
    *fetchMessage(_, { call, put }) {
      const response = yield call(queryMessage);
      yield put({
        type: 'fetchMessageSuccess',
        payload: response,
      });
    },
  },

  reducers: {
    fetchMessageSuccess(state, { payload }) {
      return {
        ...state,
        msg: payload
      };
    },
  },
};

export default Model;
