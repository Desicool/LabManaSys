import React, { useState, useEffect } from 'react';
import { Card, Steps, Radio, Button, Divider } from 'antd';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { connect, Dispatch, history } from 'umi';
import { PostFinancialFormStateType } from './model';
import styles from './style.less';
import Form from 'antd/es/form';
import { IWorkFlow } from '@/models/entity';


interface PurchaseFormProps {
  current: PostFinancialFormStateType['current'];
  dispatch: Dispatch;
  workFlowList: IWorkFlow[];
}
const formItemLayout = {
  labelCol: {
    span: 5,
  },
  wrapperCol: {
    span: 19,
  },
};
const FinancialForm: React.FC<PurchaseFormProps> = (props: PurchaseFormProps) => {
  useEffect(() => {
    dispatch({
      type: 'workFlowList/fetch',
    })
  }, [1]);
  const { dispatch, workFlowList } = props;
  const [form] = Form.useForm();
  const { validateFields } = form;
  const onValidateForm = async () => {
    const values = await validateFields();
    console.log(values);
    if (values.wid === undefined) {
      return;
    }
    if (dispatch) {
      dispatch({
        type: 'postFinancialForm/saveStepFormData',
        payload: values,
      });
      dispatch({
        type: 'postFinancialForm/saveCurrentStep',
        payload: 'input',
      });
    }
  };
  return (
    <PageHeaderWrapper>
      <Card>

        <Form
          {...formItemLayout}
          form={form}
          layout="horizontal"
          className={styles.stepForm}
          hideRequiredMark
        >
          <Form.Item name={'wid'}>
            <Radio.Group>
              {workFlowList.map(u => (
                <Radio key={u.id}
                  value={u.id}
                  style={{
                    display: 'block',
                    height: '30px',
                    fontSize: 15
                  }}>{u.description}</Radio>
              ))}
            </Radio.Group>
          </Form.Item>
          <Button type="primary" onClick={onValidateForm}>
            下一步
        </Button>
        </Form>
        <Divider style={{ margin: '40px 0 24px' }} />
        <div className={styles.desc}>
          <h3>说明</h3>
          <p>
            还没有通过审批的申请不会显示在本页面，请前往<a onClick={() => { history.push('/my/workflow') }}>这个地址</a>查看完整申请列表。
        </p>
        </div>
      </Card>
    </PageHeaderWrapper>
  );
};

export default connect(({
  postFinancialForm
}: {
  postFinancialForm: PostFinancialFormStateType
}) => ({
  current: postFinancialForm.current,
}))(FinancialForm);
