import { Effect, history } from 'umi';
import { message } from 'antd';
import { submitDeclarationForm } from './service';

export interface PostDeclarationFormModelType {
  namespace: string;
  state: {};
  effects: {
    submitDeclarationForm: Effect;
  };
}
const Model: PostDeclarationFormModelType = {
  namespace: 'postDeclarationForm',

  state: {},

  effects: {
    *submitDeclarationForm({ payload }, { call }) {
      yield call(submitDeclarationForm, payload);
      message.success('提交成功');
      history.push('/my/process');
    },
  },
};

export default Model;
