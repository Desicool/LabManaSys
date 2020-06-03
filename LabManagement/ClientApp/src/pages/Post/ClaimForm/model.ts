import { Effect, history } from 'umi';
import { message } from 'antd';
import { submitClaimForm } from './service';
import { IClaimForm, IChemical } from '@/models/entity';

export interface IPostClaimFormParam {
  form: IClaimForm;
  chemicals: IChemical[];
}
export interface PostClaimFormModelType {
  namespace: string;
  state: {};
  effects: {
    submitAdvancedForm: Effect;
  };
}
const Model: PostClaimFormModelType = {
  namespace: 'postClaimForm',

  state: {},

  effects: {
    *submitAdvancedForm({ payload }, { call }) {
      yield call(submitClaimForm, payload);
      message.success('提交成功');
      history.push('/my/process');
    },
  },
};

export default Model;
