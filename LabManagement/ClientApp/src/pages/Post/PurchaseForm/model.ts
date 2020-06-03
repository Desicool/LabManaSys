import { Effect } from 'umi';

import { submitPurchase } from './service';
import { IFinancialForm } from '@/models/entity';
import { message } from 'antd';

export interface PostFinancialFormStateType {
  current?: 'select' | 'input' | 'finished';
  formdata?: IFinancialForm;
}

export interface PostPurchaseModelType {
  namespace: string;
  state: PostFinancialFormStateType;
  effects: {
    submitPurchaseEffect: Effect;
  };
  reducers: {
  };
}

const Model: PostPurchaseModelType = {
  namespace: 'postPurchase',

  state: {
    current: 'select',
    formdata: {}
  },

  effects: {
    *submitPurchaseEffect({ payload }, { call }) {
      yield call(submitPurchase, payload);
      message.success('提交成功')
    },
  },

  reducers: {
  },
};

export default Model;
