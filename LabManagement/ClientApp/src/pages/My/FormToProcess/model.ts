import { Effect, Reducer } from 'umi';
import { queryMessage, queryNotify } from './service';
import { IClaimForm, IDeclarationForm, IFinancialForm, IWorkFlow } from '@/models/entity';

export interface IMsgResult {
  cform: IClaimForm[];
  dform: IDeclarationForm[];
  fform: IFinancialForm[];
}
export interface INotifyResult{
  cform: IClaimForm[];
  wf: IWorkFlow[];
}
export interface FormToProcessState {
  msg?: IMsgResult;
  notify?: INotifyResult;
}
export interface FormToProcessModelType {
  namespace: string;
  state: FormToProcessState;
  effects: {
    fetchMessage: Effect;
    fetchNotify: Effect;
  };
  reducers: {
    fetchMessageSuccess: Reducer<FormToProcessState>;
    fetchNotifySuccess: Reducer<FormToProcessState>;
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
    *fetchNotify(_, { call, put }) {
      const response = yield call(queryNotify);
      yield put({
        type: 'fetchNotifySuccess',
        payload: response,
      })
    }
  },

  reducers: {
    fetchMessageSuccess(state, { payload }) {
      return {
        ...state,
        msg: payload
      };
    },
    fetchNotifySuccess(state, {payload}){
      return {
        ...state,
        notify: payload,
      }
    }
  },
};

export default Model;
