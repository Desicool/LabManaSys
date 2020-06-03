import { Effect, Reducer } from 'umi';
import { queryMessage, queryNotify, updateNotify } from './service';
import { IClaimForm, IDeclarationForm, IFinancialForm, IWorkFlow } from '@/models/entity';
import { message } from 'antd';

export interface IMsgResult {
  cform: IClaimForm[];
  dform: IDeclarationForm[];
  fform: IFinancialForm[];
}
export interface INotifyResult {
  cform: IClaimForm[];
  wf: IWorkFlow[];
}
export interface INotifyUpdateParam {
  rid: number;
  type: "workflow" | "claimform" | "none" | undefined;
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
    updateNotifyStatus: Effect;
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
      console.log(response);
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
    },
    *updateNotifyStatus({ payload }, { call }) {
      yield call(updateNotify, payload);
    }
  },

  reducers: {
    fetchMessageSuccess(state, { payload }) {
      return {
        ...state,
        msg: payload
      };
    },
    fetchNotifySuccess(state, { payload }) {
      return {
        ...state,
        notify: payload,
      }
    }
  },
};

export default Model;
