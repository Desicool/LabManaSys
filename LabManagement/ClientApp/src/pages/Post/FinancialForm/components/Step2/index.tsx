import React from 'react';
import { Form, Button, Input } from 'antd';
import { connect, Dispatch, UserModelState, IUser } from 'umi';
import { PostFinancialFormStateType } from '../../model';
import styles from './index.less';
import { IFinancialForm } from '@/models/entity';

const formItemLayout = {
  labelCol: {
    span: 5,
  },
  wrapperCol: {
    span: 19,
  },
};
interface Step2Props {
  workflowid: number;
  dispatch: Dispatch;
  submitting?: boolean;
  currentUser?: IUser;
}

const Step2: React.FC<Step2Props> = (props) => {
  const [form] = Form.useForm();
  const { workflowid, dispatch, submitting, currentUser } = props;
  const { validateFields, getFieldsValue } = form;
  const onPrev = () => {
    if (dispatch) {
      const values = getFieldsValue();
      dispatch({
        type: 'postFinancialForm/saveStepFormData',
        payload: {
          wid: workflowid,
          ...values,
        },
      });
      dispatch({
        type: 'postFinancialForm/saveCurrentStep',
        payload: 'select',
      });
    }
  };
  const onValidateForm = async () => {
    const values = await validateFields();
    const data: IFinancialForm = {
      ...values,
      uid: currentUser?.userId,
      uname: currentUser?.userName,
      lid: currentUser?.labId,
      wid: workflowid,
      price: Number.parseFloat(values.price)
    }
    dispatch({
      type: 'postFinancialForm/submitStepForm',
      payload: data
    });
  };

  return (
    <Form
      {...formItemLayout}
      form={form}
      layout="horizontal"
      className={styles.stepForm}
      initialValues={{ password: '123456' }}
    >
      <Form.Item
        label="金额"
        name="price"
        rules={[{ required: true, message: '请输入需要申请的金额' }]}
      >
        <Input autoComplete="off" style={{ width: '80%' }} addonAfter='元' />
      </Form.Item>
      <Form.Item
        label="收款方"
        name="receiver"
        rules={[{ required: true }]}
      >
        <Input autoComplete="off" style={{ width: '80%' }} />
      </Form.Item>
      <Form.Item
        style={{ marginBottom: 8 }}
        wrapperCol={{
          xs: { span: 24, offset: 0 },
          sm: {
            span: formItemLayout.wrapperCol.span,
            offset: formItemLayout.labelCol.span,
          },
        }}
      >
        <Button type="primary" onClick={onValidateForm} loading={submitting}>
          提交
        </Button>
        <Button onClick={onPrev} style={{ marginLeft: 8 }}>
          上一步
        </Button>
      </Form.Item>
    </Form>
  );
};
export default connect(
  ({
    postFinancialForm,
    loading,
    user
  }: {
    postFinancialForm: PostFinancialFormStateType;
    loading: {
      effects: { [key: string]: boolean };
    };
    user: UserModelState;
  }) => ({
    submitting: loading.effects['postFinancialForm/submitStepForm'],
    workflowid: postFinancialForm.formdata?.wid as number,
    currentUser: user.currentUser,
  }),
)(Step2);
