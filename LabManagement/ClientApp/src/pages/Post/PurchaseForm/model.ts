import { Effect, Reducer } from 'umi';

import { submitFinancialForm } from './service';
import { IFinancialForm } from '@/models/entity';

export interface PostFinancialFormStateType {
  current?: 'select' | 'input' | 'finished';
  formdata?: IFinancialForm;
}

export interface PostFinancialFormModelType {
  namespace: string;
  state: PostFinancialFormStateType;
  effects: {
    submitStepForm: Effect;
  };
  reducers: {
    saveStepFormData: Reducer<PostFinancialFormStateType>;
    saveCurrentStep: Reducer<PostFinancialFormStateType>;
    clearFormData: Reducer<PostFinancialFormStateType>;
  };
}

const Model: PostFinancialFormModelType = {
  namespace: 'postPurchase',

  state: {
    current: 'select',
    formdata: {}
  },

  effects: {
    *submitStepForm({ payload }, { call, put }) {
      yield call(submitFinancialForm, payload);
      yield put({
        type: 'saveStepFormData',
        payload,
      });
      yield put({
        type: 'saveCurrentStep',
        payload: 'finished',
      });
    },
  },

  reducers: {
    saveCurrentStep(state, { payload }) {
      return {
        ...state,
        current: payload,
      };
    },

    saveStepFormData(state, { payload }) {
      return {
        ...state,
        formdata: { ...payload }
      };
    },

    clearFormData(_) {
      return {
        current: 'select',
        formdata: undefined
      }
    }
  },
};

export default Model;
